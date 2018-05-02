using System;
using System.Windows.Forms;


namespace ntrclient
{
    static class Program
    {
        public static NtrClient ntrClient;
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
            ntrClient = new NtrClient();
		
			loadConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			gCmdWindow = new CmdWindow();
            Application.Run(gCmdWindow);
        }
    }
}
