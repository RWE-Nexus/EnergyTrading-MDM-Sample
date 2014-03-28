namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data;

    using DateRange = EnergyTrading.DateRange;

    /// <summary>
    /// Maps a <see cref="MDM.SourceSystem" /> to a <see cref="Exchange" />
    /// </summary>
    public class ExchangeMapper :
        ContractMapper<Sample.Exchange, Exchange, Sample.ExchangeDetails, ExchangeDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public ExchangeMapper(IMappingEngine mappingEngine, IRepository repository)
            : base(mappingEngine)
        {
            this.repository = repository;
        }

        public override void Map(Sample.Exchange source, Exchange destination)
        {
            base.Map(source, destination);
            destination.PartyRoleType = "Exchange";
            destination.Party = this.repository.FindEntityByMapping<Party, PartyMapping>(source.Party);
        }

        protected override Sample.ExchangeDetails ContractDetails(Sample.Exchange contract)
        {
            return contract.Details;
        }

        protected override DateRange ContractDetailsValidity(Sample.Exchange contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<MdmId> Identifiers(Sample.Exchange contract)
        {
            return contract.Identifiers;
        }
    }
}