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

    public class ExchangeConfiguration : NexusEntityConfiguration<ExchangeService, Exchange, EnergyTrading.MDM.Contracts.Sample.Exchange, 
		PartyRoleMapping, ExchangeValidator>
    {
        public ExchangeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "exchange"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.Exchange, Exchange>, EnergyTrading.MDM.Contracts.Mappers.ExchangeMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.ExchangeDetails, ExchangeDetails>, EnergyTrading.MDM.Contracts.Mappers.ExchangeDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ExchangeDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<Exchange, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Exchange, EnergyTrading.MDM.Contracts.Sample.Exchange>, EnergyTrading.MDM.Mappers.ExchangeMapper>();
        }
    }
}