using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EntitiesLib.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace SampleApp.Services
{
    public interface IUserRepository
    {
        List<User> GetUsers();

        User Get(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<User> GetUsers()
        {
            using var db = new SqlConnection(_connectionString);

            return db.Query<User>("select * from Users").ToList();
        }

        public User Get(int id)
        {
            using var db = new SqlConnection(_connectionString);

            return db.Query<User>("select * from Users where Id = @id", new {id}).FirstOrDefault();
        }
    }
}