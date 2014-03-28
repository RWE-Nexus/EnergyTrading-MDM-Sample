namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data;

    using DateRange = EnergyTrading.DateRange;

    /// <summary>
    /// Maps a <see cref="MDM.SourceSystem" /> to a <see cref="Counterparty" />
    /// </summary>
    public class CounterpartyMapper :
        ContractMapper
            <Sample.Counterparty, Counterparty, Sample.CounterpartyDetails, CounterpartyDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public CounterpartyMapper(IMappingEngine mappingEngine, IRepository repository)
            : base(mappingEngine)
        {
            this.repository = repository;
        }

        public override void Map(Sample.Counterparty source, Counterparty destination)
        {
            base.Map(source, destination);
            destination.PartyRoleType = "Counterparty";
            destination.Party = this.repository.FindEntityByMapping<Party, PartyMapping>(source.Party);
        }

        protected override Sample.CounterpartyDetails ContractDetails(Sample.Counterparty contract)
        {
            return contract.Details;
        }

        protected override DateRange ContractDetailsValidity(Sample.Counterparty contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<MdmId> Identifiers(Sample.Counterparty contract)
        {
            return contract.Identifiers;
        }
    }
}