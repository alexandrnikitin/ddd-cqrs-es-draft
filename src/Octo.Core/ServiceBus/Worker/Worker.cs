using System;
using System.Threading;

namespace Octo.Core.ServiceBus.Worker
{
    public class Worker
    {
        private readonly IServiceBus _serviceBus;

        private readonly Thread _workerThread;

        private volatile bool _shouldExit;
        private volatile bool _shouldWork;

        public Worker(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;

            _workerThread = new Thread(MainLoop);
            _workerThread.Start();
        }

        public bool IsStopped { get; private set; }

        public void Start()
        {
            _shouldWork = true;
        }

        public void Stop()
        {
            if (_shouldExit) return;

            _shouldWork = false;
            _shouldExit = true;
        }

        private void MainLoop()
        {
            while (!_shouldExit)
            {
                if (!_shouldWork)
                {
                    Thread.Sleep(100);
                    continue;
                }

                TryReceiveMessage();
            }

            IsStopped = true;
        }

        private void TryReceiveMessage()
        {
            try
            {
                _serviceBus.Receive();
            }
            catch (Exception exception)
            {
                //_logger.Error("Error receiving message");
                //_logger.Error(exception);
            }
        }
    }
}