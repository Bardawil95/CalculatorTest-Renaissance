using Calculator.Core;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<ISimpleCalculator, SimpleCalculator>();

// CORS for local development
// NEW (allows any localhost port)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
                origin.StartsWith("http://localhost") ||
                origin.StartsWith("https://localhost"))
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();

// Disable automatic 400 response - let controller handle validation
builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("DevCorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();