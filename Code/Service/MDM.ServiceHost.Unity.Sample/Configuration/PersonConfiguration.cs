namespace MDM.ServiceHost.Unity.Sample.Configuration
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;
    using EnergyTrading.MDM.Services;

    using Microsoft.Practices.Unity;

    public class PersonConfiguration : NexusEntityConfiguration<PersonService, Person, EnergyTrading.MDM.Contracts.Sample.Person, PersonMapping, PersonValidator>
    {
        public PersonConfiguration(IUnityContainer container) : base(container)
        {

        }

        protected override string Name
        {
            get { return "person"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.Person, Person>, EnergyTrading.MDM.Contracts.Mappers.PersonMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.PersonDetails, PersonDetails>, EnergyTrading.MDM.Contracts.Mappers.PersonDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PersonMapping>, MappingMapper<PersonMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PersonDetailsMapper());
            this.MappingEngine.RegisterMap(new PersonMappingMapper());      
            this.Container.RegisterType<IMapper<Person, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Person, EnergyTrading.MDM.Contracts.Sample.Person>, EnergyTrading.MDM.Mappers.PersonMapper>();
        }
    }
}