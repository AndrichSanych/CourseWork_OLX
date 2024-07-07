using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface ICitiesService
    {
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<CityDto> GetByIdAsync(int id);
        Task<IEnumerable<CityDto>> GetByAreaIdAsync(int id);
    }
}
