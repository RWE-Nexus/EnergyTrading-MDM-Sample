namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    /// <summary>
    /// Maps a <see cref="MDM.Dimension" /> to a <see cref="RWEST.Nexus.MDM.Contracts.DimensionDetails" />
    /// </summary>
    public class DimensionDetailsMapper : Mapper<EnergyTrading.MDM.Dimension, DimensionDetails>
    {
        public override void Map(EnergyTrading.MDM.Dimension source, DimensionDetails destination)
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