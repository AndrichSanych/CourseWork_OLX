using BusinessLogic.Entities.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Category:BaseNameEntity
    {
        public string Image { get; set; } = string.Empty;
        public ICollection<Advert> Adverts { get; set; } = new HashSet<Advert>();
        public ICollection<CategoryFilter> Filters { get; set; } = new HashSet<CategoryFilter>();
    }
}
