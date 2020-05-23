using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ISqlProvider
    {
        TModel GetByPage<TModel>(int page, int items, string searchName);
        List<TModel> GetAll<TModel>();
        TModel GetById<TModel>(int id);
        TModel Delete<TModel>(int id);
        TModel Create<TModel>(TModel country);
        TModel Edit<TModel>(TModel country, int id);
    }
}
