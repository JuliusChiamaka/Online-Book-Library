using AutoMapper;
using Library.Domain.Entities;
using Library.Shared.Model;
using Microsoft.AspNetCore.Identity;

namespace Library.Api.Profiles
{
    public class DtoProfiles : Profile
    {
        public DtoProfiles()
        {
            // Create.Map<Source, Destination>();
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();
            CreateMap<CategoryCreationDto, Category>();
            CreateMap<SubCategoryCreationDto, SubCategory>();
            CreateMap<RatingCreationDto, Rating>();
            CreateMap<ReviewCreationDto, Review>();
            CreateMap<RegisterDto, IdentityUser>();
            CreateMap<LoginDto, AuthResponseDto>();
            CreateMap<IdentityUser, AuthResponseDto>();
            CreateMap<IdentityUser, UserResponseDto>();
            CreateMap<UserUpdateDto, IdentityUser>();
        }
    }
}
