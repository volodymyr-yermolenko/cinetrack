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
        CreateMap<AddMovieDto, Movie>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Trim()));
        CreateMap<UpdateMovieDto, Movie>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Trim()));
    }
}