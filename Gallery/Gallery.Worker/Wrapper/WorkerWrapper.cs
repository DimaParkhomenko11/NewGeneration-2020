using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gallery.Worker.Interfaces;

namespace Gallery.Worker.Wrapper
{
    public class WorkerWrapper
    {
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        private readonly IReadOnlyCollection<IWork> _works;

        public WorkerWrapper(params IWork[] works)
        {
            _works = works ?? throw new ArgumentNullException(nameof(works));
        }

        public async Task StartAsync()
        {
            foreach (var work in _works)
            {
                await Task.Factory.StartNew(work.StartAsync, _cancellationToken.Token, TaskCreationOptions.LongRunning, 
                    TaskScheduler.Current);
            }
        }

        public async Task StopAsync()
        {
            _cancellationToken.Cancel();
        }
    }
}
