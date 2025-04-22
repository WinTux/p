using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace prueba
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            string llave = "estaEsUnaClave123456ABCxyzEsUnaClaveLargaParaLaPrueba007";
            builder.Services.AddAuthorization();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireScopeMiapiCosas", policy =>
                    policy.RequireClaim("scope", "miapi:cosa"));
            });
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(o => { 
                var llaveLoging = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(llave));
                var signinCredencial = new SigningCredentials(llaveLoging, SecurityAlgorithms.HmacSha256Signature);
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = llaveLoging,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
