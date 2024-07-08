using BusinessLogic.DTOs;
using BusinessLogic.Models;
using BusinessLogic.Models.AdvertModels;


namespace BusinessLogic.Interfaces
{
    public interface IAdvertService
    {
        Task<IEnumerable<AdvertDto>> GetAllAsync();
        Task<IEnumerable<AdvertDto>> GetByUserEmailAsync(string userEmail);
        Task<AdvertDto> GetByIdAsync(int id);
        Task CreateAsync(AdvertCreationModel advertModel);
        Task UpdateAsync(AdvertUpdateModel advertModel);
        Task DeleteAsync(int id);
    }
}
