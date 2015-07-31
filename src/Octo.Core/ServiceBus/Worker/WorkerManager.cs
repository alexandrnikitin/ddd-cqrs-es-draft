using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Octo.Core.ServiceBus.Worker
{
    public class WorkerManager : IDisposable
    {
        private readonly Func<Worker> _workerFactory;

        private readonly List<Worker> _workers = new List<Worker>();

        public WorkerManager(Func<Worker> workerFactory)
        {
            _workerFactory = workerFactory;
        }

        public void Dispose()
        {
            StopWorkers();
        }

        public void StartWorkers(int numberOfWorkers)
        {
            while (_workers.Count < numberOfWorkers)
            {
                AddWorker();
            }
        }

        public void StopWorkers()
        {
            foreach (var worker in _workers)
            {
                worker.Stop();
            }

            while (!_workers.All(x => x.IsStopped))
            {
                Thread.Sleep(10);
            }
        }

        private void AddWorker()
        {
            var worker = _workerFactory();
            _workers.Add(worker);
            worker.Start();
        }
    }
}