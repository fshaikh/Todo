using System;

using DataObjects;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Server.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataAccessBase<T> : IDataAccess<T> where T : DataObjectBase
    {
        #region Members
        private ConnectionInfo _connectionInfo;
        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        #endregion Members

        #region Constructors
        protected DataAccessBase(ConnectionInfo connectionInfo)
        {
            
            _connectionInfo = connectionInfo;
            _mongoClient = new MongoClient(connectionInfo.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(connectionInfo.DatabaseName);
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the MongoDatabase
        /// </summary>
        protected IMongoDatabase Database
        {
            get
            {
                return _mongoDatabase;
            }
        }
        #endregion Properties

        #region Helper Methods



        #endregion Helper Methods

        #region Interface Methods
        public virtual Task<bool> Save(T item)
        {
            // do nothing
            throw new NotImplementedException();
        }

        public virtual Task<T> Get(object id)
        {
            throw new NotImplementedException();
        }

        public virtual BaseCollection<T> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion Interface Methods
    }
}
