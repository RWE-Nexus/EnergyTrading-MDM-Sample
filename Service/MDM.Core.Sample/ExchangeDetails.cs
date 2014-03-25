namespace EnergyTrading.MDM
{
    using EnergyTrading.Data;

    public class ExchangeDetails : PartyRoleDetails, IIdentifiable, IEntityDetail
    {
        public virtual string Fax { get; set; }

        public virtual string Phone { get; set; }
    }
}