using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public Reptile()
        {
            InitializeComponent();
            this.cmbPage.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //...test

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (Vaild() == true) return;

            //page
            page = int.Parse(cmbPage.Text);

            string url = AppDomain.CurrentDomain.BaseDirectory;
            LoadJson(Path.Combine(url, "setting.json"));

        }

        //load json
        private JObject LoadJson(string url)
        {
            using (StreamReader file = new StreamReader(url, Encoding.Default))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    pddSettingEntity pddSetting = new pddSettingEntity();
                    JObject obj = (JObject)JToken.ReadFrom(reader);
                    pddSetting.siteUrl = obj["siteUrl"].ToSafeString();
                    pddSetting.detailUrl = obj["detailUrl"].ToSafeString();


                    pddSetting.categoryList = new List<CategoryOne>();
                    int tryInt = 0;
                    JArray jarray = (JArray)obj["categoryList"];
                    for (int i = 0; i < jarray.Count; i++)
                    {
                        CategoryOne category = new CategoryOne();
                        int.TryParse(jarray[i]["level"].ToSafeString(), out tryInt);
                        category.level = tryInt;

                    }


                    return obj;
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
