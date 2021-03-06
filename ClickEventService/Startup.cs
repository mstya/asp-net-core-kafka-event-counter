﻿using System.Collections.Generic;
using KafkaLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClickEventService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Dictionary<string, object> config = new Dictionary<string, object>
            {
                {"group.id", "click-event-service-group"},
                {"bootstrap.servers", "localhost:9092"},
                {"enable.auto.commit", "true"},
                { "client.id", "event-counter" },
            };

            services.AddSingleton<IHostedService, KafkaConsumeService<ServiceHub>>(c => 
                new KafkaConsumeService<ServiceHub>(c.GetRequiredService<IHubContext<ServiceHub>>(), config, Topics.PostClickEventTopic));
            services.AddCors();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
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