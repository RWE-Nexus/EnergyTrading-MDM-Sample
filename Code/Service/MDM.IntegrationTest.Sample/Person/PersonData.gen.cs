namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Data.EntityFramework;
	using EnergyTrading.Contracts.Search;

    using DateRange = EnergyTrading.DateRange;

    public partial class PersonData
    {
        private readonly DbSetRepository repository;

        private DateTime baseDate;

        public PersonData(DbSetRepository repository)
        {
            this.repository = repository;
        }

        public MDM.Person CreateEntityWithTwoDetailsAndTwoMappings()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            var trayport = repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new MDM.Person();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            this.AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            this.AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new PersonMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = trayport,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue)
                };

            var endurMapping = new PersonMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = endur,
                    IsDefault = true,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue)
                };

            entity.ProcessMapping(trayportMapping);
            entity.ProcessMapping(endurMapping);

            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public MDM.Person CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Person>();
            this.repository.Add(entity);
            this.repository.Flush();
            return entity;
        }

        public MDM.Person CreateBasicEntityWithOneMapping()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Person>();

            var endurMapping = new PersonMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = endur,
                    IsDefault = true,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue.Subtract(new TimeSpan(72, 0, 0)))
                };

            entity.ProcessMapping(endurMapping);
            this.repository.Add(entity);
            this.repository.Flush();

            return entity;
        }

        public EnergyTrading.MDM.Contracts.Sample.Person MakeChangeToContract(EnergyTrading.MDM.Contracts.Sample.Person currentContract)
        {
            this.AddDetailsToContract(currentContract);
            currentContract.MdmSystemData.StartDate = currentContract.MdmSystemData.StartDate.Value.AddDays(2);
            return currentContract;
        }

        public EnergyTrading.MDM.Contracts.Sample.Person CreateContractForEntityCreation()
        {
            var contract = new EnergyTrading.MDM.Contracts.Sample.Person();
            this.AddDetailsToContract(contract);
            return contract;
        }

        public void CreateSearch(Search search, Person entity1, Person entity2)
        {
            this.CreateSearchData(search, entity1, entity2) ;
        }

        partial void CreateSearchData(Search search, Person entity1, Person entity2);

        partial void AddDetailsToContract(EnergyTrading.MDM.Contracts.Sample.Person contract);

        partial void AddDetailsToEntity(MDM.Person entity, DateTime startDate, DateTime endDate);

    }
}
