using Microsoft.AspNetCore.StaticFiles;

using Vira.App.Core.Interfaces;
using Vira.App.Core.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();

        builder.Services.AddSingleton<ISystemFilesService, SystemFilesOSService>();
        builder.Services.AddSingleton<IOSFileSystemService, OSFileSystemService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        var provider = new FileExtensionContentTypeProvider();

        var dict = new Dictionary<string, string>
    {
        {".pdb" , "application/octet-stream" },
        {".blat", "application/octet-stream" },
        {".dll" , "application/octet-stream" },
        {".dat" , "application/octet-stream" },
        {".json", "application/json" },
        {".wasm", "application/wasm" },
        {".symbols", "application/octet-stream" }
    };

        foreach (var kvp in dict)
        {
            provider.Mappings[kvp.Key] = kvp.Value;
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            ContentTypeProvider = provider
        });

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors("AllowAll");

        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();

        app.Run();
    }
}