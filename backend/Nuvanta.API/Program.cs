using Nuvanta.API.Extensions;
using Nuvanta.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddNuvantaServices(builder.Configuration);
builder.Services.AddNuvantaCors(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // JSON at /openapi/v1.json
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
