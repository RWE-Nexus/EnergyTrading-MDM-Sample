namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;

    /// <summary />
    public class UnitDetailsMapper : Mapper<UnitDetails, MDM.Unit>
    {
        private readonly IRepository repository;

        public UnitDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(UnitDetails source, MDM.Unit destination)
        {
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Dimension = this.repository.FindEntityByMapping<MDM.Dimension, DimensionMapping>(source.Dimension);
            destination.ConversionConstant = source.ConversionConstant;
            destination.ConversionFactor = source.ConversionFactor;
            destination.Symbol = source.Symbol;
        }
    }
}