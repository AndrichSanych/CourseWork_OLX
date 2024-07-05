using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.NewPostModels
{
    public class NPSettlementResponseViewModel
    {
        public List<NPSettlementItemViewModel> Data { get; set; } = [];
        public class NPSettlementItemViewModel
        {
            public string Ref { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Area { get; set; } = string.Empty;
        }
    }
}
