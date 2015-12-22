using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Failure when property value is required to be unique in the system. For eg: username, email must be unique
    /// </summary>
    [Serializable]
    public class PropertyValueNotUniqueFailure : IFailure
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of PropertyValueNotUniqueFailure
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="itemId"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        public PropertyValueNotUniqueFailure(DataObjectType objectType, string itemId, string propertyName, string propertyValue)
        {
            ItemType = objectType;
            ItemId = itemId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the item type
        /// </summary>
        public DataObjectType ItemType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the item id
        /// </summary>
        public string ItemId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        /// <value>
        /// The value of the property.
        /// </value>
        public string PropertyValue
        {
            get;
            private set;
        }
        #endregion Properties

        #region IFailure Methods
        #endregion Methods
    }
}
