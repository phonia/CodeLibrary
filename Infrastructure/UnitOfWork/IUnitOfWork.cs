using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Domain;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterInsertion(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void RegisterDeletion(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void RegisterUpdation(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void Commit();
    }
}
