namespace Mdm.Client.Sample.Registrars
{
    using System.Linq;

    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebApi.Registrars;
    using EnergyTrading.MDM.Contracts.Sample;

    using Microsoft.Practices.Unity;

    public class NexusMdmClientRegistrar : MdmClientRegistrar
    {
        protected override void RegisterEntityServices(IUnityContainer container)
        {
            this.RegisterMdmService<Broker>(container, "broker");
            this.RegisterMdmService<Counterparty>(container, "counterparty");
            this.RegisterMdmService<Exchange>(container, "exchange");
            this.RegisterMdmService<LegalEntity>(container, "legalentity");
            this.RegisterMdmService<Location>(container, "location");
            this.RegisterMdmService<Party>(container, "party");
            this.RegisterMdmService<PartyRole>(container, "partyrole");
            this.RegisterMdmService<Person>(container, "person");
        }

        protected override void RegisterModelEntityServices(IUnityContainer container)
        {
            foreach (var type in this.GetType().Assembly.GetTypes())
            {
                var @interface =
                    type.GetInterfaces()
                        .FirstOrDefault(
                            i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMdmModelEntityService<,>)));

                if (@interface != null)
                {
                    container.RegisterType(@interface, type);
                }
            }
        }
    }
}