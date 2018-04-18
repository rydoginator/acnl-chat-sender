using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ntrclient {
    public partial class AsmEditWindow : Form {
        string asPath = "bin/arm-none-eabi-as";
        string ocPath = "bin/arm-none-eabi-objcopy";
        string ldPath = "bin/arm-none-eabi-ld";
        byte[] compileResult = null;

        public AsmEditWindow() {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            compileAsmCode();
        }

        bool callToolchain(string asOpts, string ldOpts, string ocOpts, ref string result) {
            int ret;
            string output = null;

            result = "";
            ret = Utility.runCommandAndGetOutput(asPath, asOpts, ref output);
            result += asPath + asOpts + "\r\n" + output + "\r\n";
            if (ret != 0) return false;

            ret = Utility.runCommandAndGetOutput(ldPath, ldOpts, ref output);
            result += ldPath + ldOpts + "\r\n" + output + "\r\n";
            if (ret != 0) return false;

            ret = Utility.runCommandAndGetOutput(ocPath, ocOpts, ref output);
            result += ocPath + ocOpts + "\r\n" + output + "\r\n";
            if (ret != 0) return false;
            
            return true;
        }

        void compileAsmCode() {
            compileResult = null;
            string asmCode = txtAsmText.Text;
            string[] instructOpts = comboBox1.Text.Split(',');
            string arch = instructOpts[0];
            string asOpts = " ";
            string ldOpts = " ";
            string ocOpts = " ";
            uint baseAddr = Convert.ToUInt32(textBox1.Text, 16);

            File.WriteAllText("payload.s", asmCode);

            asOpts += "-o payload.o -mlittle-endian";
            asOpts += " -march=" + arch;
            if (instructOpts.Length > 1) {
                if (instructOpts[1] == "thumb") {
                    asOpts += " -mthumb";
                }
            }
            asOpts += " payload.s";
            ldOpts += " -Ttext 0x" + baseAddr.ToString("X8") + " payload.o";
            ocOpts += " -I elf32-little -O binary a.out payload.bin ";

            string result = "";
            bool isSuccessed = callToolchain(asOpts, ldOpts, ocOpts, ref result);
            if (!isSuccessed) {
                result += "compile failed...";
            }
            else {
                compileResult = File.ReadAllBytes("payload.bin");
                result += "result: \r\n" + Utility.convertByteArrayToHexString(compileResult);
            }
            textBox2.Text = result;

        }
    }
}
