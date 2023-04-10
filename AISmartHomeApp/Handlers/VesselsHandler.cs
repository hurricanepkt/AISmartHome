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


        internal static async Task<IResult> GetAllCallbacks(FileContext db)
        {
            return TypedResults.Ok(await db.Vessels.ToArrayAsync());
        }


        internal static async Task<IResult> GetHA(FileContext db)
        {
            return await HomeAssistantFiltered(db, (f) => true);
        }

        internal static async Task<IResult> GetHAwNSeconds(FileContext db, int seconds)
        {
            var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
            return await HomeAssistantFiltered(db, (f) => f.LastUpdate > secondsAgo);
        }

        internal static async Task<IResult> GetHAComWNSeconds(FileContext db,bool strict, int seconds)
        {
            var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
            if (strict) {
                return await HomeAssistantFiltered(db, (f) => f.LastUpdate > secondsAgo && f.IsCommercial == true);
            }else {
                return await HomeAssistantFiltered(db, (f) => f.LastUpdate > secondsAgo && (f.IsCommercial == true || f.IsCommercial == null));
            }            
        }

        private static async Task<IResult> HomeAssistantFiltered(FileContext db, Expression<Func<Vessel,bool>> filter) {
            var thelist = (await db.Vessels.ToListAsync()).Where(filter.Compile()).ToList();            
            return TypedResults.Ok(
                    new { 
                        count = thelist.Count,
                        Vessels = thelist,
                        Filter = filter.ToString(),
                        CustomString = TheConfiguration.CustomString                   
                    });
        }

    }
}