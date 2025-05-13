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


var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ SignalR
builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình Kestrel để lắng nghe trên cổng 5268 với HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5268, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var clients = new ConcurrentDictionary<string, WebSocket>();


async Task HandleWebSocket(string clientId, WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];

    while (webSocket.State == WebSocketState.Open)
    {
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        if (result.MessageType == WebSocketMessageType.Text)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Received: {message}");

            // Phát lại tin nhắn đến tất cả client
            foreach (var client in clients)
            {
                if (client.Key != clientId && client.Value.State == WebSocketState.Open)
                {
                    await client.Value.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
        else if (result.MessageType == WebSocketMessageType.Close)
        {
            clients.TryRemove(clientId, out _);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        }
    }
}

app.UseHttpsRedirection();

app.UseWebSockets();

app.Map("/wss", async (HttpContext context) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var clientId = Guid.NewGuid().ToString();
        clients.TryAdd(clientId, webSocket);

        await HandleWebSocket(clientId, webSocket);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

app.MapControllers();

// Cấu hình endpoint cho SignalR
app.UseRouting();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin());
});
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();  // Định tuyến API Controllers
app.MapHub<ChatHub>("/chathub"); // Định tuyến SignalR Hub

app.UseCors("AllowAll");
app.Run();
