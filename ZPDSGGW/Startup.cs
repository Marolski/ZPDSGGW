using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger;
using System;
using System.Text;
using ZPDSGGW.Authentication;
using ZPDSGGW.Commands;
using ZPDSGGW.Database;
using ZPDSGGW.Repository;
using System.Linq;
using ZPDSGGW.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreRateLimit;

namespace ZPDSGGW
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        string allowSpecificOrigins = "_allowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion
            (CompatibilityVersion.Version_2_2);
            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>
            (Configuration.GetSection("IpRateLimit"));
            services.AddSingleton<IIpPolicyStore,
            MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore,
            MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration,
            RateLimitConfiguration>();
            services.AddHttpContextAccessor();


            services.AddDbContext<ZPDSGGWContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ZPDSGGWConnection")));
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: allowSpecificOrigins, builder =>
                 {
                     builder.WithOrigins("https://zdpsggw.000webhostapp.com", "http://localhost:8080")
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                 });
            });
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IRepositoryProposals, ProposalCommands>();
            services.AddScoped<IRepositoryInvitationPromoter, InvitationPromoterCommands>();
            services.AddScoped<IRepositoryFile, FileCommands>();
            services.AddScoped<IRepositoryThesisTopic, ThesisTopicCommands>();
            services.AddScoped<IRepositoryUser, UserCommands>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IRepositoryMessage, MessageCommands>();

            // configure strongly typed settings objects
            var keySection = Configuration.GetSection("Key");
            services.Configure<Key>(keySection);

            // configure jwt authentication
            var getKey = keySection.Get<Key>();
            var key = Encoding.ASCII.GetBytes(getKey.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = opt =>
                    {
                        var appIdentity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                        opt.Principal.AddIdentity(appIdentity);
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseIpRateLimiting();
            app.UseRouting();

            app.UseCors(allowSpecificOrigins);

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            IdentityModelEventSource.ShowPII = true;

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var context = serviceScope.ServiceProvider.GetRequiredService<ZPDSGGWContext>();
                DbInitializer.Initialize(context, services);
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZPDSGGW");
            });
        }
    }
}
