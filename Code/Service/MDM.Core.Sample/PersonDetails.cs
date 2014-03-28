namespace EnergyTrading.MDM
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    public class PersonDetails : IIdentifiable, IEntityDetail
    {
        public PersonDetails()
        {
            this.Validity = new DateRange();
            this.Timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public string Email { get; set; }

        public string Fax { get; set; }

        public string FirstName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }

        public virtual Person Person { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }

        public byte[] Timestamp { get; set; }

        public DateRange Validity { get; set; }

        public ulong Version
        {
            get
            {
                return this.Timestamp.ToUnsignedLongVersion();
            }
        }

        IEntity IRangedChild.Entity
        {
            get
            {
                return this.Person;
            }

            set
            {
                this.Person = value as Person;
            }
        }

        object IIdentifiable.Id
        {
            get
            {
                return this.Id;
            }
        }
    }
}