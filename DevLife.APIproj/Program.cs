using DevLife.APIproj.DTO;
using DevLife.APIproj.Endpointz;
using DevLife.APIproj.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


//Services 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DevLifeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Configuration.AddEnvironmentVariables();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    else
    {
        
        app.UseExceptionHandler("/error");
    }

app.MapGet("/", () => "Hello World!");


//Endpoints
app.MapAuthEndpoints();
app.MapCasinoEndpoints();
app.MapCoadRoastEndpoint();

app.Run();
