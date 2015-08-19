using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Domain;
using Infrastructure.UnitOfWork;
using Infrastructure.DataAcess;
using Infrastructure.Configuration;

namespace CodeGeneration.Repository
{
    public enum PersistCategories
    {
        Insert,Update,Delete
    }

    public class PersistUnit
    {
        public IUnitOfWorkRepository UnitOfWorkRepository { get; set; }
        public PersistCategories PersistCategory { get; set; }
        public IAggregateRoot Entity { get; set; }
    }

    public class UnitOfWork:IUnitOfWork
    {
        private SQLHelper.SQLHelper m_sqlHelper = null;
        private IDataAcess m_dataAcess = null;
        private Queue<PersistUnit> m_queue = new Queue<PersistUnit>();

        public UnitOfWork(SQLHelper.SQLHelper sqlHelper, IDataAcess dataAcess)
        {
            m_dataAcess = dataAcess;
            m_sqlHelper = sqlHelper;
            if (m_queue == null)
                m_queue = new Queue<PersistUnit>();
        }

        public void RegisterInsertion(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            PersistUnit unit = new PersistUnit() { Entity = entity, UnitOfWorkRepository = unitOfWorkRepository, PersistCategory = PersistCategories.Insert };
            if (!m_queue.Contains(unit))
            {
                m_queue.Enqueue(unit);
            }
            else
                throw new Exception("there is a existence entity in in RegisterInsertion!");
        }

        public void RegisterDeletion(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            PersistUnit unit = new PersistUnit() { Entity = entity, UnitOfWorkRepository = unitOfWorkRepository, PersistCategory = PersistCategories.Delete };
            if (!m_queue.Contains(unit))
            {
                m_queue.Enqueue(unit);
            }
            else
                throw new Exception("there is a existence entity in in RegisterDeletion!");
        }

        public void RegisterUpdation(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            PersistUnit unit = new PersistUnit() { Entity = entity, UnitOfWorkRepository = unitOfWorkRepository, PersistCategory = PersistCategories.Update };
            if (!m_queue.Contains(unit))
            {
                m_queue.Enqueue(unit);
            }
            else
                throw new Exception("there is a existence entity in RegisterUpdation!");
        }

        public void Commit()
        {
            if ((m_queue.Count > 1))
            {
                m_dataAcess.OpenTransaction();
            }
            else
            {
                m_dataAcess.EndTransaction();
            }
            using (m_sqlHelper.GetConnection())
            {
                m_sqlHelper.OpenConnection();
                m_sqlHelper.BeginTrans();
                try
                {
                    while (m_queue.Count > 0)
                    {
                        PersistUnit unit = m_queue.Dequeue();
                        switch (unit.PersistCategory)
                        {
                            case PersistCategories.Insert:
                                unit.UnitOfWorkRepository.PersistInsertion(unit.Entity);
                                break;
                            case PersistCategories.Update:
                                unit.UnitOfWorkRepository.PersistUpdation(unit.Entity);
                                break;
                            case PersistCategories.Delete:
                                unit.UnitOfWorkRepository.PersistDeletion(unit.Entity);
                                break;
                            default: break;
                        }
                    }

                    m_sqlHelper.CommitTrans();
                }
                catch (Exception ex)
                {
                    m_sqlHelper.RollBackTans();
                    throw new Exception("unknown error on commit trans!");
                }
            }
        }
    }
}
