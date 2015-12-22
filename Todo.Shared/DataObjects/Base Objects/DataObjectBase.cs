using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// This class represents the base class for all data/meta objects. Exposes common properties applicable for all objects.
    /// For eg: Id, Audit fields , State Change fields
    /// </summary>
    [Serializable]
    public class DataObjectBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of DataObjectBase.
        /// </summary>
        /// <param name="id">Unique identifier of the object.</param>
        public DataObjectBase(Guid id)
        {
            this.Initialize(id);
        }

        /// <summary>
        /// Initializes a new instance of DataObjectBase.
        /// </summary>
        public DataObjectBase():this(Guid.NewGuid())
        {
            // Do nothing.
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets / Sets the Id value of the Base Object.
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets/sets the User Id who created the object.
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid CreatedByUserId { get; set; }
        /// <summary>
        /// Gets/sets the User Id who Last Modified the object.
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid LastModifiedByUserId { get; set; }
        /// <summary>
        /// Gets/sets the date and time the object was created.
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// Gets / sets the date and time the object was last modified.
        /// </summary>
        public DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// Gets/sets whether or not the object is new.
        /// </summary>
        [BsonIgnore]
        public bool IsNew { get; set; }
        /// <summary>
        /// Gets/sets whether or not the object has changed.
        /// </summary>
        [BsonIgnore]
        public bool IsChanged { get; set; }
        /// <summary>
        /// Gets/sets whether or not the object is deleted.
        /// </summary>
        [BsonIgnore]
        public bool IsDeleted { get; set; }

        #endregion Properties

        #region Methods
        /// <summary>
        /// This method initializes the base object with default values.
        /// </summary>
        /// <param name="id"></param>
        private void Initialize(Guid id)
        {
            this.Id = id;
            this.CreatedTime = DateTime.UtcNow;
            this.LastModifiedTime = DateTime.UtcNow;

            this.IsNew = true;
        }

        #endregion Methods
    }
}
