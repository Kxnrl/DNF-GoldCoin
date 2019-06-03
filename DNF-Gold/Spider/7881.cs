﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DNF_Gold.Spider
{
    class S7881
    {
        private const string schema = "http";
        private const string fetchURL = "http://search.7881.com/list.html?pageNum=1&gameId=G10&gtid=100001&carrierId=&groupId=G10P001&serverId=G10P001001&mobileGameType=&faceId=&tradeType=&tradePlace=0&sortType=orderbypriceunitasc&listSearchKeyWord=&mainSearchKeyWord=&minPrice=&maxPrice=&otherFilterValue=&tagName=&priceTag=";

        public static void FetchData(List<ItemData> items)
        {
            var http = new HtmlWeb();
            var html = http.Load(fetchURL);
            var node = html.DocumentNode.SelectNodes("//div[@class='list-item']");

            foreach (var n in node)
            {
                try
                {
                    var x = n.SelectSingleNode(".//div"); // list-item-box

                    var lists = x.SelectNodes("./div");

                    var ahref = lists[0].SelectSingleNode("./div").SelectSingleNode("./h2").SelectSingleNode("./a").Attributes["href"].Value;
                    var title = lists[0].SelectSingleNode("./div").SelectSingleNode("./h2").SelectSingleNode("./a").SelectSingleNode("./span").InnerText;
                    var arena = lists[0].SelectSingleNode("./div").SelectSingleNode("./h4").SelectSingleNode("./span").InnerText.Split('/')[1];
                    var regex = new Regex(@"\d{4,}").Match(title.Split('=')[0]);

                    if (!regex.Success)
                        continue;

                    var coins = regex.Value;
                    var trade = GetTrade(title);
                    var price = lists[1].SelectSingleNode("./h5").InnerText.Replace("¥ ", "");
                    var ratio = lists[2].SelectSingleNode("./h5").SelectNodes("./em")[1].InnerText;
                    var scale = lists[2].SelectSingleNode("./p").SelectSingleNode("./em").InnerText;

                    ItemData data = new ItemData
                    {
                        Coins = uint.Parse(coins),
                        Price = float.Parse(price),
                        Trade = trade,
                        Ratio = float.Parse(ratio),
                        Scale = float.Parse(scale),
                        Arena = "跨1", //arena,
                        bLink = schema + "://" + "search.7881.com" + ahref,
                        Sites = Sites.Site_7881,
                        pGUID = Guid.NewGuid().ToString()
                    };

                    items.Add(data);
                }
                catch (Exception ex) { Console.WriteLine("[7881] Exception: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace); }
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