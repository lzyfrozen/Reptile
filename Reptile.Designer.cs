
namespace Reptile
{
    partial class Reptile
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbMaxPrice = new System.Windows.Forms.TextBox();
            this.tbMinPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPage = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbThree = new System.Windows.Forms.ComboBox();
            this.cmbTwo = new System.Windows.Forms.ComboBox();
            this.cmbOne = new System.Windows.Forms.ComboBox();
            this.tbContext = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.tbMaxPrice);
            this.splitContainer1.Panel1.Controls.Add(this.tbMinPrice);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.cmbPage);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cmbThree);
            this.splitContainer1.Panel1.Controls.Add(this.cmbTwo);
            this.splitContainer1.Panel1.Controls.Add(this.cmbOne);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbContext);
            this.splitContainer1.Size = new System.Drawing.Size(882, 553);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(766, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 25);
            this.button2.TabIndex = 27;
            this.button2.Text = "生成Excel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(766, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 25);
            this.button1.TabIndex = 26;
            this.button1.Text = "生成文本";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tbMaxPrice
            // 
            this.tbMaxPrice.Location = new System.Drawing.Point(377, 61);
            this.tbMaxPrice.Name = "tbMaxPrice";
            this.tbMaxPrice.Size = new System.Drawing.Size(100, 25);
            this.tbMaxPrice.TabIndex = 25;
            this.tbMaxPrice.Text = "1";
            // 
            // tbMinPrice
            // 
            this.tbMinPrice.Location = new System.Drawing.Point(250, 61);
            this.tbMinPrice.Name = "tbMinPrice";
            this.tbMinPrice.Size = new System.Drawing.Size(100, 25);
            this.tbMinPrice.TabIndex = 24;
            this.tbMinPrice.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(356, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "价格：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "页数(*20)：";
            // 
            // cmbPage
            // 
            this.cmbPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPage.FormattingEnabled = true;
            this.cmbPage.Items.AddRange(new object[] {
            "100",
            "500",
            "1000",
            "2000",
            "5000",
            "10000",
            "∞"});
            this.cmbPage.Location = new System.Drawing.Point(116, 62);
            this.cmbPage.Name = "cmbPage";
            this.cmbPage.Size = new System.Drawing.Size(76, 23);
            this.cmbPage.TabIndex = 20;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(619, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(122, 60);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(374, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "三级：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "二级：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "一级：";
            // 
            // cmbThree
            // 
            this.cmbThree.FormattingEnabled = true;
            this.cmbThree.Location = new System.Drawing.Point(429, 21);
            this.cmbThree.Name = "cmbThree";
            this.cmbThree.Size = new System.Drawing.Size(121, 23);
            this.cmbThree.TabIndex = 15;
            // 
            // cmbTwo
            // 
            this.cmbTwo.FormattingEnabled = true;
            this.cmbTwo.Location = new System.Drawing.Point(250, 21);
            this.cmbTwo.Name = "cmbTwo";
            this.cmbTwo.Size = new System.Drawing.Size(121, 23);
            this.cmbTwo.TabIndex = 14;
            // 
            // cmbOne
            // 
            this.cmbOne.FormattingEnabled = true;
            this.cmbOne.Location = new System.Drawing.Point(71, 21);
            this.cmbOne.Name = "cmbOne";
            this.cmbOne.Size = new System.Drawing.Size(121, 23);
            this.cmbOne.TabIndex = 13;
            // 
            // tbContext
            // 
            this.tbContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbContext.Location = new System.Drawing.Point(0, 0);
            this.tbContext.Multiline = true;
            this.tbContext.Name = "tbContext";
            this.tbContext.Size = new System.Drawing.Size(882, 449);
            this.tbContext.TabIndex = 25;
            // 
            // Reptile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Reptile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reptile";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbMaxPrice;
        private System.Windows.Forms.TextBox tbMinPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPage;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbThree;
        private System.Windows.Forms.ComboBox cmbTwo;
        private System.Windows.Forms.ComboBox cmbOne;
        private System.Windows.Forms.TextBox tbContext;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

