using BrokenApiAdapter.Adapter;
using BrokenApiAdapter.Models.Contract;
using BrokenApiAdapter.Models.External;
using Microsoft.AspNetCore.Mvc;

namespace BrokenApiAdapter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentDataController(
    IBrokenApiAdapter adapter,
    IMapper<ExternalStudentRecord, StudentEnrollment> mapper
) : ControllerBase
{
    private const string DataSourceUrl =
        "https://red-tree-0d0d99603.6.azurestaticapps.net/data.json";

    [HttpGet("GetCleanData")]
    public async Task<ActionResult<IReadOnlyList<StudentEnrollment>>> GetCleanData()
    {
        var data = await adapter.GetCleanDataAsync(DataSourceUrl, mapper);
        return Ok(data);
    }
}
