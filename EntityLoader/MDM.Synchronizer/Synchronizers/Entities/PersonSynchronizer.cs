namespace MDM.Sync.Synchronizers.Entities
{
    using EnergyTrading.Mdm.Client.Services;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    public class PersonSynchronizer : MdmEntitySynchronizer<Person>
    {
        public PersonSynchronizer(IMdmService mdmService) : base(mdmService)
        {
        }
    }
}