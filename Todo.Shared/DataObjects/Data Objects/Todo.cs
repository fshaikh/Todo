using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DataObjects
{
    /// <summary>
    /// This class represents a Todo
    /// </summary>
    [Serializable]
    [BsonIgnoreExtraElements]
    public class Todo:DataObjectBase
    {
        #region Constructors
        /// <summary>
        /// Constructor to create a new Todo.
        /// </summary>
        public Todo()
            : base()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Todo"/> class.
        /// </summary>
        /// <param name="id">the id</param>
        public Todo(Guid id)
            : base(id)
        {
            Initialize();
        }
        #endregion Constructors

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
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
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

        #region Methods
        /// <summary>
        /// This method initializes the todo object
        /// </summary>
        private void Initialize()
        {
            Category = new List<string>();
            State = TodoState.Active;
        }
        #endregion Methods
    }
}
