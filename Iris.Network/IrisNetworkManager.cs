using System;
using Iris.Infrastructure.Contracts;
using Iris.Infrastructure.Contracts.Services;
using Iris.Infrastructure.Models;

namespace Iris.Network
{
    public class IrisNetworkManager : INetworkManager
    {
        private readonly UdpEndpoint Endpoint = new UdpEndpoint();

        public void Initialize()
        {
            Endpoint.DataReceived += EndpointOnDataReceived;

            Endpoint.Initialize();
        }

        public void SendMousePositionUpdate(MousePosition position)
        {
            byte[] data = new byte[20];

            Array.Copy(BitConverter.GetBytes((int)MessageType.MousePositionUpdate), 0, data, 0, 4);

            Array.Copy(BitConverter.GetBytes(position.X), 4, data, 0, 8);

            Array.Copy(BitConverter.GetBytes(position.Y), 12, data, 0, 8);

            Endpoint.Send(data);
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
