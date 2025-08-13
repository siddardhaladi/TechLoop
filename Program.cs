using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechLoop.Authenticates;
using TechLoop.Data;
using TechLoop.Repository;
using TechLoop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddScoped(x => new Authentication(builder.Configuration["JWT:Key"]));
var keyValue = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(keyValue))
    throw new InvalidOperationException("JWT key is missing from configuration!");

var key = Encoding.UTF8.GetBytes(keyValue);


//builder.Services.AddScoped<IAuthentication>(provider =>
//{
//    var context = provider.GetRequiredService<TechLoopDbContext>();
//    var key = builder.Configuration["JWT:Key"];
//    return new Authentication(context, key);
//});
builder.Services.AddDbContext<TechLoopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();



builder.Services.AddScoped<IAuthentication>(provider =>
{
    var context = provider.GetRequiredService<TechLoopDbContext>();
    var config = provider.GetRequiredService<IConfiguration>();
    var jwtKey = config["Jwt:Key"];

    if (string.IsNullOrEmpty(jwtKey))
        throw new InvalidOperationException("JWT key is missing");

    return new Authentication(context, jwtKey);
});



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TechLoopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwt = builder.Configuration.GetSection("JWT");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt["Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
