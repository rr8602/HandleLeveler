namespace HandleLeveler
{
    partial class HandleLeveler
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            cbbSpeed = new ComboBox();
            btnLed = new Button();
            btnStartAndStop = new Button();
            cbbPort = new ComboBox();
            saveFileDialog1 = new SaveFileDialog();
            tbMsg = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            txtSendValue = new TextBox();
            btnSend = new Button();
            rdoPc = new RadioButton();
            rdoBoard = new RadioButton();
            label6 = new Label();
            lb_a4 = new Label();
            label9 = new Label();
            lb_a3 = new Label();
            label4 = new Label();
            lb_a2 = new Label();
            label8 = new Label();
            lb_a1 = new Label();
            tabPage2 = new TabPage();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            tabPage3 = new TabPage();
            btnPlusTen = new Button();
            btnMinusTen = new Button();
            btnZero = new Button();
            btnRemove = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // cbbSpeed
            // 
            cbbSpeed.FormattingEnabled = true;
            cbbSpeed.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            cbbSpeed.Location = new Point(140, 70);
            cbbSpeed.Margin = new Padding(3, 2, 3, 2);
            cbbSpeed.Name = "cbbSpeed";
            cbbSpeed.Size = new Size(72, 20);
            cbbSpeed.TabIndex = 141;
            cbbSpeed.Text = "115200";
            // 
            // btnLed
            // 
            btnLed.BackColor = Color.Black;
            btnLed.Location = new Point(328, 32);
            btnLed.Margin = new Padding(3, 4, 3, 4);
            btnLed.Name = "btnLed";
            btnLed.Size = new Size(20, 25);
            btnLed.TabIndex = 115;
            btnLed.UseVisualStyleBackColor = false;
            // 
            // btnStartAndStop
            // 
            btnStartAndStop.ForeColor = SystemColors.Desktop;
            btnStartAndStop.Location = new Point(225, 28);
            btnStartAndStop.Margin = new Padding(3, 4, 3, 4);
            btnStartAndStop.Name = "btnStartAndStop";
            btnStartAndStop.Size = new Size(93, 66);
            btnStartAndStop.TabIndex = 34;
            btnStartAndStop.Text = "정지";
            btnStartAndStop.UseVisualStyleBackColor = true;
            btnStartAndStop.Click += btnStartAndStop_Click;
            // 
            // cbbPort
            // 
            cbbPort.FormattingEnabled = true;
            cbbPort.Location = new Point(140, 28);
            cbbPort.Margin = new Padding(3, 2, 3, 2);
            cbbPort.Name = "cbbPort";
            cbbPort.Size = new Size(72, 20);
            cbbPort.TabIndex = 35;
            cbbPort.Text = "COM3";
            // 
            // tbMsg
            // 
            tbMsg.Location = new Point(13, 435);
            tbMsg.Margin = new Padding(3, 4, 3, 4);
            tbMsg.Multiline = true;
            tbMsg.Name = "tbMsg";
            tbMsg.ScrollBars = ScrollBars.Vertical;
            tbMsg.Size = new Size(469, 156);
            tbMsg.TabIndex = 114;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Font = new Font("굴림", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tabControl1.ItemSize = new Size(48, 25);
            tabControl1.Location = new Point(12, 15);
            tabControl1.Margin = new Padding(3, 4, 3, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(474, 368);
            tabControl1.TabIndex = 135;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtSendValue);
            tabPage1.Controls.Add(btnSend);
            tabPage1.Controls.Add(rdoPc);
            tabPage1.Controls.Add(rdoBoard);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(lb_a4);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(lb_a3);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(lb_a2);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(lb_a1);
            tabPage1.Font = new Font("굴림", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tabPage1.ForeColor = SystemColors.ControlText;
            tabPage1.Location = new Point(4, 29);
            tabPage1.Margin = new Padding(3, 4, 3, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(466, 335);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "   동작   ";
            // 
            // txtSendValue
            // 
            txtSendValue.Location = new Point(262, 238);
            txtSendValue.Margin = new Padding(3, 4, 3, 4);
            txtSendValue.Name = "txtSendValue";
            txtSendValue.Size = new Size(100, 21);
            txtSendValue.TabIndex = 156;
            txtSendValue.Text = "500";
            txtSendValue.TextAlign = HorizontalAlignment.Center;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(368, 235);
            btnSend.Margin = new Padding(3, 4, 3, 4);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 28);
            btnSend.TabIndex = 155;
            btnSend.Text = "보내기";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // rdoPc
            // 
            rdoPc.AutoSize = true;
            rdoPc.Location = new Point(403, 46);
            rdoPc.Margin = new Padding(3, 4, 3, 4);
            rdoPc.Name = "rdoPc";
            rdoPc.Size = new Size(40, 16);
            rdoPc.TabIndex = 154;
            rdoPc.Text = "PC";
            rdoPc.UseVisualStyleBackColor = true;
            rdoPc.CheckedChanged += rdoPc_CheckedChanged;
            // 
            // rdoBoard
            // 
            rdoBoard.AutoSize = true;
            rdoBoard.Checked = true;
            rdoBoard.Location = new Point(350, 45);
            rdoBoard.Margin = new Padding(3, 4, 3, 4);
            rdoBoard.Name = "rdoBoard";
            rdoBoard.Size = new Size(47, 16);
            rdoBoard.TabIndex = 153;
            rdoBoard.TabStop = true;
            rdoBoard.Text = "보드";
            rdoBoard.UseVisualStyleBackColor = true;
            rdoBoard.CheckedChanged += rdoBoard_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("굴림", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label6.Location = new Point(46, 240);
            label6.Name = "label6";
            label6.Size = new Size(88, 19);
            label6.TabIndex = 152;
            label6.Text = "PC 각도 :";
            // 
            // lb_a4
            // 
            lb_a4.AutoSize = true;
            lb_a4.Font = new Font("굴림", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lb_a4.ForeColor = Color.Blue;
            lb_a4.Location = new Point(154, 240);
            lb_a4.Name = "lb_a4";
            lb_a4.Size = new Size(59, 19);
            lb_a4.TabIndex = 151;
            lb_a4.Text = "00000";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("굴림", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label9.Location = new Point(46, 170);
            label9.Name = "label9";
            label9.Size = new Size(103, 19);
            label9.TabIndex = 150;
            label9.Text = "보드 각도 :";
            // 
            // lb_a3
            // 
            lb_a3.AutoSize = true;
            lb_a3.Font = new Font("굴림", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lb_a3.ForeColor = Color.Blue;
            lb_a3.Location = new Point(154, 170);
            lb_a3.Name = "lb_a3";
            lb_a3.Size = new Size(59, 19);
            lb_a3.TabIndex = 149;
            lb_a3.Text = "00000";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("굴림", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label4.Location = new Point(46, 106);
            label4.Name = "label4";
            label4.Size = new Size(88, 19);
            label4.TabIndex = 148;
            label4.Text = "센서 AD :";
            // 
            // lb_a2
            // 
            lb_a2.AutoSize = true;
            lb_a2.Font = new Font("굴림", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lb_a2.ForeColor = Color.Blue;
            lb_a2.Location = new Point(154, 106);
            lb_a2.Name = "lb_a2";
            lb_a2.Size = new Size(69, 19);
            lb_a2.TabIndex = 147;
            lb_a2.Text = "000000";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("굴림", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label8.Location = new Point(46, 41);
            label8.Name = "label8";
            label8.Size = new Size(97, 19);
            label8.TabIndex = 146;
            label8.Text = "FND 표시 :";
            // 
            // lb_a1
            // 
            lb_a1.AutoSize = true;
            lb_a1.Font = new Font("굴림", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lb_a1.ForeColor = Color.Blue;
            lb_a1.Location = new Point(154, 41);
            lb_a1.Name = "lb_a1";
            lb_a1.Size = new Size(47, 19);
            lb_a1.TabIndex = 135;
            lb_a1.Text = "보드";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = SystemColors.Control;
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Margin = new Padding(3, 4, 3, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 4, 3, 4);
            tabPage2.Size = new Size(466, 335);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "   통신설정   ";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnStartAndStop);
            groupBox1.Controls.Add(cbbPort);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnLed);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cbbSpeed);
            groupBox1.Location = new Point(15, 22);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(366, 115);
            groupBox1.TabIndex = 144;
            groupBox1.TabStop = false;
            groupBox1.Text = "보드";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("굴림", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(102, 73);
            label2.Name = "label2";
            label2.Size = new Size(33, 13);
            label2.TabIndex = 143;
            label2.Text = "속도";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("굴림", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(102, 32);
            label1.Name = "label1";
            label1.Size = new Size(33, 13);
            label1.TabIndex = 142;
            label1.Text = "포트";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(btnPlusTen);
            tabPage3.Controls.Add(btnMinusTen);
            tabPage3.Controls.Add(btnZero);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Margin = new Padding(3, 4, 3, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(466, 335);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "교정";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnPlusTen
            // 
            btnPlusTen.Location = new Point(267, 118);
            btnPlusTen.Margin = new Padding(3, 4, 3, 4);
            btnPlusTen.Name = "btnPlusTen";
            btnPlusTen.Size = new Size(102, 32);
            btnPlusTen.TabIndex = 2;
            btnPlusTen.Text = "+10";
            btnPlusTen.UseVisualStyleBackColor = true;
            btnPlusTen.Click += btnPlusTen_Click;
            // 
            // btnMinusTen
            // 
            btnMinusTen.Location = new Point(51, 118);
            btnMinusTen.Margin = new Padding(3, 4, 3, 4);
            btnMinusTen.Name = "btnMinusTen";
            btnMinusTen.Size = new Size(102, 32);
            btnMinusTen.TabIndex = 1;
            btnMinusTen.Text = "-10";
            btnMinusTen.UseVisualStyleBackColor = true;
            btnMinusTen.Click += btnMinusTen_Click;
            // 
            // btnZero
            // 
            btnZero.Location = new Point(159, 118);
            btnZero.Margin = new Padding(3, 4, 3, 4);
            btnZero.Name = "btnZero";
            btnZero.Size = new Size(102, 32);
            btnZero.TabIndex = 0;
            btnZero.Text = "영점";
            btnZero.UseVisualStyleBackColor = true;
            btnZero.Click += btnZero_Click;
            // 
            // btnRemove
            // 
            btnRemove.Font = new Font("굴림", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btnRemove.ForeColor = SystemColors.Desktop;
            btnRemove.Location = new Point(407, 390);
            btnRemove.Margin = new Padding(3, 4, 3, 4);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(75, 38);
            btnRemove.TabIndex = 135;
            btnRemove.Text = "지우기";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // HandleLeveler
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 615);
            Controls.Add(btnRemove);
            Controls.Add(tabControl1);
            Controls.Add(tbMsg);
            Margin = new Padding(3, 4, 3, 4);
            Name = "HandleLeveler";
            Text = "HandleLeveler";
            FormClosing += HandleLeveler_FormClosing;
            Load += HandleLeveler_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnStartAndStop;
        private System.Windows.Forms.ComboBox cbbPort;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.Button btnLed;
        private System.Windows.Forms.ComboBox cbbSpeed;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lb_a1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lb_a3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_a2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lb_a4;
        private System.Windows.Forms.RadioButton rdoPc;
        private System.Windows.Forms.RadioButton rdoBoard;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSendValue;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnPlusTen;
        private System.Windows.Forms.Button btnMinusTen;
        private System.Windows.Forms.Button btnZero;
    }
}

