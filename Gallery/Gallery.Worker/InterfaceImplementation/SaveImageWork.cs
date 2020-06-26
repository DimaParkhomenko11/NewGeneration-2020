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
        private readonly IUsersService _usersService;
        private readonly IImagesService _imagesService;
        private readonly IMediaRepository _mediaRepository;

        public SaveImageWork(IConsumerMQ consumer, IUsersService usersService, IImagesService imagesService, IMediaRepository mediaRepository)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            _imagesService = imagesService ?? throw new ArgumentNullException(nameof(imagesService));
            _mediaRepository = mediaRepository ?? throw new ArgumentNullException(nameof(mediaRepository));
        }
        
        public async Task StartAsync()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var queues = new ParserRMQ().ParserMSMQ();
                
                var messages = _consumer.ReadMessage<MessageDto>(queues[0]);
                
                if (messages == null)
                    return;
                var collection = (IList<MessageDto>)messages;
                foreach (var message in collection)
                {
                    var isMediaUploadAttemptExist = await _mediaRepository.IsTempMediaExistAsync(message.UniqueName);
                    if (isMediaUploadAttemptExist)
                    {
                        var tempMedia =
                            await _mediaRepository.GetTempMediaByLabelAndProgressLoadingAsync(message.UniqueName, true);
                        var newTempMedia = tempMedia;
                        newTempMedia.InDuringLoading = false;
                        newTempMedia.IsSuccess = true;
                        await _mediaRepository.UpdateTemporaryMediaAsync(tempMedia, newTempMedia);
                        var userDto = await _usersService.GetUserByIdAsync(tempMedia.UserId);
                        var file = _imagesService.ReadFile(message.TempPath);
                        await _imagesService.UploadImageAsync(file, tempMedia.UserPathImages, userDto);
                    }
                }
                
                await Task.Delay(_timeSpan);
            }
        }

        public void Stop()
        {
            _cancellationToken.Cancel();
        }
    }
}
