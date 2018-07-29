using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace Customer.Models
{
    public class DataSort
    {
        public IQueryable<TModel> Sort<TModel>(IQueryable<TModel> QModel, String SortName, string SortDirection)
        {
            QModel = QModel.OrderBy($"{ SortName}  {SortDirection}");
            return QModel;
        }
    }




}