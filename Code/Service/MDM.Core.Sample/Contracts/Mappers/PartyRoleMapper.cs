namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data;

    using DateRange = EnergyTrading.DateRange;

    public class PartyRoleMapper :
        ContractMapper<Sample.PartyRole, PartyRole, Sample.PartyRoleDetails, PartyRoleDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public PartyRoleMapper(IMappingEngine mappingEngine, IRepository repository)
            : base(mappingEngine)
        {
            this.repository = repository;
        }

        public override void Map(Sample.PartyRole source, PartyRole destination)
        {
            base.Map(source, destination);
            destination.PartyRoleType = source.PartyRoleType;
            destination.Party = this.repository.FindEntityByMapping<Party, PartyMapping>(source.Party);
        }

        protected override Sample.PartyRoleDetails ContractDetails(Sample.PartyRole contract)
        {
            return contract.Details;
        }

        protected override DateRange ContractDetailsValidity(Sample.PartyRole contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<MdmId> Identifiers(Sample.PartyRole contract)
        {
            return contract.Identifiers;
        }
    }
}