using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace la_mia_pizzeria_static
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProductManager.Seed();

            var builder = WebApplication.CreateBuilder(args);
                       

            builder.Services.AddRazorPages()
               .AddRazorRuntimeCompilation();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddDbContext<ProductContext>();

                        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ProductContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(

                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
       
    }
}