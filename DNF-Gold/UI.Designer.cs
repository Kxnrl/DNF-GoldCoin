﻿namespace DNF_Gold
{
    partial class UI
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ItemsList = new System.Windows.Forms.DataGridView();
            this.pGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pCoins = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRecvs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pArena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pTrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSites = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pBLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ES_7881 = new System.Windows.Forms.CheckBox();
            this.ES_5173 = new System.Windows.Forms.CheckBox();
            this.ES_UU898 = new System.Windows.Forms.CheckBox();
            this.ES_DD373 = new System.Windows.Forms.CheckBox();
            this.ES_Refresh = new System.Windows.Forms.Button();
            this.Group_Site = new System.Windows.Forms.GroupBox();
            this.ES_EE979 = new System.Windows.Forms.CheckBox();
            this.Label_WARN = new System.Windows.Forms.Label();
            this.Label_Kxnrl = new System.Windows.Forms.Label();
            this.ES_Setting = new System.Windows.Forms.Button();
            this.Label_CD = new System.Windows.Forms.Label();
            this.BG_Notifaction = new System.Windows.Forms.NotifyIcon(this.components);
            this.ES_Arena = new System.Windows.Forms.ComboBox();
            this.Label_SourceCode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsList)).BeginInit();
            this.Group_Site.SuspendLayout();
            this.SuspendLayout();
            // 
            // ItemsList
            // 
            this.ItemsList.AllowUserToAddRows = false;
            this.ItemsList.AllowUserToDeleteRows = false;
            this.ItemsList.AllowUserToResizeColumns = false;
            this.ItemsList.AllowUserToResizeRows = false;
            this.ItemsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemsList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ItemsList.ColumnHeadersHeight = 25;
            this.ItemsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ItemsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pGUID,
            this.pCoins,
            this.pRecvs,
            this.pPrice,
            this.pRatio,
            this.pArena,
            this.pTrade,
            this.pSites,
            this.Action,
            this.pBLink});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsList.DefaultCellStyle = dataGridViewCellStyle7;
            this.ItemsList.Location = new System.Drawing.Point(12, 65);
            this.ItemsList.Name = "ItemsList";
            this.ItemsList.RowHeadersVisible = false;
            this.ItemsList.RowTemplate.Height = 23;
            this.ItemsList.Size = new System.Drawing.Size(495, 234);
            this.ItemsList.TabIndex = 0;
            this.ItemsList.TabStop = false;
            this.ItemsList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemList_Clicked);
            // 
            // pGUID
            // 
            this.pGUID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Format = "0.00";
            dataGridViewCellStyle2.NullValue = "异常";
            this.pGUID.DefaultCellStyle = dataGridViewCellStyle2;
            this.pGUID.HeaderText = "ID";
            this.pGUID.Name = "pGUID";
            this.pGUID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pGUID.Visible = false;
            this.pGUID.Width = 5;
            // 
            // pCoins
            // 
            this.pCoins.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.NullValue = "异常";
            this.pCoins.DefaultCellStyle = dataGridViewCellStyle3;
            this.pCoins.HeaderText = "金币 (万)";
            this.pCoins.MaxInputLength = 32;
            this.pCoins.Name = "pCoins";
            this.pCoins.ReadOnly = true;
            this.pCoins.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pCoins.ToolTipText = "金币总数";
            this.pCoins.Width = 65;
            // 
            // pRecvs
            // 
            dataGridViewCellStyle4.Format = "0.00";
            dataGridViewCellStyle4.NullValue = "异常";
            this.pRecvs.DefaultCellStyle = dataGridViewCellStyle4;
            this.pRecvs.HeaderText = "到手 (万)";
            this.pRecvs.MaxInputLength = 32;
            this.pRecvs.Name = "pRecvs";
            this.pRecvs.ReadOnly = true;
            this.pRecvs.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pRecvs.ToolTipText = "实际到手数值";
            this.pRecvs.Width = 65;
            // 
            // pPrice
            // 
            this.pPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = "异常";
            this.pPrice.DefaultCellStyle = dataGridViewCellStyle5;
            this.pPrice.HeaderText = "价格 (元)";
            this.pPrice.MaxInputLength = 32;
            this.pPrice.Name = "pPrice";
            this.pPrice.ReadOnly = true;
            this.pPrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pPrice.ToolTipText = "你需要卖几个肾?";
            this.pPrice.Width = 65;
            // 
            // pRatio
            // 
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.pRatio.DefaultCellStyle = dataGridViewCellStyle6;
            this.pRatio.HeaderText = "比例";
            this.pRatio.MaxInputLength = 5;
            this.pRatio.Name = "pRatio";
            this.pRatio.ReadOnly = true;
            this.pRatio.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pRatio.Width = 50;
            // 
            // pArena
            // 
            this.pArena.HeaderText = "跨区";
            this.pArena.MaxInputLength = 32;
            this.pArena.Name = "pArena";
            this.pArena.ReadOnly = true;
            this.pArena.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pArena.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pArena.Width = 50;
            // 
            // pTrade
            // 
            this.pTrade.HeaderText = "方式";
            this.pTrade.MaxInputLength = 32;
            this.pTrade.Name = "pTrade";
            this.pTrade.ReadOnly = true;
            this.pTrade.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pTrade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pTrade.Width = 50;
            // 
            // pSites
            // 
            this.pSites.HeaderText = "网站";
            this.pSites.MaxInputLength = 32;
            this.pSites.Name = "pSites";
            this.pSites.ReadOnly = true;
            this.pSites.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pSites.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pSites.Width = 50;
            // 
            // Action
            // 
            this.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Action.HeaderText = "操作";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Action.Text = "购买";
            this.Action.Width = 80;
            // 
            // pBLink
            // 
            this.pBLink.HeaderText = "pBLink";
            this.pBLink.Name = "pBLink";
            this.pBLink.ReadOnly = true;
            this.pBLink.Visible = false;
            this.pBLink.Width = 5;
            // 
            // ES_7881
            // 
            this.ES_7881.Checked = true;
            this.ES_7881.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ES_7881.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ES_7881.Location = new System.Drawing.Point(5, 20);
            this.ES_7881.Name = "ES_7881";
            this.ES_7881.Size = new System.Drawing.Size(50, 25);
            this.ES_7881.TabIndex = 1;
            this.ES_7881.Text = "7881";
            this.ES_7881.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ES_7881.UseVisualStyleBackColor = true;
            // 
            // ES_5173
            // 
            this.ES_5173.Checked = true;
            this.ES_5173.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ES_5173.Font = new System.Drawing.Font("Bahnschrift", 8.25F);
            this.ES_5173.Location = new System.Drawing.Point(55, 20);
            this.ES_5173.Name = "ES_5173";
            this.ES_5173.Size = new System.Drawing.Size(50, 25);
            this.ES_5173.TabIndex = 2;
            this.ES_5173.Text = "5173";
            this.ES_5173.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ES_5173.UseVisualStyleBackColor = true;
            // 
            // ES_UU898
            // 
            this.ES_UU898.Checked = true;
            this.ES_UU898.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ES_UU898.Font = new System.Drawing.Font("Bahnschrift", 8.25F);
            this.ES_UU898.Location = new System.Drawing.Point(165, 20);
            this.ES_UU898.Name = "ES_UU898";
            this.ES_UU898.Size = new System.Drawing.Size(60, 25);
            this.ES_UU898.TabIndex = 3;
            this.ES_UU898.Text = "UU898";
            this.ES_UU898.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ES_UU898.UseVisualStyleBackColor = true;
            // 
            // ES_DD373
            // 
            this.ES_DD373.Checked = true;
            this.ES_DD373.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ES_DD373.Font = new System.Drawing.Font("Bahnschrift", 8.25F);
            this.ES_DD373.Location = new System.Drawing.Point(105, 20);
            this.ES_DD373.Name = "ES_DD373";
            this.ES_DD373.Size = new System.Drawing.Size(60, 25);
            this.ES_DD373.TabIndex = 4;
            this.ES_DD373.Text = "DD373";
            this.ES_DD373.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ES_DD373.UseVisualStyleBackColor = true;
            // 
            // ES_Refresh
            // 
            this.ES_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ES_Refresh.Location = new System.Drawing.Point(432, 36);
            this.ES_Refresh.Name = "ES_Refresh";
            this.ES_Refresh.Size = new System.Drawing.Size(75, 23);
            this.ES_Refresh.TabIndex = 5;
            this.ES_Refresh.Text = "刷新";
            this.ES_Refresh.UseVisualStyleBackColor = true;
            this.ES_Refresh.Click += new System.EventHandler(this.ES_Refresh_Click);
            // 
            // Group_Site
            // 
            this.Group_Site.Controls.Add(this.ES_EE979);
            this.Group_Site.Controls.Add(this.ES_UU898);
            this.Group_Site.Controls.Add(this.ES_DD373);
            this.Group_Site.Controls.Add(this.ES_7881);
            this.Group_Site.Controls.Add(this.ES_5173);
            this.Group_Site.Location = new System.Drawing.Point(12, 8);
            this.Group_Site.Name = "Group_Site";
            this.Group_Site.Size = new System.Drawing.Size(290, 49);
            this.Group_Site.TabIndex = 6;
            this.Group_Site.TabStop = false;
            this.Group_Site.Text = "勾选以加入比价列表";
            // 
            // ES_EE979
            // 
            this.ES_EE979.Checked = true;
            this.ES_EE979.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ES_EE979.Font = new System.Drawing.Font("Bahnschrift", 8.25F);
            this.ES_EE979.Location = new System.Drawing.Point(225, 20);
            this.ES_EE979.Name = "ES_EE979";
            this.ES_EE979.Size = new System.Drawing.Size(60, 25);
            this.ES_EE979.TabIndex = 5;
            this.ES_EE979.Text = "EE979";
            this.ES_EE979.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ES_EE979.UseVisualStyleBackColor = true;
            // 
            // Label_WARN
            // 
            this.Label_WARN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_WARN.Font = new System.Drawing.Font("微软雅黑 Light", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_WARN.Location = new System.Drawing.Point(-3, 307);
            this.Label_WARN.Name = "Label_WARN";
            this.Label_WARN.Size = new System.Drawing.Size(204, 13);
            this.Label_WARN.TabIndex = 7;
            this.Label_WARN.Text = "本软件仅提供比价技术,不承担任何交易风险担保.";
            this.Label_WARN.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Label_WARN.Click += new System.EventHandler(this.Label_WARN_Click);
            // 
            // Label_Kxnrl
            // 
            this.Label_Kxnrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Kxnrl.Font = new System.Drawing.Font("微软雅黑 Light", 6F);
            this.Label_Kxnrl.ForeColor = System.Drawing.Color.MediumOrchid;
            this.Label_Kxnrl.Location = new System.Drawing.Point(412, 305);
            this.Label_Kxnrl.Name = "Label_Kxnrl";
            this.Label_Kxnrl.Size = new System.Drawing.Size(110, 15);
            this.Label_Kxnrl.TabIndex = 8;
            this.Label_Kxnrl.Text = "Made with ❤ by Kyle";
            this.Label_Kxnrl.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Label_Kxnrl.Click += new System.EventHandler(this.Label_Kxnrl_Click);
            // 
            // ES_Setting
            // 
            this.ES_Setting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ES_Setting.Location = new System.Drawing.Point(432, 12);
            this.ES_Setting.Name = "ES_Setting";
            this.ES_Setting.Size = new System.Drawing.Size(75, 23);
            this.ES_Setting.TabIndex = 9;
            this.ES_Setting.Text = "设置";
            this.ES_Setting.UseVisualStyleBackColor = true;
            this.ES_Setting.Click += new System.EventHandler(this.ES_Setting_Click);
            // 
            // Label_CD
            // 
            this.Label_CD.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_CD.ForeColor = System.Drawing.Color.Magenta;
            this.Label_CD.Location = new System.Drawing.Point(382, 12);
            this.Label_CD.Name = "Label_CD";
            this.Label_CD.Size = new System.Drawing.Size(44, 41);
            this.Label_CD.TabIndex = 12;
            this.Label_CD.Text = "...";
            this.Label_CD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BG_Notifaction
            // 
            this.BG_Notifaction.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.BG_Notifaction.BalloonTipClicked += new System.EventHandler(this.OpenNotifaction);
            this.BG_Notifaction.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OpenNotifaction);
            // 
            // ES_Arena
            // 
            this.ES_Arena.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ES_Arena.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ES_Arena.FormattingEnabled = true;
            this.ES_Arena.Location = new System.Drawing.Point(308, 23);
            this.ES_Arena.Name = "ES_Arena";
            this.ES_Arena.Size = new System.Drawing.Size(68, 25);
            this.ES_Arena.TabIndex = 13;
            // 
            // Label_SourceCode
            // 
            this.Label_SourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_SourceCode.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.Label_SourceCode.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Label_SourceCode.Location = new System.Drawing.Point(264, 307);
            this.Label_SourceCode.Name = "Label_SourceCode";
            this.Label_SourceCode.Size = new System.Drawing.Size(112, 13);
            this.Label_SourceCode.TabIndex = 14;
            this.Label_SourceCode.Text = "查看本软件源代码";
            this.Label_SourceCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Label_SourceCode.Click += new System.EventHandler(this.CheckSourceCode);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 320);
            this.Controls.Add(this.Label_SourceCode);
            this.Controls.Add(this.ES_Arena);
            this.Controls.Add(this.Label_CD);
            this.Controls.Add(this.ES_Setting);
            this.Controls.Add(this.Label_Kxnrl);
            this.Controls.Add(this.Label_WARN);
            this.Controls.Add(this.Group_Site);
            this.Controls.Add(this.ES_Refresh);
            this.Controls.Add(this.ItemsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DNF金币比价器 [跨1]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.UI_Load);
            this.Shown += new System.EventHandler(this.UI_Shown);
            this.SizeChanged += new System.EventHandler(this.OnFormResized);
            ((System.ComponentModel.ISupportInitialize)(this.ItemsList)).EndInit();
            this.Group_Site.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ItemsList;
        private System.Windows.Forms.CheckBox ES_7881;
        private System.Windows.Forms.CheckBox ES_5173;
        private System.Windows.Forms.CheckBox ES_UU898;
        private System.Windows.Forms.CheckBox ES_DD373;
        private System.Windows.Forms.Button ES_Refresh;
        private System.Windows.Forms.GroupBox Group_Site;
        private System.Windows.Forms.Label Label_WARN;
        private System.Windows.Forms.Label Label_Kxnrl;
        private System.Windows.Forms.Button ES_Setting;
        private System.Windows.Forms.Label Label_CD;
        private System.Windows.Forms.NotifyIcon BG_Notifaction;
        private System.Windows.Forms.DataGridViewTextBoxColumn pGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn pCoins;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRecvs;
        private System.Windows.Forms.DataGridViewTextBoxColumn pPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRatio;
        private System.Windows.Forms.DataGridViewTextBoxColumn pArena;
        private System.Windows.Forms.DataGridViewTextBoxColumn pTrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSites;
        private System.Windows.Forms.DataGridViewButtonColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn pBLink;
        private System.Windows.Forms.ComboBox ES_Arena;
        private System.Windows.Forms.Label Label_SourceCode;
        private System.Windows.Forms.CheckBox ES_EE979;
    }
}

