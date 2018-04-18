using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ntrclient
{
    public partial class CmdWindow : Form
    {
		public delegate void LogDelegate(string l);
		public LogDelegate delAddLog;
        string version = "0.1 Beta";


        public CmdWindow()
        {
			delAddLog = new LogDelegate(Addlog);

            InitializeComponent();
            textBox_Ip.Text = Program.sm.IpAddress;
            this.verToolStripMenuItem.Text = version;
        }

        string tid = "";
        string pid = "";
        static int g_text_buf_addr = 0;
        static int g_text_count = 0;
        static int g_send_asm = 0;

        private void ChangeAddresses()
        {
            String tidLow = tid.Substring(7, 9);
            switch(tidLow)
            {
                case "000086300":
                    g_text_buf_addr = 0x32DC4A10;
                    g_text_count = 0x32dc5ce8;
                    g_send_asm = 0x193883;
                    break;
                default:
                    break;

            }
        }

        public void Addlog(string l) {
			if (!l.Contains("\r\n")) {
				l = l.Replace("\n", "\r\n");
			}
            if (!l.EndsWith("\n")) {
                l += "\r\n";
            }
            if (l.Contains("GARDEN"))
            {
                tid = GetTID(l);
                pid = GetPID(l);
                textBox1.Text = pid;
                ChangeAddresses();
                
            }
            txtLog.AppendText(l);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

		private void txtCmd_TextChanged(object sender, EventArgs e) {

		}
		void runCmd(String cmd) {
			try {
				Addlog("> " + cmd);
				object ret = Program.pyEngine.CreateScriptSourceFromString(cmd).Execute(Program.globalScope);
				if (ret != null) {
					Addlog(ret.ToString());
				} else {
					Addlog("null");
				}
			}
			catch (Exception ex) {
				Addlog(ex.Message);
				Addlog(ex.StackTrace);
			}
		}
		private void txtCmd_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				string cmd =  txtCmd.Text  ;
				txtCmd.Clear();
				runCmd(cmd);

			}
		}

		void updateProgress() {
			string text = "";
			if (Program.ntrClient.progress != -1) {
				text = String.Format("{0}%", Program.ntrClient.progress);
			}
			toolStripStatusLabel1.Text = text;
		}
		private void timer1_Tick(object sender, EventArgs e) {
			try {
				updateProgress();
				Program.ntrClient.sendHeartbeatPacket();
				
			} catch(Exception ex) {
			}
		}

		private void CmdWindow_Load(object sender, EventArgs e) {
            Addlog("NTR debugger by cell9");
			runCmd("import sys;sys.path.append('.\\python\\Lib')");
			runCmd("for n in [n for n in dir(nc) if not n.startswith('_')]: globals()[n] = getattr(nc,n)    ");
			Addlog("Commands available: ");
			runCmd("repr([n for n in dir(nc) if not n.startswith('_')])");
		}

		private void CmdWindow_FormClosed(object sender, FormClosedEventArgs e) {
			Program.saveConfig();
			Program.ntrClient.disconnect();
		}

		private void 窗口ToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		private void 命令输入器ToolStripMenuItem_Click(object sender, EventArgs e) {
			(new QuickCmdWindow()).Show();
		}

		private void CmdWindow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
		

		}

		private void CmdWindow_KeyDown(object sender, KeyEventArgs e) {
			if (e.Control) {
				int t = e.KeyValue;
				if (t >= 48 && t <= 57) {
					runCmd(Program.sm.quickCmds[t-48]);
					e.SuppressKeyPress = true;

				}
			}
			
		}

        public void startAutoDisconnect()
        {
            disconnectTimer.Enabled = true;

        }

		private void toolStripStatusLabel1_Click(object sender, EventArgs e) {

		}

        private void asmScratchPadToolStripMenuItem_Click(object sender, EventArgs e) {
            (new AsmEditWindow()).Show();
        }

        private void disconnectTimer_Tick(object sender, EventArgs e)
        {
            disconnectTimer.Enabled = false;
            runCmd("disconnect()");
        }



        private void textBox_dummy_addr_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                button_dummy_read_Click_1(this, new EventArgs());
            }

        }

        private void button_Connect_Click_1(object sender, EventArgs e)
        {
            runCmd("connect('" + textBox_Ip.Text + "', 8000)");
            Program.sm.IpAddress = textBox_Ip.Text;
            runCmd("listprocess()");
        }
        
        private String GetTID(String message)
        {
            int index = message.IndexOf("GARDEN");
            if (index != -1)
                message = message.Substring(index, 32);
            else
                return "";
            return (message.Substring(message.IndexOf("tid:") + 5, 16));
        }

        private String GetPID(String message)
        {
            int index = message.IndexOf("GARDEN");
            if (index != -1)
                message = message.Substring(index - 13, 2);
            else
                return "";
            return (message);
        }

        private void button_disconnect_Click_1(object sender, EventArgs e)
        {
            disconnectTimer.Enabled = false;
            runCmd("disconnect()");
        }

        private void button_hello_Click_1(object sender, EventArgs e)
        {
            runCmd("sayhello()");
        }

        private void button_dummy_read_Click_1(object sender, EventArgs e)
        {
            runCmd(GenerateWriteString(g_text_count, textBox_dummy_addr.TextLength, 1));
            byte[] bytes = Encoding.ASCII.GetBytes(textBox_dummy_addr.Text);
            int index = 0;
            foreach (byte b in bytes)
            {
                runCmd(GenerateWriteString(g_text_buf_addr + (index * 2), b, 1));
                index++;
            }
            Thread.Sleep(index * 20);
            runCmd(GenerateWriteString(g_send_asm, 0x1A, 1));
            Thread.Sleep(200);
            runCmd(GenerateWriteString(g_send_asm, 0x0A, 1));
            Thread.Sleep(index * 10);
            for (int i = 0; i < textBox_dummy_addr.TextLength; i++)
                runCmd(GenerateWriteString(g_text_buf_addr + (i * 2), 0, 1)); // clear text buffer
            textBox_dummy_addr.Text = "";
            Thread.Sleep(500);
        }

        public string GenerateHexChunk(int value, uint length)
        {
            string data = "(";
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                if (i < length - 1)
                {
                    data += string.Format("0x{0:X}, ", b);
                }
                else
                {
                    data += string.Format("0x{0:X},", b);
                    break;
                }
            }
            return data + ")";
        }

        public string GenerateWriteString(int addr, int value, uint length)
        {
            string data = GenerateHexChunk(value, length);

            return string.Format("write(0x{0:X}, {1}, pid=0x", addr, data) + pid + ")";
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About c = new About();
            c.Show();
            c.Focus();
        }

        private void chatSenderByRydoginatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCrhH19ztjidXUP0sAa-wx4w");
        }

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/RyDog199/acnl-chat-sender/");
        }
    }
}
