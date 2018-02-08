using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MouseMoveEventService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Dictionary<string, object> config = new Dictionary<string, object>
            {
                {"group.id", "mouse-move-event-service-group"},
                {"bootstrap.servers", "localhost:9092"},
                {"enable.auto.commit", "true"},
                { "client.id", "event-counter" },
            };

            services.AddSingleton<IHostedService, KafkaConsumeService<ServiceHub>>(c =>
                new KafkaConsumeService<ServiceHub>(c.GetRequiredService<IHubContext<ServiceHub>>(), config, Topics.PostMouseMoveEvent));
            services.AddCors();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:54191").AllowAnyHeader().AllowAnyMethod());
            app.UseSignalR(routes =>
            {
                routes.MapHub<ServiceHub>("EventService");
            });
        }
    }
}
