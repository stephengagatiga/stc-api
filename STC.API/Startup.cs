using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
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

            services.AddScoped<ICategoryData, SqlCategoryData>();
            services.AddScoped<IComponentData, SqlComponentData>();
            services.AddScoped<IComponentTypeData, SqlComponentTypeData>();
            services.AddScoped<IComponentVersionData, SqlComponentVersion>();
            services.AddScoped<IStageData, SqlStageData>();
            services.AddScoped<IOpportunityData, SqlOpportunityData>();

            services.AddScoped<ICashReimbursementData, SqlCashReimbursementData>();
            services.AddScoped<IReimburseeData, SqlReimburseeData>();
            services.AddScoped<IUserPermissionData, SqlPermissionData>();

            services.AddScoped<IRequestData, SqlRequestData>();

            services.AddScoped<IPOPendingData, SqlPOPendingData>();
            services.AddScoped<IPOAuditTrailData, SqlPOAuditTrailData>();
            services.AddScoped<IPOGuidStatusData, SqlPOGuidStatusData>();


            services.AddTransient<IUtils, Utils>();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            //services.AddControllers().AddXmlSerializerFormatters();

            services.AddSingleton(typeof (IInMemoryData), new InMemoryData());

            services.AddCors();
            //config mvc
            services.AddMvc()
                .AddMvcOptions(mvcOptions =>
                {
                    //add formatter for xml
                    mvcOptions.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    //require https only
                    //mvcOptions.Filters.Add(typeof(RequireHttpsAttribute));
                    mvcOptions.FormatterMappings.SetMediaTypeMappingForFormat
                        ("xml", MediaTypeHeaderValue.Parse("application/xml"));
                    mvcOptions.FormatterMappings.SetMediaTypeMappingForFormat
                        ("config", MediaTypeHeaderValue.Parse("application/xml"));
                    mvcOptions.FormatterMappings.SetMediaTypeMappingForFormat
                        ("js", MediaTypeHeaderValue.Parse("application/json"));
                })
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                    .AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
