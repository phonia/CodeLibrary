using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Domain
{
    public interface IRepository<T,Tld> :IReadOnlyRepository<T,Tld> where T :IAggregateRoot
    {
        void Insert(T t);
        void Update(T t);
        void Delete(Tld key);
    }
}
