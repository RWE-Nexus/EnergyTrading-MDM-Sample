namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    public class CalendarDetailsMapper : Mapper<CalendarDetails, MDM.Calendar>
    {
        public override void Map(CalendarDetails source, MDM.Calendar destination)
        {
            destination.Name = source.Name;

            foreach(var cd in source.CalendayDays)
            {
                destination.Days.Add(
                    new MDM.CalendarDay() { Date = cd.CalendarDate, DayType = (int)cd.CalendarDayType });
            }

            // destination.Days = source.CalendayDays;
        }
    }
}
