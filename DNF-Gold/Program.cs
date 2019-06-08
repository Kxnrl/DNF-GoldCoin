using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
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

            // 统计追踪
            new Thread(PostUserInfo)
            {
                Priority = ThreadPriority.Lowest,
                IsBackground = true
            }.Start();

            // 检查更新
            new Thread(CheckVersion)
            {
                Priority = ThreadPriority.Lowest,
                IsBackground = true
            }.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI());
        }

        private static Assembly AssemblyResolve(object sender, ResolveEventArgs e)
        {
            try
            {
                var filename = new AssemblyName(e.Name).Name;
                if (filename.Equals("HtmlAgilityPack"))
                {
                    Console.WriteLine("Redirect {0} to local binary.", Path.GetFileName(filename));
                    return Assembly.Load(Properties.Resources.HtmlAgilityPack);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            return null;
        }

        static void PostUserInfo()
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
            catch (Exception e) { Console.WriteLine("[PostUserInfo] Exception: {0}", e.Message); }
        }

        static void CheckVersion()
        {
            try
            {
                using (var http = new WebClient())
                {
                    var data = http.DownloadString("https://api.kxnrl.com/DNF/GoldCoins/ICheckVersion/v1/?v=" + Assembly.GetEntryAssembly().GetName().Version.ToString());
                    if (data.Contains("Out-Of-Date"))
                    {
                        MessageBox.Show("当前版本已过期");
                        Process.Start("https://github.com/Kxnrl/DNF-GoldCoin/releases");
                    }
                }
            }
            catch (Exception e) { Console.WriteLine("[CheckVersion] Exception: {0}", e.Message); }
        }
    }
}
