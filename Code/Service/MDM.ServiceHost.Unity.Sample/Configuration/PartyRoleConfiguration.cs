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

    public class PartyRoleConfiguration : NexusEntityConfiguration<PartyRoleService, PartyRole, EnergyTrading.MDM.Contracts.Sample.PartyRole, PartyRoleMapping, PartyRoleValidator>
    {
        public PartyRoleConfiguration(IUnityContainer container)
            : base(container)
        {
        }

        protected override string Name
        {
            get { return "partyrole"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.PartyRole, PartyRole>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.MDM.Contracts.Sample.PartyRoleDetails, PartyRoleDetails>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PartyRoleDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());
            this.Container.RegisterType<IMapper<PartyRole, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<PartyRole, EnergyTrading.MDM.Contracts.Sample.PartyRole>, EnergyTrading.MDM.Mappers.PartyRoleMapper>();
        }
    }
}
