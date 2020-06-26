using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileSystemStorage;
using Gallery.BLL.Services;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Models;
using Gallery.MQ.InterfaceImplementation;
using Gallery.MQ.Interfaces;
using Gallery.MQ.RabbitMQ.Implementation;
using Gallery.Worker.InterfaceImplementation;
using Gallery.Worker.Wrapper;
using Topshelf;

namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SQLDB"] ?? throw new ArgumentException("SQL");
            var rabbitMqConnectionString = ConfigurationManager.ConnectionStrings["RabbitMQ"] ?? throw new ArgumentException("SQL");
            var saveImageWork = new SaveImageWork(new ConsumerRMQ(rabbitMqConnectionString.ConnectionString), 
                new UserService(new UsersRepository(new SqlDbContext(connectionString.ConnectionString))),
                new ImageServices(
                    new MediaProvider(),
                    new MediaRepository(new SqlDbContext(connectionString.ConnectionString)),
                    new UsersRepository(new SqlDbContext(connectionString.ConnectionString))),
                new MediaRepository(
                    new SqlDbContext(connectionString.ConnectionString)));

            var exitCode = HostFactory.Run(x =>
            {
                x.Service<WorkerWrapper>(s =>
                {
                    s.ConstructUsing(workWrapper => new WorkerWrapper(saveImageWork));
                    s.WhenStarted(async st => await st.StartAsync());
                    s.WhenStopped( sp =>  sp.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDisplayName("Worker Wrapper Service");
                x.SetServiceName("WorkerWrapperService");
                x.SetDescription("This service for Gallery Worker.");
            });

            var exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
