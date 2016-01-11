namespace Iris.Infrastructure.Contracts.Services
{
    public interface IConfigurationService
    {
        string GetConfigurationValue(string key);

        string InstanceId { get; }

        int Port { get; }

        string ApplicationIdentifier { get; }
    }
}
