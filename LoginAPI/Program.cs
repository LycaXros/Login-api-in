using LoginAPI.Data;
using LoginAPI.Service;
using LoginAPI.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<LoginContext>(opt =>
    opt.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!)
);

builder.Services.Configure<LoginOptions>(
    builder.Configuration.GetSection(LoginOptions.OptionRoute));


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(TokenGenerator.KEY),

            ValidIssuer = "http://localhost:5222/",
            ValidAudience = "http://localhost:5222/",
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
        };
    });

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddSingleton<TokenGenerator>();



var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(cfg =>
    {
        cfg.SwaggerEndpoint("/openapi/v1.json", "Login API");
    });
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.RequireAuthorization()
.WithName("GetWeatherForecast");

app.MapPost("/login", async (ILoginService service, [FromBody]Credentials cred) =>
{


    try
    {
        var r = await service.Login(cred.email, cred.password);
        return Results.Ok(r);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Mensaje = ex.Message });
    }

})
.WithName("Login Endpoint");

app.MapPost("/register", async (ILoginService service, [FromBody] LoginRegisterData loginData) =>
{

    try
    {
        var r = await service.Register(loginData);
        return Results.Ok(r);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Mensaje = ex.Message });
    }
})
.WithName("Register Endpoint");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record Credentials(string email, string password)  ;