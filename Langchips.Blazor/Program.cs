using Langchips.Blazor.AuthProviders;
using Langchips.Blazor.Components;
using Langchips.Data;
using Langchips.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Langchips.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, TestAuthStateProvider>();

            builder.Services.AddSingleton<ConfigurationService>();
            var configService = new ConfigurationService(builder.Configuration);
            var apiBaseUrl = configService.GetApiBaseUrl();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

            builder.Services.AddScoped<ApiService>();


            var app = builder.Build();//TODO: async

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
            //await builder.Build().RunAsync();

        }
    }
}
