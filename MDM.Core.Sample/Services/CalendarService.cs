namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    using Calendar = EnergyTrading.MDM.Calendar;

    public class CalendarService : MdmService<Contracts.Sample.Calendar, Calendar, CalendarMapping, Calendar, CalendarDetails>
    {

        public CalendarService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Calendar> Details(Calendar entity)
        {
            return new List<Calendar> { entity };
        }

        protected override IEnumerable<CalendarMapping> Mappings(Calendar entity)
        {
            return entity.Mappings;
        }
    }
}