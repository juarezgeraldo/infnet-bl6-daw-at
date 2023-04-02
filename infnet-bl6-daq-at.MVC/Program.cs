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
using AT.Data.Repositories;

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
builder.Services.AddIdentity<Usuario, IdentityRole>(o =>
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
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// configure strongly typed settings objects
//var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");
//builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);
//var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();
//var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidIssuer = jwtBearerTokenSettings.Issuer,
//        ValidateAudience = true,
//        ValidAudience = jwtBearerTokenSettings.Audience,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero
//    };
//});


//builder.Services.AddIdentity<Usuario, IdentityRole>(o =>
//{
//    o.Password.RequireDigit = false;
//    o.Password.RequireLowercase = false;
//    o.Password.RequireNonAlphanumeric = false;
//    o.Password.RequireUppercase = false;
//    o.User.RequireUniqueEmail = false;
//})
//    .AddEntityFrameworkStores<infnet_bl6_daw_atDbContext>()
//    .AddDefaultTokenProviders();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
