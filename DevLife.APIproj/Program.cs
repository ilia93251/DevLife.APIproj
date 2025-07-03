using DevLife.APIproj.Data;
using DevLife.APIproj.DTO;
using DevLife.APIproj.Endpoints;
using DevLife.APIproj.Endpointz;
using DevLife.APIproj.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


//Services 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddDbContext<DevLifeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});




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

app.UseSession();


//Endpoints
app.MapAuthEndpoints();
app.MapCasinoEndpoints();
app.MapCoadRoastEndpoint();
app.MapBugChaseEndpoints();
app.MapExcuseEndpoints();
app.MapGitHubEndpoints();
app.MapDatingEndpoints();
app.MapHub<BugChaseHub>("/hub/bugchase");


app.Run(); 
