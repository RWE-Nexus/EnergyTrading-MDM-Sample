namespace EnergyTrading.MDM.Data
{
    using System.Linq;

    using EnergyTrading.Data;

    public static class RepositoryExtensions
    {
        public static int FindPartyRoleOverlappingMappingCount<TMapping>(this IRepository repository, string sourceSystem, string mapping, EnergyTrading.DateRange range, string partyRoleType, int mappingId = 0)
            where TMapping : class, IEntityMapping
        {
            var mappings = repository.FindOverlappingMappings<TMapping>(sourceSystem, mapping, range, mappingId);

            return mappings.Cast<PartyRoleMapping>().Count(x => x.PartyRole.PartyRoleType == partyRoleType);
        }
    }
}
