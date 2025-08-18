using System.IO.Ports;
using NModbus;
using NModbus.Device;
using NModbus.IO;
using NModbus.Serial;
using System.Runtime.InteropServices;

namespace HandleLeveler
{
    public partial class HandleLeveler : Form
    {
        private IModbusMaster? master;
        private SerialPort? port;
        private byte slaveId = 1;
        private bool isConnected = false;

        public HandleLeveler()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            readTimer.Enabled = false;

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
                    port.ReadTimeout = 300;
                    port.WriteTimeout = 300;
                    port.Open();

                    var factory = new ModbusFactory();
                    var adapter = new SerialPortAdapter(port);
                    master = factory.CreateRtuMaster(adapter);

                    isConnected = true;
                    btnStartAndStop.Text = "정지";
                    btnLed.BackColor = Color.Green;
                    readTimer.Start();
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
                Disconnect();
            }
        }

        private void Disconnect()
        {
            try
            {
                readTimer.Stop();
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
                isConnected = false;
                btnStartAndStop.Text = "연결";
                btnLed.BackColor = Color.Black;
                msgNbSend("통신 정지됨");
            }
        }

        private void readTimer_Tick(object sender, EventArgs e)
        {
            if (!isConnected || master == null)
            {
                return;
            }

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
                msgNbSend($"데이터 읽기 오류: {ex.Message}");
                Disconnect();
            }
        }

        private void UpdateUIFromData(ushort[] registers)
        {
            this.Invoke(new Action(() =>
            {
                lb_a1.Text = registers[0] == 0 ? "보드" : "PC";

                int sensorAdValue = (registers[1] << 16) | registers[2];
                lb_a2.Text = sensorAdValue.ToString();

                short boardAngle = (short)registers[3];
                lb_a3.Text = boardAngle.ToString();

                short pcAngle = (short)registers[4];
                lb_a4.Text = pcAngle.ToString();
            }));
        }

        private void WriteRegister(ushort address, ushort value)
        {
            if (!isConnected || master == null) return;
            try
            {
                master.WriteSingleRegister(slaveId, address, value);
                msgNbSend($"Register {address}에 {value} 쓰기 성공");
            }
            catch (Exception ex)
            {
                msgNbSend($"쓰기 오류: {ex.Message}");
                Disconnect();
            }
        }

        private void rdoBoard_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBoard.Checked)
            {
                WriteRegister(1, 0);
            }
        }

        private void rdoPc_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPc.Checked)
            {
                WriteRegister(1, 1);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (ushort.TryParse(txtSendValue.Text, out ushort value))
            {
                if (value > 1000) value = 1000;
                WriteRegister(5, value);
            }
            else
            {
                msgNbSend("유효하지 않은 숫자입니다.");
            }
        }

        private void Velocity_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isConnected)
            {
                Disconnect();
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
    }
}