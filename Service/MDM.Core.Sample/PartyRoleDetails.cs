namespace EnergyTrading.MDM
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleDetails : IIdentifiable, IEntityDetail
    {
        public PartyRoleDetails()
        {
            this.Validity = new DateRange();
            this.Timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public IEntity Entity
        {
            get
            {
                return this.PartyRole;
            }

            set
            {
                this.PartyRole = value as PartyRole;
            }
        }

        public int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual PartyRole PartyRole { get; set; }

        public byte[] Timestamp { get; set; }

        public DateRange Validity { get; set; }

        public ulong Version
        {
            get
            {
                return this.Timestamp.ToUnsignedLongVersion();
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