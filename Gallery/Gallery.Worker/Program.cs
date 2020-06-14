using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileSystemStorage;
using Gallery.BLL.Services;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Models;
using Gallery.MQ.InterfaceImplementation;
using Gallery.MQ.Interfaces;
using Gallery.Worker.InterfaceImplementation;
using Gallery.Worker.Wrapper;

namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var connectionString = ConfigurationManager.ConnectionStrings["SQLDB"] ?? throw new ArgumentException("SQL");
              var saveImageWork = new SaveImageWork(
            new ConsumerMQ(),
            new UserService(new UsersRepository(new SqlDbContext(connectionString.ConnectionString))),
            new ImageServices(
                new MediaProvider(),
                new MediaRepository(new SqlDbContext(connectionString.ConnectionString)),
                new UsersRepository(new SqlDbContext(connectionString.ConnectionString))),
            new MediaRepository(
                new SqlDbContext(connectionString.ConnectionString)));


            var wrapper = new WorkerWrapper(saveImageWork);
                await wrapper.StartAsync();
                Console.ReadKey();
                await wrapper.StopAsync();

        }
    }
}
