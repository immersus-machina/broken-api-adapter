using BrokenApiAdapter.Adapter;
using BrokenApiAdapter.Mapping;
using BrokenApiAdapter.Models.Contract;
using BrokenApiAdapter.Models.External;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<IBrokenApiAdapter, BrokenApiAdapter.Adapter.BrokenApiAdapter>();
builder.Services.AddSingleton<IMapper<ExternalStudentRecord, StudentEnrollment>, StudentRecordMapper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
