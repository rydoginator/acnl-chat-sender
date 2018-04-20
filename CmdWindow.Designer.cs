namespace ntrclient
{
    partial class CmdWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cell9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatSenderByRydoginatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_dummy_read = new System.Windows.Forms.Button();
            this.textBox_dummy_addr = new System.Windows.Forms.TextBox();
            this.button_hello = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_Connect = new System.Windows.Forms.Button();
            this.textBox_Ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creditsToolStripMenuItem,
            this.versionToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cell9ToolStripMenuItem,
            this.chatSenderByRydoginatorToolStripMenuItem,
            this.githubToolStripMenuItem});
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.creditsToolStripMenuItem.Text = "Credits";
            // 
            // cell9ToolStripMenuItem
            // 
            this.cell9ToolStripMenuItem.Name = "cell9ToolStripMenuItem";
            this.cell9ToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.cell9ToolStripMenuItem.Text = "cell9";
            // 
            // chatSenderByRydoginatorToolStripMenuItem
            // 
            this.chatSenderByRydoginatorToolStripMenuItem.Name = "chatSenderByRydoginatorToolStripMenuItem";
            this.chatSenderByRydoginatorToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.chatSenderByRydoginatorToolStripMenuItem.Text = "Chat sender by rydoginator";
            this.chatSenderByRydoginatorToolStripMenuItem.Click += new System.EventHandler(this.chatSenderByRydoginatorToolStripMenuItem_Click);
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.githubToolStripMenuItem.Text = "Github";
            this.githubToolStripMenuItem.Click += new System.EventHandler(this.githubToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verToolStripMenuItem});
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.versionToolStripMenuItem.Text = "Version";
            // 
            // verToolStripMenuItem
            // 
            this.verToolStripMenuItem.Name = "verToolStripMenuItem";
            this.verToolStripMenuItem.Size = new System.Drawing.Size(90, 22);
            this.verToolStripMenuItem.Text = "ver";
            this.verToolStripMenuItem.Click += new System.EventHandler(this.verToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // disconnectTimer
            // 
            this.disconnectTimer.Interval = 10000;
            this.disconnectTimer.Tick += new System.EventHandler(this.disconnectTimer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(644, 301);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button_dummy_read);
            this.tabPage1.Controls.Add(this.textBox_dummy_addr);
            this.tabPage1.Controls.Add(this.button_hello);
            this.tabPage1.Controls.Add(this.button_disconnect);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.button_Connect);
            this.tabPage1.Controls.Add(this.textBox_Ip);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 275);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_dummy_read
            // 
            this.button_dummy_read.Location = new System.Drawing.Point(461, 233);
            this.button_dummy_read.Name = "button_dummy_read";
            this.button_dummy_read.Size = new System.Drawing.Size(150, 20);
            this.button_dummy_read.TabIndex = 26;
            this.button_dummy_read.Text = "Send to chat";
            this.button_dummy_read.UseVisualStyleBackColor = true;
            this.button_dummy_read.Click += new System.EventHandler(this.button_dummy_read_Click_1);
            // 
            // textBox_dummy_addr
            // 
            this.textBox_dummy_addr.Location = new System.Drawing.Point(9, 233);
            this.textBox_dummy_addr.MaxLength = 54;
            this.textBox_dummy_addr.Name = "textBox_dummy_addr";
            this.textBox_dummy_addr.Size = new System.Drawing.Size(425, 20);
            this.textBox_dummy_addr.TabIndex = 25;
            this.textBox_dummy_addr.Text = "Text";
            this.textBox_dummy_addr.TextChanged += new System.EventHandler(this.textBox_dummy_addr_TextChanged);
            this.textBox_dummy_addr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_dummy_addr_TextChanged);
            // 
            // button_hello
            // 
            this.button_hello.Location = new System.Drawing.Point(461, 104);
            this.button_hello.Name = "button_hello";
            this.button_hello.Size = new System.Drawing.Size(150, 20);
            this.button_hello.TabIndex = 24;
            this.button_hello.Text = "Hello! - Test connection";
            this.button_hello.UseVisualStyleBackColor = true;
            this.button_hello.Click += new System.EventHandler(this.button_hello_Click_1);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Location = new System.Drawing.Point(461, 66);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(150, 20);
            this.button_disconnect.TabIndex = 23;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Process ID";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 20);
            this.textBox1.TabIndex = 21;
            this.textBox1.Text = "PID";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(461, 19);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(150, 20);
            this.button_Connect.TabIndex = 20;
            this.button_Connect.Text = "Connect";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click_1);
            // 
            // textBox_Ip
            // 
            this.textBox_Ip.Location = new System.Drawing.Point(9, 19);
            this.textBox_Ip.Name = "textBox_Ip";
            this.textBox_Ip.Size = new System.Drawing.Size(150, 20);
            this.textBox_Ip.TabIndex = 19;
            this.textBox_Ip.Text = "Nintendo 3DS IP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nintendo 3DS IP";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(636, 275);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtLog, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCmd, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(630, 269);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.MaxLength = 32767000;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(624, 209);
            this.txtLog.TabIndex = 0;
            // 
            // txtCmd
            // 
            this.txtCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCmd.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCmd.Location = new System.Drawing.Point(3, 218);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(624, 26);
            this.txtCmd.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 247);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(630, 22);
            this.statusStrip1.TabIndex = 2;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // CmdWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 341);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CmdWindow";
            this.Text = "Animal Crossing New Leaf Chat Sender";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CmdWindow_FormClosed);
            this.Load += new System.EventHandler(this.CmdWindow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmdWindow_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.CmdWindow_PreviewKeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Timer disconnectTimer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button_dummy_read;
        private System.Windows.Forms.TextBox textBox_dummy_addr;
        private System.Windows.Forms.Button button_hello;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.TextBox textBox_Ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cell9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chatSenderByRydoginatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;
    }
}

