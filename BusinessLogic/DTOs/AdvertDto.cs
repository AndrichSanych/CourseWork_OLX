
namespace BusinessLogic.DTOs
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        public int CityId { get; set; }

        public string CityName { get; set; } = string.Empty;

        public string AreaName { get; set; } = string.Empty;

        public int AreaId { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPersone { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsNew { get; set; }

        public bool IsVip { get; set; }

        public bool IsContractPrice { get; set; }

        public decimal Price { get; set; }

        public string FirstImage { get; set; } = string.Empty;

        public bool isFavorite { get; set; }
    }
}
