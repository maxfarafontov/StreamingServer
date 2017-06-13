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
            this.btn_start_client = new System.Windows.Forms.Button();
            this.btn_stop_client = new System.Windows.Forms.Button();
            this.btn_show_form2 = new System.Windows.Forms.Button();
            this.textbox_host = new System.Windows.Forms.TextBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.btn_clear_log = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_start_client
            // 
            this.btn_start_client.Location = new System.Drawing.Point(12, 60);
            this.btn_start_client.Name = "btn_start_client";
            this.btn_start_client.Size = new System.Drawing.Size(75, 23);
            this.btn_start_client.TabIndex = 0;
            this.btn_start_client.Text = "Start";
            this.btn_start_client.UseVisualStyleBackColor = true;
            this.btn_start_client.Click += new System.EventHandler(this.btn_start_client_Click);
            // 
            // btn_stop_client
            // 
            this.btn_stop_client.Location = new System.Drawing.Point(93, 60);
            this.btn_stop_client.Name = "btn_stop_client";
            this.btn_stop_client.Size = new System.Drawing.Size(75, 23);
            this.btn_stop_client.TabIndex = 1;
            this.btn_stop_client.Text = "Stop";
            this.btn_stop_client.UseVisualStyleBackColor = true;
            this.btn_stop_client.Click += new System.EventHandler(this.btn_stop_client_Click);
            // 
            // btn_show_form2
            // 
            this.btn_show_form2.Location = new System.Drawing.Point(199, 60);
            this.btn_show_form2.Name = "btn_show_form2";
            this.btn_show_form2.Size = new System.Drawing.Size(75, 23);
            this.btn_show_form2.TabIndex = 2;
            this.btn_show_form2.Text = "Stream";
            this.btn_show_form2.UseVisualStyleBackColor = true;
            this.btn_show_form2.Click += new System.EventHandler(this.btn_show_form2_Click);
            // 
            // textbox_host
            // 
            this.textbox_host.Location = new System.Drawing.Point(12, 25);
            this.textbox_host.Name = "textbox_host";
            this.textbox_host.Size = new System.Drawing.Size(100, 20);
            this.textbox_host.TabIndex = 3;
            this.textbox_host.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_host_KeyPress);
            // 
            // logBox
            // 
            this.logBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.logBox.Location = new System.Drawing.Point(12, 129);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.ShortcutsEnabled = false;
            this.logBox.Size = new System.Drawing.Size(262, 189);
            this.logBox.TabIndex = 4;
            // 
            // btn_clear_log
            // 
            this.btn_clear_log.Location = new System.Drawing.Point(199, 100);
            this.btn_clear_log.Name = "btn_clear_log";
            this.btn_clear_log.Size = new System.Drawing.Size(75, 23);
            this.btn_clear_log.TabIndex = 5;
            this.btn_clear_log.Text = "Clear";
            this.btn_clear_log.UseVisualStyleBackColor = true;
            this.btn_clear_log.Click += new System.EventHandler(this.btn_clear_log_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.statusConnect});
            this.statusStrip1.Location = new System.Drawing.Point(0, 327);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(562, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // statusConnect
            // 
            this.statusConnect.Name = "statusConnect";
            this.statusConnect.Size = new System.Drawing.Size(39, 17);
            this.statusConnect.Text = "Status";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(119, 25);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(49, 20);
            this.textBox_port.TabIndex = 7;
            this.textBox_port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_port_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Recieve frames:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(294, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 349);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_clear_log);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.textbox_host);
            this.Controls.Add(this.btn_show_form2);
            this.Controls.Add(this.btn_stop_client);
            this.Controls.Add(this.btn_start_client);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "MJPEG Streaming Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start_client;
        private System.Windows.Forms.Button btn_stop_client;
        private System.Windows.Forms.Button btn_show_form2;
        private System.Windows.Forms.TextBox textbox_host;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button btn_clear_log;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel statusConnect;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

