using System;
using System.Threading;
using System.Threading.Tasks;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.MQ.Abstraction;
using Gallery.Worker.Interfaces;
using NLog;

namespace Gallery.Worker.InterfaceImplementation
{
    public class SaveImageWork : IWork
    {
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        private readonly TimeSpan _timeSpan = TimeSpan.FromSeconds(1);
        private readonly ConsumerMQ _consumer;
        private readonly IImagesService _imagesService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public SaveImageWork(ConsumerMQ consumer, IImagesService imagesService)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _imagesService = imagesService ?? throw new ArgumentNullException(nameof(imagesService));
        }

        public async Task StartAsync()
        {
            _logger.Info("Started " + nameof(SaveImageWork) + ".");
            while (!_cancellationToken.IsCancellationRequested)
            {
                var queues = new ParserMQ().ParserMq();
               
                _consumer.ReadMessage<MessageDto>(queues["queue:upload-image"], async dto => await _imagesService.UploadTempToUserDirectory(dto));
               
                _logger.Info("Image uploaded successfully.");
                
                await Task.Delay(_timeSpan);
            }
        }

        public void Stop()
        {
            _cancellationToken.Cancel();
            _logger.Info("Stoped " + nameof(SaveImageWork) + "successfully.");
        }
    }
}
