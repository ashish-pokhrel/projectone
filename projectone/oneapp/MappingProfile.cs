using AutoMapper;
using oneapp.Entities;
using oneapp.Models;

namespace oneapp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();
        }
    }

}

