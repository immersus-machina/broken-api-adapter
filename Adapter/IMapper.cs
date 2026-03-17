namespace BrokenApiAdapter.Adapter;

/// <summary>
/// Maps a raw external model to a clean contract model.
/// </summary>
public interface IMapper<in TRaw, out TClean>
{
    TClean Map(TRaw source);
}
