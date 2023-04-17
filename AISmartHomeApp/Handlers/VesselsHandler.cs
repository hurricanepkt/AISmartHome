using System.Linq.Expressions;
using Database;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Handlers
{
    public class VesselsHandler
    {
        public VesselsHandler(IVesselRepository? db)
        {
            _db = db;
        }
        private IVesselRepository? _db;
        internal void Setup(RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllCallbacks);
            group.MapGet("/homeassistant", GetHA);
            group.MapGet("/ha/{seconds}", GetHAwNSeconds);
            group.MapGet("/commercial/{strict}/{seconds}", GetHAComWNSeconds);
        }


        internal async Task<IResult> GetAllCallbacks()
        {
            if (_db == null ) { throw new Exception("Not Initialized"); }
            return TypedResults.Ok(await _db.AllVesselsAsync());
        }


        internal IResult GetHA()
        {
            return HomeAssistantFiltered((f) => true);
        }

        internal IResult GetHAwNSeconds(int seconds)
        {
            var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
            return HomeAssistantFiltered((f) => f.LastUpdate > secondsAgo);
        }

        internal IResult GetHAComWNSeconds(bool strict, int seconds)
        {
            var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
            if (strict)
            {
                return HomeAssistantFiltered((f) => f.LastUpdate > secondsAgo && f.IsCommercial == true);
            }
            else
            {
                return HomeAssistantFiltered((f) => f.LastUpdate > secondsAgo && (f.IsCommercial == true || f.IsCommercial == null));
            }
        }

        private IResult HomeAssistantFiltered(Expression<Func<Vessel, bool>> filter)
        {
            if (_db == null) { throw new Exception("Not Initialized"); }
            var thelist = _db.Filtered(filter);
            return TypedResults.Ok(
                    new
                    {
                        count = thelist.Count,
                        Vessels = thelist,
                        Filter = filter.ToString(),
                        CustomString = TheConfiguration.CustomString
                    });
        }

    }
}