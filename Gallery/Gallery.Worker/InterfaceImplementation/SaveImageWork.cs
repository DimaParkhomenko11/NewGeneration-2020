﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileSystemStorage;
using Gallery.BLL.Interfaces;
using Gallery.DAL.Interfaces;
using Gallery.MQ.Interfaces;
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
                var queuePath = ConfigurationManager.AppSettings["MessageQueuingPath"] ?? @".\private$\GalleryMQ";
                var message = _consumer.ReadMessage(queuePath);
                if (message == null)
                    return;
                var fileBody = (byte[])message.Body;
                var name = message.Label;
                var isMediaUploadAttemptExist = await _mediaRepository.IsTempMediaExistAsync(message.Label);
                if (isMediaUploadAttemptExist)
                {
                    var tempMedia = await _mediaRepository.GetTempMediaByLabelAndProgressLoadingAsync(message.Label, true);
                    var newTempMedia = tempMedia;
                    newTempMedia.InDuringLoading = false;
                    newTempMedia.IsSuccess = true;
                    await _mediaRepository.UpdateTemporaryMediaAsync(tempMedia, newTempMedia);
                    var userDto = await _usersService.GetUserByIdAsync(tempMedia.UserId);
                    await _imagesService.UploadImageAsync(fileBody, tempMedia.UserPathImages, userDto);
                }
                await Task.Delay(_timeSpan);
            }
        }

        public async Task StopAsync()
        {
            _cancellationToken.Cancel();
        }
    }
}