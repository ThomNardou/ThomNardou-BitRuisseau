﻿using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Utils;
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
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bit_Ruisseau.Enums;

namespace Bit_Ruisseau.Pages
{
    public partial class LoginPages : Form
    {
        
        
        public LoginPages()
        {
            InitializeComponent();
            Utils.Utils.LocalMusicList = new List<MediaData>();
            Utils.Utils.SendersCatalogs = new Dictionary<string, List<MediaData>>();
            this.hostBox.Text = "127.0.0.1";
            this.userBox.Text = "ict";
            this.passwordBox.Text = "321";
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
            Utils.Utils.Engine.Connect(this.hostBox.Text, this.userBox.Text, this.passwordBox.Text, this);
        }
    }
}
