using System.Net.Mime;
using System.Text.Json;

namespace RemoteConfiguation;

public class RemoteConfigurationProvider : ConfigurationProvider
{
    private readonly string _endpoint;
    private readonly TimeSpan _reloadInterval;
    private Timer? _timer;

    public RemoteConfigurationProvider(string endpoint, TimeSpan reloadInterval)
    {
        _endpoint = endpoint;
        _reloadInterval = reloadInterval;
    }

    public override void Load()
    {
        LoadConfigurationAsync().Wait();
        StartReloading();
    }

    private async Task LoadConfigurationAsync()
    {
        using var client = new HttpClient();
        var ip = new FileInPut()
        {
         Key   = "Supersecret"
        };         
        
        var response = await client.PostAsync(_endpoint, new StringContent(JsonSerializer.Serialize(ip),null,MediaTypeNames.Application.Json));

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStreamAsync();
            if (content is not null)
            {
                var data = JsonConfigurationFileParser.Parse(content);

                if (data != null)
                {
                    Data = data;
                    OnReload();
                }
            }
        }
    }

    private void StartReloading()
    {
        _timer = new Timer(async _ =>
        {
            try
            {
                await LoadConfigurationAsync();
            }
            catch
            {
                // Log or handle errors (e.g., server down)
            }
        }, null, _reloadInterval, _reloadInterval);
    }
}

public class FileInPut
{
    public string Key { get; set; }
}