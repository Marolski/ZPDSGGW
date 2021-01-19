using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger;
using System;
using ZPDSGGW.Commands;
using ZPDSGGW.Database;
using ZPDSGGW.Repository;

namespace ZPDSGGW
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ZPDSGGWContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ZPDSGGWConnection")));
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
            services.AddScoped<IRepositoryPromoter, PromoterCommands>();
            services.AddScoped<IRepositoryStudent, StudentCommands>();
            //swagger
            services.AddSwaggerGen();
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZPDSGGW");
            });
        }
    }
}
