using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iris.Infrastructure.Contracts;
using Iris.Infrastructure.Contracts.Services;
using Iris.Infrastructure.Models;
using Lidgren.Network;
using Newtonsoft.Json;

namespace Iris.Network
{
    public class IrisNetworkManager : INetworkManager
    {
        private readonly IConfigurationService ConfigurationService;

        private NetPeer peer;

        private Thread serverThread;

        private List<NetIncomingMessage> messages = new List<NetIncomingMessage>();

        public IrisNetworkManager(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public void Initialize()
        {
            NetPeerConfiguration config = new NetPeerConfiguration(ConfigurationService.ApplicationIdentifier);

            config.Port = ConfigurationService.Port;

            peer = new NetPeer(config);

            peer.Start();

            serverThread = new Thread(Listen);

            serverThread.Start();

            peer.DiscoverLocalPeers(ConfigurationService.Port);
        }

        public async Task<bool> SendMousePositionUpdate(MousePosition position)
        {
            var message = peer.CreateMessage();

            message.WriteAllProperties(position);

            peer.SendMessage(message, peer.Connections, NetDeliveryMethod.ReliableUnordered, (int) MessageType.MousePositionUpdate);

            return true;
        }

        public event MousePositionUpdateEventHandler MousePositionUpdate;

        private void Listen()
        {
            while (true)
            {
                peer.MessageReceivedEvent.WaitOne();

                peer.MessageReceivedEvent.Reset();

                peer.ReadMessages(messages);

                ProcessMessages();
            }
        }

        private void ProcessMessages()
        {
            foreach (var message in messages)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        ProcessDiscoveryRequest(message);
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        ProcessDiscoveryResponse(message);
                        break;
                    case NetIncomingMessageType.Data:
                        ProcessTransaction(message);
                        break;
                    default:
                        break;
                }
            }

            messages.Clear();
        }

        private void ProcessTransaction(NetIncomingMessage message)
        {
            switch (message.SequenceChannel)
            {
                case (int)MessageType.MousePositionUpdate:
                    MousePosition position = new MousePosition();
                    message.ReadAllProperties(position);
                    MousePositionUpdate?.Invoke(position);
                    break;
                default:
                    break;
            }
        }

        private void ProcessDiscoveryRequest(NetIncomingMessage message)
        {
            if (peer.Connections.All(x => x.RemoteEndPoint.Address.ToString() != message.SenderEndPoint.Address.ToString()))
            {
                NetConnection connection = peer.Connect(message.SenderEndPoint);

                NetOutgoingMessage response = peer.CreateMessage();

                peer.SendDiscoveryResponse(response, connection.RemoteEndPoint);
            }
        }

        private void ProcessDiscoveryResponse(NetIncomingMessage message)
        {
            if (peer.Connections.All(x => x.RemoteEndPoint.Address.ToString() != message.SenderEndPoint.Address.ToString()))
            {
                NetConnection connection = peer.Connect(message.SenderEndPoint);
            }
        }
    }
}
