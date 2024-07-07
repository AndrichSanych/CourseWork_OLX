using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class City:BaseNameEntity
    {
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public ICollection<Advert> Adverts { get; set; } = new HashSet<Advert>();
    }
}
