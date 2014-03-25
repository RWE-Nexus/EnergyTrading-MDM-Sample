namespace MDM.Sync.Loaders
{
    public class DataCache
    {
        public DataCache(LoaderProcessor processor)
        {
            Processor = processor;
        }

        /// <summary>
        /// Do we create Trayport Mappings
        /// </summary>
        public bool CreateTrayportMappings { get; set; }

        /// <summary>
        /// Do we process sequence items
        /// </summary>
        public bool LoadItems { get; set; }

        public LoaderProcessor Processor { get; set; }
    }
}