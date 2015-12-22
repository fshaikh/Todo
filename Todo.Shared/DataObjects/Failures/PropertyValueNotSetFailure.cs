using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Failure to represent when the property value is not set. For eg: mandatory values
    /// </summary>
    [Serializable]
    public class PropertyValueNotSetFailure : IFailure
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="PropertyValueNotSetFailure"/>
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="propertyName"></param>
        public PropertyValueNotSetFailure(DataObjectType itemType, string propertyName)
        {
            ItemType = itemType;
            PropertyName = propertyName;
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

        
        #endregion Properties
    }
}
