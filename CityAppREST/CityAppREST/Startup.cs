using System;
using CityAppREST.Data;
using CityAppREST.Data.Repositories;
using CityAppREST.Helpers;
using CityAppREST.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CityAppREST
{
    public class Startup
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly byte[] _key;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenGenerator = new TokenGenerator();
            _key = _tokenGenerator.Key;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("CityAppDB"));

            // Add repository services
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Company>, CompanyRepository>();

            // Add jwt token authentication
            services.AddSingleton<TokenGenerator>(_tokenGenerator);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,

                };
            });

            // Add data initializer
            services.AddTransient<CityAppDataInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CityAppDataInitializer cityAppDataInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            cityAppDataInitializer.InitializeData();
        }
    }
}
