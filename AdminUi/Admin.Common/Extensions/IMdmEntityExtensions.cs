namespace Common.Extensions
{
    using System.Linq;

    using EnergyTrading.Mdm.Contracts;

    public static class IMdmEntityExtensions
    {
        public static int? MdmId(this IMdmEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return
                entity.Identifiers.Where(id => id.IsMdmId)
                    .Select(nexusId => nexusId.Identifier == null ? null : new int?(int.Parse(nexusId.Identifier)))
                    .FirstOrDefault();
        }
    }
}