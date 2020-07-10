using System;
using System.Web.Http;
using Gallery.ASQ.Implementation;
using Gallery.Configurations.Management;
using Gallery.MQ.Abstraction;
using Gallery.MSMQ;
using Gallery.MSMQ.Implementation;
using Gallery.RMQ;
using Gallery.RMQ.Implementation;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Gallery.App_Start.Startup))]

namespace Gallery.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
                SlidingExpiration = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(60)
            });
            DIConfig.Configure(new HttpConfiguration());
            var parserQueue = new ParserMQ().ParserMq();
            var rabbitConnectionString = ConfigurationManagement.RabbitMqConnectionString();
            var azureConnectionString = ConfigurationManagement.AzureMqConnectionString();
            new InitializerMSMQ().Initializer(parserQueue);
            new InitializerRMQ(rabbitConnectionString).Initializer(parserQueue);
            new InitializerASQ(azureConnectionString).Initializer(parserQueue);
        }
    }
}
