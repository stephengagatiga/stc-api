using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STC.API.Data;
using STC.API.Services;


namespace STC.API
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = _configuration["AzureAd:ClientId"];
                options.Authority = _configuration["AzureAd:Authority"];
            });

            services.AddDbContext<STCDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ClientUserDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("ClientUserConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ClientUserDbContext>();

            services.AddScoped<IUserData, SqlUserData>();
            services.AddScoped<IGroupData, SqlGroupData>();
            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IProductAssignmentData, SqlProductAssignmentData>();
            services.AddScoped<IAccountData, SqlAccountData>();
            services.AddScoped<ITicketData, SqlTicketData>();
            services.AddScoped<IUserRoleData, SqlUserRoleData>();
            services.AddTransient<IUtils, Utils>();

            //config mvc
            services.AddMvc()
                .AddMvcOptions(mvcOptions =>
                {
                    //add formatter for xml
                    mvcOptions.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    //require https only
                    mvcOptions.Filters.Add(typeof(RequireHttpsAttribute));
                })
                .AddJsonOptions(jsonOptions =>
                {
                    //I forgout about this
                    jsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
