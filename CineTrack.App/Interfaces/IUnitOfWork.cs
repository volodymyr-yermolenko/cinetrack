namespace CineTrack.App.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
