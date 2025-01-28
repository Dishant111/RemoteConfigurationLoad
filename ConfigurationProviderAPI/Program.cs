using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapPost("GetConfiguration/{appName}", async (string appNAme,ConfigurationInput ip,IWebHostEnvironment environment) =>
{
    if (ip.Key == "Supersecret")
    {
        var file =Path.Combine(environment.ContentRootPath,"Settings.json") ;
        var stream = System.IO.File.OpenRead(file);
        return Results.File(stream,MediaTypeNames.Application.Json);
    }
    return Results.BadRequest();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class ConfigurationInput
{
    public string Key { get; set; }
}