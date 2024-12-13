﻿using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Enums;
using Bit_Ruisseau.Pages;
using MQTTnet;

namespace Bit_Ruisseau.Classes;

public class P2PEngine
{
    public async void Connect(string hostBoxText, string userBoxText, string passwordBoxText, Form _form)
    {
        //todo Environment.SpecialFolder.LocalApplicationData
        if (!Directory.Exists($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics"))
        {
            Directory.CreateDirectory($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics");
        }

        List<string> paths = Directory.GetFiles($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics").ToList();


        paths.ForEach(path =>
        {
            MediaData media = new MediaData();
            var tfile = TagLib.File.Create(path);

            FileInfo fi = new FileInfo(path);
            media.Size = fi.Length;

            media.Title = tfile.Tag.Title;
            media.Type = Path.GetExtension(path);
            media.Artist = tfile.Tag.FirstPerformer;
            TimeSpan duration = tfile.Properties.Duration;
            media.Duration = $"{duration.Minutes:D2}:{duration.Seconds:D2}";
            Utils.Utils.LocalMusicList.Add(media);
        });

        MqttClientFactory factory = new MqttClientFactory();

        var mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(hostBoxText, 1883)
            .WithCredentials(userBoxText, passwordBoxText)
            .WithClientId(Utils.Utils.GetGuid())
            .WithCleanSession()
            .Build();


        var res = await mqttClient.ConnectAsync(options);

        if (res.ResultCode == MqttClientConnectResultCode.Success)
        {
            Debug.WriteLine("Connected to MQTT broker successfully.");
            Debug.WriteLine("GUID: " + Utils.Utils.GetGuid());
            GenericEnvelope sender =
                Utils.Utils.CreateEnveloppeCatalogSender(Utils.Utils.LocalMusicList, MessageType.DEMANDE_CATALOGUE);

            Utils.Utils.SendMessage(mqttClient, sender, Utils.Utils.GetTopic());


            /////////////////////////// RECEIVE MESSAGES EVENT ///////////////////////////
            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string receivedMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                GenericEnvelope envelope = JsonSerializer.Deserialize<GenericEnvelope>(receivedMessage);

                if (envelope.SenderId != Utils.Utils.GetGuid())
                {
                    switch (envelope.MessageType)
                    {
                        case MessageType.DEMANDE_CATALOGUE:
                            GenericEnvelope res = Utils.Utils.CreateEnveloppeCatalogSender(Utils.Utils.LocalMusicList,
                                MessageType.ENVOIE_CATALOGUE);

                            if (mqttClient == null || !mqttClient.IsConnected)
                            {
                                MessageBox.Show("Client not connected. Reconnecting...");
                                await mqttClient.ConnectAsync(options);
                            }

                            Utils.Utils.SendMessage(mqttClient, res, Utils.Utils.GetTopic());

                            Console.WriteLine("Message sent successfully!");
                            break;

                        case MessageType.ENVOIE_CATALOGUE:
                            SendCatalog enveloppe = JsonSerializer.Deserialize<SendCatalog>(envelope.EnveloppeJson);
                            Utils.Utils.SendersCatalogs.Add(envelope.SenderId, enveloppe.Content);

                            enveloppe.Content.ForEach(media => { Utils.Utils.CatalogList.Add(media); });
                            break;
                    }
                }
            };


            LobbyPage lobby = new LobbyPage(mqttClient, Utils.Utils.LocalMusicList);
            lobby.Location = _form.Location;
            lobby.StartPosition = FormStartPosition.Manual;
            lobby.FormClosing += delegate { _form.Close(); };
            _form.Hide();
            lobby.Show();
        }
    }
}