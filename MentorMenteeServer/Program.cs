using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MentorMenteeServer.Hubs;
using MentorMenteeServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ SignalR
builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Cấu hình Kestrel để lắng nghe trên cổng 5268 với HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5268, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
//Add swagger
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
// Thêm cấu hình authentication (Cookie, có thể thay bằng JWT nếu muốn)
//builder.Services.AddAuthentication("Cookies")
//    .AddCookie("Cookies", options =>
//    {
//        options.LoginPath = "/api/auth/login";
//        options.AccessDeniedPath = "/api/auth/denied";
//    });

// Thêm cấu hình JWT authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "super_secret_key_123!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "MentorMenteeServer";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

    // Bật log lỗi chi tiết JWT (giúp debug 401)
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("JWT Error: " + context.Exception.ToString());
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.UseSession();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

var clients = new ConcurrentDictionary<string, WebSocket>();


/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin());
});*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();  // Định tuyến API Controllers
app.MapHub<ChatHub>("/chathub"); // Định tuyến SignalR Hub

app.Run();
