namespace MDM.Loader
{
    public interface IMDMDataLoaderService
    {
        void Load(string entityName, string xmlFilePath, bool candidateData, int workerCount = 1, bool canStopLoadProcessorOnLoadComplete = false);
    }
}