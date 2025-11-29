using FleetManagement.EF;
using FleetManagement.EF.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

#region Db Init

var conn = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContextFactory<ReferralDbContext>(o =>
    o.UseNpgsql(conn)); 

#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FleetManagement API",
        Version = "v1"
    });

    // Define the Bearer scheme
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header. Example: Bearer {token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    // Register the scheme under the name "Bearer"
    c.AddSecurityDefinition("Bearer", securityScheme);

    // Require Bearer token for all operations (you can customize later)
    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    };

    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.Configure<DocumentService>(
    builder.Configuration.GetSection("FileStorage"));

builder.Services.AddScoped<DocumentService>();

builder.Services.AddDbContextFactory<ReferralDbContext>(opts => { });

builder.Services.AddAuthorization();

#region Authanticator

var jwt = builder.Configuration.GetRequiredSection("Jwt");
var keyBytes = Convert.FromBase64String(jwt["Key"]!);  
var signingKey = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateIssuer = true,
            ValidIssuer = jwt["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwt["Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

#endregion

var app = builder.Build();

#region Db Setup

ReferralDbContext.ConfigureFactory(app.Services.GetRequiredService<IDbContextFactory<ReferralDbContext>>());

#endregion

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
