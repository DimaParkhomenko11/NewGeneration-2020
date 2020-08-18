using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using FileSystemStorage.Implementation;
using Gallery.ASQ.Implementation;
using Gallery.BLL.Services;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Models;
using Gallery.Worker.InterfaceImplementation;
using Gallery.Worker.Wrapper;
using Topshelf;
using System.IO.Abstractions;

namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SQLDB"] ?? throw new ArgumentException("SQL");
            var rabbitMqConnectionString = ConfigurationManager.ConnectionStrings["RabbitMQ"] ?? throw new ArgumentException("RabbitMQ");
            var azureConnectionString = ConfigurationManager.ConnectionStrings["AzureStorageConnectionString"] ?? throw new ArgumentException("AzureStorageConnectionString");
            
            var saveImageWork = new SaveImageWork(
                new ConsumerASQ(azureConnectionString.ConnectionString), 
                new ImageServices(
                    new MediaProvider(new FileSystem().File),
                    new MediaRepository(new SqlDbContext(connectionString.ConnectionString)),
                    new UsersRepository(new SqlDbContext(connectionString.ConnectionString))));

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
