using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class VesselRepository : IVesselRepository
{
    private readonly Context _context;

    public VesselRepository(Context context) => _context = context;
    public async Task<List<Vessel>> AllVesselsAsync() => await _context.Vessels.ToListAsync();
    public List<Vessel> AllVessels() => _context.Vessels.ToList();
    public List<Vessel> Filtered(Expression<Func<Vessel, bool>> q) => _context.Vessels.Where(q.Compile()).ToList();
    public List<VesselDto> FilteredDto(Expression<Func<Vessel, bool>> q) => _context.Vessels.Where(q.Compile()).ToList().Select(q => VesselDto.Create(q)).ToList();
    public async Task<Vessel?> FindByMmsi(uint mmsi) => await _context.Vessels.FindAsync(mmsi);
    public async Task Add(Vessel vessel) => await _context.Vessels.AddAsync(vessel);
    public async Task Save() => await _context.SaveChangesAsync();
    public async Task EnsureCreated(CancellationToken token) => await _context.Database.EnsureCreatedAsync(token);
}

public interface IVesselRepository {

    Task<List<Vessel>> AllVesselsAsync();
    List<Vessel> AllVessels();
    List<Vessel> Filtered(Expression<Func<Vessel, bool>> filter);
    List<VesselDto> FilteredDto(Expression<Func<Vessel, bool>> filter);
    Task<Vessel?> FindByMmsi(uint mmsi);
    Task Add(Vessel vessel);
    Task Save();
    Task EnsureCreated(CancellationToken token);


}