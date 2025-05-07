using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

app.UseTus(_ => new DefaultTusConfiguration
{
    Store = new TusDiskStore(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads")),
    UrlPath = "/tusfiles",
    Events = new Events
    {
        OnBeforeCreateAsync = ctx =>
        {
            var metadata = ctx.Metadata;
            if (!metadata.ContainsKey("filetype") ||
                !(metadata["filetype"].GetString(Encoding.UTF8) == "image/png" ||
                  metadata["filetype"].GetString(Encoding.UTF8) == "image/jpeg"))
            {
                ctx.FailRequest("Only PNG or JPEG allowed.");
            }
            return Task.CompletedTask;
        }
    }
});

app.MapDefaultControllerRoute();
app.Run();
