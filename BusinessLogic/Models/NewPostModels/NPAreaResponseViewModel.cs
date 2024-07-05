using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.NewPostModels
{
    internal class NPAreaResponseViewModel
    {
        public List<NPAreaItemViewModel> Data { get; set; } = [];

    }
    public class NPAreaItemViewModel
    {
        public string Ref { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
