namespace EnergyTrading.MDM.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Mapping;

    public class PartyLinksMapper : Mapper<Party, List<Link>>
    {
        public override void Map(Party source, List<Link> destination)
        {
            foreach (var partyRole in source.PartyRoles)
            {
                var entityIdentifier = partyRole.GetType().BaseType.Name;

                destination.Add(
                    new Link
                        {
                            Rel = "get-related-" + entityIdentifier.ToLower(), 
                            Type = entityIdentifier, 
                            Uri = "/" + entityIdentifier + "/" + partyRole.Id
                        });
            }
        }
    }
}