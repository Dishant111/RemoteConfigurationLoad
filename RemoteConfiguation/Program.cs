using RemoteConfiguation;

var builder = WebApplication.CreateBuilder(args);

var remoteConfigEndpoint = "https://localhost:44372/GetConfiguration/app";
var reloadInterval = TimeSpan.FromSeconds(15);

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