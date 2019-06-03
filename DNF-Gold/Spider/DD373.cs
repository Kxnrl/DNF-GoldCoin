using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DNF_Gold.Spider
{
    class DD373
    {
        private const string schema = "https";
        private const string fetchURL = "https://www.dd373.com/s/rbg22w-br53m0-d49m0f-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";

        public static void FetchData(List<ItemData> items)
        {
            var http = new HtmlWeb();
            var html = http.Load(fetchURL);
            var node = html.DocumentNode.SelectNodes("//div[contains(@class, 'box') and contains(@class, 'money_ner')]");

            foreach (var x in node)
            {
                try
                {
                    var lists = x.SelectNodes("./div");

                    var ahref = lists[0].SelectSingleNode("./span").SelectNodes("./a")[1].Attributes["href"].Value;
                    var title = lists[0].SelectSingleNode("./span").SelectNodes("./a")[1].Attributes["title"].Value;
                    var arena = lists[0].SelectSingleNode("./div[@class='qufu']").SelectNodes("./a")[2].Attributes["title"].Value;
                    var regex = new Regex(@"\d{4,}").Match(title.Split('=')[0]);

                    if (!regex.Success)
                        continue;

                    var coins = regex.Value;
                    var trade = GetTrade(title);
                    var price = lists[1].SelectSingleNode("./div").SelectSingleNode("./strong").SelectSingleNode("./span").InnerText;
                    var ratio = lists[4].SelectNodes("./div")[0].SelectNodes("./p")[0].InnerText.Trim().Replace("1元=", "").Replace("万金币", "");
                    var scale = lists[4].SelectNodes("./div")[0].SelectNodes("./p")[1].InnerText.Trim().Replace("元/万金币", "");

                    ItemData data = new ItemData
                    {
                        Coins = uint.Parse(coins),
                        Price = float.Parse(price),
                        Trade = trade,
                        Ratio = float.Parse(ratio),
                        Scale = float.Parse(scale),
                        Arena = "跨1", //arena,
                        bLink = schema + "://" + "www.dd373.com" + ahref,
                        Sites = Sites.Site_DD373,
                        pGUID = Guid.NewGuid().ToString()
                    };

                    items.Add(data);
                }
                catch (Exception ex) { Console.WriteLine("[DD373] Exception: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace); }
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
