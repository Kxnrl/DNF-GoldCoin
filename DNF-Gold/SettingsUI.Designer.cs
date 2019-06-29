namespace DNF_Gold
{
    partial class SettingsUI
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
            this.ES_MinRatio = new System.Windows.Forms.NumericUpDown();
            this.Group_HightRatio = new System.Windows.Forms.GroupBox();
            this.ES_MaxPrice = new System.Windows.Forms.NumericUpDown();
            this.Label_Price = new System.Windows.Forms.Label();
            this.Label_Ratio = new System.Windows.Forms.Label();
            this.ES_Notifaction = new System.Windows.Forms.CheckBox();
            this.ES_Confirm = new System.Windows.Forms.Button();
            this.ES_CloseOnTray = new System.Windows.Forms.CheckBox();
            this.ES_AutoRefresh = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ES_MinRatio)).BeginInit();
            this.Group_HightRatio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ES_MaxPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // ES_MinRatio
            // 
            this.ES_MinRatio.DecimalPlaces = 2;
            this.ES_MinRatio.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ES_MinRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ES_MinRatio.InterceptArrowKeys = false;
            this.ES_MinRatio.Location = new System.Drawing.Point(82, 27);
            this.ES_MinRatio.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            65536});
            this.ES_MinRatio.Name = "ES_MinRatio";
            this.ES_MinRatio.Size = new System.Drawing.Size(73, 24);
            this.ES_MinRatio.TabIndex = 0;
            this.ES_MinRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ES_MinRatio.Value = new decimal(new int[] {
            520,
            0,
            0,
            65536});
            // 
            // Group_HightRatio
            // 
            this.Group_HightRatio.Controls.Add(this.ES_MaxPrice);
            this.Group_HightRatio.Controls.Add(this.Label_Price);
            this.Group_HightRatio.Controls.Add(this.Label_Ratio);
            this.Group_HightRatio.Controls.Add(this.ES_MinRatio);
            this.Group_HightRatio.Location = new System.Drawing.Point(12, 12);
            this.Group_HightRatio.Name = "Group_HightRatio";
            this.Group_HightRatio.Size = new System.Drawing.Size(170, 100);
            this.Group_HightRatio.TabIndex = 1;
            this.Group_HightRatio.TabStop = false;
            this.Group_HightRatio.Text = "高比例提醒";
            // 
            // ES_MaxPrice
            // 
            this.ES_MaxPrice.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ES_MaxPrice.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ES_MaxPrice.Location = new System.Drawing.Point(82, 61);
            this.ES_MaxPrice.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ES_MaxPrice.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ES_MaxPrice.Name = "ES_MaxPrice";
            this.ES_MaxPrice.Size = new System.Drawing.Size(73, 24);
            this.ES_MaxPrice.TabIndex = 3;
            this.ES_MaxPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ES_MaxPrice.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // Label_Price
            // 
            this.Label_Price.Location = new System.Drawing.Point(6, 61);
            this.Label_Price.Name = "Label_Price";
            this.Label_Price.Size = new System.Drawing.Size(70, 25);
            this.Label_Price.TabIndex = 2;
            this.Label_Price.Text = "最高价格";
            this.Label_Price.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Ratio
            // 
            this.Label_Ratio.Location = new System.Drawing.Point(6, 27);
            this.Label_Ratio.Name = "Label_Ratio";
            this.Label_Ratio.Size = new System.Drawing.Size(70, 25);
            this.Label_Ratio.TabIndex = 1;
            this.Label_Ratio.Text = "最低比例";
            this.Label_Ratio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ES_Notifaction
            // 
            this.ES_Notifaction.Checked = true;
            this.ES_Notifaction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ES_Notifaction.Font = new System.Drawing.Font("微软雅黑 Light", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ES_Notifaction.Location = new System.Drawing.Point(12, 179);
            this.ES_Notifaction.Name = "ES_Notifaction";
            this.ES_Notifaction.Size = new System.Drawing.Size(170, 24);
            this.ES_Notifaction.TabIndex = 2;
            this.ES_Notifaction.Text = "启用系统通知";
            this.ES_Notifaction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ES_Notifaction.UseVisualStyleBackColor = true;
            // 
            // ES_Confirm
            // 
            this.ES_Confirm.Font = new System.Drawing.Font("微软雅黑 Light", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ES_Confirm.Location = new System.Drawing.Point(12, 209);
            this.ES_Confirm.Name = "ES_Confirm";
            this.ES_Confirm.Size = new System.Drawing.Size(170, 30);
            this.ES_Confirm.TabIndex = 3;
            this.ES_Confirm.Text = "确认";
            this.ES_Confirm.UseVisualStyleBackColor = true;
            this.ES_Confirm.Click += new System.EventHandler(this.ES_Confirm_Click);
            // 
            // ES_CloseOnTray
            // 
            this.ES_CloseOnTray.Checked = true;
            this.ES_CloseOnTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ES_CloseOnTray.Font = new System.Drawing.Font("微软雅黑 Light", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ES_CloseOnTray.Location = new System.Drawing.Point(12, 149);
            this.ES_CloseOnTray.Name = "ES_CloseOnTray";
            this.ES_CloseOnTray.Size = new System.Drawing.Size(170, 24);
            this.ES_CloseOnTray.TabIndex = 4;
            this.ES_CloseOnTray.Text = "保持后台运行";
            this.ES_CloseOnTray.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ES_CloseOnTray.UseVisualStyleBackColor = true;
            // 
            // ES_AutoRefresh
            // 
            this.ES_AutoRefresh.Checked = true;
            this.ES_AutoRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ES_AutoRefresh.Font = new System.Drawing.Font("微软雅黑 Light", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ES_AutoRefresh.Location = new System.Drawing.Point(12, 119);
            this.ES_AutoRefresh.Name = "ES_AutoRefresh";
            this.ES_AutoRefresh.Size = new System.Drawing.Size(170, 24);
            this.ES_AutoRefresh.TabIndex = 5;
            this.ES_AutoRefresh.Text = "自动刷新列表";
            this.ES_AutoRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ES_AutoRefresh.UseVisualStyleBackColor = true;
            // 
            // SettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 251);
            this.Controls.Add(this.ES_AutoRefresh);
            this.Controls.Add(this.ES_CloseOnTray);
            this.Controls.Add(this.ES_Confirm);
            this.Controls.Add(this.ES_Notifaction);
            this.Controls.Add(this.Group_HightRatio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "通知中心";
            this.Shown += new System.EventHandler(this.OnShown);
            ((System.ComponentModel.ISupportInitialize)(this.ES_MinRatio)).EndInit();
            this.Group_HightRatio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ES_MaxPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ES_MinRatio;
        private System.Windows.Forms.GroupBox Group_HightRatio;
        private System.Windows.Forms.NumericUpDown ES_MaxPrice;
        private System.Windows.Forms.Label Label_Price;
        private System.Windows.Forms.Label Label_Ratio;
        private System.Windows.Forms.CheckBox ES_Notifaction;
        private System.Windows.Forms.Button ES_Confirm;
        private System.Windows.Forms.CheckBox ES_CloseOnTray;
        private System.Windows.Forms.CheckBox ES_AutoRefresh;
    }
}