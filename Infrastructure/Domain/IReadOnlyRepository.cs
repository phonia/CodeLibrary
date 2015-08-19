using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Domain
{
    public interface IReadOnlyRepository<T,Tld> where T:IAggregateRoot
    {
        T GetByKey(Tld key);
        List<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> exp);
        List<T> GetALL();
    }
}
