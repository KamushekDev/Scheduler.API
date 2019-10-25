using System;
using System.IO;
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            var logger = new StreamWriter(File.Create("log.txt"));

            logger.WriteLine("Hello there!");

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                logger.WriteLine("start search");
                logger.Flush();
                options.ForwardedHeaders = ForwardedHeaders.All;
                var ips = Dns.GetHostAddresses("nginx");
                logger.WriteLine("searched");
                logger.Flush();


                logger.WriteLine($"ips.Count = {ips.Length}");
                logger.Flush();
                foreach (var ip in ips)
                {
                    logger.WriteLine(ip.ToString());
                    logger.Flush();
                    options.KnownProxies.Add(ip);
                }

                //options.KnownNetworks.Clear();
                //var localNetwork = new IPNetwork(IPAddress.Parse("localhost"), 24);
                //options.KnownNetworks.Add(localNetwork);
                //options.KnownProxies.Clear();
                //options.KnownProxies.Add(Ip);
            });
            logger.Close();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod());

            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}