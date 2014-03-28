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

    public class PartyConfiguration : NexusEntityConfiguration<PartyService, Party, EnergyTrading.MDM.Contracts.Sample.Party, PartyMapping, PartyValidator>
    {
        public PartyConfiguration(IUnityContainer container)
            : base(container)
        {
        }

        protected override string Name
        {
            get { return "party"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.Party, Party>, EnergyTrading.MDM.Contracts.Mappers.PartyMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.PartyDetails, PartyDetails>, EnergyTrading.MDM.Contracts.Mappers.PartyDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyMapping>, MappingMapper<PartyMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PartyDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyMappingMapper());
            this.Container.RegisterType<IMapper<Party, List<Link>>, PartyLinksMapper>();
            this.Container.RegisterType<IMapper<Party, EnergyTrading.MDM.Contracts.Sample.Party>, EnergyTrading.MDM.Mappers.PartyMapper>();
        }
    }
}