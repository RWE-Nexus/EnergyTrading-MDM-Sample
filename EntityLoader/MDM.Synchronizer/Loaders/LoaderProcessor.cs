namespace MDM.Sync.Loaders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using EnergyTrading.Logging;

    public class LoaderProcessor
    {
        private readonly object syncLock;
        private readonly Queue<Loader> work;
        private readonly ILogger logger = LoggerFactory.GetLogger(typeof(LoaderProcessor));
        private readonly List<Thread> workers;
        private readonly EventWaitHandle handle;
        private readonly EventWaitHandle exitHandle;
        private readonly WaitHandle[] handles;

        public LoaderProcessor()
        {
            syncLock = new object();

            WorkerCount = 1;
            Running = false;

            workers = new List<Thread>();
            work = new Queue<Loader>();

            handle = new AutoResetEvent(false);
            exitHandle = new ManualResetEvent(false);
            handles = new[] { handle, exitHandle };
        }

        public bool Running { get; private set; }

        public int WorkerCount { get; set; }

        public void Start()
        {
            this.Stop();

            for (var i = 0; i < WorkerCount; i++)
            {
                var worker = new Thread(Work);
                workers.Add(worker);
                worker.Start();
            }

            Running = true;
        }

        public void AddWork(Loader loader)
        {
            lock (syncLock)
            {
                work.Enqueue(loader);
            }
            handle.Set();
        }

        public void Clear()
        {
            lock (syncLock)
            {
                logger.Info("Clearing " + work.Count() + " jobs");
                work.Clear();
                exitHandle.Reset();
            }
        }

        public void Stop()
        {
            Running = false;

            // Tell them to quit
            exitHandle.Set();

            // Wait for all threads to finish their current job
            foreach (var worker in workers)
            {
                worker.Join();
            }

            Clear();
            logger.Info("Processor stopped");
        }

        private void Work()
        {
            var index = 0;
            do
            {
                logger.Info("Checking job count");

                // NB Use if rather than while so we can quit faster under user control
                if (work.Count() > 0)
                {
                    logger.Info(work.Count() + " jobs to process");

                    Loader loader = null;
                    lock (syncLock)
                    {
                        if (work.Count() > 0)
                        {
                            loader = work.Dequeue();
                            logger.Info("Acquiring loader: " + loader.GetType().Name);
                        }
                    }

                    // Check as someone else might have got the work
                    // NB Must be outside lock to get good concurrency.
                    if (loader != null)
                    {
                        loader.Load();
                    }
                }

                // Go back to sleep until signalled, or timeout.
                logger.Info("Worker sleeping");
                index = WaitHandle.WaitAny(handles, 2000);
            } while (index != 1);

            // NB Hangs on this call to logger - prob 'cos the thread is going away
            //logger.Log("Worker quitting");
         }
    }
}