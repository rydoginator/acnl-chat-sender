using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Collections.Specialized;


namespace ntrclient
{
    static class Program
    {

        public static ScriptEngine pyEngine;
        public static NtrClient ntrClient;
		public static ScriptHelper scriptHelper;
		public static ScriptScope globalScope;
		public static CmdWindow gCmdWindow;
		public static SettingsManager sm;

		public static void loadConfig() {
			sm = SettingsManager.LoadFromXml("ntrconfig.xml");
			sm.init();
		}

		public static void saveConfig() {
			SettingsManager.SaveToXml("ntrconfig.xml", sm);
		}

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            pyEngine = Python.CreateEngine();
            ntrClient = new NtrClient();
			scriptHelper = new ScriptHelper();

			globalScope = pyEngine.CreateScope();
			globalScope.SetVariable("nc", scriptHelper);
			
			loadConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			gCmdWindow = new CmdWindow();
            Application.Run(gCmdWindow);
        }
    }
}
