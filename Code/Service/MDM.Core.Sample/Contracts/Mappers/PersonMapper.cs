namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    using DateRange = EnergyTrading.DateRange;

    public class PersonMapper :
        ContractMapper<Sample.Person, Person, Sample.PersonDetails, PersonDetails, PersonMapping>
    {
        public PersonMapper(IMappingEngine mappingEngine)
            : base(mappingEngine)
        {
        }

        protected override Sample.PersonDetails ContractDetails(Sample.Person contract)
        {
            return contract.Details;
        }

        protected override DateRange ContractDetailsValidity(Sample.Person contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<MdmId> Identifiers(Sample.Person contract)
        {
            return contract.Identifiers;
        }
    }
}