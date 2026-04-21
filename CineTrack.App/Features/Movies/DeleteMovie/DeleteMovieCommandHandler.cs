using MediatR;
using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.Movies.DeleteMovie;

public class DeleteMovieCommandHandler(IMovieRepository repository) 
    : IRequestHandler<DeleteMovieCommand, Unit>
{
    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await repository.GetMovieAsync(request.UserId, request.MovieId);
        if (movie == null)
        {
            throw new AppNotFoundException(MovieErrorMessages.MovieNotFound);
        }
        
        repository.DeleteMovie(movie);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return Unit.Value;
    }
}