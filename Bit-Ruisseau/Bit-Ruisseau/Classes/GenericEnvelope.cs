using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit_Ruisseau.Enums;

namespace Bit_Ruisseau.Classes
{
    public class GenericEnvelope
    {
        string _senderId;
        MessageType _messageType;

        string _enveloppeJson;//classe specefique serialisee

        public MessageType MessageType { get => _messageType; set => _messageType = value; }
        public string SenderId { get => _senderId; set => _senderId = value; }
        public string EnveloppeJson { get => _enveloppeJson; set => _enveloppeJson = value; }
    }
}
