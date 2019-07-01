using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DNF_Gold.Spider
{
    class DD373
    {
        private const string schema = "https";
        private static Dictionary<string, string> GuidDict = new Dictionary<string, string>();

        private static string GetURL(Arena arena)
        {
            switch (arena)
            {
                case Arena.跨1 : return "https://www.dd373.com/s/rbg22w-br53m0-d49m0f-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
                case Arena.跨2 : return "https://www.dd373.com/s/rbg22w-45vnhx-45vnhx-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
                case Arena.跨3A: return "https://www.dd373.com/s/rbg22w-w95dvc-tkwb6e-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
                case Arena.跨3B: return "https://www.dd373.com/s/rbg22w-pcc0jn-n28e76-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
                case Arena.跨4 : return "https://www.dd373.com/s/rbg22w-nvprvh-j7fp6k-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
                case Arena.跨5 : return "https://www.dd373.com/s/rbg22w-nvprvh-nvprvh-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
                case Arena.跨6 : return "https://www.dd373.com/s/rbg22w-50s4nq-4mnka8-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
                case Arena.跨7 : return "https://www.dd373.com/s/rbg22w-k2c5ab-gc43kd-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
                case Arena.跨8 : return "https://www.dd373.com/s/rbg22w-50s4nq-0m9x8u-0-0-0-42hcun-0-0-0-0-0-0-0-0.html";
            }

            return "https://www.kxnrl.com/";
        }

        public static void FetchData(Arena arena, List<ItemData> items)
        {
            var http = new HtmlWeb();
            var html = http.Load(GetURL(arena));
            var node = html.DocumentNode.SelectNodes("//div[contains(@class, 'box') and contains(@class, 'money_ner')]");
            var area = Enum.GetName(typeof(Arena), arena);

            foreach (var x in node)
            {
                try
                {
                    var lists = x.SelectNodes("./div");

                    var ahref = lists[0].SelectSingleNode("./span").SelectNodes("./a")[1].Attributes["href"].Value;
                    var title = lists[0].SelectSingleNode("./span").SelectNodes("./a")[1].Attributes["title"].Value;
                    //var arena = lists[0].SelectSingleNode("./div[@class='qufu']").SelectNodes("./a")[2].Attributes["title"].Value;
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
                        Arena = area, //arena,
                        bLink = schema + "://" + "www.dd373.com" + ahref,
                        Sites = Sites.Site_DD373,
                        pGUID = RepairGuid(ahref)
                    };

                    items.Add(data);
                }
                catch (Exception ex) { Debug.Print("[DD373] Exception: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace); }
            }
        }

        private static string RepairGuid(string unique)
        {
            if (!GuidDict.ContainsKey(unique))
            {
                // creation
                GuidDict.Add(unique, Guid.NewGuid().ToString());
            }
            return GuidDict[unique];
        }

        public static bool Buyable(string link)
        {
            var http = new HtmlWeb();
            var html = http.Load(link);
            var node = html.DocumentNode.SelectSingleNode("//a[@id='buyBtn']");
            return node != null;
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
