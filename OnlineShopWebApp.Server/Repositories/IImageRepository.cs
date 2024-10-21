using OnlineShop.API.Models.Domain;

namespace OnlineShop.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
