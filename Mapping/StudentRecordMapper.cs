using System.Globalization;
using BrokenApiAdapter.Adapter;
using BrokenApiAdapter.Models.Contract;
using BrokenApiAdapter.Models.External;

namespace BrokenApiAdapter.Mapping;

/// <summary>
/// Maps from the external student API model to the stable contract model.
///
/// ExternalStudentRecord may change at any time — when it does, update this mapper.
/// StudentEnrollment is the fixed contract and must not change.
/// </summary>
public class StudentRecordMapper : IMapper<ExternalStudentRecord, StudentEnrollment>
{
    public StudentEnrollment Map(ExternalStudentRecord source)
    {
        // "CS201 - Data Structures" → code and title separately
        var (courseCode, courseTitle) = ParseCourse(source.Course);

        return new StudentEnrollment
        {
            ExternalStudentId = source.StudentId, // "S1001" → always passed through
            StudentId = ParseStudentId(source.StudentId), // "S" prefix is ours — parse the numeric part
            FullName = source.Name,             // "Alice Johnson" → renamed for clarity
            CourseCode = courseCode,             // extracted from combined Course field
            CourseTitle = courseTitle,           // extracted from combined Course field
            EnrolledOn = DateOnly.Parse(source.EnrollmentDate, CultureInfo.InvariantCulture),
            Grade = source.Grade,               // numeric grade, passed through
            Age = null                          // not provided by external source
        };
    }

    /// <summary>
    /// "S" prefix indicates our student ID scheme — parse the numeric part.
    /// Returns null if the ID doesn't match.
    /// </summary>
    private static long? ParseStudentId(string externalId)
    {
        if (externalId.StartsWith('S') && long.TryParse(externalId.AsSpan(1), out var id))
            return id;

        return null;
    }

    /// <summary>
    /// Splits "CS201 - Data Structures" into ("CS201", "Data Structures").
    /// </summary>
    private static (string Code, string Title) ParseCourse(string course)
    {
        var parts = course.Split(" - ", 2);
        return parts.Length == 2
            ? (parts[0].Trim(), parts[1].Trim())
            : (course, string.Empty);
    }
}
