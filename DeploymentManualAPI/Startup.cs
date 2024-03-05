using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Repository.Entities;
using Microsoft.EntityFrameworkCore;
using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Servicios;
using DeploymentManualAPI.Bussines;
using Serilog;
using Repository.Models;

namespace DeploymentManualAPI
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
            services.AddDbContext<ManualDeploymentContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ManualConnection"));
            });
            services.AddControllers();
            //Inyección de dependencias capa de datos
            services.AddScoped<ISTypology, STypology>();
            services.AddScoped<ISApplicative, SApplicative>();
            services.AddScoped<ISUser, SUser>();
            services.AddScoped<ISResponsibleArea, SResponsibleArea>();
            services.AddScoped<ISEnvironment, SEnvironment>();
            services.AddScoped<ISRequestType, SRequestType>();
            services.AddScoped<ISChange, SChange>();
            services.AddScoped<ISSignature, SSignature>();
            services.AddScoped<ISType, SType>();
            services.AddScoped<ISTraining, STraining>();
            services.AddScoped<ISContact, SContact>();
            services.AddScoped<ISServer, SServer>();
            services.AddScoped<ISPrerequisite, SPrerequisite>();
            services.AddScoped<ISPlan, SPlan>();
            services.AddScoped<ISPostImplantation, SPostImplantation>();
            services.AddScoped<ISFunctionalUser, SFunctionalUser>();
            services.AddScoped<ISRollbackPre, SRollbackPre>();
            services.AddScoped<ISRollbackPlan, SRollbackPlan>();
            services.AddScoped<ISBlueprint, SBlueprint>();
            services.AddScoped<ISStatus, SStatus>();
            services.AddScoped<ISChangeApplicative, SChangeApplicative>();

            //Inyección de dependencias capa de negocios
            services.AddScoped<IApplicativeBll, ApplicativeBll>();
            services.AddScoped<ITypologyBll, TypologyBll>();
            services.AddScoped<IUserBll, UserBll>();
            services.AddScoped<IResponsibleAreaBll, ResponsibleAreaBll>();
            services.AddScoped<IEnvironmentBll, EnvironmentBll>();
            services.AddScoped<IRequestTypeBll, RequestTypeBll>();
            services.AddScoped<IChangeBll, ChangeBll>();
            services.AddScoped<ISignatureBll, SignatureBll>();
            services.AddScoped<ITypeBll, TypeBll>();
            services.AddScoped<ITrainingBll, TrainingBll>();
            services.AddScoped<IContactBll, ContactBll>();
            services.AddScoped<IServerBll, ServerBll>();
            services.AddScoped<IPrerequisiteBll, PrerequisiteBll>();
            services.AddScoped<IPlanBll, PlanBll>();
            services.AddScoped<IPostImplantationBll, PostImplantationBll>();
            services.AddScoped<IFunctionalUserBll, FunctionalUserBll>();
            services.AddScoped<IRollbackPreBll, RollbackPreBll>();
            services.AddScoped<IRollbackPlanBll, RollbackPlanBll>();
            services.AddScoped<IBlueprintBll, BlueprintBll>();
            services.AddScoped<IChangeApplicativeBll, ChangeApplicativeBll>();


            services.AddScoped<ChangeObj>();
            services.AddScoped<FilterObj>();


            services.AddTransient<MessageResponse>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Documentacion
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeploymentManualAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeploymentManualAPI v1"));
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
