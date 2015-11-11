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
        public string GetConfigurationValue(string key)
        {
            throw new NotImplementedException();
        }

        public string InstanceId => "ID";
    }
}
