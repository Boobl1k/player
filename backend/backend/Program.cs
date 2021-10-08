using System;
using backend.Database;

namespace backend
{
    static class Program
    {
        static void Main(string[] args)
        {
            Engine.CheckConnection();
            var user = new User(1, "Nick");
            user.Insert();
        }
    }
}