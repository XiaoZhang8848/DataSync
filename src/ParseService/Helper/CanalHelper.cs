using System.Text.Json;
using ParseService.Basic;

namespace ParseService.Helper;

public class CanalHelper : ICanalHelper
{
    public CanalModel<T> ParseData<T>(string result) where T : class, new()
    {
        var data = JsonSerializer.Deserialize<CanalModel<T>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        ArgumentNullException.ThrowIfNull(data);

        data.ParseData();
        return data;
    }
}