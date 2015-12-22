using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common
{
    /// <summary>
    /// DTO for Todo
    /// </summary>
    public class TodoDTO : BaseDTO
    {
        #region Properties
        /// <summary>
        /// Gets/sets the title of todo
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets/sets the description of todo
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets/sets the Owner of the Todo
        /// </summary>
        public Guid Owner { get; set; }
        /// <summary>
        /// Gets/sets whether the todo is completed
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Gets/sets the category the Todo belongs to
        /// </summary>
        public List<string> Category { get; set; }
        /// <summary>
        /// Gets/sets the state of the Todo.
        /// </summary>
        public TodoState State { get; set; }
        #endregion Properties
    }
}
