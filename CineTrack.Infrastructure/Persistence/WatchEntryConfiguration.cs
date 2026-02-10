using CineTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineTrack.Infrastructure.Persistence;

public class WatchEntryConfiguration : IEntityTypeConfiguration<WatchEntry>
{
    public void Configure(EntityTypeBuilder<WatchEntry> builder)
    {
        builder.ToTable("WatchEntries");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ViewingContext).HasConversion<int>();
        builder.Property(p => p.Mood).HasMaxLength(500);
        
        builder.HasOne(p => p.Movie)
            .WithMany(m => m.WatchEntries)
            .HasForeignKey(p => p.MovieId)
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}