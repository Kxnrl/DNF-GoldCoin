﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DNF_Gold.Spider
{
    class UU898
    {
        private const string schema = "https";
        private const string fetchURL = "https://www.uu898.com/newTrade.aspx?gm=95&area=2322&srv=24986&c=-3&o=5&sa=0&p=1&ps=20&rm=1";

        public static void FetchData(List<ItemData> items)
        {
            var http = new HtmlWeb();
            var html = http.Load(fetchURL);
            var node = html.DocumentNode.SelectNodes("//ul[@class='splb_list']");

            foreach (var x in node)
            {
                try
                {
                    var sp_li0 = x.SelectSingleNode(".//li[contains(@class, 'sp_li0') and contains(@class, 'pos')]");
                    var sp_li1 = x.SelectSingleNode(".//li[@class='sp_li1']");

                    var li0_a = sp_li0.SelectSingleNode("./h2").SelectSingleNode("./a");

                    var ahref = schema + ":" + li0_a.Attributes["href"].Value;
                    var title = li0_a.InnerText;
                    var arena = HtmlEntity.DeEntitize(sp_li0.SelectSingleNode("./p[@class='qf_txt']").SelectSingleNode("./i").InnerText).Replace(">>", "");  //.SelectNodes("./a[@target='_blank']")[1].InnerText;
                    var price = x.SelectSingleNode("./li[contains(@class, 'Red') and contains(@class, 'zuan_dh')]").SelectSingleNode("./span").InnerText;
                    var match = sp_li1.SelectSingleNode("./h6").SelectNodes("./span");
                    var scale = string.Format("{0} [{1}]", match[0].InnerText, match[1].InnerText);
                    var regex = new Regex(@"\d{4,}").Match(title.Split('=')[1]);

                    if (!regex.Success)
                        continue;

                    var coins = regex.Value;
                    var trade = GetTrade(title);

                    ItemData data = new ItemData
                    {
                        Coins = uint.Parse(coins),
                        Price = float.Parse(price),
                        Trade = trade,
                        Ratio = float.Parse(match[0].InnerText.Replace("1元=", "").Replace("万金", "")),
                        Scale = float.Parse(match[1].InnerText.Replace("元/万金", "")),
                        Arena = arena,
                        bLink = ahref,
                        Sites = Sites.Site_UU898,
                        pGUID = Guid.NewGuid().ToString()
                    };

                    items.Add(data);
                }
                catch (Exception ex) { Console.WriteLine("[UU898] Exception: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace); }
            }
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
