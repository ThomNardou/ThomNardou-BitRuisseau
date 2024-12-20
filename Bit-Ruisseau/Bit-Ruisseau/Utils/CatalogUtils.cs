using System.Text.Json;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;

namespace Bit_Ruisseau.Utils;

public class CatalogUtils
{
    
    /// <summary>
    /// Fonction qui est appelée lorsqu'un catalogue est reçu
    /// </summary>
    /// <param name="_envelope">Générique enveloppe reçu</param>
    public static void OnCatalogReceived(GenericEnvelope _envelope)
    {
        SendCatalog enveloppeSendCatalog = JsonSerializer.Deserialize<SendCatalog>(_envelope.EnvelopeJson);
        Utils.SendersCatalogs.Add(_envelope.SenderId, enveloppeSendCatalog.Content);

        enveloppeSendCatalog.Content.ForEach(media => { Utils.CatalogList.Add(media); });
    }
}