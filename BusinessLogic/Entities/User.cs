using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class User:IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public DateTime RegisterDate { get; set; }
        public ICollection<Advert> Adverts { get; set; } = new HashSet<Advert>();
        public ICollection<UserAdvert> UserFavouriteAdverts { get; set; } = new HashSet<UserAdvert>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    }
}
