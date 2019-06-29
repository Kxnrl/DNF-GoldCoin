using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace DNF_Gold.Spider
{
    class S5173
    {
        private const string schema = "http";

        private static string GetURL(Arena arena)
        {
            switch (arena)
            {
                case Arena.跨1 : return "http://s.5173.com/dnf-xptjnl-f10pkw-uokzto-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
                case Arena.跨2 : return "http://s.5173.com/dnf-xptjnl-kt0l3t-qrw5cb-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
                case Arena.跨3A: return "http://s.5173.com/dnf-xptjnl-xsiupy-deer3j-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
                case Arena.跨3B: return "http://s.5173.com/dnf-xptjnl-riodz0-p1zmcp-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
                case Arena.跨4 : return "http://s.5173.com/dnf-xptjnl-43yfk3-nypj3u-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
                case Arena.跨5 : return "http://s.5173.com/dnf-xptjnl-43yfk3-ctrpel-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
                case Arena.跨6 : return "http://s.5173.com/dnf-xptjnl-joq4bd-dlrujw-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
                case Arena.跨7 : return "http://s.5173.com/dnf-xptjnl-uttqq3-hbbd3j-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
                case Arena.跨8 : return "http://s.5173.com/dnf-xptjnl-a0rpqj-lzoq2j-0-bx1xiv-0-0-0-a-a-a-a-a-0-itemprice_asc-0-0.shtml";
            }

            return "https://www.kxnrl.com/";
        }

        public static void FetchData(Arena arena, List<ItemData> items)
        {
            var http = new HtmlWeb();
            // 奇葩网站
            http.OverrideEncoding = Encoding.GetEncoding("gb2312");
            var html = http.Load(GetURL(arena));
            var node = html.DocumentNode.SelectNodes("//div[@class='sin_pdlbox']");
            var area = Enum.GetName(typeof(Arena), arena);

            foreach (var x in node)
            {
                try
                {
                    var lists = x.SelectNodes("./ul");

                    var ahref = lists[0].SelectNodes("./li")[0].SelectSingleNode("./h2").SelectSingleNode("./a").Attributes["href"].Value;
                    var title = lists[0].SelectNodes("./li")[0].SelectSingleNode("./h2").SelectSingleNode("./a").InnerText;
                  //var arena = lists[0].SelectNodes("./li")[3].SelectNodes("./a")[2].InnerText;
                    var regex = new Regex(@"\d{4,}").Match(title.Split('=')[0]);

                    if (!regex.Success)
                        continue;

                    var coins = regex.Value;
                    var trade = GetTrade(title);
                    var price = lists[1].SelectSingleNode("./li").SelectSingleNode("./strong").InnerText;
                    var ratio = lists[3].SelectNodes("./li")[0].SelectSingleNode("./b").InnerText.Replace("1元=", "");
                    var scale = lists[3].SelectNodes("./li")[1].InnerText.Replace("元/万金币", "");

                    ItemData data = new ItemData
                    {
                        Coins = uint.Parse(coins),
                        Price = float.Parse(price),
                        Trade = trade,
                        Ratio = float.Parse(ratio),
                        Scale = float.Parse(scale),
                        Arena = area, //arena,
                        bLink = ahref,
                        Sites = Sites.Site_5173,
                        pGUID = Guid.NewGuid().ToString()
                    };

                    items.Add(data);
                }
                catch (Exception ex) { Debug.Print("[5173] Exception: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace); }
            }
        }

        public static bool Buyable(string link)
        {
            var http = new HtmlWeb();
            var html = http.Load(link);
            var node = html.DocumentNode.SelectSingleNode("//div[@id='divNotBuy']");
            return node == null;
        }

        static Trade GetTrade(string text)
        {
            if (text.Contains("拍卖"))
                return Trade.拍卖;

            if (text.Contains("邮寄"))
                return Trade.邮寄;

            if (text.Contains("交易"))
                return Trade.交易;

            return Trade.未知;
        }
    }
}
