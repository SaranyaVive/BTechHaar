using BTechHaar.Data.Context;
using BTechHaar.Data.Repository;
using BTechHaar.Main.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("BTechHaarDB");
//builder.Services.AddMvc();
builder.Services.AddDbContext<BTechDBContext>(item => item.UseSqlServer(connectionString), ServiceLifetime.Transient);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


/* Dependancy Resolved Here */

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserLogService, UserLogService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserLogRepository, UserLogRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbCore = scope.ServiceProvider.GetRequiredService<BTechDBContext>();
    dbCore.Database.Migrate();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages();

app.Run();
