using System;
using System.Windows.Forms;

namespace DNF_Gold
{
    public partial class SettingsUI : Form
    {
        public SettingsUI()
        {
            InitializeComponent();

            Icon = Properties.Resources.dfo;
        }

        private void ES_Confirm_Click(object sender, EventArgs e)
        {
            if (!UI.N_AutoRefresh && ES_AutoRefresh.Checked)
            {
                // resume
                UI.tickTimer = 30u;
            }

            DialogResult = DialogResult.OK;
            UI.N_Enabled = ES_Notifaction.Checked;
            UI.N_AutoRefresh = ES_AutoRefresh.Checked;
            UI.N_Background = ES_CloseOnTray.Checked;
            UI.N_MaxPrice = (float)ES_MaxPrice.Value;
            UI.N_MinRatio = (float)ES_MinRatio.Value;
            ((UI)Owner).Activate();
            Close();
        }

        private void OnShown(object sender, EventArgs e)
        {
            ES_Notifaction.Checked = UI.N_Enabled;
            ES_MaxPrice.Value = (decimal)UI.N_MaxPrice;
            ES_MinRatio.Value = (decimal)UI.N_MinRatio;
            ES_AutoRefresh.Checked = UI.N_AutoRefresh;
            ES_CloseOnTray.Checked = UI.N_Background;
        }
    }
}
