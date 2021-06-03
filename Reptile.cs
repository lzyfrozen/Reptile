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

            //load setting
            pddSettingEntity entity;
            try
            {
                entity = LoadJson(Path.Combine(url, "setting.json"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Json文件配置错误！请联系管理员！");
                return;
            }



        }

        //load json
        private pddSettingEntity LoadJson(string url)
        {
            using (StreamReader file = new StreamReader(url, Encoding.Default))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    pddSettingEntity pddSetting = new pddSettingEntity();
                    JObject obj = (JObject)JToken.ReadFrom(reader);
                    pddSetting.siteUrl = obj["siteUrl"].ToSafeString();
                    pddSetting.detailUrl = obj["detailUrl"].ToSafeString();

                    int out_level = 0, out_optId = 0;
                    JArray array_one = (JArray)obj["categoryList"];
                    for (int i = 0; i < array_one.Count; i++)
                    {
                        CategoryOne category_one = new CategoryOne();
                        int.TryParse(array_one[i]["level"].ToSafeString(), out out_level);
                        int.TryParse(array_one[i]["optId"].ToSafeString(), out out_optId);
                        category_one.level = out_level;
                        category_one.optId = out_optId;
                        category_one.name = array_one[i]["name"].ToSafeString();
                        JArray array_two = (JArray)array_one[i]["goodsCategory"];
                        for (int j = 0; j < array_two.Count; j++)
                        {
                            CategoryTwo category_two = new CategoryTwo();
                            int.TryParse(array_two[j]["level"].ToSafeString(), out out_level);
                            int.TryParse(array_two[j]["optId"].ToSafeString(), out out_optId);
                            category_two.level = out_level;
                            category_two.optId = out_optId;
                            category_two.name = array_two[j]["name"].ToSafeString();
                            JArray array_category = (JArray)array_two[j]["goodsCategory"];
                            for (int k = 0; k < array_category.Count; k++)
                            {
                                Category category = new Category();
                                int.TryParse(array_category[k]["level"].ToSafeString(), out out_level);
                                int.TryParse(array_category[k]["optId"].ToSafeString(), out out_optId);
                                category.level = out_level;
                                category.optId = out_optId;
                                category.name = array_two[k]["name"].ToSafeString();
                                category_two.goodsCategory.Add(category);
                            }
                            category_one.goodsCategory.Add(category_two);
                        }
                        pddSetting.categoryList.Add(category_one);
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
