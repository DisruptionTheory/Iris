using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iris.Infrastructure.Contracts.Services;

namespace Iris.Core
{
    internal class IrisConfigurationService : IConfigurationService
    {
        public IrisConfigurationService()
        {
            InstanceId = System.Environment.MachineName;

            Port = 12345;

            ApplicationIdentifier = "Iris";
        }

        public string GetConfigurationValue(string key)
        {
            throw new NotImplementedException();
        }

        public string InstanceId { get; private set; }

        public int Port { get; private set; }
        
        public string ApplicationIdentifier { get; private set; }
    }
}
