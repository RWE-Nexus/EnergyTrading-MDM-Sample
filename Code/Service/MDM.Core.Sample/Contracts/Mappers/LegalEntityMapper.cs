namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data;

    using DateRange = EnergyTrading.DateRange;

    /// <summary>
    /// Maps a <see cref="MDM.SourceSystem" /> to a <see cref="LegalEntity" />
    /// </summary>
    public class LegalEntityMapper :
        ContractMapper<Sample.LegalEntity, LegalEntity, Sample.LegalEntityDetails, LegalEntityDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public LegalEntityMapper(IRepository repository, IMappingEngine mappingEngine)
            : base(mappingEngine)
        {
            this.repository = repository;
        }

        public override void Map(Sample.LegalEntity source, LegalEntity destination)
        {
            base.Map(source, destination);

            destination.PartyRoleType = "Legal Entity";
            destination.Party = this.repository.FindEntityByMapping<Party, PartyMapping>(source.Party);
        }

        protected override Sample.LegalEntityDetails ContractDetails(Sample.LegalEntity contract)
        {
            return contract.Details;
        }

        protected override DateRange ContractDetailsValidity(Sample.LegalEntity contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<MdmId> Identifiers(Sample.LegalEntity contract)
        {
            return contract.Identifiers;
        }
    }
}