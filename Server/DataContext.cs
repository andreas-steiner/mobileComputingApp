using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
	}

	public DbSet<Species> Specien { get; set; }

    public DbSet<Lang> Langs { get; set; }
    public DbSet<Trait> Traits { get; set; }
    public DbSet<SubRace> SubRaces { get; set; }

    public IQueryable<Species> SpecienIncludingAll { 
        get => Specien.Include(i => i.Langs)
                .Include(i => i.Traits)
                .Include(i => i.SubRaces);
    }
}
