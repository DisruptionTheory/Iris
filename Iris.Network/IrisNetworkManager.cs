using System;
using System.Text;
using System.Threading.Tasks;
using Iris.Infrastructure.Contracts;
using Iris.Infrastructure.Contracts.Services;
using Iris.Infrastructure.Models;

namespace Iris.Network
{
    public class IrisNetworkManager : INetworkManager
    {
        private readonly UdpEndpoint Endpoint = new UdpEndpoint();

        private readonly IConfigurationService ConfigurationService;

        public IrisNetworkManager(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public void Initialize()
        {
            Endpoint.DataReceived += EndpointOnDataReceived;

            Endpoint.Initialize();
        }

        public async Task<bool> SendMousePositionUpdate(MousePosition position)
        {
            byte[] data = new byte[20];

            Array.Copy(BitConverter.GetBytes((int)MessageType.MousePositionUpdate), 0, data, 0, 4);

            Array.Copy(BitConverter.GetBytes(position.X), 0, data, 4, 8);

            Array.Copy(BitConverter.GetBytes(position.Y), 0, data, 12, 8);

            return await Endpoint.Send(data);
        }

        public event MousePositionUpdateEventHandler MousePositionUpdate;

        private void EndpointOnDataReceived(byte[] data)
        {
            MessageType messageType = (MessageType)BitConverter.ToInt32(data, 0);

            switch (messageType)
            {
                case MessageType.MousePositionUpdate:
                    ProcessMousePositionUpdateMessage(data);
                    break;
            }
        }

        private void ProcessMousePositionUpdateMessage(byte[] message)
        {
            MousePosition position = new MousePosition();

            position.X = BitConverter.ToInt64(message, 4);

            position.Y = BitConverter.ToInt64(message, 12);

            MousePositionUpdate?.Invoke(position);
        }
    }
}
