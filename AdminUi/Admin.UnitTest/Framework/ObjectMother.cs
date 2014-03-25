namespace Admin.UnitTest.Framework
{
    using System;
    using System.Collections.Generic;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public static class ObjectMother
    {
        public static T Create<T>() where T : IMdmEntity
        {
            IMdmEntity value = Create(typeof(T).Name);

            return (T)value;
        }

        public static IMdmEntity Create(string name)
        {
            switch (name)
            {
                case "Party":
                    return new Party
                        {
                            Details = new PartyDetails { Name = G(), FaxNumber = G(), Role = G(), TelephoneNumber = G() }, 
                            Identifiers = CreateMdmIdList()
                        };

                default:
                    throw new NotImplementedException("No OM for " + name);
            }
        }

        public static IList<T> CreateInList<T>() where T : IMdmEntity
        {
            return new List<T> { Create<T>() };
        }

        public static MdmId CreateMapping(int id)
        {
            return new MdmId
                {
                    MappingId = id,
                    Identifier = "string", 
                    IsMdmId = false, 
                    StartDate = DateTime.MinValue, 
                    EndDate = DateTime.MaxValue
                };
        }

        public static MdmId CreateMdmId()
        {
            return new MdmId
                {
                   Identifier = "1", IsMdmId = true, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue 
                };
        }

        private static MdmIdList CreateMdmIdList()
        {
            return new MdmIdList { CreateMdmId() };
        }

        private static string G()
        {
            return Guid.NewGuid().ToString();
        }
    }
}