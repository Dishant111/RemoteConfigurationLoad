namespace RemoteConfiguation;

public static class RemoteConfigurationExtensions
{
    public static IConfigurationBuilder AddRemoteConfiguration(this IConfigurationBuilder builder, string endpoint,
        TimeSpan reloadInterval)
    {
        return builder.Add(new RemoteConfigurationSource(endpoint, reloadInterval));
    }
}