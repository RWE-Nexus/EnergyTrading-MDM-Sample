namespace MDM.Loader
{
    using System;
    using System.Threading;

    using EnergyTrading.Logging;

    using MDM.Sync.Loaders;

    public class MDMDataLoaderService : IMDMDataLoaderService
    {
        private readonly ILogger logger = LoggerFactory.GetLogger(typeof(MDMDataLoaderService));

        private readonly ICreateMDMLoader mdmLoaderFactory;

        private readonly LoaderProcessor processor;

        public MDMDataLoaderService(ICreateMDMLoader mdmLoaderFactory)
        {
            this.mdmLoaderFactory = mdmLoaderFactory;
            this.processor = new LoaderProcessor { WorkerCount = 1 };
        }

        public void Load(
            string entityName, 
            string xmlFilePath, 
            bool candidateData, 
            int workersCount = 1, 
            bool canStopLoadProcessorOnLoadComplete = false)
        {
            var loader = this.mdmLoaderFactory.Create(entityName, xmlFilePath, candidateData);

            if (loader == null)
            {
                this.logger.ErrorFormat(
                    "Unable to create the MDM loader for the entity: {0}. Please check the entity name and file path.", 
                    entityName);
            }
            else
            {
                // Remove the handler first, in case if someone is aleady added this... If nothing is added, it will ignore.
                loader.LoadCompleted -= this.OnLoadCompleted;

                if (canStopLoadProcessorOnLoadComplete)
                {
                    loader.LoadCompleted += this.OnLoadCompleted;
                }

                this.AddWork(loader, workersCount);
            }
        }

        public void Stop()
        {
            try
            {
                if (this.processor.Running)
                {
                    // Put this on a separate thread so that the UI continues to respond.
                    var worker = new Thread(this.processor.Stop);
                    worker.Start();
                }
            }
            catch (Exception exception)
            {
                this.logger.ErrorFormat(
                    "Exception occurred whilst stopping the current load process: {0}. {1}.", 
                    exception.Message, 
                    exception.InnerException);
            }
        }

        private void AddWork(Loader loader, int workers)
        {
            if (!this.processor.Running)
            {
                this.processor.WorkerCount = workers;
                this.processor.Start();
            }

            this.processor.AddWork(loader);
        }

        private void OnLoadCompleted(object sender, EventArgs e)
        {
            this.Stop();
        }
    }
}