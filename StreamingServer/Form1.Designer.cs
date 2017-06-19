namespace StreamingServer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_start_client_1 = new System.Windows.Forms.Button();
            this.btn_stop_client_1 = new System.Windows.Forms.Button();
            this.btn_show_form2 = new System.Windows.Forms.Button();
            this.textbox_host_1 = new System.Windows.Forms.TextBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.btn_clear_log = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox_port_1 = new System.Windows.Forms.TextBox();
            this.textBox_port_2 = new System.Windows.Forms.TextBox();
            this.textBox_port_3 = new System.Windows.Forms.TextBox();
            this.btn_stop_client_2 = new System.Windows.Forms.Button();
            this.btn_start_client_2 = new System.Windows.Forms.Button();
            this.btn_stop_client_3 = new System.Windows.Forms.Button();
            this.btn_start_client_3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_start_stream_3 = new System.Windows.Forms.Button();
            this.btn_start_stream_2 = new System.Windows.Forms.Button();
            this.textBox_portS_3 = new System.Windows.Forms.TextBox();
            this.textBox_portS_2 = new System.Windows.Forms.TextBox();
            this.textBox_portS_1 = new System.Windows.Forms.TextBox();
            this.btn_start_stream_1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_stop_stream_3 = new System.Windows.Forms.Button();
            this.btn_stop_stream_1 = new System.Windows.Forms.Button();
            this.btn_stop_stream_2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_start_client_1
            // 
            this.btn_start_client_1.Location = new System.Drawing.Point(121, 64);
            this.btn_start_client_1.Name = "btn_start_client_1";
            this.btn_start_client_1.Size = new System.Drawing.Size(45, 23);
            this.btn_start_client_1.TabIndex = 0;
            this.btn_start_client_1.Text = "Start";
            this.btn_start_client_1.UseVisualStyleBackColor = true;
            this.btn_start_client_1.Click += new System.EventHandler(this.btn_start_client_1_Click);
            // 
            // btn_stop_client_1
            // 
            this.btn_stop_client_1.Location = new System.Drawing.Point(172, 64);
            this.btn_stop_client_1.Name = "btn_stop_client_1";
            this.btn_stop_client_1.Size = new System.Drawing.Size(45, 23);
            this.btn_stop_client_1.TabIndex = 1;
            this.btn_stop_client_1.Text = "Stop";
            this.btn_stop_client_1.UseVisualStyleBackColor = true;
            this.btn_stop_client_1.Click += new System.EventHandler(this.btn_stop_client_Click);
            // 
            // btn_show_form2
            // 
            this.btn_show_form2.Location = new System.Drawing.Point(697, 134);
            this.btn_show_form2.Name = "btn_show_form2";
            this.btn_show_form2.Size = new System.Drawing.Size(75, 23);
            this.btn_show_form2.TabIndex = 2;
            this.btn_show_form2.Text = "Stream";
            this.btn_show_form2.UseVisualStyleBackColor = true;
            this.btn_show_form2.Click += new System.EventHandler(this.btn_show_form2_Click);
            // 
            // textbox_host_1
            // 
            this.textbox_host_1.Location = new System.Drawing.Point(66, 38);
            this.textbox_host_1.Name = "textbox_host_1";
            this.textbox_host_1.Size = new System.Drawing.Size(151, 20);
            this.textbox_host_1.TabIndex = 3;
            this.textbox_host_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_host_KeyPress);
            // 
            // logBox
            // 
            this.logBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.logBox.Location = new System.Drawing.Point(460, 12);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.ShortcutsEnabled = false;
            this.logBox.Size = new System.Drawing.Size(312, 116);
            this.logBox.TabIndex = 4;
            // 
            // btn_clear_log
            // 
            this.btn_clear_log.Location = new System.Drawing.Point(460, 134);
            this.btn_clear_log.Name = "btn_clear_log";
            this.btn_clear_log.Size = new System.Drawing.Size(75, 23);
            this.btn_clear_log.TabIndex = 5;
            this.btn_clear_log.Text = "Clear";
            this.btn_clear_log.UseVisualStyleBackColor = true;
            this.btn_clear_log.Click += new System.EventHandler(this.btn_clear_log_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox_port_1
            // 
            this.textBox_port_1.Location = new System.Drawing.Point(66, 66);
            this.textBox_port_1.Name = "textBox_port_1";
            this.textBox_port_1.Size = new System.Drawing.Size(49, 20);
            this.textBox_port_1.TabIndex = 7;
            this.textBox_port_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_port_KeyPress);
            // 
            // textBox_port_2
            // 
            this.textBox_port_2.Location = new System.Drawing.Point(66, 92);
            this.textBox_port_2.Name = "textBox_port_2";
            this.textBox_port_2.Size = new System.Drawing.Size(49, 20);
            this.textBox_port_2.TabIndex = 12;
            // 
            // textBox_port_3
            // 
            this.textBox_port_3.Location = new System.Drawing.Point(66, 118);
            this.textBox_port_3.Name = "textBox_port_3";
            this.textBox_port_3.Size = new System.Drawing.Size(49, 20);
            this.textBox_port_3.TabIndex = 14;
            // 
            // btn_stop_client_2
            // 
            this.btn_stop_client_2.Location = new System.Drawing.Point(172, 90);
            this.btn_stop_client_2.Name = "btn_stop_client_2";
            this.btn_stop_client_2.Size = new System.Drawing.Size(45, 23);
            this.btn_stop_client_2.TabIndex = 16;
            this.btn_stop_client_2.Text = "Stop";
            this.btn_stop_client_2.UseVisualStyleBackColor = true;
            this.btn_stop_client_2.Click += new System.EventHandler(this.btn_stop_client_2_Click);
            // 
            // btn_start_client_2
            // 
            this.btn_start_client_2.Location = new System.Drawing.Point(121, 90);
            this.btn_start_client_2.Name = "btn_start_client_2";
            this.btn_start_client_2.Size = new System.Drawing.Size(45, 23);
            this.btn_start_client_2.TabIndex = 15;
            this.btn_start_client_2.Text = "Start";
            this.btn_start_client_2.UseVisualStyleBackColor = true;
            this.btn_start_client_2.Click += new System.EventHandler(this.btn_start_client_2_Click);
            // 
            // btn_stop_client_3
            // 
            this.btn_stop_client_3.Location = new System.Drawing.Point(172, 116);
            this.btn_stop_client_3.Name = "btn_stop_client_3";
            this.btn_stop_client_3.Size = new System.Drawing.Size(45, 23);
            this.btn_stop_client_3.TabIndex = 18;
            this.btn_stop_client_3.Text = "Stop";
            this.btn_stop_client_3.UseVisualStyleBackColor = true;
            this.btn_stop_client_3.Click += new System.EventHandler(this.btn_stop_client_3_Click);
            // 
            // btn_start_client_3
            // 
            this.btn_start_client_3.Location = new System.Drawing.Point(121, 116);
            this.btn_start_client_3.Name = "btn_start_client_3";
            this.btn_start_client_3.Size = new System.Drawing.Size(45, 23);
            this.btn_start_client_3.TabIndex = 17;
            this.btn_start_client_3.Text = "Start";
            this.btn_start_client_3.UseVisualStyleBackColor = true;
            this.btn_start_client_3.Click += new System.EventHandler(this.btn_start_client_3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Camera 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Camera 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Camera 3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Host";
            // 
            // btn_start_stream_3
            // 
            this.btn_start_stream_3.Location = new System.Drawing.Point(68, 115);
            this.btn_start_stream_3.Name = "btn_start_stream_3";
            this.btn_start_stream_3.Size = new System.Drawing.Size(45, 23);
            this.btn_start_stream_3.TabIndex = 28;
            this.btn_start_stream_3.Text = "Start";
            this.btn_start_stream_3.UseVisualStyleBackColor = true;
            this.btn_start_stream_3.Click += new System.EventHandler(this.btn_start_stream_3_Click);
            // 
            // btn_start_stream_2
            // 
            this.btn_start_stream_2.Location = new System.Drawing.Point(68, 89);
            this.btn_start_stream_2.Name = "btn_start_stream_2";
            this.btn_start_stream_2.Size = new System.Drawing.Size(45, 23);
            this.btn_start_stream_2.TabIndex = 27;
            this.btn_start_stream_2.Text = "Start";
            this.btn_start_stream_2.UseVisualStyleBackColor = true;
            this.btn_start_stream_2.Click += new System.EventHandler(this.btn_start_stream_2_Click);
            // 
            // textBox_portS_3
            // 
            this.textBox_portS_3.Location = new System.Drawing.Point(13, 117);
            this.textBox_portS_3.Name = "textBox_portS_3";
            this.textBox_portS_3.Size = new System.Drawing.Size(49, 20);
            this.textBox_portS_3.TabIndex = 26;
            // 
            // textBox_portS_2
            // 
            this.textBox_portS_2.Location = new System.Drawing.Point(13, 91);
            this.textBox_portS_2.Name = "textBox_portS_2";
            this.textBox_portS_2.Size = new System.Drawing.Size(49, 20);
            this.textBox_portS_2.TabIndex = 25;
            // 
            // textBox_portS_1
            // 
            this.textBox_portS_1.Location = new System.Drawing.Point(13, 65);
            this.textBox_portS_1.Name = "textBox_portS_1";
            this.textBox_portS_1.Size = new System.Drawing.Size(49, 20);
            this.textBox_portS_1.TabIndex = 24;
            // 
            // btn_start_stream_1
            // 
            this.btn_start_stream_1.Location = new System.Drawing.Point(68, 63);
            this.btn_start_stream_1.Name = "btn_start_stream_1";
            this.btn_start_stream_1.Size = new System.Drawing.Size(45, 23);
            this.btn_start_stream_1.TabIndex = 23;
            this.btn_start_stream_1.Text = "Start";
            this.btn_start_stream_1.UseVisualStyleBackColor = true;
            this.btn_start_stream_1.Click += new System.EventHandler(this.btn_start_stream_1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btn_stop_client_3);
            this.panel1.Controls.Add(this.btn_start_client_1);
            this.panel1.Controls.Add(this.btn_stop_client_1);
            this.panel1.Controls.Add(this.textbox_host_1);
            this.panel1.Controls.Add(this.textBox_port_1);
            this.panel1.Controls.Add(this.textBox_port_2);
            this.panel1.Controls.Add(this.textBox_port_3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btn_start_client_2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btn_stop_client_2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btn_start_client_3);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 144);
            this.panel1.TabIndex = 29;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(220, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(220, 117);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.TabIndex = 26;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(220, 92);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Connect to the cameras:";
            // 
            // btn_stop_stream_3
            // 
            this.btn_stop_stream_3.Location = new System.Drawing.Point(119, 115);
            this.btn_stop_stream_3.Name = "btn_stop_stream_3";
            this.btn_stop_stream_3.Size = new System.Drawing.Size(45, 23);
            this.btn_stop_stream_3.TabIndex = 25;
            this.btn_stop_stream_3.Text = "Stop";
            this.btn_stop_stream_3.UseVisualStyleBackColor = true;
            this.btn_stop_stream_3.Click += new System.EventHandler(this.btn_stop_stream_3_Click);
            // 
            // btn_stop_stream_1
            // 
            this.btn_stop_stream_1.Location = new System.Drawing.Point(119, 63);
            this.btn_stop_stream_1.Name = "btn_stop_stream_1";
            this.btn_stop_stream_1.Size = new System.Drawing.Size(45, 23);
            this.btn_stop_stream_1.TabIndex = 23;
            this.btn_stop_stream_1.Text = "Stop";
            this.btn_stop_stream_1.UseVisualStyleBackColor = true;
            this.btn_stop_stream_1.Click += new System.EventHandler(this.btn_stop_stream_1_Click);
            // 
            // btn_stop_stream_2
            // 
            this.btn_stop_stream_2.Location = new System.Drawing.Point(119, 89);
            this.btn_stop_stream_2.Name = "btn_stop_stream_2";
            this.btn_stop_stream_2.Size = new System.Drawing.Size(45, 23);
            this.btn_stop_stream_2.TabIndex = 24;
            this.btn_stop_stream_2.Text = "Stop";
            this.btn_stop_stream_2.UseVisualStyleBackColor = true;
            this.btn_stop_stream_2.Click += new System.EventHandler(this.btn_stop_stream_2_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.btn_stop_stream_3);
            this.panel2.Controls.Add(this.btn_start_stream_1);
            this.panel2.Controls.Add(this.textBox_portS_1);
            this.panel2.Controls.Add(this.btn_stop_stream_1);
            this.panel2.Controls.Add(this.textBox_portS_2);
            this.panel2.Controls.Add(this.btn_stop_stream_2);
            this.panel2.Controls.Add(this.textBox_portS_3);
            this.panel2.Controls.Add(this.btn_start_stream_3);
            this.panel2.Controls.Add(this.btn_start_stream_2);
            this.panel2.Location = new System.Drawing.Point(269, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 144);
            this.panel2.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Ports:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Start MJPEG streaming:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 425);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(12, 162);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(256, 256);
            this.pictureBox4.TabIndex = 31;
            this.pictureBox4.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(275, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "Draw";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 447);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_clear_log);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.btn_show_form2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "MJPEG Streaming Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start_client_1;
        private System.Windows.Forms.Button btn_stop_client_1;
        private System.Windows.Forms.Button btn_show_form2;
        private System.Windows.Forms.TextBox textbox_host_1;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button btn_clear_log;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox_port_1;
        private System.Windows.Forms.TextBox textBox_port_2;
        private System.Windows.Forms.TextBox textBox_port_3;
        private System.Windows.Forms.Button btn_stop_client_2;
        private System.Windows.Forms.Button btn_start_client_2;
        private System.Windows.Forms.Button btn_stop_client_3;
        private System.Windows.Forms.Button btn_start_client_3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_start_stream_3;
        private System.Windows.Forms.Button btn_start_stream_2;
        private System.Windows.Forms.TextBox textBox_portS_3;
        private System.Windows.Forms.TextBox textBox_portS_2;
        private System.Windows.Forms.TextBox textBox_portS_1;
        private System.Windows.Forms.Button btn_start_stream_1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_stop_stream_3;
        private System.Windows.Forms.Button btn_stop_stream_1;
        private System.Windows.Forms.Button btn_stop_stream_2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button button1;
    }
}

