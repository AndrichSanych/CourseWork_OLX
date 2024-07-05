using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Area : BaseNameEntity
    {
        public ICollection<City> Cities { get; set; } = new HashSet<City>();
    }
}
