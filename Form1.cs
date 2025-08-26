using NModbus;
using NModbus.Device;
using NModbus.IO;
using NModbus.Serial;

using System.IO.Ports;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace HandleLeveler
{
    public partial class HandleLeveler : Form
    {
        private IModbusMaster? master;
        private SerialPort? port;
        private byte slaveId = 1;
        private bool isConnected = false;
        private readonly object _modbusLock = new object();
        private readonly object _queueLock = new object();
        private double incline = 0.0;
        private int calibrationValue = 0;
        private double? overrideAngle = null;
        private CancellationTokenSource? resetOverrideTimerCts = null;

        private CancellationTokenSource? _cancellationTokenSource;
        private Task? _communicationTask;
        private Queue<Tuple<ushort, ushort>> _writeQueue = new Queue<Tuple<ushort, ushort>>();

        // INI 파일 데이터
        private static string iniFilePath = Path.Combine(Application.StartupPath, "HandleLeveler.ini");
        private IniFile? iniFile = new IniFile(iniFilePath);

        public HandleLeveler()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        public void SaveIniFile(string fndDisplay, string sensorAD, string boardAngle, double incline, string pcAngle)
        {
            try
            {
                iniFile?.WriteValue("Movement", "FND", fndDisplay);
                iniFile?.WriteValue("Movement", "SensorAD", sensorAD);
                iniFile?.WriteValue("Movement", "BoardAngle", boardAngle);
                iniFile?.WriteValue("Movement", "Incline", incline.ToString("F2"));
                iniFile?.WriteValue("Movement", "PcAngle", pcAngle);

                msgNbSend($"INI 파일이 성공적으로 저장 되었습니다: {iniFilePath}");
            }
            catch (Exception ex)
            {
                msgNbSend($"INI 파일 생성 중 오류: {ex.Message}");
            }
        }

        public void LoadFromIni()
        {
            try
            {
                string? fndDisplay = iniFile?.ReadString("Movement", "FND");
                lb_a1.Text = fndDisplay;

                if (fndDisplay == "PC")
                {
                    rdoPc.Checked = true;
                }
                else
                {
                    rdoBoard.Checked = true;
                }

                lb_a2.Text = (iniFile?.ReadInteger("Movement", "SensorAD")).ToString();
                lb_a3.Text = (iniFile?.ReadInteger("Movement", "BoardAngle")).ToString();
                lb_a4.Text = (iniFile?.ReadInteger("Movement", "PcAngle")).ToString();
                incline = iniFile?.ReadDouble("Movement", "Incline") ?? 0.0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("INI 파일을 읽는 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeCustomComponents()
        {
            cbbPort.Items.Clear();
            string[] portsArray = SerialPort.GetPortNames();
            cbbPort.Items.AddRange(portsArray);

            if (cbbPort.Items.Count > 0)
            {
                cbbPort.SelectedIndex = 0;
            }

            btnStartAndStop.Text = "연결";
        }

        private void btnStartAndStop_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                try
                {
                    port = new SerialPort(cbbPort.Text, Convert.ToInt32(cbbSpeed.Text));
                    port.ReadTimeout = 50;
                    port.WriteTimeout = 50;
                    port.Open();

                    var factory = new ModbusFactory();
                    var adapter = new SerialPortAdapter(port);
                    master = factory.CreateRtuMaster(adapter);

                    isConnected = true;

                    _cancellationTokenSource = new CancellationTokenSource();
                    _communicationTask = Task.Run(() => CommunicationLoop(_cancellationTokenSource.Token), _cancellationTokenSource.Token);

                    btnStartAndStop.Text = "정지";
                    btnLed.BackColor = Color.Green;
                    msgNbSend("통신 연결됨");
                }
                catch (Exception ex)
                {
                    msgNbSend($"연결 오류: {ex.Message}");

                    if (port != null && port.IsOpen)
                    {
                        port.Close();
                    }

                    isConnected = false;
                }
            }
            else
            {
                Disconnect(false);
            }
        }

        private void Disconnect(bool resetPcAngle = false)
        {
            if (!isConnected) return;

            isConnected = false;

            try
            {
                _cancellationTokenSource?.Cancel();
            }
            catch (Exception ex)
            {
                msgNbSend($"Cancellation-Token 해제 오류: {ex.Message}");
            }

            _communicationTask?.Wait(500);

            if (resetPcAngle && master != null)
            {
                try
                {
                    master.WriteSingleRegister(slaveId, 5, 0);
                    msgNbSend("PC 각도를 0으로 초기화했습니다.");
                    Thread.Sleep(50); 
                }
                catch (Exception ex)
                {
                    msgNbSend($"PC 각도 초기화 오류: {ex.Message}");
                }
            }

            try
            {
                _cancellationTokenSource?.Dispose();
                master?.Dispose();
                port?.Close();
                port?.Dispose();
            }
            catch (Exception ex)
            {
                msgNbSend($"종료 오류: {ex.Message}");
            }
            finally
            {
                master = null;
                port = null;
                _communicationTask = null;
                _cancellationTokenSource = null;

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        btnStartAndStop.Text = "연결";
                        btnLed.BackColor = Color.Black;
                        msgNbSend("통신 정지됨");
                    }));
                }
                else
                {
                    btnStartAndStop.Text = "연결";
                    btnLed.BackColor = Color.Black;
                    msgNbSend("통신 정지됨");
                }
            }
        }

        private void CommunicationLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (!Monitor.TryEnter(_modbusLock, 50))
                {
                    try { Task.Delay(50, token).Wait(); } catch (OperationCanceledException) { break; }

                    continue;
                }

                try
                {
                    Tuple<ushort, ushort>? writeRequest = null;

                    lock (_queueLock)
                    {
                        if (_writeQueue.Count > 0)
                        {
                            writeRequest = _writeQueue.Dequeue();
                        }
                    }

                    if (writeRequest != null)
                    {
                        try
                        {
                            master.WriteSingleRegister(slaveId, writeRequest.Item1, writeRequest.Item2);
                        }
                        catch (Exception ex)
                        {
                            if (!token.IsCancellationRequested)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    msgNbSend($"쓰기 오류: {ex.Message}. 연결을 종료합니다.");
                                    Disconnect(false);
                                }));
                            }

                            break;
                        }
                    }
                    else
                    {
                        try
                        {
                            ushort[] registers = master.ReadHoldingRegisters(slaveId, 1, 5);

                            if (registers.Length == 5)
                            {
                                UpdateUIFromData(registers);
                            }
                        }
                        catch (Exception ex)
                        { 
                            if (!token.IsCancellationRequested)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    msgNbSend($"읽기 오류: {ex.Message}. 연결을 종료합니다.");
                                    Disconnect(false);
                                }));
                            }
                            break;
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(_modbusLock);
                }
            }
        }

        private void UpdateUIFromData(ushort[] registers)
        {
            this.Invoke(new Action(() =>
            {
                lb_a1.Text = registers[0] == 0 ? "보드" : "PC";

                int sensorAdValue = (registers[1] * 0x010000) + registers[2];
                lb_a2.Text = sensorAdValue.ToString();

                int boardAngleInt = (int)registers[3];

                if (boardAngleInt >= 0x8000)
                {
                    boardAngleInt = (boardAngleInt - 0x8000) * -1;
                }

                double boardAngle = boardAngleInt / 100.0;
                lb_a3.Text = boardAngle.ToString("F2");

                double pcAngle;

                if (registers[0] == 1)
                {
                    if (overrideAngle.HasValue)
                    {
                        pcAngle = overrideAngle.Value;
                    }
                    else
                    {
                        const double adZeroPoint = 130850;
                        const double angleZeroPoint = 0.0;
                        const double adPlusPoint = 149300;
                        const double anglePlusPoint = 10.0;
                        const double adMinusPoint = 112400;
                        const double angleMinusPoint = -10.0;

                        const double plusSensitivity = (anglePlusPoint - angleZeroPoint) / (adPlusPoint - adZeroPoint);
                        const double plusOffset = angleZeroPoint - plusSensitivity * adZeroPoint;

                        const double minusSensitivity = (angleMinusPoint - angleZeroPoint) / (adMinusPoint - adZeroPoint);
                        const double minusOffset = angleZeroPoint - minusSensitivity * adZeroPoint;

                        double calculatedAngle;

                        if (sensorAdValue >= adZeroPoint)
                        {
                            calculatedAngle = (sensorAdValue * plusSensitivity) + plusOffset;
                        }
                        else
                        {
                            calculatedAngle = (sensorAdValue * minusSensitivity) + minusOffset;
                        }

                        pcAngle = Math.Max(-15.00, Math.Min(15.00, calculatedAngle));
                    }

                    int writeValue = (int)(pcAngle * 100);
                    ushort writeAngle;

                    if (writeValue >= 0)
                    {
                        writeAngle = (ushort)writeValue;
                    }
                    else
                    {
                        writeAngle = (ushort)(Math.Abs(writeValue) | 0x8000);
                    }

                    lb_a4.Text = pcAngle.ToString("F2");
                    WriteRegister(5, writeAngle);
                }
            }));
        }

        private void WriteRegister(ushort address, ushort value)
        {
            if (!isConnected || master == null) return;

            lock (_queueLock)
            {
                _writeQueue.Enqueue(Tuple.Create(address, value));
            }
        }

        private void rdoBoard_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBoard.Checked)
            {
                WriteRegister(1, 0);
                iniFile?.WriteValue("Movement", "FND", rdoBoard.Text);
            }
        }

        private void rdoPc_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPc.Checked)
            {
                WriteRegister(1, 1);
                iniFile?.WriteValue("Movement", "FND", rdoPc.Text);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtSendValue.Text, out double angleValue))
            {
                int value = (int)(angleValue * 100);

                value = Math.Max(-1000, Math.Min(1000, value));

                ushort valueToWrite;

                if (value >= 0)
                {
                    valueToWrite = (ushort)value;
                }
                else
                {
                    valueToWrite = (ushort)(Math.Abs(value) | 0x8000);
                }

                WriteRegister(5, valueToWrite);
            }
            else
            {
                msgNbSend("유효하지 않은 숫자입니다.");
            }
        }

        private void HandleLeveler_Load(object sender, EventArgs e)
        {
            LoadFromIni();
        }

        private void HandleLeveler_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveIniFile(lb_a1.Text, lb_a2.Text, lb_a3.Text, incline, lb_a4.Text);
            
            if (isConnected)
            {
                Disconnect(true);
            }
        }

        private int tbMsgSendNb = 0;

        public void msgNbSend(string msg)
        {
            if (tbMsg.InvokeRequired)
            {
                tbMsg.Invoke(new Action(() => msgNbSend(msg)));
                return;
            }

            string str = Convert.ToString(tbMsgSendNb).PadLeft(2, '0');
            str += ": ";

            if (++tbMsgSendNb > 99) tbMsgSendNb = 0;

            tbMsg.AppendText(str);
            tbMsg.AppendText(msg);
            tbMsg.AppendText(Environment.NewLine);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            tbMsg.Text = "";
        }

        private void SetOverrideAngle(double? angle)
        {
            resetOverrideTimerCts?.Cancel();
            resetOverrideTimerCts?.Dispose();

            overrideAngle = angle;

            if (angle.HasValue)
            {
                resetOverrideTimerCts = new CancellationTokenSource();
                CancellationToken token = resetOverrideTimerCts.Token;

                Task.Delay(5000, token).ContinueWith(t =>
                {
                    if (!t.IsCanceled)
                    {
                        overrideAngle = null;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void btnPlusTen_Click(object sender, EventArgs e)
        {
            SetOverrideAngle(10.0);
        }

        private void btnMinusTen_Click(object sender, EventArgs e)
        {
            SetOverrideAngle(-10.0);
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            SetOverrideAngle(0.0);
        }
    }
}