var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Setup Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

// 🔐 Allow CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:5000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(); // 🔥 THIS is critical
app.UseHttpsRedirection();

app.MapPost("/api/SecureData", (HttpRequest request) =>
{
    var keyFromHeader = request.Headers["X-API-KEY"].ToString();
    var expectedKey = Environment.GetEnvironmentVariable("MY_API_KEY");

    if (keyFromHeader != expectedKey)
    {
        return Results.BadRequest("❌ Invalid API Key");
    }

    return Results.Ok("✅ Authorized. Your API key is valid.");
});




app.Run();
