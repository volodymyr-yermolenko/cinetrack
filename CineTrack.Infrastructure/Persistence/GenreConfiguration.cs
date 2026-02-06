using CineTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineTrack.Infrastructure.Persistence;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres");
        builder.HasKey(p => p.Id);
        //builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
    }
}