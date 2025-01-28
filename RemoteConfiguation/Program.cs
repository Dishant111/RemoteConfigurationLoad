using RemoteConfiguation;

var builder = WebApplication.CreateBuilder(args);

var remoteConfigEndpoint = "https://service1.dishant.dev/swagger/v1/swagger.json";
var reloadInterval = TimeSpan.FromMinutes(5);

builder.Configuration.AddRemoteConfiguration(remoteConfigEndpoint, reloadInterval);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();