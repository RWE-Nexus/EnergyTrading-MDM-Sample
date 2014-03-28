using System.Linq;

namespace MDM.Loader.AdcSync
{
    public interface IPartyCrossMapService
    {
        IQueryable FetchData();
    }

    public class PartyCrossMapService : IPartyCrossMapService
    {
        private PartyCrossMapDataContext dataContext;
        private IQueryable partyCrossMap;

        public PartyCrossMapService()
        {
            dataContext = new PartyCrossMapDataContext();
        }

        public IQueryable FetchData()
        {
            partyCrossMap = from m in dataContext.PartyCrossMaps
                            where m.Commodity != null &&
                             m.System1.ToUpper() == "ENDUR"
                            orderby m.MapId1 
                            select m;
           
            return partyCrossMap;
        }
    }
}