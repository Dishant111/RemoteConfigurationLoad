namespace RemoteConfiguation;

public class RemoteConfigurationSource : IConfigurationSource
{
    private readonly string _endpoint;
    private readonly TimeSpan _reloadInterval;

    public RemoteConfigurationSource(string endpoint, TimeSpan reloadInterval)
    {
        _endpoint = endpoint;
        _reloadInterval = reloadInterval;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new RemoteConfigurationProvider(_endpoint, _reloadInterval);
    }
}