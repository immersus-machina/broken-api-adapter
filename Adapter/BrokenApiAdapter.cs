using System.Net.Http.Json;

namespace BrokenApiAdapter.Adapter;

/// <summary>
/// Generic adapter: calls a URL, deserializes as TRaw, maps each item to TClean.
/// </summary>
public class BrokenApiAdapter(HttpClient httpClient) : IBrokenApiAdapter
{
    public async Task<IReadOnlyList<TClean>> GetCleanDataAsync<TRaw, TClean>(string url, IMapper<TRaw, TClean> mapper)
    {
        var rawRecords = await httpClient.GetFromJsonAsync<List<TRaw>>(url) ?? [];

        return [.. rawRecords.Select(mapper.Map)];
    }
}
