namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    using DateRange = EnergyTrading.DateRange;

    public class PartyMapper : ContractMapper<Sample.Party, Party, Sample.PartyDetails, PartyDetails, PartyMapping>
    {
        public PartyMapper(IMappingEngine mappingEngine)
            : base(mappingEngine)
        {
        }

        protected override Sample.PartyDetails ContractDetails(Sample.Party contract)
        {
            return contract.Details;
        }

        protected override DateRange ContractDetailsValidity(Sample.Party contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<MdmId> Identifiers(Sample.Party contract)
        {
            return contract.Identifiers;
        }
    }
}