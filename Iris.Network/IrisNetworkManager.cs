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

        private UdpClient listener;

        private UdpClient sender;

        private IPEndPoint anyEndPoint = new IPEndPoint(IPAddress.Any, 1805);

        private IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse("239.255.190.7"), 1805);

        public IrisNetworkManager(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public event MousePositionUpdateEventHandler MousePositionUpdate;

        public void Initialize()
        {
            sender = new UdpClient();

            sender.DontFragment = true;

            sender.JoinMulticastGroup(remoteEndPoint.Address);

            listener = new UdpClient();

            listener.ExclusiveAddressUse = false;

            listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            listener.ExclusiveAddressUse = false;

            listener.Client.Bind(anyEndPoint);

            listener.DontFragment = true;

            listener.JoinMulticastGroup(remoteEndPoint.Address);

            listener.BeginReceive(ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            byte[] message = listener.EndReceive(result, ref anyEndPoint);

            ProcessMessage(message);

            listener.BeginReceive(ReceiveCallback, null);
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

            int result = await sender.SendAsync(messageData.ToArray(), messageData.Count, remoteEndPoint);

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
