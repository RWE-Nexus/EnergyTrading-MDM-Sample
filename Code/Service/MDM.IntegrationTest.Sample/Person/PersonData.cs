namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Search;

    using DateRange = EnergyTrading.DateRange;
    using Person = EnergyTrading.MDM.Person;
    using PersonDetails = EnergyTrading.MDM.PersonDetails;

    public partial class PersonData
    {
        partial void AddDetailsToContract(EnergyTrading.MDM.Contracts.Sample.Person contract)
        {
            contract.Details = new EnergyTrading.MDM.Contracts.Sample.PersonDetails { Forename = Guid.NewGuid().ToString(), Surname = Guid.NewGuid().ToString(), Email = "test@test.com" };
        }

        partial void AddDetailsToEntity(Person entity, DateTime startDate, DateTime endDate)
        {
            entity.AddDetails(
                new PersonDetails
                    { FirstName = Guid.NewGuid().ToString(), Email = "test@test.com", Validity = new DateRange(startDate, endDate) });
        }

        partial void CreateSearchData(Search search, Person entity1, Person entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Firstname", SearchCondition.Equals, entity1.LatestDetails.FirstName)
                .AddCriteria("Firstname", SearchCondition.Equals, entity2.LatestDetails.FirstName);
        }
    }
}