﻿namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;
    using EnergyTrading.MDM.ServiceHost.Wcf.Sample;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using EnergyTrading;
    using EnergyTrading.Mapping;

    using EnergyTrading.MDM;

    public class EntityMappingFixture : Fixture
    {
        public readonly TimeSpan Interval = new TimeSpan(0, 0, 0, 1);

        protected void ConvertToNexusId<TMapping>()
            where TMapping : EntityMapping, new()
        {
            var container = new UnityContainer();

            // Self-register and set up service location 
            container.RegisterInstance<IUnityContainer>(container);
            var locator = new UnityServiceLocator(container);
            Global.ServiceLocator = locator;

            var task = new SimpleMappingEngineConfiguration(container);
            task.Configure();

            var start = new DateTime(2000, 12, 31);
            var expected = new EnergyTrading.Mdm.Contracts.MdmId
            {
                MappingId = 34,
                SystemName = "Test",
                Identifier = "1",
                SourceSystemOriginated = true,
                StartDate = start,
                EndDate = DateUtility.MaxDate
            };
            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping
            {
                Id = 34,
                System = s1,
                MappingValue = "1",
                IsMaster = true,
                Validity = new DateRange(start, DateUtility.MaxDate)
            };

            var mappingEngine = container.Resolve<IMappingEngine>();

            var candidate = mappingEngine.Map<IEntityMapping, EnergyTrading.Mdm.Contracts.MdmId>(m1);
            Check(expected, candidate);
        }
    }
}