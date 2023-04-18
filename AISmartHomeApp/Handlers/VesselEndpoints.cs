using System.Linq.Expressions;
using Database;
using Infrastructure;

namespace Handlers;

public static class VesselEndpoints
{

    public static RouteGroupBuilder MapVesselEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/",GetAllCallbacks);
        group.MapGet("/homeassistant",GetHA);
        group.MapGet("/ha/{seconds}", GetHAwNSeconds);
        group.MapGet("/commercial/{strict}/{seconds}", GetHAComWNSeconds);
        return group;
    }


    public static async Task<IResult> GetAllCallbacks(VesselRepository repo)
    {
        if (repo == null ) { throw new Exception("Not Initialized"); }
        return TypedResults.Ok(await repo.AllVesselsAsync());
    }


    public static IResult GetHA(VesselRepository repo)
    {
        return HomeAssistantFiltered((f) => true, repo);
    }

    public static IResult GetHAwNSeconds(int seconds,VesselRepository repo)
    {
        var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
        return HomeAssistantFiltered((f) => f.LastUpdate > secondsAgo, repo);
    }

    public static IResult GetHAComWNSeconds(bool strict, int seconds,VesselRepository repo)
    {
        var secondsAgo = DateTime.UtcNow.AddSeconds(-1 * seconds);
        if (strict)
        {
            return HomeAssistantFiltered((f) => f.LastUpdate > secondsAgo && f.IsCommercial == true, repo);
        }
        else
        {
            return HomeAssistantFiltered((f) => f.LastUpdate > secondsAgo && (f.IsCommercial == true || f.IsCommercial == null), repo);
        }
    }

    public static IResult HomeAssistantFiltered(Expression<Func<Vessel, bool>> filter,VesselRepository repo)
    {
        if (repo == null) { throw new Exception("Not Initialized"); }
        var thelist = repo.Filtered(filter);
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