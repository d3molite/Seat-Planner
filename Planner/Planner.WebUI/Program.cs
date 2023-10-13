using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Planner.Data.Interfaces;
using Planner.Data.Services;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Trace().WriteTo.Console().CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices();
builder.Services.AddScoped<ISeatingConfigurationService, SeatingConfigurationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");

	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

# if DEBUG
app.UseHttpsRedirection();
# endif

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();