using Application.Interfaces;
using Application.Services;
using Domain.Model.Interface;
using Infra.DataAccess.Datas;
using Infra.DataAccess.Repositories;
using Infra.ExternalServices.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IConnectionFactory, ConnectionFactory>();
builder.Services.AddHttpClient<ILibreHardwareMonitorApi, LibreHardwareMonitorApi>();
builder.Services.AddScoped<ILibreHardwareMonitorService, LibreHardwareMonitorService>();
builder.Services.AddScoped<ISensoresRepository, SensoresRepository>();
builder.Services.AddScoped<ISensoresService, SensoresService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
