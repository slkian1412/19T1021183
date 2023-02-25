﻿using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021183.DataLayers.SQL_Server
{
    public class EmployeeAccountDAL : _BaseDAL, IUserAccountDAL
    {
        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Employees WHERE Email=@Email AND Password=@Password";
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue ("@Password", password);
                cmd.CommandType = System.Data.CommandType.Text;

                using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            UserID = Convert.ToString(dbReader["EmployeeID"]),
                            UserName = Convert.ToString(dbReader["Email"]),
                            FullName = $"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = "",
                            RoleNames = "",
                        };
                    }
                        dbReader.Close();
                }

                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
