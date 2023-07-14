using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using CodeCrateData;
using BlazorBootstrap;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddScoped<PasswordLogService>();
builder.Services.AddSingleton<CodeCrateDataCsv>();
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddScoped<PassLogValidator>();
builder.Services.AddScoped<ActiveLogService>();
builder.Services.AddScoped<Cipher>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
