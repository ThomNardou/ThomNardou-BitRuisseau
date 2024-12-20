using System.Text.Json;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;

namespace Bit_Ruisseau.Utils;

public class CatalogUtils
{
    public static void OnCatalogReceived(GenericEnvelope _envelope)
    {
        SendCatalog enveloppeSendCatalog = JsonSerializer.Deserialize<SendCatalog>(_envelope.EnveloppeJson);
        Utils.SendersCatalogs.Add(_envelope.SenderId, enveloppeSendCatalog.Content);

        enveloppeSendCatalog.Content.ForEach(media => { Utils.CatalogList.Add(media); });
    }
}