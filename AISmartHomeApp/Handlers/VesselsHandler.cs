using System.Linq.Expressions;
using Database;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Handlers
{
    public class CallbackHandlers
    {
        private static IVesselRepository? _db;
        internal static void Setup(RouteGroupBuilder callbacksGroup, IVesselRepository db)
        {
            _db = db;
            callbacksGroup.MapGet("/", CallbackHandlers.GetAllCallbacks);
            callbacksGroup.MapGet("/homeassistant", CallbackHandlers.GetHA);
            callbacksGroup.MapGet("/ha/{seconds}", CallbackHandlers.GetHAwNSeconds);
            callbacksGroup.MapGet("/commercial/{strict}/{seconds}", CallbackHandlers.GetHAComWNSeconds);
        }


        internal static async Task<IResult> GetAllCallbacks()
        {
            if (_db == null ) { throw new Exception("Not Initialized"); }
            return TypedResults.Ok(await _db.AllVesselsAsync());
        }


        internal static IResult GetHA()
        {
            return HomeAssistantFiltered((f) => true);
        }

        internal static IResult GetHAwNSeconds(int seconds)
        {
            var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
            return HomeAssistantFiltered((f) => f.LastUpdate > secondsAgo);
        }

        internal static IResult GetHAComWNSeconds(bool strict, int seconds)
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

        private static IResult HomeAssistantFiltered(Expression<Func<Vessel, bool>> filter)
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