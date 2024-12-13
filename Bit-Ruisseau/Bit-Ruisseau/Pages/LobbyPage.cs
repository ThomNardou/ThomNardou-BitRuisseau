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
        }

        private async void sendButton_Click(object sender, EventArgs e)
        {
            Utils.Utils.SendMessage(this.client, "HELLO, qui a des musiques ?", Utils.Utils.GetTopic(), MessageType.DEMANDE_CATALOGUE);
        }
    }
}
