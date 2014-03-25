namespace Mdm.Client.Sample.Services
{
    using System;

    using EnergyTrading.Mdm.Client.Model;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;

    /// <summary>
    /// Standard MDM queries and helper methods for retrieving MDM and model entities.
    /// </summary>
    public static class MdmQueryExtensions
    {
        public static Broker Broker(this IMdmModelEntityService service, int brokerId)
        {
            return service.Get<Broker>(brokerId);
        }

        public static Exchange Exchange(this IMdmModelEntityService service, int exchangeId)
        {
            return service.Get<Exchange>(exchangeId);
        }

        public static Location Location(this IMdmModelEntityService service, int locationId)
        {
            return service.Get<Location>(locationId);
        }

        public static Party Party(this IMdmModelEntityService service, int partyId)
        {
            return service.Get<Party>(partyId);
        }

        public static PartyRole PartyRole(this IMdmModelEntityService service, int partyId)
        {
            return service.Get<PartyRole>(partyId);
        }

        public static PartyRole PartyRole<T>(this IMdmModelEntityService service, T entity, Func<T, EntityId> access)
            where T : class, IMdmEntity
        {
            return service.Get<T, PartyRole>(entity, access);
        }

        /// <summary>
        /// Get a MDM contract using data in another entity
        /// </summary>
        /// <typeparam name="T">Type of the entity to use.</typeparam>
        /// <typeparam name="TContract">Type of the contract to use</typeparam>
        /// <param name="service">MDM service to use</param>
        /// <param name="entity">Entity to use</param>
        /// <param name="access">Function to return the identity of the contract from the entity.</param>
        /// <returns>MDM contract if found, otherwise null.</returns>
        public static TContract Get<T, TContract>(this IMdmModelEntityService service, T entity, Func<T, EntityId> access)
            where T : class, IMdmEntity
            where TContract : class, IMdmEntity
        {
            return service.Get<TContract>(entity.ToMdmKey(access));
        }

        public static TModel Model<TContract, TModel>(this IMdmModelEntityService service, TContract entity)
            where TContract : class, IMdmEntity
            where TModel : IMdmModelEntity<TContract>
        {
            return service.Get<TContract, TModel>(entity);
        }

        public static TModel Model<T, TContract, TModel>(this IMdmModelEntityService service, T entity, Func<T, EntityId> access)
            where T : class, IMdmEntity
            where TContract : class, IMdmEntity
            where TModel : IMdmModelEntity<TContract>
        {
            return service.Get<TContract, TModel>(service.Get<T, TContract>(entity, access));
        }
    }
}