
namespace BusinessLogic.Entities
{
    public class Image:BaseNameEntity
    {
        public int Priority { get; set; }
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }
    }
}
