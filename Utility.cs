using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntrclient {
    class Utility {
        public static int runCommandAndGetOutput(string exeFile, string args, ref string output) {
            string processOutput = null;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            int ret = -1;

            proc.StartInfo = new ProcessStartInfo {
                FileName = exeFile,
                Arguments = args,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true
            };

            try {
                proc.Start();
                proc.WaitForExit();
                processOutput = proc.StandardError.ReadToEnd();
                processOutput += proc.StandardOutput.ReadToEnd();
                ret = proc.ExitCode;
                output = processOutput;
                proc.Close();
                return ret;
            }
            catch (Exception e) {
                output = e.Message;
                return -1;
            }
        }

        public static string convertByteArrayToHexString(byte[] arr) {
            string ret = "";
            for (int i = 0; i < arr.Length; i++) {
                ret += arr[i].ToString("X2") + " ";
            }
            return ret;
        }
    }
}
