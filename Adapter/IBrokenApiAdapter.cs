namespace BrokenApiAdapter.Adapter;

/// <summary>
/// Generic adapter: fetches data from an unreliable API and maps it to a clean model.
/// </summary>
public interface IBrokenApiAdapter
{
    Task<IReadOnlyList<TClean>> GetCleanDataAsync<TRaw, TClean>(string url, IMapper<TRaw, TClean> mapper);
}
