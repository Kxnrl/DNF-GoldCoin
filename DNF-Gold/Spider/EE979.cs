using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DNF_Gold.Spider
{
    class EE979
    {
        // 这种有良心API的网站真的好评

        private const string schema = "https";

        const string url = "https://api.ee979.com/api/Goods/list?access_token=";

        #region Item definition
        public class Item
        {
            [JsonProperty("sellCnt")]
            public long SellCnt;

            [JsonProperty("game")]
            public string Game;

            [JsonProperty("areaName")]
            public string AreaName;

            [JsonProperty("serverName")]
            public string ServerName;

            [JsonProperty("goodsType")]
            public string GoodsType;

            [JsonProperty("tradeType")]
            public string TradeType;

            [JsonProperty("titleSys")]
            public string TitleSys;

            [JsonProperty("goodsSN")]
            public string GoodsSn;

            [JsonProperty("totalNum")]
            public long TotalNum;

            [JsonProperty("unitPrice")]
            public double UnitPrice;

            [JsonProperty("startH")]
            public long StartH;

            [JsonProperty("endH")]
            public long EndH;

            [JsonProperty("seller")]
            public string Seller;

            [JsonProperty("target")]
            public long Target;

            [JsonProperty("status")]
            public string Status;

            [JsonProperty("scale1")]
            public double Scale1;

            [JsonProperty("scale2")]
            public double Scale2;

            [JsonProperty("mask")]
            public long Mask;

            [JsonProperty("sellerValid")]
            public long SellerValid;

            [JsonProperty("overpaid")]
            public long Overpaid;

            [JsonProperty("created")]
            public DateTimeOffset Created;

            [JsonProperty("modified")]
            public DateTimeOffset Modified;

            [JsonProperty("isPlatinumTrader")]
            public bool IsPlatinumTrader;
        }
        #endregion

        class Response
        {
            public string code;
            public List<Item> data;
        }

        public static void FetchData(Arena arena, List<ItemData> items)
        {
            var area = Enum.GetName(typeof(Arena), arena);

            try
            {
                using (var web = new WebClient())
                {
                    web.Headers[HttpRequestHeader.ContentType] = "application/json";
                    web.Headers[HttpRequestHeader.Accept] = "application/json";
                    web.Headers[HttpRequestHeader.Referer] = "https://www.ee979.com/goods?ex=cro" + arena + "&g=0&o=sc-d";
                    web.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36";

                    var j = JsonConvert.SerializeObject(new JObject
                    {
                        ["game"] = "dnf",
                        ["page"] = 1,
                        ["size"] = 20,
                        ["filter"] = new JObject
                        {
                            ["goodsType"] = "游戏币",
                            ["orderBy"] = "scale1-desc",
                            ["extra"] = new JObject
                            {
                                ["cross"] = "DNF跨1"
                            }
                        }
                    });

                    string json = Encoding.UTF8.GetString(web.UploadData(new Uri(url), "POST", Encoding.UTF8.GetBytes(j)));

                    var result = JsonConvert.DeserializeObject<Response>(json);

                    if (!result.code.Equals("success"))
                    {
                        throw new Exception("Response code: " + result.code);
                    }

                    foreach (var item in result.data)
                    {
                        var regex = new Regex(@"\d{4,}").Match(item.TitleSys);

                        if (!regex.Success)
                            continue;

                        ItemData data = new ItemData
                        {
                            Coins = uint.Parse(regex.Value),
                            Price = (float)item.UnitPrice,
                            Trade = GetTrade(item.TitleSys),
                            Ratio = (float)item.Scale1,
                            Scale = (float)item.Scale2,
                            Arena = area, //arena,
                            bLink = "https://www.ee979.com/goods/" + item.GoodsSn,
                            Sites = Sites.Site_EE979,
                            pGUID = Guid.NewGuid().ToString()
                        };

                        items.Add(data);
                    }
                }
            }
            catch (Exception ex) { Debug.Print("[EE979] Exception: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace); }
        }

        public static bool Buyable(string link)
        {
            // 暂时不支持...
            return true;
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
