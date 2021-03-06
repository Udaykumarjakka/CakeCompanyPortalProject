using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.DataAccess
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(object id);

        void Insert(TEntity obj);

        void Update(TEntity obj);

        void Delete(TEntity obj);

        void Delete(object id);

        void DeleteAll();

    }
}
