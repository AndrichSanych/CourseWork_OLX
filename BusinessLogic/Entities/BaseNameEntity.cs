using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class BaseNameEntity:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
