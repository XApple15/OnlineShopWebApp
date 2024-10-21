using AutoMapper;
using OnlineShop.API.Models.Domain;
using OnlineShop.API.Models.DTO;

namespace OnlineShop.API.Mappings
{
    public class AutoMapperProfilescs : Profile
    {
        public AutoMapperProfilescs()
        {
            CreateMap<Products, ProductDTO>().ReverseMap();
            CreateMap<Products, AddProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, AddCategoryDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
