using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reptile
{
    public partial class Form1 : Form
    {
        private int optId;
        private int page;
        private decimal minPrice, maxPrice = 0m;
        private string url = string.Empty;
        public Form1()
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
            if (Vaild() == true) return;

            //page
            page = (int)cmbPage.SelectedValue;



        }

        //load json
        private void LoadJsonSetting()
        {

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
