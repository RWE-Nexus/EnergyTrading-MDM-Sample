namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    /// <summary>
	///
	/// </summary>
    public class DimensionDetailsMapper : Mapper<DimensionDetails, MDM.Dimension>
    {
        public override void Map(DimensionDetails source, MDM.Dimension destination)
        {
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.LengthDimension = source.LengthDimension;
            destination.MassDimension = source.MassDimension;
            destination.TimeDimension = source.TimeDimension;
            destination.ElectricCurrentDimension = source.ElectricCurrentDimension;
            destination.TemperatureDimension = source.TemperatureDimension;
            destination.LuminousIntensityDimension = source.LuminousIntensityDimension;
        }
    }
}