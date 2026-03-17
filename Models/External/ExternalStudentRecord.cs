namespace BrokenApiAdapter.Models.External;

/// <summary>
/// Represents the raw student record as received from the external data source.
/// This model may change without notice — it mirrors the upstream API shape.
/// </summary>
public record ExternalStudentRecord
{
    public string StudentId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Course { get; init; } = string.Empty;
    public string EnrollmentDate { get; init; } = string.Empty;
    public int Grade { get; init; }
}
