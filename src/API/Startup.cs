using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(ILogger logger)
        {
            _logger = logger;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                var ips = Dns.GetHostAddresses("nginx");

                _logger.LogInformation($"ips.Count = ", ips.Length);
                foreach (var ip in ips)
                {
                    _logger.LogInformation(ip.ToString());
                    options.KnownProxies.Add(ip);
                }

                //options.KnownNetworks.Clear();
                //var localNetwork = new IPNetwork(IPAddress.Parse("localhost"), 24);
                //options.KnownNetworks.Add(localNetwork);
                //options.KnownProxies.Clear();
                //options.KnownProxies.Add(Ip);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod());

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}