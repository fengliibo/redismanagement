using System;
using ServiceStack.Redis;
using ServiceStack.Text;
using ServiceStack.DataAnnotations;
using System.IO;
using System.Configuration;

namespace RedisManagement
{
    class MainClass
    {
        public static void Main(string[] args)
        {
           
            DoRedisString drs = new DoRedisString();
            drs.Append("nick"," is bad guy");
            string nick = drs.Get("nick");
            Console.WriteLine(nick);
            var redisManager = new RedisManagerPool("localhost:6379");
            using (var client = redisManager.GetClient())
            {
                var redisTodos = client.As<Todo>();
                var newTodo = new Todo
                {
                    Id = redisTodos.GetNextSequence(),
                    Content = "Learn Redis",
                    Order = 1,
                };
                redisTodos.Store(newTodo);
                Todo savedTodo = redisTodos.GetById(newTodo.Id);
                "Saved Todo: {0}".Print(savedTodo.Dump());

                savedTodo.Done = true;
                redisTodos.Store(savedTodo);

                var updatedTodo = redisTodos.GetById(newTodo.Id);
                "Updated Todo: {0}".Print(updatedTodo.Dump());

                redisTodos.DeleteById(newTodo.Id);

                var remainingTodos = redisTodos.GetAll();
                "No more Todos:".Print(remainingTodos.Dump());
            }

        }
    }
}
