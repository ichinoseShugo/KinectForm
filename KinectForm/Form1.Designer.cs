namespace KinectForm
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.rgbImage = new System.Windows.Forms.PictureBox();
            this.kinectRec = new System.Windows.Forms.CheckBox();
            this.jointNum = new System.Windows.Forms.Label();
            this.limit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rgbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // rgbImage
            // 
            this.rgbImage.Location = new System.Drawing.Point(139, 104);
            this.rgbImage.Name = "rgbImage";
            this.rgbImage.Size = new System.Drawing.Size(640, 480);
            this.rgbImage.TabIndex = 0;
            this.rgbImage.TabStop = false;
            // 
            // kinectRec
            // 
            this.kinectRec.AutoSize = true;
            this.kinectRec.Location = new System.Drawing.Point(139, 81);
            this.kinectRec.Name = "kinectRec";
            this.kinectRec.Size = new System.Drawing.Size(76, 16);
            this.kinectRec.TabIndex = 1;
            this.kinectRec.Text = "KinectRec";
            this.kinectRec.UseVisualStyleBackColor = true;
            // 
            // jointNum
            // 
            this.jointNum.AutoSize = true;
            this.jointNum.Location = new System.Drawing.Point(221, 82);
            this.jointNum.Name = "jointNum";
            this.jointNum.Size = new System.Drawing.Size(29, 12);
            this.jointNum.TabIndex = 2;
            this.jointNum.Text = "joint:";
            // 
            // limit
            // 
            this.limit.AutoSize = true;
            this.limit.Location = new System.Drawing.Point(269, 82);
            this.limit.Name = "limit";
            this.limit.Size = new System.Drawing.Size(29, 12);
            this.limit.TabIndex = 3;
            this.limit.Text = "limit:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 586);
            this.Controls.Add(this.limit);
            this.Controls.Add(this.jointNum);
            this.Controls.Add(this.kinectRec);
            this.Controls.Add(this.rgbImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.rgbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox rgbImage;
        private System.Windows.Forms.CheckBox kinectRec;
        private System.Windows.Forms.Label jointNum;
        private System.Windows.Forms.Label limit;
    }
}

