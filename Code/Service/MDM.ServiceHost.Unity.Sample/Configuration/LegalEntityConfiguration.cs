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

    public class LegalEntityConfiguration : NexusEntityConfiguration<LegalEntityService, LegalEntity, EnergyTrading.MDM.Contracts.Sample.LegalEntity,
        PartyRoleMapping, LegalEntityValidator>
    {
        public LegalEntityConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "legalentity"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.LegalEntity, LegalEntity>, EnergyTrading.MDM.Contracts.Mappers.LegalEntityMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.LegalEntityDetails, LegalEntityDetails>, EnergyTrading.MDM.Contracts.Mappers.LegalEntityDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.LegalEntityDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());
            this.Container.RegisterType<IMapper<LegalEntity, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<LegalEntity, EnergyTrading.MDM.Contracts.Sample.LegalEntity>, EnergyTrading.MDM.Mappers.LegalEntityMapper>();
        }
    }
}