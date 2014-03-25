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

    public class CounterpartyConfiguration : NexusEntityConfiguration<CounterpartyService, Counterparty, EnergyTrading.MDM.Contracts.Sample.Counterparty, 
		PartyRoleMapping, CounterpartyValidator>
    {
        public CounterpartyConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "counterparty"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.Counterparty, Counterparty>, EnergyTrading.MDM.Contracts.Mappers.CounterpartyMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.CounterpartyDetails, CounterpartyDetails>, EnergyTrading.MDM.Contracts.Mappers.CounterpartyDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.CounterpartyDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<Counterparty, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Counterparty, EnergyTrading.MDM.Contracts.Sample.Counterparty>, EnergyTrading.MDM.Mappers.CounterpartyMapper>();
        }
    }
}