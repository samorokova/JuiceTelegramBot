using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuiceTelegramBot.Core.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace JuiceTelegramBot
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IJuiceRepository, InFileJuiceRepository>();
            services.AddSingleton<IOrderRepository, InFileOrderRepository>();
            services.AddScoped<ITelegramBotClient, TelegramBotClient>(srvs => new TelegramBotClient("695500475:AAE2Yur7FVFxm0bjQsz6yZZg5rNNUwzi_m4"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

    }
}
