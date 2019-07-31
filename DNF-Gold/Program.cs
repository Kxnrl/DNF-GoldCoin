using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNF_Gold
{
    static class Program
    {
        public static string baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Kxnrl", "DNF");

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            var self = new Mutex(true, "com.kxnrl.dnf.dnf-gold", out bool allow);
            if (!allow)
            {
                MessageBox.Show("已有一个实例在运行了...", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            try
            {
                if (!Directory.Exists(baseFolder))
                {
                    Directory.CreateDirectory(baseFolder);
                }
            }
            catch { Environment.Exit(-1); }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, true);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);

            var form = new UI();

            Task.Run(() => PostUserInfo(form)); // 统计追踪
            Task.Run(() => CheckVersion(form)); // 检查更新

            Application.Run(form);
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Debug.Print("UnhandledException: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);
        }

        private static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var ex = e.Exception as Exception;
            Debug.Print("ThreadException: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);
        }

        private static Assembly AssemblyResolve(object sender, ResolveEventArgs e)
        {
            try
            {
                var filename = new AssemblyName(e.Name).Name;
                if (filename.Equals("HtmlAgilityPack"))
                {
                    Debug.Print("Redirect {0} to local binary.", Path.GetFileName(filename));
                    return Assembly.Load(Properties.Resources.HtmlAgilityPack);
                }
                if (filename.Equals("Newtonsoft.Json"))
                {
                    Debug.Print("Redirect {0} to local binary.", Path.GetFileName(filename));
                    return Assembly.Load(Properties.Resources.Newtonsoft_Json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            return null;
        }

        static void PostUserInfo(UI ui)
        {
            try
            {
                using (var http = new WebClient())
                {
                    var ip = http.DownloadString("https://api.ipify.org/");

                    http.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    http.UploadValues("https://api.kxnrl.com/DNF/GoldCoins/IStats/v1/", "POST", new NameValueCollection
                    {
                        { "ip", ip },
                        { "vs", Assembly.GetEntryAssembly().GetName().Version.ToString() }
                    });
                }
            }
            catch (Exception e) { Debug.Print("[PostUserInfo] Exception: {0}", e.Message); }
        }

        static void CheckVersion(UI ui)
        {
            try
            {
                using (var http = new WebClient())
                {
                    var json = http.DownloadString("https://api.kxnrl.com/DNF/GoldCoins/ICheckVersion/v1/?v=" + Assembly.GetEntryAssembly().GetName().Version.ToString());
                    var data = JObject.Parse(json);
                    if (data["Message"].ToString().Contains("Out-Of-Date"))
                    {
                        ui.Invoke(new Action(() =>
                        {
                            if (MessageBox.Show(data["Message"] + Environment.NewLine + "点击是退出并更新", "发现新版本 v" + data["Version"]["Major"] + "." + data["Version"]["Minor"] + "." + data["Version"]["Build"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Process.Start("https://github.com/Kxnrl/DNF-GoldCoin/releases/latest");
                                Environment.Exit(1);
                            }
                        }));
                        Debug.Print("Cancel update");
                    }
                }
            }
            catch (Exception e) { Debug.Print("[CheckVersion] Exception: {0}", e.Message); }
        }
    }
}
