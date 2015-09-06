using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFInfrastructure.Domain;

namespace CFInfrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterInserting(IAggregateRoot entity, IUnitofWorkRepository unitOfWorkRepository);
        void RegisterDeleting(IAggregateRoot entity, IUnitofWorkRepository unitOfWorkRepository);
        void RegisterUpdating(IAggregateRoot entity, IUnitofWorkRepository unitOfWorkRepsoitory);
        void Commit();
    }
}
