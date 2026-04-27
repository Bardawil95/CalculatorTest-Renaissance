using Calculator.Core;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<ISimpleCalculator, SimpleCalculator>();

// CORS for local development
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
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