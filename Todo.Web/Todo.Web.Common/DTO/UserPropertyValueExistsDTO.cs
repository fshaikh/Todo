using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common
{
    /// <summary>
    /// DTO to represent the property value exists or not
    /// </summary>
    public class UserPropertyValueExistsDTO
    {
        /// <summary>
        /// Gets/sets the property name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets/sets the property value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Gets/sets whether value exists
        /// </summary>
        public Boolean Exists { get; set; }
    }
}
