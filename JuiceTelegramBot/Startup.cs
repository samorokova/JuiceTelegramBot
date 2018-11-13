using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuiceTelegramBot.Core.Model;
using JuiceTelegramBot.Core.Repository;
using JuiceTelegramBot.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var connectionString = Configuration.GetValue<string>("JuiceTelegramBotConnectionString");
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(connectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IJuiceRepository, InDbJuiceRepository>();
            services.AddScoped<IOrderRepository, InDbOrderRepository>();

            services.AddScoped<IJuiceService, JuiceService>();
            services.AddScoped<IOrderService, OrderService>();

            var token = Configuration.GetValue<string>("JuiceTelegramBotToken");
            services.AddScoped<ITelegramBotClient, TelegramBotClient>(srvs => new TelegramBotClient(token));
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
