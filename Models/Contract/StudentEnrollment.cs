namespace BrokenApiAdapter.Models.Contract;

/// <summary>
/// The canonical student enrollment representation returned by this API.
/// This is the contract — consumers depend on this shape.
/// </summary>
public record StudentEnrollment
{
    /// <summary>Raw student identifier from the external source (e.g. "S1001").</summary>
    public string ExternalStudentId { get; init; } = string.Empty;

    /// <summary>Parsed numeric student ID. Only populated when the external ID starts with 'S'.</summary>
    public long? StudentId { get; init; }

    /// <summary>Full name of the student.</summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>Course code (e.g. "CS201").</summary>
    public string CourseCode { get; init; } = string.Empty;

    /// <summary>Human-readable course title (e.g. "Data Structures").</summary>
    public string CourseTitle { get; init; } = string.Empty;

    /// <summary>Date the student enrolled in the course.</summary>
    public DateOnly EnrolledOn { get; init; }

    /// <summary>Numeric grade achieved by the student.</summary>
    public int Grade { get; init; }

    /// <summary>Age of the student. Null if not provided by the external source.</summary>
    public int? Age { get; init; }
}
