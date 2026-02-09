using CineTrack.App.Exceptions;
using CineTrack.App.Interfaces;
using MediatR;

namespace CineTrack.App.Features.Movies.DeleteMovie;

public class DeleteMovieCommandHandler(IMovieRepository repository) 
    : IRequestHandler<DeleteMovieCommand, Unit>
{
    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await repository.GetMovieAsync(request.UserId, request.MovieId);
        if (movie == null)
        {
            throw new AppNotFoundException("Movie not found");
        }
        
        repository.DeleteMovie(movie);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return Unit.Value;
    }
}