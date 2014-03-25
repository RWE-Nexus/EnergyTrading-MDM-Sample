namespace MDM.Sync.Loaders
{
    public class DataCache
    {
        public DataCache(LoaderProcessor processor)
        {
            Processor = processor;
        }
        
        public LoaderProcessor Processor { get; set; }

        /// <summary>
        /// Do we process sequence items
        /// </summary>
        public bool LoadItems { get; set; }

        /// <summary>
        /// Do we create Trayport Mappings
        /// </summary>
        public bool CreateTrayportMappings { get; set; }
    }
}