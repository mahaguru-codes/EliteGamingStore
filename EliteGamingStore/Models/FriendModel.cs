using EliteGamingStore.Classes;
using EliteGamingStore.Models.Beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EliteGamingStore.Models
{
    public class FriendModel
    {
        SqlConnectionManager connection = new SqlConnectionManager();
        SqlCommand command = new SqlCommand();

        public Result<List<Friend>> GetFriends()
        {
            Result<List<Friend>> result = new Result<List<Friend>>();
            List<Friend> friends = new List<Friend>();
            try
            {
                command = new SqlCommand();

                command.CommandText = "SELECT * FROM FRIENDS";
                var dt = connection.ExecuteSelectorQuery(command);

                if(dt.Rows.Count > 0)
                {
                    friends = dt.AsEnumerable().OrderBy(row => row.Field<string>("GENDER")).Select(row =>
                        new Friend()
                        {
                            RowId = row.Field<long>("ROWID"),
                            Name = row.Field<string>("NAME"),
                            Address = row.Field<string>("ADDRESS"),
                            Gender = row.Field<string>("GENDER")
                    }).ToList();

                    result.isSuccess = true;
                    result.Data = friends;
                }
            }
            catch(Exception err)
            {
                result.isSuccess = false;
                result.exception = err;
            }

            return result;
        }

        public Result<Friend> CreateFriend(Friend friend)
        {
            Result<Friend> result = new Result<Friend>();
            try
            {
                command = new SqlCommand();

                command.CommandText = "INSERT INTO FRIENDS VALUES(@name, @address, @gender)";

                command.Parameters.Add("@name", SqlDbType.VarChar).Value = friend.Name;
                command.Parameters.Add("@address", SqlDbType.VarChar).Value = friend.Address;
                command.Parameters.Add("@gender", SqlDbType.VarChar).Value = friend.Gender;

                int i = connection.ExecuteDMLQuery(command);
                if(i > 0)
                {
                    result.isSuccess = true;
                    result.message = "Friend has been created successfully!";
                    result.Data = friend;
                }
                else
                {
                    result.isSuccess = false;
                    result.message = "Friend creation has been failed due to an internal error!";
                }
            }
            catch(Exception err)
            {
                result.isSuccess = false;
                result.message = err.Message;
                result.exception = err;
            }

            return result;
        }
    }
}
