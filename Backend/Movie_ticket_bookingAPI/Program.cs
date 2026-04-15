using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Movie_ticket_bookingAPI.Data;
using Movie_ticket_bookingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// ------- EF Core + SQLite -------
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=cinebook.db"));

// ------- Auth Service -------
builder.Services.AddScoped<AuthService>();

// ------- JWT Authentication -------
var jwtKey = builder.Configuration["Jwt:Key"] ?? "CineBookSuperSecretKeyForJWT2024!!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "CineBook",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "CineBookUsers",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// ------- CORS for Angular dev server -------
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CineBook API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] {}
        }
    });
});

var app = builder.Build();

// ------- Migrate and seed DB -------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.EnsureCreated();
        // Check if WalletBalance column exists; if not, drop and recreate
        var conn = db.Database.GetDbConnection();
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM pragma_table_info('Users') WHERE name='WalletBalance'";
        var result = cmd.ExecuteScalar();
        conn.Close();
        if (Convert.ToInt64(result) == 0)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
    catch
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}

// ------- Middleware -------
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CineBook API v1"));

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
