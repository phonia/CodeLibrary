using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Domain;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        void PersistInsertion(IAggregateRoot enity);
        void PersistUpdation(IAggregateRoot entity);
        void PersistDeletion(IAggregateRoot entity);
    }
}
