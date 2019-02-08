using BlueDiscosOnline.API.DependencyInjection;
using BlueDiscosOnline.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlueDiscosOnline.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }       

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpClient();
            services.AddSingleton(Configuration);
          
            var connection = Configuration["ConnectionStrings:SqliteConnectionString"];
            services.AddDbContext<BlueDiscosOnlineContext>(options => options.UseSqlite(connection, b => b.MigrationsAssembly("BlueDiscosOnline.API")));

            //Polly            
            var registry = services.AddPolicyRegistry();

            var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));
            registry.Add("HttWaitAndpRetryPolicy", retryPolicy);

            var noPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
            registry.Add("NoOpPolicy", noPolicy);

            services.AddHttpClient("BlueDiscosOnlineClient", client =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddMvcCore().AddDataAnnotationsLocalization();

            //Dependency Injection
            DependencyResolver.Resolve(services);

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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
