namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Transactions;

    using EnergyTrading;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    public class ObjectScript
    {
        public LocationData LocationData;

        public LocationDataChecker LocationDataChecker;

        public PartyData PartyData;

        public PartyDataChecker PartyDataChecker;
        
        public PersonData PersonData;

        public PersonDataChecker PersonDataChecker;

        public SourceSystemData SourceSystemData;

        public SourceSystemDataChecker SourceSystemDataChecker;

        public DateTime baseDate = SystemTime.UtcNow().AddDays(1);

        private SourceSystem endur;

        private SourceSystem gastar;

        private SourceSystem trayport;

        public static DbSetRepository Repository;

        static ObjectScript()
        {
            Repository = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext()));
        }

        public void RunScript()
        {
            using (var t = new TransactionScope(TransactionScopeOption.Required))
            {
                this.PersonData = new PersonData(Repository);
                this.PersonDataChecker = new PersonDataChecker();
                this.PartyData = new PartyData(Repository);
                this.PartyDataChecker = new PartyDataChecker();
                this.LocationData = new LocationData(Repository);
                this.LocationDataChecker = new LocationDataChecker();
                this.SourceSystemData = new SourceSystemData(Repository);
                this.SourceSystemDataChecker = new SourceSystemDataChecker();

                var z = new Zapper(Repository);
                z.Zap();

                this.CreateSystems();
                Repository.Flush();
                t.Complete();
            }
        }

        private void CreateSystems()
        {
            this.endur = new SourceSystem { Name = "Endur" };
            this.gastar = new SourceSystem { Name = "Gastar" };
            this.trayport = new SourceSystem { Name = "Trayport" };
            var spreadsheet = new SourceSystem { Name = "Spreadsheet" };

            Repository.Add(this.endur);
            Repository.Add(this.trayport);
            Repository.Add(this.gastar);
            Repository.Add(spreadsheet);
            Repository.Flush();
        }
    }
}