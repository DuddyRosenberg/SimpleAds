using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SimpleAds.Data
{
    public class GiveAwayDB
    {
        private string _connectionString;
        public GiveAwayDB(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Post> GetPosts()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Posts p
                                        LEFT JOIN Users u ON p.UserID = u.ID";
                connection.Open();
                var reader = command.ExecuteReader();
                List<Post> posts = new List<Post>();
                while (reader.Read())
                {
                    posts.Add(new Post
                    {
                        ID = (int)reader["ID"],
                        Name = reader.GetOrNull<string>("Name"),
                        DatePosted = (DateTime)reader["DatePosted"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Text = (string)reader["Text"],
                        UserID = reader.GetOrNull<int>("UserID"),
                    });
                }
                return posts;
            }
        }
        public List<Post> GetPosts(int userID)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Posts p
                                        LEFT JOIN Users u ON p.UserID = u.ID
                                        WHERE u.ID=@id";
                command.Parameters.AddWithValue("@id", userID);
                connection.Open();
                var reader = command.ExecuteReader();
                List<Post> posts = new List<Post>();
                while (reader.Read())
                {
                    posts.Add(new Post
                    {
                        ID = (int)reader["ID"],
                        Name = reader.GetOrNull<string>("Name"),
                        DatePosted = (DateTime)reader["DatePosted"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Text = (string)reader["Text"],
                        UserID = reader.GetOrNull<int>("UserID"),
                    });
                }
                return posts;
            }
        }
        public void DeletePost(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE Posts WHERE ID=@id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public User GetUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE ID=@id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                return new User
                {
                    ID = (int)reader["ID"],
                    Name = (string)reader["Name"],
                    Email = (string)reader["Email"],
                    PhoneNumber = (string)reader["PhoneNumber"]
                };
            }
        }
        public User GetUser(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE Email=@email";
                command.Parameters.AddWithValue("@email", email);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                return new User
                {
                    ID = (int)reader["ID"],
                    Name = (string)reader["Name"],
                    Email = (string)reader["Email"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    PasswordHash = (string)reader["PasswordHash"]
                };
            }
        }
        public void AddPost(Post post)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Posts(DatePosted, Text, UserID) " +
                                      "VALUES(@date, @text,@userID)";
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@text", post.Text);
                command.Parameters.AddWithValue("@userID", post.UserID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
