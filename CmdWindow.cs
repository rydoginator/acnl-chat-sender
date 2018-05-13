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
        string version = "1.0";

        public static ScriptHelper sh = new ScriptHelper();


        public CmdWindow()
        {
			delAddLog = new LogDelegate(Addlog);

            InitializeComponent();
            textBox_Ip.Text = Program.sm.IpAddress;
            this.verToolStripMenuItem.Text = version;
        }

        string tid = "";
        int pid = 0;
        static uint g_text_buf_addr = 0;
        static uint g_text_count = 0;
        static uint g_send = 0;
        static uint g_button = 0;
        public uint ReadValue = 0xdeadbeef;
        public static bool readPointer = false; //bool to reduce reading
        public static bool dontRead = true; // bool to prevent reading before offsets are initalized
        public uint pointer = 0;
        static uint keyboard = 0;
        static uint pointKey = 0;

        public void setReadValue(uint r)
        {
            ReadValue = r;
        }

        private void ChangeAddresses()
        {
            String tidLow = tid.Substring(7, 9);
            switch(tidLow)
            {
                case "000086200": // jap rev 0
                    g_text_buf_addr = 0x958108;
                    g_send = 0x9580E1;
                    g_text_count = 0x958114;
                    g_button = 0xAD0278;
                    break;
                case "000086300": // usa rev 0
                    g_text_buf_addr = 0x95F110;
                    g_send = 0x95F0E9;
                    g_text_count = 0x95F11C;
                    g_button = 0xAD7278;
                    break;
                case "000086400": // eur rev 0
                    g_text_buf_addr = 0x95E108;
                    g_send = 0x95E0E1;
                    g_text_count = 0x95E114;
                    g_button = 0xAD6278;
                    break;
                case "000086500": // kor rev 0
                    g_text_buf_addr = 0x957108;
                    g_send = 0x9570E1;
                    g_text_count = 0x957114;
                    g_button = 0xACF278;
                    break;
                case "000198d00": // jap rev 1
                    g_text_buf_addr = 0x957108;
                    g_send = 0x9570E1;
                    g_text_count = 0x957114;
                    g_button = 0xACF278;
                    break;
                case "000198e00": // usa rev 1
                    g_text_buf_addr = 0x0095E0F0;
                    g_send = 0x95E0C9;
                    g_text_count = 0x95E0FC;
                    g_button = 0xAD6278;
                    break;
                case "000198f00": // eur rev 1
                    g_text_buf_addr = 0x95E108;
                    g_send = 0x95E0E1;
                    g_text_count = 0x95E114;
                    g_button = 0xAD6278;
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
                textBox1.Text = pid.ToString();
                ChangeAddresses();
                textBox_dummy_addr.Text = "";
            }
            txtLog.AppendText(l);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void readKeyboard()
        {
            byte[] chars = new byte[textBox_dummy_addr.MaxLength * 2];
            pointKey = readValue(g_text_count, 4);
            pointer = readValue(g_text_buf_addr, 4);
            if (pointKey != 0)
            {
                int count = (int)readValue(pointKey + 8, 1) >> 24;
                int i = 0;
                while (i < count * 2) // copy bytes to array if there is already text in the keyboard
                {
                    chars[i] = (byte)((int)readValue((uint)(pointer + i), 1) >> 24); // double cast and right shift as readValue returns bytes in the wrong order
                    i++;
                }
                if (chars != null)
                {
                    dontRead = false;
                    textBox_dummy_addr.Text = Encoding.Unicode.GetString(chars);
                    //textBox_dummy_addr.SelectionStart = textBox_dummy_addr.TextLength;
                }
                else
                {
                    textBox_dummy_addr.Text = "";
                    dontRead = true;
                }


            }
            else
            {
                dontRead = true;
            }
        }

		private void txtCmd_TextChanged(object sender, EventArgs e) {

		}
		/*void runCmd(String cmd) {
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
		}*/
		private void txtCmd_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				string cmd =  txtCmd.Text;
				txtCmd.Clear();
				//runCmd(cmd);

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
					//runCmd(Program.sm.quickCmds[t-48]); stubbed. will be replaced with emotes
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
            sh.disconnect();
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
            sh.connect(textBox_Ip.Text, 8000);
            Program.sm.IpAddress = textBox_Ip.Text;
            sh.listprocess();
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

        private int GetPID(String message)
        {
            int offset = 13;
            if (message.Contains("GARDEN_P"))
                offset = 11;
            int index = message.IndexOf("GARDEN");

            if (index != -1)
                message = message.Substring(index - offset, 2);
            else
                return -1;
            return (Convert.ToInt32(message, 16));
        }

        private void button_disconnect_Click_1(object sender, EventArgs e)
        {
            disconnectTimer.Enabled = false;
            sh.disconnect();
        }

        private void button_hello_Click_1(object sender, EventArgs e)
        {
            sh.sayhello();
        }

        private void button_dummy_read_Click_1(object sender, EventArgs e)
        {
            uint pointKey = readValue(g_text_count, 4);
            int len = (int)readValue(pointKey + 8, 1) >> 24;
            pointer = readValue(g_text_buf_addr, 4);
            if (len == textBox_dummy_addr.TextLength && textBox_dummy_addr.Text != "")
            {
                sh.write(g_button, 2, 4, pid); // button id
                sh.write((uint)g_send, 1, 1, pid);  //button pressed bool
                Thread.Sleep(300);
                for (int i = 0; i < len * 2; i++)
                    sh.write((uint)(pointer + i), 0, 1, pid);
                textBox_dummy_addr.Text = "";
                readPointer = false;
            }
        }

        public uint readValue(uint addr, uint size)
        {
            if (size < 1)
                size = 1;
            sh.data((uint)addr, size, pid);
            int retry = 0;
            while (ReadValue == 0xdeadbeef && retry < 300000)
            {
                Task.Delay(25);
                retry++;
            }
            uint v = ReadValue;
            ReadValue = 0xdeadbeef;
            return v;
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

        private void textBox_dummy_addr_TextChanged(object sender, EventArgs e)
        {
            if (dontRead)
                readKeyboard();

            if (!readPointer && textBox_dummy_addr.Text != "")
            {
                pointer = readValue(g_text_buf_addr, 4);
                keyboard = readValue(pointer, 4);
                if (pointKey != 0)
                    readPointer = true;
            }
            int cursor = textBox_dummy_addr.SelectionStart;
           // byte[] bytes = Encoding.Unicode.GetBytes(textBox_dummy_addr.Text);
            pointKey = readValue(g_text_count, 4);
            if (pointKey != 0) // check if buffer is empty and you're on the keyboard
            {
                int count = (int)readValue(pointKey + 8, 1) >> 24;
                if (count < 0)
                    return;
                if (textBox_dummy_addr.Text == "")
                {
                    if (count <= 1)
                    {
                        sh.write(pointer, 0, 2, pid);
                        sh.write(pointKey + 8, 0, 1, pid);
                    }
                    readPointer = false;
                }
                else if (count + 1 < textBox_dummy_addr.TextLength) // if you pasted something into the textbox
                {
                    if (cursor < textBox_dummy_addr.TextLength) // if you moved the cursor
                    {
                        sh.sendText((uint)(pointer + ((textBox_dummy_addr.TextLength - cursor) * 2)), textBox_dummy_addr.Text.Substring(textBox_dummy_addr.TextLength - cursor, cursor), pid);
                    }
                    else
                    {
                        sh.sendText((uint)(pointer + (count * 2)), textBox_dummy_addr.Text.Substring(count, textBox_dummy_addr.TextLength - count), pid);
                    }
                    sh.write(pointKey + 8, textBox_dummy_addr.TextLength, 1, pid);
                }
                else if (count < 0 || (textBox_dummy_addr.TextLength > 1 && count == 0))
                {
                    textBox_dummy_addr.Text = "";
                    readPointer = false;
                }
                else if (count > textBox_dummy_addr.TextLength + 1) // if you typed something ingame
                {
                    readKeyboard();
                }
                else if (count > textBox_dummy_addr.TextLength) // if you pressed backspace, write null chars
                {
                    if (cursor < textBox_dummy_addr.TextLength) // if you moved the cursor
                    {
                        sh.sendText((uint)(pointer + ((textBox_dummy_addr.TextLength - cursor) * 2)), textBox_dummy_addr.Text.Substring(textBox_dummy_addr.TextLength - cursor, cursor), pid);
                    }
                    sh.write((uint)(pointer + (count * 2) - 2), 0, 2, pid);
                    sh.write(pointKey + 8, textBox_dummy_addr.TextLength, 1, pid);
                }
                else
                {
                    if (cursor < textBox_dummy_addr.TextLength) // if you moved the cursor
                    {
                        sh.sendText((uint)(pointer + ((textBox_dummy_addr.TextLength - cursor) * 2)), textBox_dummy_addr.Text.Substring(textBox_dummy_addr.TextLength - cursor, cursor), pid);
                        sh.write(pointKey + 8, textBox_dummy_addr.TextLength, 1, pid);
                        count++;
                    }
                    else
                    {
                        sh.write(pointKey + 8, textBox_dummy_addr.TextLength, 1, pid);
                        sh.sendText((uint)(pointer + (cursor * 2) - 2), textBox_dummy_addr.Text.Substring(textBox_dummy_addr.TextLength - 1, 1), pid);
                    }
                }
            }
            else
            {
                textBox_dummy_addr.Text = "";
                readPointer = false;
            }
        }
    }
}
