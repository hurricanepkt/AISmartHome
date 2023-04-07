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
                        incompleteCallbacks = thelist,
                        CustomString = TheConfiguration.CustomString                   
                    });
        }


    }
}