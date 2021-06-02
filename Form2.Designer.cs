namespace Reptile
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.textMsg = new System.Windows.Forms.TextBox();
            this.btnUplod = new System.Windows.Forms.Button();
            this.textFile = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(58, 86);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "查询生成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(196, 86);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // textMsg
            // 
            this.textMsg.Location = new System.Drawing.Point(30, 126);
            this.textMsg.Multiline = true;
            this.textMsg.Name = "textMsg";
            this.textMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textMsg.Size = new System.Drawing.Size(285, 313);
            this.textMsg.TabIndex = 2;
            // 
            // btnUplod
            // 
            this.btnUplod.Location = new System.Drawing.Point(30, 38);
            this.btnUplod.Name = "btnUplod";
            this.btnUplod.Size = new System.Drawing.Size(75, 23);
            this.btnUplod.TabIndex = 3;
            this.btnUplod.Text = "文件";
            this.btnUplod.UseVisualStyleBackColor = true;
            this.btnUplod.Click += new System.EventHandler(this.btnUplod_Click);
            // 
            // textFile
            // 
            this.textFile.Location = new System.Drawing.Point(112, 39);
            this.textFile.Name = "textFile";
            this.textFile.ReadOnly = true;
            this.textFile.Size = new System.Drawing.Size(203, 21);
            this.textFile.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 451);
            this.Controls.Add(this.textFile);
            this.Controls.Add(this.btnUplod);
            this.Controls.Add(this.textMsg);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnCreate);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox textMsg;
        private System.Windows.Forms.Button btnUplod;
        private System.Windows.Forms.TextBox textFile;
    }
}