using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class UserAdvert:BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; }
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }
    }
}
