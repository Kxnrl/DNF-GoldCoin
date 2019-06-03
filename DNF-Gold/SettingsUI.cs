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
            DialogResult = DialogResult.OK;
            UI.N_Enabled = ES_Notifaction.Checked;
            UI.N_MaxPrice = (float)ES_MaxPrice.Value;
            UI.N_MinRatio = (float)ES_MinRatio.Value;
            ((UI)Owner).Activate();
            Close();
        }
    }
}
