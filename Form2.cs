using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reptile
{
    public partial class Form2 : Form
    {
        private static Dictionary<string, string> headerData = new Dictionary<string, string>();
        private static readonly string WMS_Pushing_URL = "http://192.168.100.83:85/api/V1/Dpi/UploadMessageReceipt";
        private string strconn = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        private string strcookie = ConfigurationManager.AppSettings["Cookie"];
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnUplod_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.txt)|*.txt";
            //dialog.InitialDirectory = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textFile.Text = dialog.FileName;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            textMsg.Text = string.Empty;
            List<string> list = new List<string>();
            //FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"File/20170623.txt", FileMode.Open, FileAccess.Read);
            FileStream fs = new FileStream(textFile.Text, FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string s;
            int i = 0;
            while ((s = sr.ReadLine()) != null && s != string.Empty)
            {
                list.Add(s);
                //i++;
                //if (i == 1) break;
            }
            string url = @"http://ceb2.chinaport.gov.cn/agentdeclaredinvt/queryPage?timeMilli={0}&invtNo=&orderNo={1}&sysDateFrom=2017-06-18+00%3A00%3A00&sysDateTo=2017-06-22+23%3A59%3A59&copNo=&appStatusText=&appStatus=&customsCodeText=&customsCode=&pageNo=1&pageSize=50";

            StringBuilder sql = new StringBuilder();
            foreach (var orderno in list)
            {
                string responseHtml;
                TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1");
                try
                {
                    responseHtml = PostData(string.Format(url, Convert.ToInt64(ts.TotalMilliseconds), orderno), "");

                    //var doc = new HtmlAgilityPack.HtmlDocument();
                    //doc.LoadHtml(responseHtml);
                    JObject jo = JsonConvert.DeserializeObject<JObject>(responseHtml);
                    var sResult = jo["result"].ToString();
                    jo = JsonConvert.DeserializeObject<JObject>(sResult);
                    var orderNo = jo["itemList"][0]["orderNo"].ToString();
                    var appStatus = jo["itemList"][0]["appStatus"].ToString();
                    var copNo = jo["itemList"][0]["copNo"].ToString();
                    var preNo = jo["itemList"][0]["preNo"].ToString();
                    var invtNo = jo["itemList"][0]["invtNo"].ToString();

                    //sql.Append(@"SELECT '" + orderNo + "' as orderNo,'" + appStatus + "' as appStatus,'" + copNo + "' as copNo,'" + preNo + "' as preNo,'" + invtNo + "' as invtNo UNION ALL" + Environment.NewLine);
                    string sqlStr = @"insert into db_Log(orderNo,appStatus,copNo,preNo,invtNo) values(@orderNo,@appStatus,@copNo,@preNo,@invtNo)";
                    SqlParameter[] parameters = {
                        new SqlParameter("@orderNo", SqlDbType.NVarChar),  //自定义参数  与参数类型    
                        new SqlParameter("@appStatus", SqlDbType.NVarChar),
                        new SqlParameter("@copNo", SqlDbType.NVarChar),
                        new SqlParameter("@preNo", SqlDbType.NVarChar),
                        new SqlParameter("@invtNo", SqlDbType.NVarChar)
                    };
                    parameters[0].Value = orderNo;  //给参数赋值
                    parameters[1].Value = appStatus;
                    parameters[2].Value = copNo;
                    parameters[3].Value = preNo;
                    parameters[4].Value = invtNo;

                    if (ExecuteSql(sqlStr, parameters) > 1)
                    {
                        i++;
                        textMsg.Text += $"{orderno}:OK" + Environment.NewLine;
                    }
                    else
                    {
                        throw new ApplicationException("插入数据失败!");
                    }
                }
                catch (Exception ex)
                {
                    //throw new ApplicationException($"{orderno}:{ex.Message}");
                    textMsg.Text += $"{orderno}:{ex.Message}" + Environment.NewLine;
                }
                Thread.Sleep(200);
            }

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
                //webClient.Headers.Add("Accept", "application/ json, text / javascript, */*; q=0.01");
                webClient.Headers.Add("Content-Type", "application/Json");
                ////webClient.Headers.Add("X-Requested-With", "XMLHttpRequest");
                //webClient.Headers.Add("Referer", "http://ceb2.chinaport.gov.cn/agentdeclaredinvt/invt-query");
                webClient.Headers.Add("Accept-Language", "application/ json, text / javascript, */*; q=0.01");
                //webClient.Headers.Add("Accept", "zh-CN");
                webClient.Headers.Add("Accept-Encoding", "gzip, deflate");
                //webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko");
                //webClient.Headers.Add("Host", "ceb2.chinaport.gov.cn");
                //webClient.Headers.Add("DNT", "1");
                ////webClient.Headers.Add("Connection", "Keep-Alive");
                //webClient.Headers.Add("Cache-Control", "no-cache");
                webClient.Headers.Add("Cookie", strcookie);//"JSESSIONID=406657185A9409336122C706E8A7F8ED"

                AddHeader(webClient, headerData);

                byte[] post = Encoding.UTF8.GetBytes(postData);
                byte[] responseData = webClient.UploadData(url, "POST", post);//得到返回字符流  
                string responseHtml = Encoding.GetEncoding("UTF-8").GetString(responseData);//解码//GB2312

                return responseHtml;
            }
        }

        private DataTable ExecuteReader(string sql, SqlParameter[] parameters)
        {
            #region MyRegion
            //string path = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "log.txt";
            //FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            ////StreamWriter sw = new StreamWriter(fs);
            ////byte[] bt = new byte[(int)fs.Length];
            //byte[] bytes = Encoding.UTF8.GetBytes(txt + Environment.NewLine);
            //fs.Position = fs.Length;
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close(); 
            #endregion
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (parameters != null && parameters.Length != 0)
                        cmd.Parameters.AddRange(parameters);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
                catch (Exception ex)
                {
                    //throw;
                }
                finally
                {
                    conn.Close();
                }
                return dt;
            }
        }

        private int ExecuteSql(string sql, SqlParameter[] parameters)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (parameters != null && parameters.Length != 0)
                        cmd.Parameters.AddRange(parameters);
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            textMsg.Text = string.Empty;
            DataTable dt = ExecuteReader("SELECT * FROM dbo.db_Log", null);

            for (int i = 0; i < dt.Rows.Count; i++)//
            {
                DataRow dr = dt.Rows[i];
                string no = dr["orderNo"].ToString();
                string txt = BuildLog(dr["appStatus"].ToString(), dr["copNo"].ToString(), dr["preNo"].ToString(), dr["invtNo"].ToString());

                try
                {
                    PostToWms(no, txt);
                    textMsg.Text += $"{no}:OK" + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    textMsg.Text += $"{no}:{ex.Message}" + Environment.NewLine;
                    //throw;
                }
                Thread.Sleep(200);
            }
        }

        private string BuildLog(string appStatus, string copNo, string preNo, string invtNo)
        {

            string txt = "{'MessageType':'CEB622','Sender':'CHCUS','MessageContent':'<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><CEB622Message xmlns=\"http://www.chinaport.gov.cn/ceb\" guid=\"fc435bf6-acc7-4c40-940f-9fc6f19b3835\" version=\"1.0\">    <InventoryReturn>        <guid>28c8ce0b-7273-43ab-a525-846d1e272eb2</guid>        <customsCode>5349</customsCode>        <ebpCode>3301540039</ebpCode>        <ebcCode>3301540039</ebcCode>        <agentCode>4403181672</agentCode>        <copNo>" + copNo + "</copNo>        <preNo>" + preNo + "</preNo>        <invtNo>" + invtNo + "</invtNo>        <returnStatus>" + appStatus + "</returnStatus>        <returnTime>20170622155349698</returnTime>        <returnInfo>放行</returnInfo>    </InventoryReturn></CEB622Message>','SendTime':'2017-06-14T13:56:39.953646+08:00'}";
            //string.Format(txt, copNo, preNo, invtNo, appStatus);
            return txt;
        }

        private void PostToWms(string no, string msg)
        {
            //记录参数
            //Logger.Info(input.ToJsonString());
            try
            {
                string methodName = "";
                var input = JsonConvert.DeserializeObject<Rootobject>(msg); //JsonConvert.DeserializeObject(msg);
                //input.orderNo = no.ToString();
                //WmsReturnDto returnData = new WmsReturnDto() { success = false };

                var client = new RestClient(WMS_Pushing_URL ?? "");
                var request = new RestRequest(methodName) { RequestFormat = DataFormat.Json, Method = Method.POST };
                request.AddHeader("Authorization", "Bearer QmfCFXiuMMknatwzxL4MP41iJImLcIUkf-fWOPwj1EKSf0BKSGL2ZqKP8L5M7UcSs4sVAcbA_6PmRyFE896MFtHj-10jgZMdMlxvRR_aobavM3SRNzsQwAoUhzcZhz7gs3UK893MntodYipjS89fSIM_PUbz4L1fOcA476FfQgI3GmZyiOsMNI7ONyM4Prlvx_gAHP5RPf8aWv0YRCtiYiGTrYyoaen5SCvbZncg2wtTHH4_e-gCsI1p5-30CkihRHx2iU4Vg-AergIHT6EO3YzOhqqgS14VKC6E9T6sovjj5nR9LaeOhapx9Nh9UDHAYZwjpZj__sHhkHw3QaLYUp6TIYndMUHX18ca4g2uQXnkh5oWkFQ4MtzHzS6EAaWZXOCDI5QLtiRPlQAGSXJGK4G91AXdK8Z-9hdyx-1WianV2RpnGPnyxNwYj0vN9F5gjRU9B6PbOEdzj0nXRuEqUJIJxFlrhjjwS8bvA2JfhlhQrd1Y4uBh1hcxSCbZtWkp8ulyh7P1JXwTBC4Vpbymdg");
                request.AddBody(input);
                var response = client.Execute(request);//<Abp.Web.Models.AjaxResponse<WmsReturnDto>>
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Logger.Debug("WMS反馈发送返回内容：" + response.Content);


                    //WriteText("反馈发送返回内容：" + response.Content, no.ToString());

                    //string json = response.Content;
                    //JObject jo = JsonConvert.DeserializeObject<JObject>(json);
                    //var sResult = jo["result"].ToString();

                    //var result = JsonConvert.DeserializeObject<WmsReturnDto>(sResult);
                    //if (!response.Success)
                    //{
                    //    //Logger.Error("WMS反馈发送错误：" + response.Data.Error.ToJsonString());
                    //    WriteText("反馈发送错误：" + response.Data.Error.ToJsonString(), no.ToString());
                    //    returnData.errorMsg = response.Data.Error.Message;
                    //}
                    //else
                    //{
                    //    returnData = response.Data.Result;
                    //}
                }
                else
                {
                    //Logger.Error("WMS反馈发送错误：网络异常" + response.StatusCode);
                    //WriteText("反馈发送错误：网络异常" + response.StatusCode, no.ToString());
                    //returnData.errorMsg = "反馈发送错误：网络异";
                }

                //if (!returnData.success)
                //{
                //    //throw new UserFriendlyException($"订单推送到WMS失败{methodName}：{returnData.errorMsg}");
                //    //throw new Exception($"订单推送到WMS失败{methodName}：{returnData.errorMsg}");
                //    WriteText($"推送失败{methodName}：{returnData.errorMsg}", no.ToString());
                //}
            }
            catch (Exception ex) { }
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
    }
}
