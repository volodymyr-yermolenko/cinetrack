using CineTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineTrack.Infrastructure.Persistence;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).HasMaxLength(100);
        builder.Property(p => p.MovieType).HasConversion<int>();

        builder.HasMany(p => p.Genres).WithMany(p => p.Movies).UsingEntity("MovieGenres",
            j => j.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.ClientNoAction),
            j => j.HasOne(typeof(Movie)).WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.Cascade)
        );
    }
}