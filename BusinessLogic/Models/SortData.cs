using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class SortData(Expression<Func<Advert, object?>>? sortExpr, bool descending)
    {
        public Expression<Func<Advert, object?>>? SortExpr { get; set; } = sortExpr;
        public bool Descending { get; set; } = descending;
    }
}
