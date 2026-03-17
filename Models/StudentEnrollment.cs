namespace BrokenApiAdapter.Models;

/// <summary>
/// The canonical student enrollment representation returned by this API.
/// This is the contract — consumers depend on this shape.
/// </summary>
public class StudentEnrollment
{
    /// <summary>Unique identifier for the student (e.g. "1001").</summary>
    public long? StudentId { get; set; }

    public string ExternalStudentId { get; set; } = string.Empty;

    /// <summary>Full name of the student.</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>Course code (e.g. "CS201").</summary>
    public string CourseCode { get; set; } = string.Empty;

    /// <summary>Human-readable course title (e.g. "Data Structures").</summary>
    public string CourseTitle { get; set; } = string.Empty;

    /// <summary>Date the student enrolled in the course.</summary>
    public DateOnly EnrolledOn { get; set; }

    /// <summary>Numeric grade achieved by the student.</summary>
    public int Grade { get; set; }
}
