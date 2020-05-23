using BLL.Abstract;
using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class SqlProvider : ISqlProvider
    {
        private readonly ISqlRepository _sqlRepository;

        public SqlProvider(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public TModel Create<TModel>(TModel country)
        {
            throw new NotImplementedException();
        }

        public TModel Delete<TModel>(int id)
        {
            throw new NotImplementedException();
        }

        public TModel Edit<TModel>(TModel country, int id)
        {
            throw new NotImplementedException();
        }

        public List<TModel> GetAll<TModel>()
        {
            throw new NotImplementedException();
        }

        public TModel GetById<TModel>(int id)
        {
            throw new NotImplementedException();
        }

        public TModel GetByPage<TModel>(int page, int items, string searchName)
        {
            throw new NotImplementedException();
        }
    }
}
