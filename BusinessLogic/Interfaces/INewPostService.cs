using BusinessLogic.Models.NewPostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.Models.NewPostModels.NPSettlementResponseViewModel;

namespace BusinessLogic.Interfaces
{
    public interface INewPostService
    {
        Task<IEnumerable<NPAreaItemViewModel?>> GetAreas();
        Task<IEnumerable<NPSettlementItemViewModel?>> GetCities();
    }
}
