using MaziForum.Server.Data;
using MaziForum.Server.Data.Entities;
using MaziForum.Server.Main.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

// ////////////////////////////////////////////////////////////

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ForumDbContext>(options =>
{
   options.UseSqlServer(
      builder.Configuration.GetConnectionString("DefaultConnection"),
      opt => opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
   );
   options.EnableSensitiveDataLogging(true);
});

builder.Services
   .AddIdentity<User, IdentityRole>(opt => opt.User.RequireUniqueEmail = true)
   .AddEntityFrameworkStores<ForumDbContext>();

builder.Services.ConfigureApplicationCookie(
   opt => opt.Events.DisableRedirectionForApiClients()
);

builder.Services.AddCors(
   opt =>
      opt.AddPolicy(
         "CorsPolicy",
         b =>
            b.WithOrigins("http://localhost:5173")
               //.AllowAnyMethod()
               .AllowAnyHeader()
      //.AllowCredentials()
      )
);


// //////////////////////////////////////////////////////////////

var app = builder.Build();

app.UseCors("CorsPolicy");

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
