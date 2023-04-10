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
            callbacksGroup.MapGet("/homeassistant", CallbackHandlers.GetHAformatted);
            callbacksGroup.MapGet("/ha/{seconds}", CallbackHandlers.GetHAformattedNSeconds);
        }


        internal static async Task<IResult> GetAllCallbacks(FileContext db)
        {
            return TypedResults.Ok(await db.Vessels.ToArrayAsync());
        }


        internal static async Task<IResult> GetHAformatted(FileContext db)
        {
            var thelist = await db.Vessels.ToListAsync();

            return TypedResults.Ok(
                    new { 
                        count = thelist.Count,
                        Vessels = thelist,
                        CustomString = TheConfiguration.CustomString                   
                    });
        }

        internal static async Task<IResult> GetHAformattedNSeconds(FileContext db, int seconds)
        {
            var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
            var thelist = (await db.Vessels.ToListAsync()).Where(f => f.LastUpdate > secondsAgo).ToList();            
            return TypedResults.Ok(
                    new { 
                        count = thelist.Count,
                        Vessels = thelist,
                        CustomString = TheConfiguration.CustomString                   
                    });
        }

    }
}