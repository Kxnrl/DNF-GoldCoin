using System;
using System.IO;
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
    }
}
