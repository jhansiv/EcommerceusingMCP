using EcommerceApi.Data;
using EcommerceApi.Dtos;
using EcommerceApi.Infrastructure;
using EcommerceApi.Models;
using EcommerceusingMCP.Tools;


var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Add the MCP services: the transport to use (http) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

var app = builder.Build();
app.MapMcp("mcp");
app.UseHttpsRedirection();

// Enable attribute routing for controllers
app.MapControllers();

app.Run("http://localhost:6020");
