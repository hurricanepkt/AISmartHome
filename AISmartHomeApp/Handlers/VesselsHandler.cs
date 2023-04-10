using System.Linq.Expressions;
using Database;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Handlers
{
    public class CallbackHandlers
    {
        internal static void Setup(RouteGroupBuilder callbacksGroup)
        {
            callbacksGroup.MapGet("/", CallbackHandlers.GetAllCallbacks);
            callbacksGroup.MapGet("/homeassistant", CallbackHandlers.GetHA);
            callbacksGroup.MapGet("/ha/{seconds}", CallbackHandlers.GetHAwNSeconds);
            callbacksGroup.MapGet("/commercial/{strict}/{seconds}", CallbackHandlers.GetHAComWNSeconds);
        }


        internal static IResult GetAllCallbacks(ReadOnlyDBRepo db)
        {
            return TypedResults.Ok(db.GetVessels()));
        }


        internal static IResult GetHA(ReadOnlyDBRepo db)
        {
            return HomeAssistantFiltered(db, (f) => true);
        }

        internal static IResult GetHAwNSeconds(ReadOnlyDBRepo db, int seconds)
        {
            var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
            return HomeAssistantFiltered(db, (f) => f.LastUpdate > secondsAgo);
        }

        internal static IResult GetHAComWNSeconds(ReadOnlyDBRepo db, bool strict, int seconds)
        {
            var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
            if (strict)
            {
                return HomeAssistantFiltered(db, (f) => f.LastUpdate > secondsAgo && f.IsCommercial == true);
            }
            else
            {
                return HomeAssistantFiltered(db, (f) => f.LastUpdate > secondsAgo && (f.IsCommercial == true || f.IsCommercial == null));
            }
        }

        private static IResult HomeAssistantFiltered(ReadOnlyDBRepo db, Expression<Func<Vessel, bool>> filter)
        {
            var thelist = db.GetVessels().Where(filter.Compile()).ToList();
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