using MQTTnet;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Enums;
using Bit_Ruisseau.Utils;

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

        private void changeviewCatalog_button_Click(object sender, EventArgs e)
        {
            CatalogPage catalogPage = new CatalogPage();
            catalogPage.Show();
        }

        private void fileDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string path = $"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics\\{Utils.Utils.LocalMusicList[e.RowIndex].Title}{Utils.Utils.LocalMusicList[e.RowIndex].Type}";

                if (File.Exists(path))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Le fichier n'existe pas.");
                }

            }
        }

        private void refreshfilesButton_Click(object sender, EventArgs e)
        {
            Utils.FilesUtils.GetLocalFiles();
            this.fileDataGridView.DataSource = null;
            this.fileDataGridView.DataSource = Utils.Utils.LocalMusicList;
        }
    }
}
