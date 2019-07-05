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
        #region tray
        ContextMenu notifyMenu;
        MenuItem showHide;
        MenuItem exitButton;
        #endregion
        #region global variables
        private bool closing = false;
        private bool beginUpdate = false;
        private static string configFile;

        private static IntPtr myHandle;
        private static SettingsUI stsUI;
        #endregion
        #region timer
        // timer handler
        private System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();
        public static uint tickTimer = 30u;
        #endregion
        #region configs
        public static float N_MaxPrice = 0.0f;
        public static float N_MinRatio = 30.0f;
        public static bool N_Enabled = false;
        public static bool N_Background = false;
        public static bool N_AutoRefresh = false;
        public static bool N_AllowTrade_T = true;
        public static bool N_AllowTrade_M = true;
        public static bool N_AllowTrade_S = true;
        #endregion

        static Dictionary<string, ItemData> ItemDict = new Dictionary<string, ItemData>();

        public UI()
        {
            InitializeComponent();
            InitializeTable();
            InitializeNotifaction();
            InitializeArenas();
            InitializeConfings();

            Icon = Properties.Resources.dfo;

            stsUI = new SettingsUI();
        }

        private void BackgroundRefresh()
        {
            List<string> sellOut = new List<string>();

            while (!closing)
            {
                BeginCheckPrice(sellOut);
            }
        }

        private void InitializeArenas()
        {
            foreach (var a in Enum.GetNames(typeof(Arena)))
            {
                ES_Arena.Items.Add(a);
            }
        }

        private void InitializeTable()
        {
            //ItemsList.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            //ItemsList.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            //ItemsList.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Transparent;
            ItemsList.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ItemsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ItemsList.Columns["pCoins"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ItemsList.Columns["pRecvs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

            new Thread(RefreshData)
            {
                IsBackground = true,
                Priority = ThreadPriority.AboveNormal,
            }.Start();
            new Thread(BackgroundRefresh)
            {
                IsBackground = true,
                Priority = ThreadPriority.Lowest,
                Name = "Check Buyable"
            }.Start();
        }

        private void UI_Shown(object sender, EventArgs e)
        {
            ES_Arena.SelectedIndexChanged += new EventHandler(ArenaOnChanged);
        }

        private void OnRefreshTimer(object sender, EventArgs e)
        {
            if (beginUpdate)
                return;

            tickTimer--;

            if (tickTimer <= 0 && N_AutoRefresh)
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
            if (closing) return;

            beginUpdate = true;

            var arena = Arena.跨1;

            Invoke(new Action(() =>
            {
                ES_Refresh.Enabled = false;
                Label_CD.Text = "...";
                arena = (Arena)Enum.Parse(typeof(Arena), ES_Arena.Items[ES_Arena.SelectedIndex].ToString());
            }));

            List<ItemData> items = new List<ItemData>();

            var resetEvent = new ManualResetEvent[5];

            for (int e = 0; e < 5; ++e)
            {
                resetEvent[e] = new ManualResetEvent(false);
            }

            if (ES_UU898.Checked)
            {
                new Thread(() =>
                {
                    Spider.UU898.FetchData(arena, items);
                    resetEvent[0].Set();
                })
                {
                    IsBackground = true,
                    Priority = ThreadPriority.BelowNormal,
                    Name = "Fetch UU898"
                }.Start();
            }

            if (ES_DD373.Checked)
            {
                new Thread(() =>
                {
                    Spider.DD373.FetchData(arena, items);
                    resetEvent[1].Set();
                })
                {
                    IsBackground = true,
                    Priority = ThreadPriority.BelowNormal,
                    Name = "Fetch DD373"
                }.Start();
            }

            if (ES_EE979.Checked)
            {
                new Thread(() =>
                {
                    Spider.EE979.FetchData(arena, items);
                    resetEvent[2].Set();
                })
                {
                    IsBackground = true,
                    Priority = ThreadPriority.BelowNormal,
                    Name = "Fetch EE979"
                }.Start();
            }

            if (ES_7881.Checked)
            {
                new Thread(() =>
                {
                    Spider.S7881.FetchData(arena, items);
                    resetEvent[3].Set();
                })
                {
                    IsBackground = true,
                    Priority = ThreadPriority.BelowNormal,
                    Name = "Fetch 7881"
                }.Start();
            }

            if (ES_5173.Checked)
            {
                new Thread(() =>
                {
                    Spider.S5173.FetchData(arena, items);
                    resetEvent[4].Set();
                })
                {
                    IsBackground = true,
                    Priority = ThreadPriority.BelowNormal,
                    Name = "Fetch 5173"
                }.Start();
            }

            WaitHandle.WaitAll(resetEvent);

            foreach (var e in resetEvent)
            {
                e.Close();
                e.Dispose();
            }

            Invoke(new Action(() =>
            {
                var count = 0;
                var ratio = 0.0f;

                SetAllControls(false);

                ItemsList.Rows.Clear();

                foreach (var item in items.OrderByDescending(p => p.Ratio).ToList())
                {
                    // 过滤异常不能交易的金币?
                    // 黑商你妈死了?
                    // 哄抬你妈的金价呢?
                    if (item.Price >= 10000f || item.Ratio >= 100f)
                    {
                        continue;
                    }

                    // 交易设置过滤
                    if (item.Trade is Trade.交易 && !N_AllowTrade_T)
                    {
                        // 不允许当面交易
                        continue;
                    }
                    if (item.Trade is Trade.邮寄 && !N_AllowTrade_M)
                    {
                        // 不允许邮寄交易
                        continue;
                    }
                    if (item.Trade is Trade.拍卖 && !N_AllowTrade_S)
                    {
                        // 不允许拍卖交易
                        continue;
                    }

                    // 通知中心
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

                    ItemDict[item.pGUID] = item;
                    ItemsList.Rows.Add(item.pGUID, item.Coins, (float)(item.Coins * (item.Trade == Trade.邮寄 ? 0.95 : 0.97)), item.Price, item.Ratio, item.Arena, Enum.GetName(typeof(Trade), item.Trade), Enum.GetName(typeof(Sites), item.Sites).Replace("Site_", ""), RandomButton(), item.bLink);
                }

                if (count > 0)
                {
                    BG_Notifaction.BalloonTipTitle = "DNF金币比价器";
                    BG_Notifaction.BalloonTipText = "发现了 " + count + " 件商品符合要求, 最高比例为 1:" + ratio.ToString("N2");
                    BG_Notifaction.ShowBalloonTip(10000);
                }

                SetAllControls(true);

                ItemsList.Update();
                ItemsList.ClearSelection();

                Text = "DNF金币比价器" + "   " + "[" + Enum.GetName(typeof(Arena), arena) + "]";
            }));

            tickTimer = 30;
            TimerCountDown();
            beginUpdate = false;
        }

        private void BeginCheckPrice(List<string> sellOut)
        {
            if (closing) return;

            var list = new List<string>();

            Invoke(new Action(() =>
            {
                for (int index = 0; index < ItemsList.Rows.Count; ++index)
                {
                    if (!ItemsList.Rows[index].Displayed)
                        continue;

                    var pGuid = ItemsList.Rows[index].Cells["pGUID"].Value.ToString();

                    if (string.IsNullOrEmpty(pGuid) || sellOut.Contains(pGuid))
                        continue;

                    list.Add(pGuid);
                }
            }));

            foreach (var key in list)
            {
                var time = DateTime.Now;

                CheckItemBuyable(key, sellOut);

                var diff = (int)(DateTime.Now - time).TotalMilliseconds;
                if (diff < 1000)
                {
                    // delay
                    Thread.Sleep(1000 - diff);
                }
            }
        }

        private void CheckItemBuyable(string guid, List<string> sellOut)
        {
            try
            {
                var item = ItemDict[guid];
                var buyable = true;

                switch (item.Sites)
                {
                    case Sites.Site_5173:  buyable = Spider.S5173.Buyable(item.bLink); break;
                    case Sites.Site_7881:  buyable = Spider.S7881.Buyable(item.bLink); break;
                    case Sites.Site_DD373: buyable = Spider.DD373.Buyable(item.bLink); break;
                    case Sites.Site_UU898: buyable = Spider.UU898.Buyable(item.bLink); break;
                    case Sites.Site_EE979: buyable = Spider.EE979.Buyable(item.bLink); break;
                }

#if DEBUG
                //Debug.Print("Check [{0}] on [{1}] result {2}", item.pGUID, item.bLink, buyable);
#endif

                if (buyable)
                    return;

                sellOut.Add(item.pGUID);

                Invoke(new Action(() =>
                {
                    for (int index = 0; index < ItemsList.Rows.Count; ++index)
                    {
                        if (item.pGUID.Equals(ItemsList.Rows[index].Cells["pGUID"].Value.ToString()))
                        {
                            ItemsList.Rows[index].Cells["Action"].Value = "已被购买";
#if DEBUG
                            Debug.Print("Update Status [{0}]", item.pGUID);
#endif
                            break;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                Debug.Print("Background Check Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
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
                if (!N_AutoRefresh)
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
            switch (new Random(seed++).Next(0, 6))
            {
                case 0: return "马上剁手";
                case 1: return "再来五北";
                case 2: return "我要氪金";
                case 3: return "借我捌万";
                case 4: return "最后一百";
                case 5: return "我还能充";
                case 6: return "充满七万";
            }

            return "";
        }

        private void ItemList_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                try
                {
                    if (ItemsList.Rows[e.RowIndex].Cells["Action"].Value.ToString().Equals("已被购买"))
                        return;

                    var url = ItemsList.Rows[e.RowIndex].Cells["pBLink"].Value.ToString();
                    Process.Start(url);
                }
                catch { }
            }
            else
            {
                //ItemsList.ClearSelection();
            }
        }

        private void CheckSourceCode(object sender, EventArgs e)
        {
            try { Process.Start("https://github.com/Kxnrl/DNF-GoldCoin"); } catch { }
        }

        private void Label_Kxnrl_Click(object sender, EventArgs e)
        {
            MessageBox.Show("跨①纯女鬼剑士工会" + Environment.NewLine +
                            "[小萝莉玫瑰花店]" + Environment.NewLine +
                            "欢迎你的加入!",
                            "嘤嘤嘤~ 打死你个嘤嘤怪",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

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

        private void ArenaOnChanged(object sender, EventArgs e)
        {
            tickTimer = 30;

            ES_Arena.Enabled = false;

            new Thread(RefreshData)
            {
                IsBackground = true,
                Name = "Update Thread",
                Priority = ThreadPriority.BelowNormal
            }.Start();
        }

        private void ES_Refresh_Click(object sender, EventArgs e)
        {
            tickTimer = 30;

            ES_Refresh.Enabled = false;

            new Thread(RefreshData)
            {
                IsBackground = true,
                Name = "Update Thread",
                Priority = ThreadPriority.BelowNormal
            }.Start();
        }

        private void ES_Setting_Click(object sender, EventArgs e)
        {
            if (beginUpdate)
                return;

            if (stsUI.ShowDialog(this) == DialogResult.OK)
            {
                SaveConfigs();

                if (!beginUpdate)
                {
                    new Thread(RefreshData)
                    {
                        IsBackground = true,
                        Name = "Update Thread",
                        Priority = ThreadPriority.BelowNormal
                    }.Start();
                }
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (N_Background)
            {
                myHandle = Process.GetCurrentProcess().MainWindowHandle;
                Win32Api.Window.ShowWindow(myHandle, Win32Api.Window.SW_HIDE);
                BG_Notifaction.BalloonTipTitle = "DNF金币比价器";
                BG_Notifaction.BalloonTipText = "现在已经开始后台工作啦.~";
                BG_Notifaction.ShowBalloonTip(3000);
            }
            else if (!closing)
            {
                BG_Notifaction.Visible = false;
                BG_Notifaction.Icon = null;
                BG_Notifaction.Dispose();

                closing = true;

                new Thread(() =>
                {
                    Thread.Sleep(1000);
                    while (beginUpdate)
                    {
                        Thread.Sleep(100);
                    }
                    Invoke(new Action(() =>
                    {
                        Close();
                        Dispose();
                        Environment.ExitCode = 10086;
                    }));
                })
                {
                    IsBackground = true,
                    Priority = ThreadPriority.Lowest,
                    Name = "Cleaner thread"
                }.Start();
            }
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfigs();
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

        private void OpenNotifaction(object sender, MouseEventArgs e)
        {
            Win32Api.Window.ShowWindow(myHandle, Win32Api.Window.SW_SHOW);
        }

        private void OpenNotifaction(object sender, EventArgs e)
        {
            Win32Api.Window.ShowWindow(myHandle, Win32Api.Window.SW_SHOW);
        }

        #region Ini helper
        private object Ini2Enum(string section, string key, Type type)
        {
            var data = Win32Api.Profile.GetIniSectionValue(configFile, section, key, "跨1");
            return Enum.Parse(type, data);
        }

        private void Enum2Ini(string section, string key, Type type, object val)
        {
            Win32Api.Profile.SetIniSectionValue(configFile, section, key, Enum.GetName(type, val));
        }

        private bool Ini2Bool(string section, string key, bool def = false)
        {
            var data = Win32Api.Profile.GetIniSectionValue(configFile, section, key);
            return string.IsNullOrEmpty(data) ? def : data.Equals("true");
        }

        private void Bool2Ini(string section, string key, bool value)
        {
            Win32Api.Profile.SetIniSectionValue(configFile, section, key, value ? "true" : "false");
        }

        private float Ini2Float(string section, string key, float def = 0f)
        {
            var data = Win32Api.Profile.GetIniSectionValue(configFile, section, key);
            if (float.TryParse(data, out float ret))
                return ret;
            return def;
        }

        private void Float2Ini(string section, string key, float value)
        {
            Win32Api.Profile.SetIniSectionValue(configFile, section, key, value.ToString("N2"));
        }
        #endregion
        #region config loader
        private void InitializeConfings()
        {
            configFile = Path.Combine(Program.baseFolder, "dnf.conf");

            var inited = Ini2Bool("DNF-Gold.Global", "Initialized");

            if (!inited)
            {
                Bool2Ini("DNF-Gold.Global", "Initialized", true);

                ImportConfigs(new Dictionary<string, object>
                {
                    ["ES_Arena"] = Arena.跨1,

                    ["ES_7881"] = true,
                    ["ES_5173"] = true,

                    ["ES_DD373"] = true,
                    ["ES_UU898"] = true,
                    ["ES_EE979"] = true,

                    ["AutoRefresh"] = true,
                    ["BackGround"] = false,

                    ["N_Enabled"] = false,
                    ["N_MaxPrice"] = 2000.0f,
                    ["N_MinRatio"] = 50.0f,

                    ["N_AllowTrade_T"] = true,
                    ["N_AllowTrade_S"] = true,
                    ["N_AllowTrade_M"] = true,
                });

                SaveConfigs();
            }
            else
            {
                ImportConfigs(new Dictionary<string, object>
                {
                    ["ES_Arena"] = Ini2Enum("DNF-Gold.Global", "Arena", typeof(Arena)),

                    ["ES_7881"] = Ini2Bool("DNF-Gold.Enabled", "7881", true),
                    ["ES_5173"] = Ini2Bool("DNF-Gold.Enabled", "5173", true),

                    ["ES_DD373"] = Ini2Bool("DNF-Gold.Enabled", "DD373", true),
                    ["ES_UU898"] = Ini2Bool("DNF-Gold.Enabled", "UU898", true),
                    ["ES_EE979"] = Ini2Bool("DNF-Gold.Enabled", "EE979", true),

                    ["AutoRefresh"] = Ini2Bool("DNF-Gold.Enabled", "AutoRefresh", false),
                    ["BackGround"] = Ini2Bool("DNF-Gold.Enabled", "BackgroundWorker", false),

                    ["N_Enabled"] = Ini2Bool("DNF-Gold.Notifaction", "Enabled", false),
                    ["N_MaxPrice"] = Ini2Float("DNF-Gold.Notifaction", "MaxPrice", 2000f),
                    ["N_MinRatio"] = Ini2Float("DNF-Gold.Notifaction", "MinRatio", 55f),

                    ["N_AllowTrade_T"] = Ini2Bool("DNF-Gold.TradeRules", "Face", true),
                    ["N_AllowTrade_S"] = Ini2Bool("DNF-Gold.TradeRules", "Sale", true),
                    ["N_AllowTrade_M"] = Ini2Bool("DNF-Gold.TradeRules", "Mail", true),
                });
            }
        }

        private void ImportConfigs(Dictionary<string, object> conf)
        {
            var arena = Enum.GetName(typeof(Arena), conf["ES_Arena"]);
            for (int index = 0; index < ES_Arena.Items.Count; ++index)
            {
                if (arena.Equals(ES_Arena.Items[index].ToString()))
                {
                    ES_Arena.SelectedIndex = index;
                    break;
                }
            }

            ES_7881.Checked = (bool)conf["ES_7881"];
            ES_5173.Checked = (bool)conf["ES_5173"];

            ES_DD373.Checked = (bool)conf["ES_DD373"];
            ES_UU898.Checked = (bool)conf["ES_UU898"];
            ES_EE979.Checked = (bool)conf["ES_EE979"];

            N_AutoRefresh = (bool)conf["AutoRefresh"];
            N_Background = (bool)conf["BackGround"];

            N_Enabled = (bool)conf["N_Enabled"];
            N_MaxPrice = (float)conf["N_MaxPrice"];
            N_MinRatio = (float)conf["N_MinRatio"];

            N_AllowTrade_T = (bool)conf["N_AllowTrade_T"];
            N_AllowTrade_S = (bool)conf["N_AllowTrade_S"];
            N_AllowTrade_M = (bool)conf["N_AllowTrade_M"];
        }

        private void SaveConfigs()
        {
            Bool2Ini("DNF-Gold.Enabled", "7881", ES_7881.Checked);
            Bool2Ini("DNF-Gold.Enabled", "5173", ES_5173.Checked);

            Bool2Ini("DNF-Gold.Enabled", "DD373", ES_DD373.Checked);
            Bool2Ini("DNF-Gold.Enabled", "UU898", ES_UU898.Checked);
            Bool2Ini("DNF-Gold.Enabled", "EE979", ES_EE979.Checked);

            Bool2Ini("DNF-Gold.Enabled", "AutoRefresh", N_AutoRefresh);
            Bool2Ini("DNF-Gold.Enabled", "BackgroundWorker", N_Background);

            Bool2Ini("DNF-Gold.Notifaction", "Enabled", N_Enabled);
            Float2Ini("DNF-Gold.Notifaction", "N_MaxPrice", N_MaxPrice);
            Float2Ini("DNF-Gold.Notifaction", "MinRatio", N_MinRatio);

            Bool2Ini("DNF-Gold.TradeRules", "Face", N_AllowTrade_T);
            Bool2Ini("DNF-Gold.TradeRules", "Sale", N_AllowTrade_S);
            Bool2Ini("DNF-Gold.TradeRules", "Mail", N_AllowTrade_M);
        }
        #endregion
    }
}
