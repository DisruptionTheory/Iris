using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iris.Infrastructure.Contracts;
using Iris.Infrastructure.Contracts.Services;
using Iris.Infrastructure.Models;
using Newtonsoft.Json;

namespace Iris.Network
{
    public class IrisNetworkManager : INetworkManager
    {
        private readonly IConfigurationService ConfigurationService;

        private UdpClient client;

        private IPEndPoint anyEndpoint = new IPEndPoint(IPAddress.Any, 2783);

        private IPEndPoint multicastEndpoint = new IPEndPoint(IPAddress.Parse("224.100.0.77"), 2783);

        public IrisNetworkManager(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public event MousePositionUpdateEventHandler MousePositionUpdate;

        public void Initialize()
        {
            client = new UdpClient(multicastEndpoint.Port);

            client.Client.Bind(anyEndpoint);

            client.DontFragment = true;

            client.JoinMulticastGroup(multicastEndpoint.Address, 100);

            client.BeginReceive(ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            byte[] message = client.EndReceive(result, ref multicastEndpoint);

            ProcessMessage(message);

            client.BeginReceive(ReceiveCallback, null);
        }

        public async Task<bool> SendMousePositionUpdate(MousePosition position)
        {
            return await SendMessage(position, MessageType.MousePositionUpdate);
        }

        private async Task<bool> SendMessage(object msgObj, MessageType type)
        {
            List<byte> messageData = new List<byte>();

            messageData.AddRange(BitConverter.GetBytes((int)type));

            string msg = JsonConvert.SerializeObject(msgObj);

            messageData.AddRange(Encoding.UTF8.GetBytes(msg));

            int result = await client.SendAsync(messageData.ToArray(), messageData.Count, multicastEndpoint);

            return true;
        }

        private async Task ProcessMessage(byte[] message)
        {
            int msgType = BitConverter.ToInt32(message.ToArray(), 0);

            string msg = Encoding.UTF8.GetString(message.Skip(4).ToArray());

            switch ((MessageType)msgType)
            {
                case MessageType.MousePositionUpdate:
                    ProcessMousePositionUpdate(JsonConvert.DeserializeObject<MousePosition>(msg));
                    break;
                default:
                    break;
            }
        }

        private void ProcessMousePositionUpdate(MousePosition mousePosition)
        {
            if (ConfigurationService.InstanceId == mousePosition.RecipientId)
            {
                if (MousePositionUpdate != null)
                {
                    MousePositionUpdate(mousePosition);
                }
            }
        }
    }
}
