using MQTTnet;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Enums;

namespace Bit_Ruisseau.Pages
{
    public partial class LobbyPage : Form
    {
        private IMqttClient client;
        private List<MediaData> localCatalog;

        public LobbyPage(IMqttClient _client, List<MediaData> _localCatalog)
        {
            InitializeComponent();
            client = _client;
            localCatalog = _localCatalog;

            this.fileDataGridView.DataSource = Utils.Utils.LocalMusicList;
            this.fileDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void yourFiles_Click(object sender, EventArgs e)
        {
            this.fileDataGridView.DataSource = null;
            this.fileDataGridView.DataSource = Utils.Utils.LocalMusicList;
        }

        private void changeviewCatalog_button_Click(object sender, EventArgs e)
        {
            this.fileDataGridView.DataSource = null;
            Utils.Utils.SendersCatalogs.ToList().ForEach(senderCata =>
            {
                this.fileDataGridView.DataSource = senderCata.Value;
            });
        }
    }
}
