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

namespace Bit_Ruisseau.Pages
{
    public partial class LobbyPage : Form
    {
        private IMqttClient client;
        public LobbyPage(IMqttClient _client)
        {
            InitializeComponent();
            client = _client;
        }

        private async void sendButton_Click(object sender, EventArgs e)
        {
            Utils.Utils.SendMessage(this.client, "HELLO, qui a des musiques ?", "testTiago");
        }
    }
}