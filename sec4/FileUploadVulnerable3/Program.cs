using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tusdotnet;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;
using System.Text;
using MimeDetective;
using MimeDetective.Engine;
using System.IO;
using System.Linq;
using System;
using tusdotnet.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Ensure the uploads folder exists
var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
Directory.CreateDirectory(uploadsRoot);

app.UseStaticFiles();
app.UseRouting();

app.UseTus(_ => new DefaultTusConfiguration
{
    Store = new TusDiskStore(uploadsRoot),
    UrlPath = "/tusfiles",
    Events = new Events
    {
        OnBeforeCreateAsync = ctx =>
        {
            // Pre‑flight: only allow image/png or image/jpeg
            var md = ctx.Metadata;
            var ft = md.ContainsKey("filetype")
                   ? md["filetype"].GetString(Encoding.UTF8)
                   : "";
            if (ft != "image/png" && ft != "image/jpeg")
            {
                ctx.FailRequest("Only PNG or JPEG allowed.");
            }
            return Task.CompletedTask;
        },

        OnFileCompleteAsync = async ctx =>
        {
            try
            {
                // 1) Grab the completed upload as a stream
                var tusFile = await ctx.GetFileAsync();
                var ct = ctx.CancellationToken;

                // 2) Read out the original filename from metadata
                var meta = await tusFile.GetMetadataAsync(ct);
                meta.TryGetValue("filename", out var fnMeta);
                var rawName = fnMeta?.GetString(Encoding.UTF8) ?? "";
                Console.WriteLine($"[Tus] metadata filename = '{rawName}'");

                // 3) Determine extension: use metadata first
                var ext = Path.GetExtension(rawName);
                // 4) If metadata gave no extension, detect by content
                if (string.IsNullOrEmpty(ext))
                {
                    await using var contentStream = await tusFile.GetContentAsync(ct);
                    contentStream.Seek(0, SeekOrigin.Begin);
                    var insp = new ContentInspectorBuilder().Build();
                    var match = insp.Inspect(contentStream).ByMimeType().FirstOrDefault();
                    if (match != null)
                    {
                        ext = match.MimeType switch
                        {
                            "image/png" => ".png",
                            "image/jpeg" => ".jpg",
                            _ => ""
                        };
                        Console.WriteLine($"[Tus] detected MIME = {match.MimeType}, using ext = '{ext}'");
                    }
                }

                if (string.IsNullOrEmpty(ext))
                {
                    Console.WriteLine("[Tus] no extension could be determined; skipping save.");
                    return;
                }

                // 5) Write out the final file with GUID + extension
                var finalName = $"{ctx.FileId}{ext}";
                var finalPath = Path.Combine(uploadsRoot, finalName);
                await using var inStream = await tusFile.GetContentAsync(ct);
                await using var outStream = new FileStream(finalPath, FileMode.Create, FileAccess.Write);
                inStream.Seek(0, SeekOrigin.Begin);
                await inStream.CopyToAsync(outStream, ct);

                Console.WriteLine($"[Tus] saved: {finalName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Tus] OnFileCompleteAsync error: {ex}");
            }
        }
    }
});

app.MapDefaultControllerRoute();
app.Run();
