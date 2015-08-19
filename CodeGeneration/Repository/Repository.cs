using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.UnitOfWork;
using SQLHelper;
using Base.SQLDataAcess;
using Infrastructure.DataAcess;

namespace CodeGeneration.Repository
{
    /// <summary>
    ///Configuration  PersistLevel
    /// </summary>
    public class RepositoryManager
    {
        public IUnitOfWork m_unitOfWork { get; set; }
        public SQLHelper.SQLHelper m_sqlHelper { get; set; }
        public IDataAcess m_dataAcess { get; set; }
        List<IUnitOfWorkRepository> m_unitOfWorkList = null;

        public RepositoryManager()
        {
            string connectionString = Infrastructure.Configuration.ApplicationSettings.GetPropertyValue("connectionString");
            m_sqlHelper = new SQLHelper.SQLHelper(connectionString);
            m_dataAcess = new DataAccess(m_sqlHelper);
            m_unitOfWork = new UnitOfWork(m_sqlHelper, m_dataAcess);
            m_unitOfWorkList = new List<IUnitOfWorkRepository>();
        }

        public IUnitOfWorkRepository GetUnitOfWorkRepository(Type type)
        {
            IUnitOfWorkRepository val = m_unitOfWorkList.Where(it => it.GetType() == type).FirstOrDefault();

            if (val != null)
            {
                return val;
            }
            else
            {
                //val = (IUnitOfWorkRepository)Assembly.Load(type.Assembly.FullName)
                //    .CreateInstance(type.Namespace + "." + type.Name);
                try
                {
                    val = (IUnitOfWorkRepository)Activator.CreateInstance(type, new object[] { m_unitOfWork, m_sqlHelper, m_dataAcess });
                }
                catch (Exception ex)
                {
                    throw new Exception("");
                }
                if (val != null)
                {
                    m_unitOfWorkList.Add(val);
                    return val;
                }
                else
                {
                    throw new Exception("can't retun " + type.ToString() + " for unknown error");
                }
            }
        }
    }

    public class Sys_objectsRepository
        :Base.SQLRepository.BaseRepository<Model.Sys_objects,System.String>,Infrastructure.Domain.IReadOnlyRepository<Model.Sys_objects,System.String>
    {
        public Sys_objectsRepository(IUnitOfWork unitOfWork, SQLHelper.SQLHelper sqlHelper, IDataAcess dataAcess)
            : base(unitOfWork, sqlHelper, dataAcess)
        { }

        public override Model.Sys_objects GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_objects> GetALL()
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_objects> Get(System.Linq.Expressions.Expression<Func<Model.Sys_objects, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_objects> Get(string cmdText, DbParameter[] parms)
        {
            List<Model.Sys_objects> list = m_dataAcess.Get<Model.Sys_objects>(cmdText, parms);
            return list;
        }
    }

    public class Sys_columnsRepository
        : Base.SQLRepository.BaseRepository<Model.Sys_columns, System.String>, Infrastructure.Domain.IReadOnlyRepository<Model.Sys_columns, System.String>
    {
        public Sys_columnsRepository(IUnitOfWork unitOfWork, SQLHelper.SQLHelper sqlHelper, IDataAcess dataAcess)
            : base(unitOfWork, sqlHelper, dataAcess)
        { }

        public override Model.Sys_columns GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_columns> GetALL()
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_columns> Get(System.Linq.Expressions.Expression<Func<Model.Sys_columns, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_columns> Get(string cmdText, DbParameter[] parms)
        {
            return m_dataAcess.Get<Model.Sys_columns>(cmdText, parms);
        }
    }

    public class Sys_TrelationRepository
        : Base.SQLRepository.BaseRepository<Model.Sys_TRelation, System.String>, Infrastructure.Domain.IReadOnlyRepository<Model.Sys_TRelation, System.String>
    {

        public Sys_TrelationRepository(IUnitOfWork unitOfWork, SQLHelper.SQLHelper sqlHelper, IDataAcess dataAcess)
            : base(unitOfWork, sqlHelper, dataAcess)
        { }
        public override Model.Sys_TRelation GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_TRelation> GetALL()
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_TRelation> Get(System.Linq.Expressions.Expression<Func<Model.Sys_TRelation, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public override List<Model.Sys_TRelation> Get(string cmdText, DbParameter[] parms)
        {
            return m_dataAcess.Get<Model.Sys_TRelation>(cmdText, parms);
        }
    }
}
