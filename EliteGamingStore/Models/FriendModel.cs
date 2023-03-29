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

        public List<Friend> GetFriends()
        {
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
                }
            }
            catch(Exception err)
            {

            }

            return friends;
        }
    }
}
