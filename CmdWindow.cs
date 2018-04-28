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
        string version = "0.4 Beta";




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
        static int g_send = 0;
        static int g_button = 0;
        public uint ReadValue = 0xdeadbeef;
        public static bool readPointer = false; //bool to reduce reading
        public static bool dontRead = true; // bool to prevent reading before offsets are initalized
        public int pointer = 0;
        static int keyboard = 0;
        static int pointKey = 0;

        public void setReadValue(uint r)
        {
            ReadValue = r;
        }

        private void ChangeAddresses()
        {
            String tidLow = tid.Substring(7, 9);
            switch(tidLow)
            {
                case "000086200":
                    g_text_buf_addr = 0x958108;
                    g_send = 0x9580E1;
                    g_text_count = 0x958114;
                    g_button = 0xAD0278;
                    break;
                case "000086300":
                    g_text_buf_addr = 0x95F110;
                    g_text_count = 0x95F11C;
                    g_send = 0x95F0E9;
                    g_button = 0xAD7278;
                    break;
                case "000086400":
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
                textBox1.Text = pid;
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
            pointKey = (int)readValue(g_text_count, 4);
            pointer = (int)readValue(g_text_buf_addr, 4);
            if (pointKey != 0)
            {
                int count = (int)readValue(pointKey + 8, 1) >> 24;
                int i = 0;
                while (i < count * 2) // copy bytes to array if there is already text in the keyboard
                {
                    chars[i] = (byte)((int)readValue(pointer + i, 1) >> 24); // double cast and right shift as readValue returns bytes in the wrong order
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
            int pointKey = (int)readValue(g_text_count, 4);
            int len = (int)readValue(pointKey + 8, 1) >> 24;
            pointer = (int)readValue(g_text_buf_addr, 4);
            if (len == textBox_dummy_addr.TextLength)
            {
                runCmd(GenerateWriteString(g_button, 2, 4)); // button id
                runCmd(GenerateWriteString(g_send, 1, 1)); // button pressed bool
                Thread.Sleep(300);
                for (int i = 0; i < len * 2; i++)
                    runCmd(GenerateWriteString(pointer + i, 0, 1));
                textBox_dummy_addr.Text = "";
                readPointer = false;
            }
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

        public uint readValue(int addr, int size)
        {
            if (size < 1)
                size = 1;

            runCmd(string.Format("data(0x{0:X}, 0x{1:X}, pid=0x", addr, size) + pid + ")");
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
                pointer = (int)readValue(g_text_buf_addr, 4);
                keyboard = (int)readValue(pointer, 4);
                if (pointKey != 0)
                    readPointer = true;
            }
            int cursor = textBox_dummy_addr.SelectionStart;
            byte[] bytes = Encoding.Unicode.GetBytes(textBox_dummy_addr.Text);
            pointKey = (int)readValue(g_text_count, 4);
            if (pointKey != 0) // check if buffer is empty and you're on the keyboard
            {
                int count = (int)readValue(pointKey + 8, 1) >> 24;
                if (count < 0)
                    return;
                if (textBox_dummy_addr.Text == "")
                {
                    if (count <= 1)
                    {
                        runCmd(GenerateWriteString(pointer, 0, 2));
                        runCmd(GenerateWriteString(pointKey + 8, 0, 1));
                    }
                    readPointer = false;
                }
                else if (count + 1 < textBox_dummy_addr.TextLength) // if you pasted something into the textbox
                {
                    if (cursor < textBox_dummy_addr.TextLength) // if you moved the cursor
                    {
                        cursor -= count;
                        while (cursor <= textBox_dummy_addr.TextLength)
                        {
                            if (cursor == 0)
                                cursor = 1;
                            runCmd(GenerateWriteString(pointer + (cursor * 2) - 2, bytes[(cursor * 2) - 2], 1));
                            runCmd(GenerateWriteString(pointer + (cursor * 2) - 1, bytes[(cursor * 2) - 1], 1));
                            cursor++;
                        }
                    }
                    else
                    {
                        while (count < textBox_dummy_addr.TextLength)
                        {
                            count++;
                            runCmd(GenerateWriteString(pointer + (count * 2) - 2, bytes[(count * 2) - 2], 1));
                            runCmd(GenerateWriteString(pointer + (count * 2) - 1, bytes[(count * 2) - 1], 1));

                        }
                    }
                    runCmd(GenerateWriteString(pointKey + 8, textBox_dummy_addr.TextLength, 1));
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
                        while (cursor <= textBox_dummy_addr.TextLength)
                        {
                            if (cursor == 0)
                                cursor = 1;
                            runCmd(GenerateWriteString(pointer + (cursor * 2) - 2, bytes[(cursor * 2) - 2], 1));
                            runCmd(GenerateWriteString(pointer + (cursor * 2) - 1, bytes[(cursor * 2) - 1], 1));
                            cursor++;
                        }
                    }
                    runCmd(GenerateWriteString(pointer + (count * 2) - 2, 0, 1));
                    runCmd(GenerateWriteString(pointer + (count * 2) - 1, 0, 1));
                    runCmd(GenerateWriteString(pointKey + 8, textBox_dummy_addr.TextLength, 1));
                }
                else
                {
                    if (cursor < textBox_dummy_addr.TextLength) // if you moved the cursor
                    {
                        //cursor++;
                        while (cursor <= textBox_dummy_addr.TextLength)
                        {
                            if (cursor == 0)
                                cursor = 1;
                            runCmd(GenerateWriteString(pointer + (cursor * 2) - 2, bytes[(cursor * 2) - 2], 1));
                            runCmd(GenerateWriteString(pointer + (cursor * 2) - 1, bytes[(cursor * 2) - 1], 1));
                            cursor++;
                        }
                        runCmd(GenerateWriteString(pointKey + 8, textBox_dummy_addr.TextLength, 1));
                        count++;
                    }
                    else
                    {
                        runCmd(GenerateWriteString(pointKey + 8, textBox_dummy_addr.TextLength, 1));
                        count++;
                        runCmd(GenerateWriteString(pointer + (count * 2) - 2, bytes[(count * 2) - 2], 1));
                        runCmd(GenerateWriteString(pointer + (count * 2) - 1, bytes[(count * 2) - 1], 1));
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
