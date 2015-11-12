using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iris.Infrastructure.Contracts;
using Iris.Infrastructure.Contracts.Services;
using Iris.Infrastructure.Models;
using Newtonsoft.Json;

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
            position.SenderId = ConfigurationService.InstanceId;

            string json = JsonConvert.SerializeObject(position);

            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

            List<byte> data = new List<byte>();

            data.AddRange(BitConverter.GetBytes((int)MessageType.MousePositionUpdate));

            data.AddRange(jsonBytes);

            return await Endpoint.Send(data.ToArray());
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
            byte[] jsonData = message.Skip(4).ToArray();

            MousePosition position = JsonConvert.DeserializeObject<MousePosition>(Encoding.UTF8.GetString(jsonData));

            if (position.RecipientId == ConfigurationService.InstanceId)
            {
                MousePositionUpdate?.Invoke(position);
            }
        }
    }
}
