﻿using MQTTnet;
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
using Bit_Ruisseau.Utils;

namespace Bit_Ruisseau.Pages
{
    public partial class LobbyPage : Form
    {
        private IMqttClient client;
        private List<MediaData> localCatalog;
        private bool isLocal = true;

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
            isLocal = true;
        }

        private void changeviewCatalog_button_Click(object sender, EventArgs e)
        {
            this.fileDataGridView.DataSource = null;
            this.fileDataGridView.DataSource = Utils.Utils.CatalogList;
            isLocal = false;
        }

        private void fileDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!isLocal)
                {
                    MediaData selectedMedia = Utils.Utils.CatalogList[e.RowIndex];
                    MessageUtilis.AskFile(selectedMedia);
            
                    Console.WriteLine("File selected : " + Utils.Utils.CatalogList[e.RowIndex].Title);
                }
            }
        }
    }
}
