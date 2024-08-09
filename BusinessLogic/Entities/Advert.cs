
using BusinessLogic.Entities.Filter;

namespace BusinessLogic.Entities
{
    public class Advert:BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPersone { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsNew { get; set; }

        public bool IsVip { get; set; }

        public bool IsContractPrice { get; set; }

        public decimal Price { get; set; }

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();
        public ICollection<UserAdvert> UserFavouriteAdverts { get; set; } = new HashSet<UserAdvert>();
        public ICollection<AdvertValue> Values { get; set; } = new HashSet<AdvertValue>();
    }
}
