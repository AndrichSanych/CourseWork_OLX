using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class SortModel
    {
        public SortModel(Expression<Func<Advert, object?>>? sortExpr, bool descending)
        {
            SortExpr = sortExpr;
            Descending = descending;
        }

        public Expression<Func<Advert, object?>>? SortExpr { get; set;}
        public bool Descending { get; set;}
    }
}
