namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading;

    public partial class PartyData
    {
        partial void AddDetailsToContract(EnergyTrading.MDM.Contracts.Sample.Party contract)
        {
            contract.Details = new EnergyTrading.MDM.Contracts.Sample.PartyDetails() { Name = Guid.NewGuid().ToString() };
        }

        partial void AddDetailsToEntity(MDM.Party entity, DateTime startDate, DateTime endDate)
        {
            entity.AddDetails(
                new PartyDetails()
                    { Name = Guid.NewGuid().ToString(), Validity = new DateRange(startDate, endDate) });
        }
    }
}
