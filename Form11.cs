using ScrapySharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reptile
{
    public partial class Form11 : Form
    {
        private static Dictionary<string, string> headerData = new Dictionary<string, string>();
        private static Dictionary<string, string> dic = new Dictionary<string, string>();
        public Form11()
        {
            InitializeComponent();
            dic.Add("CustomUnit", "计量单位代码");
            dic.Add("CustomAreaCode", "行政区号代码");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbType.DataSource = dic.ToList();
            cbType.ValueMember = "Key";
            cbType.DisplayMember = "Value";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string dicTypeId = tbDicTypeId.Text.Trim();
            string pageCount = tbPageCount.Text.Trim();
            Regex reg = new Regex(@"^[1-9]\d*$");
            if (!reg.IsMatch(pageCount))
            {
                MessageBox.Show("pageCount必须为正整数");
                return;
            }

            string genre = cbType.SelectedValue.ToString();
            string url = string.Empty;
            try
            {
                switch (genre)
                {
                    case "CustomUnit"://计量单位代码
                        url = "http://www.szceb.cn/publicQueryAction.do?method=searchUnit&loginSignal=1";
                        GetWrite("CustomUnit", url, dicTypeId, int.Parse(pageCount));
                        break;
                    case "CustomAreaCode"://行政区号代码
                        url = "http://www.szceb.cn/publicQueryAction.do?method=searchCity&loginSignal=1";
                        GetWrite("CustomAreaCode", url, dicTypeId, int.Parse(pageCount));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private void GetWrite(string type, string url, string dicTypeId, int pageIndex)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(dicTypeId))
            {
                dicTypeId = "NEWID()";
            }
            string sql_main = @"DECLARE @Id VARCHAR(50);" + Environment.NewLine;
            sql_main += @"INSERT INTO dbo.DicType(Id,DicTypeCode,DicTypeName,IsEditable,Usage,IsDeleted ,GroupName) VALUES ({0},'{1}','{2}',1,0,0,'包裹直购进口');";
            sb.Append(string.Format(sql_main, dicTypeId, dic.Where(x => x.Key == type).First().Key, dic.Where(x => x.Key == type).First().Value) + Environment.NewLine);
            sb.Append(string.Format("SELECT TOP 1 @Id=Id FROM DicType WHERE DicTypeCode='{0}'", dic.Where(x => x.Key == type).First().Key) + Environment.NewLine);

            Dictionary<string, string> dicUnit = new Dictionary<string, string>();
            for (int i = 1; i <= pageIndex; i++)
            {
                string param = $"page={i}&d={Guid.NewGuid()}";
                string responseHtml;
                try
                {
                    responseHtml = PostData(url, param);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"{i}:{ex.Message}");
                }

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(responseHtml);

                var tr = doc.DocumentNode.CssSelect(".dm03 table tr.dm05");
                if (tr == null || tr.Count() <= 0)
                {
                    throw new ApplicationException($"{i}:没有数据");
                }

                foreach (var item in tr)
                {
                    var tds = item.CssSelect("td").ToArray();
                    string code = tds[0].InnerText;
                    string name = tds[1].InnerText;
                    string v = string.Empty;
                    if (!dicUnit.TryGetValue(code, out v))
                    {
                        dicUnit.Add(code, name);
                    }
                }
            }
            string sql = @"INSERT INTO dbo.Dic(Id,DicTypeId,DicCode,DicName,Sequence,IsEditable,IsDisable,IsDeleted,CreationTime,CreatorUserId) VALUES ({0},{1},'{2}','{3}',{4},1,0,0,'{5}',1);";
            var num = 0;
            foreach (var item in dicUnit)
            {
                num++;
                string[] array = new string[] {
                    "NEWID()",
                    "@Id",
                    item.Key.Trim(),
                    item.Value.Trim(),
                    num.ToString(),
                    DateTime.Now.ToString()
                };
                sb.Append(string.Format(sql, array) + Environment.NewLine);
            }

            tbResult.Text = sb.ToString();
        }

        private string PostData<T>(string url, T postData)
        {
            //var token = _zoneTokenAppService.GetZoneToken(new Zones.Dto.GetZoneTokenInput() { CardNo = USER_CARD_NO });
            //if (token == null)
            //{
            //    throw new Exception("发送失败，Token未找到!");
            //}
            using (XWebClient webClient = new XWebClient())
            {
                //headerData["Cookie"] = token.Token;
                AddHeader(webClient, headerData);
                var res = postData.ToDictionary();
                var poststr = string.Join("&", res.Select(l => l.Key + "=" + System.Web.HttpUtility.UrlEncode(l.Value.ToSafeString(), Encoding.GetEncoding("GB2312"))));

                byte[] post = Encoding.UTF8.GetBytes(poststr);
                byte[] responseData = webClient.UploadData(url, "POST", post);//得到返回字符流  
                string responseHtml = Encoding.GetEncoding("GB2312").GetString(responseData);//解码

                return responseHtml;
            }
        }

        private string PostData(string url, string postData)
        {
            //var token = _zoneTokenAppService.GetZoneToken(new Zones.Dto.GetZoneTokenInput() { CardNo = USER_CARD_NO });
            //if (token == null)
            //{
            //    throw new Exception("发送失败，Token未找到!");
            //}
            using (XWebClient webClient = new XWebClient())
            {
                webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                webClient.Headers.Add("Accept-Language", "zh-CN");
                webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.75 Safari/537.36");
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                //webClient.Headers.Add("X-Requested-With", "XMLHttpRequest");
                webClient.Headers.Add("Referer", "http://www.szceb.cn/publicQueryAction.do?method=searchUnit&loginSignal=1");
                webClient.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                webClient.Headers.Add("Accept-Encoding", "gzip, deflate");
                webClient.Headers.Add("Host", "www.szceb.cn");
                webClient.Headers.Add("Pragma", "no-cache");
                webClient.Headers.Add("Cookie", "_gscu_273004030=79259444vm88y321; JSESSIONID=0000D2gMDH6ASP1goZqvfZ7WEuB:1a65drjq6");
                //headerData["Cookie"] = token.Token;
                AddHeader(webClient, headerData);

                byte[] post = Encoding.UTF8.GetBytes(postData);
                byte[] responseData = webClient.UploadData(url, "POST", post);//得到返回字符流  
                string responseHtml = Encoding.GetEncoding("GB2312").GetString(responseData);//解码

                return responseHtml;
            }
        }

        private void AddHeader(WebClient client, Dictionary<string, string> data)
        {
            foreach (var key in data.Keys)
            {
                client.Headers.Add(key, data[key]);
            }
        }

        public class XWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                return request;
            }
        }


        private void tbResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }
    }
}
