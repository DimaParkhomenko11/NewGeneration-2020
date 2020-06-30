using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileSystemStorage;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.DAL.Interfaces;
using Gallery.MQ.InterfaceImplementation;
using Gallery.MQ.Interfaces;
using Gallery.MQ.RabbitMQ.Implementation;
using Gallery.Worker.Interfaces;

namespace Gallery.Worker.InterfaceImplementation
{
    public class SaveImageWork : IWork
    {
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        private readonly TimeSpan _timeSpan = TimeSpan.FromSeconds(1);
        private readonly IConsumerMQ _consumer;
        private readonly IImagesService _imagesService;
        

        public SaveImageWork(IConsumerMQ consumer, IImagesService imagesService)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _imagesService = imagesService ?? throw new ArgumentNullException(nameof(imagesService));
        }

        public async Task StartAsync()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var queues = new ParserRMQ().ParserMQ();

                var message = _consumer.ReadMessage<MessageDto>(queues[0]);

                if (message == null)
                    return;

                await _imagesService.UploadTempToUserDirectory(message);

                await Task.Delay(_timeSpan);
            }
        }

        public void Stop()
        {
            _cancellationToken.Cancel();
        }
    }
}
