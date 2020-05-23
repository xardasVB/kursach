using DAL.Abstract;
using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;

        public void CommitTransaction()
        {
            _transaction.Complete();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
        }

        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }
    }
}
