using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DNF_Gold
{
    public partial class UI : Form
    {
        ContextMenu notifyMenu;
        MenuItem showHide;
        MenuItem exitButton;

        private System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();
        private uint tickTimer = 30u;
        private bool beginUpdate = false;
        private static string configFile;

        private static IntPtr myHandle;
        private static SettingsUI stsUI;

        public static float N_MaxPrice = 0.0f;
        public static float N_MinRatio = 30.0f;
        public static bool  N_Enabled = false;

        public UI()
        {
            InitializeComponent();
            InitializeTable();
            InitializeNotifaction();
            InitializeConfings();

            Icon = Properties.Resources.dfo;

            stsUI = new SettingsUI();
        }

        private void InitializeTable()
        {
            ItemsList.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ItemsList.Columns["pCoins"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ItemsList.Columns["pPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void InitializeNotifaction()
        {
            notifyMenu = new ContextMenu();
            showHide = new MenuItem("Show");
            exitButton = new MenuItem("Exit");
            notifyMenu.MenuItems.Add(0, showHide);
            notifyMenu.MenuItems.Add(1, exitButton);
            showHide.Click += new EventHandler(ApplicationHandler_TrayIcon);
            exitButton.Click += new EventHandler(ApplicationHandler_TrayIcon);

            BG_Notifaction.Icon = Properties.Resources.dfo;
            BG_Notifaction.ContextMenu = notifyMenu;
            BG_Notifaction.Visible = true;
        }

        private void ApplicationHandler_TrayIcon(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;

            if (item == exitButton)
            {
                BG_Notifaction.Visible = false;
                BG_Notifaction.Icon = null;
                BG_Notifaction.Dispose();
                Thread.Sleep(100);
                SaveConfigs();
                Environment.Exit(0);
            }
            else if (item == showHide)
            {
                Win32Api.Window.ShowWindow(myHandle, Win32Api.Window.SW_SHOW);
            }
        }

        private void UI_Load(object sender, EventArgs e)
        {
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += OnRefreshTimer;
            refreshTimer.Enabled = true;
            refreshTimer.Start();

            new Thread(RefreshData).Start();
        }

        private void OnRefreshTimer(object sender, EventArgs e)
        {
            if (beginUpdate)
                return;

            tickTimer--;

            if (tickTimer <= 0 && ES_AutoRefresh.Checked)
            {
                new Thread(RefreshData)
                {
                    IsBackground = true,
                    Name = "Update Thread",
                    Priority = ThreadPriority.BelowNormal
                }.Start();
            }

            TimerCountDown();
        }

        private void RefreshData()
        {
            beginUpdate = true;

            Invoke(new Action(() =>
            {
                ES_Refresh.Enabled = false;
                Label_CD.Text = "...";
            }));

            List<ItemData> items = new List<ItemData>();

            if (ES_UU898.Checked) Spider.UU898.FetchData(items);
            if (ES_DD373.Checked) Spider.DD373.FetchData(items);
            if (ES_7881.Checked) Spider.S7881.FetchData(items);
            if (ES_5173.Checked) Spider.S5173.FetchData(items);

            Invoke(new Action(() =>
            {
                var count = 0;
                var ratio = 0.0f;

                SetAllControls(false);

                ItemsList.Rows.Clear();

                foreach (var item in items.OrderByDescending(p => p.Ratio).ToList())
                {
                    if (N_Enabled &&
                        item.Price <= N_MaxPrice &&
                        item.Ratio >= N_MinRatio)
                    {
                        count++;

                        if (item.Ratio > ratio)
                        {
                            ratio = item.Ratio;
                        }
                    }

                    ItemsList.Rows.Add(item.pGUID, item.Coins, item.Price, item.Ratio, item.Arena, Enum.GetName(typeof(Trade), item.Trade), Enum.GetName(typeof(Sites), item.Sites).Replace("Site_", ""), RandomButton(), item.bLink);
                }

                if (count > 0)
                {
                    BG_Notifaction.BalloonTipTitle = "DNF金币比价器";
                    BG_Notifaction.BalloonTipText = "发现了 " + count + " 件商品符合要求, 最高比例为 1:" + ratio.ToString("N2");
                    BG_Notifaction.ShowBalloonTip(10000);
                }

                SetAllControls(true);
            }));

            tickTimer = 30;
            TimerCountDown();
            beginUpdate = false;
        }

        private void SetAllControls(bool enabled)
        {
            foreach (Control c in Controls)
            {
                c.Enabled = enabled;
            }
        }

        private void TimerCountDown()
        {
            Invoke(new Action(() =>
            {
                if (!ES_AutoRefresh.Checked)
                {
                    Label_CD.Text = "∞";
                }
                else
                {
                    Label_CD.Text = tickTimer.ToString();
                }
            }));
        }

        int seed = 0;
        private string RandomButton()
        {
            switch (new Random(seed++).Next(0, 3))
            {
                case 0: return "马上剁手";
                case 1: return "再来五北";
                case 2: return "我要氪金";
                case 3: return "借我捌万";
            }

            return "";
        }

        private void ItemList_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                try
                {
                    var url = ItemsList.Rows[e.RowIndex].Cells["pBLink"].Value.ToString();
                    Process.Start(url);
                }
                catch { }
            }
        }

        private void Label_Kxnrl_Click(object sender, EventArgs e)
        {
            try { Process.Start("https://www.kxnrl.com/"); } catch { }
        }

        private void Label_WARN_Click(object sender, EventArgs e)
        {
            MessageBox.Show("注意:" + Environment.NewLine +
                            "确认网站是否为钓鱼网站或是否DNS污染" + Environment.NewLine +
                            "交易过程中请不要相信任何人" + Environment.NewLine +
                            "" + Environment.NewLine +
                            "使用本软件引发的一切后果均由使用者承担!",
                            "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        private void ES_Refresh_Click(object sender, EventArgs e)
        {
            tickTimer = 30;

            new Thread(RefreshData)
            {
                IsBackground = true,
                Name = "Update Thread",
                Priority = ThreadPriority.BelowNormal
            }.Start();
        }

        private void ES_Notifaction_Click(object sender, EventArgs e)
        {
            if (stsUI.ShowDialog(this) == DialogResult.OK)
            {
                SaveConfigs();
            }
        }

        private void ES_AutoUpdateOnChecked(object sender, EventArgs e)
        {
            if (ES_AutoRefresh.Checked)
            {
                tickTimer = 30;
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (ES_KeepAlive.Checked)
            {
                e.Cancel = true;
                myHandle = Process.GetCurrentProcess().MainWindowHandle;
                Win32Api.Window.ShowWindow(myHandle, Win32Api.Window.SW_HIDE);
                BG_Notifaction.BalloonTipTitle = "DNF金币比价器";
                BG_Notifaction.BalloonTipText = "现在已经开始后台工作啦.~";
                BG_Notifaction.ShowBalloonTip(3000);
            }
            else
            {
                BG_Notifaction.Visible = false;
                BG_Notifaction.Icon = null;
                BG_Notifaction.Dispose();
                Thread.Sleep(100);
            }
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfigs();
        }

        private void OpenNotifaction(object sender, MouseEventArgs e)
        {
            Win32Api.Window.ShowWindow(myHandle, Win32Api.Window.SW_SHOW);
        }

        private void OpenNotifaction(object sender, EventArgs e)
        {
            Win32Api.Window.ShowWindow(myHandle, Win32Api.Window.SW_SHOW);
        }

        private void InitializeConfings()
        {
            configFile = Path.Combine(Program.baseFolder, "dnf.conf");

            var inited = Ini2Bool("DNF-Gold.Global", "Initialized");

            if (!inited)
            {
                Bool2Ini("DNF-Gold.Global", "Initialized", true);

                ImportConfigs(new Dictionary<string, object>
                {
                    ["ES_7881"] = true,
                    ["ES_5173"] = true,

                    ["ES_DD373"] = true,
                    ["ES_UU898"] = true,

                    ["AutoRefresh"] = true,
                    ["BackGround"] = true,

                    ["N_Enabled"] = false,
                    ["N_MaxPrice"] = 2000.0f,
                    ["N_MinRatio"] = 50.0f
                });

                SaveConfigs();
            }
            else
            {
                ImportConfigs(new Dictionary<string, object>
                {
                    ["ES_7881"] = Ini2Bool("DNF-Gold.Enabled", "7881"),
                    ["ES_5173"] = Ini2Bool("DNF-Gold.Enabled", "5173"),

                    ["ES_DD373"] = Ini2Bool("DNF-Gold.Enabled", "DD373"),
                    ["ES_UU898"] = Ini2Bool("DNF-Gold.Enabled", "UU898"),

                    ["AutoRefresh"] = Ini2Bool("DNF-Gold.Enabled", "AutoRefresh"),
                    ["BackGround"] = Ini2Bool("DNF-Gold.Enabled", "BackgroundWorker"),

                    ["N_Enabled"] = Ini2Bool("DNF-Gold.Notifaction", "Enabled"),
                    ["N_MaxPrice"] = Ini2Float("DNF-Gold.Notifaction", "MaxPrice"),
                    ["N_MinRatio"] = Ini2Float("DNF-Gold.Notifaction", "MinRatio")
                });
            }
        }

        private bool Ini2Bool(string section, string key)
        {
            var data = Win32Api.Profile.GetIniSectionValue(configFile, section, key);
            return data.Equals("true");
        }

        private void Bool2Ini(string section, string key, bool value)
        {
            Win32Api.Profile.SetIniSectionValue(configFile, section, key, value ? "true" : "false");
        }

        private float Ini2Float(string section, string key)
        {
            var data = Win32Api.Profile.GetIniSectionValue(configFile, section, key);
            if (float.TryParse(data, out float ret))
                return ret;
            return 0.0f;
        }

        private void Float2Ini(string section, string key, float value)
        {
            Win32Api.Profile.SetIniSectionValue(configFile, section, key, value.ToString("N2"));
        }

        private void ImportConfigs(Dictionary<string, object> conf)
        {
            ES_7881.Checked = (bool)conf["ES_7881"];
            ES_5173.Checked = (bool)conf["ES_5173"];

            ES_DD373.Checked = (bool)conf["ES_DD373"];
            ES_UU898.Checked = (bool)conf["ES_UU898"];

            ES_AutoRefresh.Checked = (bool)conf["AutoRefresh"];
            ES_KeepAlive.Checked = (bool)conf["BackGround"];

            N_Enabled = (bool)conf["N_Enabled"];
            N_MaxPrice = (float)conf["N_MaxPrice"];
            N_MinRatio = (float)conf["N_MinRatio"];
        }

        private void SaveConfigs()
        {
            Bool2Ini("DNF-Gold.Enabled", "7881", ES_7881.Checked);
            Bool2Ini("DNF-Gold.Enabled", "5173", ES_5173.Checked);

            Bool2Ini("DNF-Gold.Enabled", "DD373", ES_DD373.Checked);
            Bool2Ini("DNF-Gold.Enabled", "UU898", ES_UU898.Checked);

            Bool2Ini("DNF-Gold.Enabled", "AutoRefresh", ES_AutoRefresh.Checked);
            Bool2Ini("DNF-Gold.Enabled", "BackgroundWorker", ES_KeepAlive.Checked);

            Bool2Ini("DNF-Gold.Notifaction", "Enabled", N_Enabled);
            Float2Ini("DNF-Gold.Notifaction", "MaxPrice", N_MaxPrice);
            Float2Ini("DNF-Gold.Notifaction", "MinRatio", N_MinRatio);
        }

        private void OnFormResized(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                myHandle = Process.GetCurrentProcess().MainWindowHandle;
                Win32Api.Window.ShowWindow(myHandle, Win32Api.Window.SW_HIDE);
                BG_Notifaction.BalloonTipTitle = "DNF金币比价器";
                BG_Notifaction.BalloonTipText = "现在已经开始后台工作啦.~";
                BG_Notifaction.ShowBalloonTip(3000);
            }
        }
    }
}
