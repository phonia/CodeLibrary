using Infrastructure.DataAcess;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.Service
{
    public abstract class BaseServices
    {
        protected SQLHelper.SQLHelper m_sqlHelper = null;
        protected IDataAcess m_dataAcess = null;
        protected IUnitOfWork m_unitOfWork = null;
        protected Repository.RepositoryManager _manager = new Repository.RepositoryManager();

        public BaseServices()
        {
            m_sqlHelper = _manager.m_sqlHelper;
            m_dataAcess = _manager.m_dataAcess;
            m_unitOfWork = _manager.m_unitOfWork;
        }
    }
}
