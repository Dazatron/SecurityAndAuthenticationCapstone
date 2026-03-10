using SecurityAndAuthenticationCapstone.Data;
using SecurityAndAuthenticationCapstone.Services;

namespace SecurityAndAuthenticationCapstone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddRazorPages();

            //builder.Services.AddScoped<UserRepository>(serviceProvider =>
            //{
            //    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            //    var connectionString = configuration.GetConnectionString("DefaultConnection");

            //    return new UserRepository(connectionString);
            //});
            builder.Services.AddScoped<UserRepository>(sp =>
                new UserRepository(connectionString)
            );

            builder.Services.AddSession();

            builder.Services.AddScoped<AuthService>(sp =>
                new AuthService(connectionString)
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();
            app.UseSession();

            app.Run();
        }
    }
}
