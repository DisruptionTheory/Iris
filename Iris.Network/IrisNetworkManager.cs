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

        private IPEndPoint anyEndpoint = new IPEndPoint(IPAddress.Any, 0);

        private IPEndPoint multicastEndpoint = new IPEndPoint(IPAddress.Parse("224.100.0.77"), 2783);

        public IrisNetworkManager(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public event MousePositionUpdateEventHandler MousePositionUpdate;

        public void Initialize()
        {
            client = new UdpClient(multicastEndpoint.Port);

            client.JoinMulticastGroup(multicastEndpoint.Address, 100);

            client.BeginReceive(ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            byte[] message = client.EndReceive(result, ref anyEndpoint);

            client.BeginReceive(ReceiveCallback, null);
        }

        public async Task<bool> SendMousePositionUpdate(MousePosition position)
        {
            string message = JsonConvert.SerializeObject(position);

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            int result = await client.SendAsync(messageBytes, messageBytes.Length, multicastEndpoint);

            return true;
        }
    }
}
