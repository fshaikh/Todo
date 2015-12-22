using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using Server.DataAccess;



namespace DataAccess.Tests
{
    [TestClass]
    public class DataAccessTodoUnitTest : DataAccessTestBase
    {
        [TestMethod]
        public void DataAccess_Todo_Insert()
        {
                TodoDA todoDA = new TodoDA(GetConnectionInfo());

                Todo todo = GetNewTodo();

                todoDA.Save(todo);
                Console.ReadLine();
        }

        [TestMethod]
        public void DataAccess_Todo_Update_Title()
        {
            string id = "ec7447c2-92a5-4bbc-b667-d510d7257e37";
            Todo todo = new Todo(new Guid(id));
            todo.Title = "Todo - updated";
            todo.IsChanged = true;
            todo.IsNew = false;

            TodoDA todoDA = new TodoDA(GetConnectionInfo());
            todoDA.Save(todo);
        }

        [TestMethod]
        public void DataAccess_Todo_Remove()
        {
            string id = "ec7447c2-92a5-4bbc-b667-d510d7257e37";
            Todo todo = new Todo(new Guid(id));
            todo.IsChanged = false;
            todo.IsNew = false;
            todo.IsDeleted = true;

            TodoDA todoDA = new TodoDA(GetConnectionInfo());
            todoDA.Save(todo);
        }

        [TestMethod]
        public void DataAccess_Todo_CheckGetTodosForUser()
        {
            string id = "00000000-0000-0000-0000-000000000000";
            TodoDA todoDA = new TodoDA(GetConnectionInfo());
            var todos = todoDA.GetTodosForUser(new Guid(id));
            Assert.AreNotEqual<int>(0, todos.Result.Count);
        }

        [TestMethod]
        public void DataAccess_Todo_GetTodosByState()
        {
            string id = "00000000-0000-0000-0000-000000000000";
            TodoDA todoDA = new TodoDA(GetConnectionInfo());
            var todos = todoDA.GetTodosByState(new Guid(id), TodoState.Active);
            Assert.AreNotEqual<int>(0, todos.Result.Count);
        }

        private Todo GetNewTodo()
        {
            Todo todo = new Todo();
            todo.Title = "Todo 1";
            todo.Description = "todo description";
            todo.Category = new System.Collections.Generic.List<string> { "Personal" };
            todo.State = TodoState.Active;

            return todo;
        }
    }
}
