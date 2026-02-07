using AutoMapper;
using CineTrack.App.Models;
using CineTrack.Domain.Entities;

namespace CineTrack.App.Mapping;

public class CineTrackProfile : Profile
{
    public CineTrackProfile()
    {
        CreateMap<Genre, GenreDto>();
        CreateMap<Movie, MovieDto>();
    }
}