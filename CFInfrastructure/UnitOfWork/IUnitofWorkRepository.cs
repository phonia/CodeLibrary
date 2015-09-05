using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFInfrastructure.Domain;

namespace CFInfrastructure.UnitOfWork
{
    public interface IUnitofWorkRepository
    {
        void PersistInserting(IAggregateRoot entity);
        void PersistDeleting(IAggregateRoot entity);
        void PersistUpdating(IAggregateRoot entity);
    }
}
