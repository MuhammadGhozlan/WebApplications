using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TutoringWebApplication.Data;
using TutoringWebApplication.Interfaces;
using TutoringWebApplication.Models;
using TutoringWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//AddDbContext
builder.Services.AddDbContext<DataDbContext>(options=>options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<UserDbContext>();
//Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme=CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme=CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(cfg =>
{
    cfg.Cookie.HttpOnly = true;
    cfg.Cookie.SecurePolicy= CookieSecurePolicy.Always;
    cfg.Cookie.SameSite=SameSiteMode.Strict;
    cfg.Cookie.Name="AuthCookie";
    cfg.ExpireTimeSpan=TimeSpan.FromMinutes(60);
    cfg.LoginPath="/api/account/login";
    cfg.AccessDeniedPath="/api/account/accessdenied";
    cfg.SlidingExpiration=true;
});

//AddCors
builder.Services.AddCors(options => options.AddPolicy("TutoringSite", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
}));
//Service Registration
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Repository Registration
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("TutoringSite");

app.MapControllers();

using(var scoped = app.Services.CreateScope())
{
    var roleManager=scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles=new[] {"Admin","Instructor","Student"};
    foreach(var role in roles)
    {
        if(! await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();

