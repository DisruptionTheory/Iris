namespace Iris.Infrastructure.Contracts.Services
{
    public interface IConfigurationService
    {
        string GetConfigurationValue(string key);

        string InstanceId { get; }
    }
}
