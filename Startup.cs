using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;

namespace UseCase14
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllersWithViews();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en-US", "fr-FR", "uk-UA", "de-DE" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Adding our UrlRequestCultureProvider as first object in the list
            var localizationOptions = app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            localizationOptions.Value.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider
            {
                Options = localizationOptions.Value
            });

            app.UseRequestLocalization(localizationOptions.Value);

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Test/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "LocalizedDefault",
                        pattern: "{culture}/{controller=Test}/{action=Index}"
                );

                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{*catchall}",
                      defaults: new { culture = localizationOptions.Value.DefaultRequestCulture.UICulture.Name, controller = "Test", action = "Index" });
            });
        }
    }
}
