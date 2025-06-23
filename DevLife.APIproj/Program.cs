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






var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // <--- ეს ხსნის UI-ს
}
    else
    {
        
        app.UseExceptionHandler("/error");
    }

app.MapGet("/", () => "Hello World!");


//Endpoints
app.MapAuthEndpoints();

app.Run();
