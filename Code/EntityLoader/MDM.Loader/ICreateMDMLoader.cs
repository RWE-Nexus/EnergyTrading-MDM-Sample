namespace MDM.Loader
{
    using MDM.Sync.Loaders;

    public interface ICreateMDMLoader
    {
        Loader Create(string entityName, string entitiesXmlfileName, bool candidateData);
    }
}