using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reptile
{
    public partial class Reptile : Form
    {
        private int optId;
        private int page;
        private decimal minPrice, maxPrice = 0m;
        private string url = string.Empty;
        PddSettingEntity data;
        private static Dictionary<string, string> headerData = new Dictionary<string, string>();
        public Reptile()
        {
            InitializeComponent();
            this.cmbPage.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //...test
            //load setting
            string url = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                data = LoadJson(Path.Combine(url, "setting2.json"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Json文件配置错误！请联系管理员！");
                return;
            }
            //绑定类目一
            cmbOne.DataSource = data.optList.Select(l => new ComboBoxItem { Value = l.optId, Text = l.optName }).ToList();
            cmbOne.ValueMember = "Value";
            cmbOne.DisplayMember = "Text";

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (Vaild() == true) return;

            //page
            page = int.Parse(cmbPage.Text);

            int i_optId = 9981;
            string siteUrl = data.siteUrl;
            string detailUrl = data.detailUrl;
            string requestUrl = @"https://pifa.pinduoduo.com/pifa/search/searchOptGoods";
            string methodName = "";
            while (page > 0)
            {
                var client = new RestClient(requestUrl ?? "");
                var request = new RestRequest(methodName) { RequestFormat = DataFormat.Json, Method = Method.POST };
                request.AddHeader("accept", "*/*");
                request.AddHeader("accept-encoding", "gzip, deflate, br");
                request.AddHeader("accept-language", "zh-CN,zh;q=0.9");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("origin", "https://pifa.pinduoduo.com");
                request.AddHeader("referer", "https://pifa.pinduoduo.com/search");
                request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36");
                request.AddHeader("cookie", "_nano_fp=XpExX5EbnpEYXqdxlT_0mkHtWLNvFnhqbw6dw61m; webp=true; api_uid=rBUUqmC4XtxjgWo+VhQ9Ag==; VISITOR_PASS_ID=RhhxhWnIhaI1kG4ZUhEgd0fByCgfYMZqTt8fG893H6ZCuO6hZFD3NMzj7xrk5yLzT_grlBCBallKNp7rzv416usUqJqahj8SKGBKjffRjwQ_f43bfafa89");

                var body = new
                {
                    level = 2,
                    optId = i_optId,
                    page = 1,
                    size = 20,
                    sort = 0,
                    propertyItems = new string[] { },
                    url = string.Empty
                };

                request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body), ParameterType.RequestBody);

                //object obj = new object();
                //request.AddJsonBody(obj);

                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                }



                //using (XWebClient webClient = new XWebClient())
                //{
                //    webClient.Headers.Add("accept", "*/*");
                //    webClient.Headers.Add("accept-encoding", "gzip, deflate, br");
                //    webClient.Headers.Add("accept-language", "zh-CN,zh;q=0.9");
                //    webClient.Headers.Add("content-type", "application/json");
                //    webClient.Headers.Add("origin", "https://pifa.pinduoduo.com");
                //    webClient.Headers.Add("referer", "https://pifa.pinduoduo.com/search");
                //    webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36");


                //    headerData.Add("level", "2");
                //    headerData.Add("optId", str_optId);
                //    headerData.Add("page", "1");
                //    headerData.Add("size", "20");
                //    headerData.Add("sort", "0");

                //    AddHeader(webClient, headerData);

                //    //byte[] post = Encoding.UTF8.GetBytes(postData);
                //    byte[] post = Encoding.UTF8.GetBytes(string.Empty);
                //    byte[] responseData = webClient.UploadData(requestUrl, "POST", post);//得到返回字符流  
                //    string responseHtml = Encoding.GetEncoding("UTF-8").GetString(responseData);//解码
                //}



                //            : 2
                //optId: 9494
                //page: 2
                //propertyItems:[]
                //            rn: "02e7f62b-7bbf-4321-a055-9d8a6d932805"
                //size: 20
                //sort: 0
                //url: ""

                page--;
                Thread.Sleep(100);
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

        //load json
        private PddSettingEntity LoadJson(string url)
        {
            using (StreamReader file = new StreamReader(url, Encoding.Default))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    PddSettingEntity pddSetting = new PddSettingEntity();
                    JObject obj = (JObject)JToken.ReadFrom(reader);
                    pddSetting.siteUrl = obj["siteUrl"].ToSafeString();
                    pddSetting.detailUrl = obj["detailUrl"].ToSafeString();

                    int out_level = 0, out_optId = 0;
                    JArray optList = (JArray)obj["optList"];
                    for (int i = 0; i < optList.Count; i++)
                    {
                        CategoryOne category_one = new CategoryOne();
                        int.TryParse(optList[i]["level"].ToSafeString(), out out_level);
                        int.TryParse(optList[i]["optId"].ToSafeString(), out out_optId);
                        category_one.level = out_level;
                        category_one.optId = out_optId;
                        category_one.optName = optList[i]["optName"].ToSafeString();
                        JArray array_two = (JArray)optList[i]["children"];
                        for (int j = 0; j < array_two.Count; j++)
                        {
                            CategoryTwo category_two = new CategoryTwo();
                            int.TryParse(array_two[j]["level"].ToSafeString(), out out_level);
                            int.TryParse(array_two[j]["optId"].ToSafeString(), out out_optId);
                            category_two.level = out_level;
                            category_two.optId = out_optId;
                            category_two.optName = array_two[j]["optName"].ToSafeString();
                            JArray array_category = (JArray)array_two[j]["children"];
                            for (int k = 0; k < array_category.Count; k++)
                            {
                                CategoryThree category_three = new CategoryThree();
                                int.TryParse(array_category[k]["level"].ToSafeString(), out out_level);
                                int.TryParse(array_category[k]["optId"].ToSafeString(), out out_optId);
                                category_three.level = out_level;
                                category_three.optId = out_optId;
                                category_three.optName = array_category[k]["optName"].ToSafeString();
                                category_two.children.Add(category_three);
                            }
                            category_one.children.Add(category_two);
                        }
                        pddSetting.optList.Add(category_one);
                    }


                    return pddSetting;
                }
            }
        }

        private void tbContext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void cmbOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbContext.Text = "level:1" + "\r\n value:" + cmbOne.SelectedValue.ToString() + "  \r\n text:" + cmbOne.Text.ToString();

            ComboBoxItem item = (ComboBoxItem)cmbOne.SelectedItem;
            ////绑定类目二
            cmbTwo.DataSource = data.optList.Where(l => l.optId == item.Value && l.level == 1).FirstOrDefault()
                .children.Select(m => new ComboBoxItem { Value = m.optId, Text = m.optName }).ToList();
            cmbTwo.ValueMember = "Value";
            cmbTwo.DisplayMember = "Text";

        }

        private void cmbTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbContext.Text += "\r\n level:2" + "\r\n value:" + cmbTwo.SelectedValue.ToString() + "  \r\n text:" + cmbTwo.Text.ToString();

            ComboBoxItem item_one = (ComboBoxItem)cmbOne.SelectedItem;
            ComboBoxItem item_two = (ComboBoxItem)cmbTwo.SelectedItem;
            //绑定类目
            cmbThree.DataSource = data.optList.Where(l => l.optId == item_one.Value && l.level == 1).FirstOrDefault()
                .children.Where(m => m.optId == item_two.Value && m.level == 2).FirstOrDefault().children
                .Select(n => new ComboBoxItem { Value = n.optId, Text = n.optName }).ToList();
            cmbThree.ValueMember = "Value";
            cmbThree.DisplayMember = "Text";
        }

        private bool Vaild()
        {
            bool flag = false;

            #region 验证类目

            if (this.cmbOne.SelectedIndex == -1)
            {
                MessageBox.Show("请选择一级类目！");
                return true;
            }
            optId = (int)this.cmbOne.SelectedValue;
            if (this.cmbTwo.SelectedIndex != -1)
            {
                optId = (int)this.cmbTwo.SelectedValue;
            }
            if (this.cmbThree.SelectedIndex != -1)
            {
                optId = (int)this.cmbThree.SelectedValue;
            }
            if (optId == 0)
            {
                MessageBox.Show("请选择类目！");
                return true;
            }

            #endregion

            #region 验证价格

            string regonStr = @"^[+]{0,1}(\d+)$";
            Regex reg = new Regex(regonStr);
            if (string.IsNullOrWhiteSpace(tbMinPrice.Text) && string.IsNullOrWhiteSpace(tbMaxPrice.Text))
            {
                MessageBox.Show("请填写价格!");
                return true;
            }
            else if (!string.IsNullOrWhiteSpace(tbMinPrice.Text) && string.IsNullOrWhiteSpace(tbMaxPrice.Text))
            {
                if (!reg.IsMatch(tbMinPrice.Text.Trim()))
                {
                    MessageBox.Show("价格为正整数!");
                    return true;
                }
            }
            else if (string.IsNullOrWhiteSpace(tbMinPrice.Text) && !string.IsNullOrWhiteSpace(tbMaxPrice.Text))
            {
                if (!reg.IsMatch(tbMaxPrice.Text.Trim()))
                {
                    MessageBox.Show("价格为正整数!");
                    return true;
                }
            }
            else if (!string.IsNullOrWhiteSpace(tbMinPrice.Text) && !string.IsNullOrWhiteSpace(tbMaxPrice.Text))
            {
                if (!reg.IsMatch(tbMinPrice.Text.Trim()) || !reg.IsMatch(tbMaxPrice.Text.Trim()))
                {
                    MessageBox.Show("价格为正整数!");
                    return true;
                }
                if (decimal.Parse(tbMinPrice.Text) > decimal.Parse(tbMaxPrice.Text))
                {
                    MessageBox.Show("最小价格不能大于最大价格!");
                    return true;
                }
            }

            //赋值
            minPrice = decimal.Parse(tbMinPrice.Text);
            maxPrice = decimal.Parse(tbMaxPrice.Text);

            #endregion

            return flag;
        }
    }
}
