namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data;

    using DateRange = EnergyTrading.DateRange;

    /// <summary>
    /// Maps a <see cref="MDM.SourceSystem" /> to a <see cref="Broker" />
    /// </summary>
    public class BrokerMapper :
        ContractMapper<Sample.Broker, Broker, Sample.BrokerDetails, BrokerDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public BrokerMapper(IMappingEngine mappingEngine, IRepository repository)
            : base(mappingEngine)
        {
            this.repository = repository;
        }

        public override void Map(Sample.Broker source, Broker destination)
        {
            base.Map(source, destination);
            destination.PartyRoleType = "Broker";
            destination.Party = this.repository.FindEntityByMapping<Party, PartyMapping>(source.Party);
        }

        protected override Sample.BrokerDetails ContractDetails(Sample.Broker contract)
        {
            return contract.Details;
        }

        protected override DateRange ContractDetailsValidity(Sample.Broker contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<MdmId> Identifiers(Sample.Broker contract)
        {
            return contract.Identifiers;
        }
    }
}