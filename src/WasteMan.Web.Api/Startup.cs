using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WasteMan.SignalR.Hubs;
using WasteMan.Web.Api.Bindings;
using WasteMan.Web.Api.Mappings;
using WasteMan.Web.Api.Registrations;

namespace WasteMan.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            BindingsRegistry binder = new BindingsRegistry(services, Configuration);
            MappingRegistry mapper = new MappingRegistry();

            #region Mapping
            mapper
                .Register(new GarbageBinMapping())
                .Register(new AdjacencyMapping())
                .Register(new RouteMapping())
                .Register(new CoordinateMapping())
                .Register(new ResultMapping())
                .Map();
            #endregion

            #region Binding
            binder
                .RegisterWithConfiguration(new RedisBinding())
                .RegisterWithConfiguration(new MongoDbBinding())
                .RegisterWithConfiguration(new MqttBinding())
                .Register(new SignalRBinding())
                .Register(new AlgorithmBinding())
                .Register(new WebBinding())
                .Register(new StartupBinding())
                .Bind();
            #endregion

            #region MongoDB Serialization
            MongoDBRegistry.Register();
            #endregion

            #region CORS
            services.AddCors(options =>
                options.AddPolicy("CorsPolicy", builder =>
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
            ));
            #endregion

            #region SignalR
            services.AddSignalR();
            #endregion

            #region MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseSignalR(routes => routes.MapHub<GarbageBinHub>("/garbageBinHub"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
