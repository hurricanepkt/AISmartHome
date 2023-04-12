using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class VesselRepository : IVesselRepository
{
    private readonly Context _context;

    public VesselRepository(Context context) => _context = context;
    public async Task<List<Vessel>> AllVesselsAsync() => await _context.Vessels.ToListAsync();
    public List<Vessel> AllVessels() => _context.Vessels.ToList();
    public async Task<List<Vessel>> FilteredAsync(Expression<Func<Vessel, bool>> q) => await _context.Vessels.Where(q.Compile()).ToListAsync();
    public List<Vessel> Filtered(Expression<Func<Vessel, bool>> q) => _context.Vessels.Where(q.Compile()).ToList();
}

public interface IVesselRepository {

    Task<List<Vessel>> AllVesselsAsync();
    List<Vessel> AllVessels();
    Task<List<Vessel>> FilteredAsync(Expression<Func<Vessel, bool>> filter);
    List<Vessel> Filtered(Expression<Func<Vessel, bool>> filter);

}