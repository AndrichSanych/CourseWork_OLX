using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Models.AdvertModels;


namespace BusinessLogic.Interfaces
{
    public interface IAdvertService
    {
        Task<IEnumerable<AdvertDto>> GetAllAsync();
        Task<SearchResult<Advert,AdvertDto>> GetByFilterAsync(SearchModel<Advert> filter);
        Task<IEnumerable<AdvertDto>> GetByUserEmailAsync(string userEmail);
        Task<AdvertDto> GetByIdAsync(int id);
        Task<IEnumerable<AdvertDto>> getAdvertsAsync(int[] ids);
        Task<IEnumerable<ImageDto>> GetImagesAsync(int id);
        Task<IEnumerable<AdvertDto>> GetVIPAsync(int count);
        Task CreateAsync(AdvertCreationModel advertModel);
        Task UpdateAsync(AdvertCreationModel advertModel);
        Task DeleteAsync(int id);
    }
}
