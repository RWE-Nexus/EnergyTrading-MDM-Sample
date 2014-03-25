namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;

    /// <summary>
	///
	/// </summary>
    public class PartyAccountabilityDetailsMapper : Mapper<PartyAccountabilityDetails, MDM.PartyAccountability>
    {
        private readonly IRepository repository;

        public PartyAccountabilityDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(PartyAccountabilityDetails source, MDM.PartyAccountability destination)
        {
            destination.Name = source.Name;
            destination.PartyAccountabilityType = source.PartyAccountabilityType;
            destination.SourceParty = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.SourceParty);
            destination.TargetParty = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.TargetParty);
            destination.SourcePerson = this.repository.FindEntityByMapping<MDM.Person, PersonMapping>(source.SourcePerson);
            destination.TargetPerson = this.repository.FindEntityByMapping<MDM.Person, PersonMapping>(source.TargetPerson);
        }
    }
}