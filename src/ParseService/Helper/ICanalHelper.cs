using ParseService.Basic;

namespace ParseService.Helper;

public interface ICanalHelper
{
    CanalModel<T> ParseData<T>(string result) where T : class, new();
}