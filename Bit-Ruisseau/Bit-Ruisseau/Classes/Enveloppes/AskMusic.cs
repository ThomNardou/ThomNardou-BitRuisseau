using System.Text.Json;
using Bit_Ruisseau.Interface;

namespace Bit_Ruisseau.Classes.Enveloppes;

public class AskMusic : IMessage
{
    /*
        type 4
    */
    private int _type;
    private string _guid;
    private string _personnal_topic;
    private string _file_name;

    public int Type
    {
        get => _type;
        set => _type = value;
    }

    public string Guid
    {
        get => _guid;
        set => _guid = value;
    }

    public string FileName
    {
        get => _file_name;
        set => _file_name = value;
    }

    public string PersonnalTopic
    {
        get => _personnal_topic;
        set => _personnal_topic = value;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}