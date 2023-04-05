using AutoMapper;
using infnet_bl6_daw_at.MVC.Mapper;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using infnet_bl6_daw_at.Service;
using infnet_bl6_daw_at.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RedeSocial.API.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using infnet_bl6_daw_at.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutorProfile());
    mc.AddProfile(new LivroProfile());
    //mc.AddProfile(new ContaProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

builder.Services.AddDbContext<infnet_bl6_daw_atDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbEditora")));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//options.SignIn.RequireConfirmedAccount = true)
//.AddEntityFrameworkStores<infnet_bl6_daw_atDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;
    o.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<infnet_bl6_daw_atDbContext>();

builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IAutoresRepository, AutoresRepository>();
builder.Services.AddScoped<ILivrosRepository, LivrosRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = "/Usuarios/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Livro}/{action=Index}");

app.Run();
