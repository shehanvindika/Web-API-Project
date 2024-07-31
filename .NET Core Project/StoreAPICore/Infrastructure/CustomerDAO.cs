using StoreAPICore.Common;
using StoreAPICore.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Infrastructure
{
    public interface CustomerDAO
    {
        void CreateCustomer(Customer customer,DBConnection dBConnection);
        void UpdateCustomer(Customer customer, DBConnection dBConnection);
        void DeleteCustomer(Guid customerId, DBConnection dBConnection);
        List<Customer> GetAllCustomers(DBConnection dBConnection);
        bool CustomerExists(Guid CustomerId,DBConnection dBConnection);
    }

    public class CustomerDAOImpl : CustomerDAO
    {
        public void CreateCustomer(Customer customer, DBConnection dBConnection)
        {
            
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "insert into Customer (UserId,UserName,Email,FirstName,LastName,CreatedOn,IsActive) values (@UserId,@UserName,@Email,@FirstName,@LastName,@CreatedOn,@IsActive)";
            dBConnection.cmd.Parameters.AddWithValue("@UserId", customer.UserId);
            dBConnection.cmd.Parameters.AddWithValue("@UserName", customer.UserName);
            dBConnection.cmd.Parameters.AddWithValue("@Email", customer.Email);
            dBConnection.cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            dBConnection.cmd.Parameters.AddWithValue("@LastName", customer.LastName); 
            dBConnection.cmd.Parameters.AddWithValue("@CreatedOn", customer.CreatedOn);
            dBConnection.cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);
            dBConnection.cmd.ExecuteNonQuery();

        }

        public void DeleteCustomer(Guid customerId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "delete from Customer where UserId='"+customerId+"'";
            dBConnection.cmd.ExecuteNonQuery();
        }

        public List<Customer> GetAllCustomers(DBConnection dBConnection)
        {
            List<Customer> data = new List<Customer>();
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "select * from Customer";
            dBConnection.dr = dBConnection.cmd.ExecuteReader();

            while (dBConnection.dr.Read())
            {
                Customer customer = new Customer();
                customer.UserId = dBConnection.dr.GetGuid(0);
                customer.UserName = dBConnection.dr.GetString(1);
                customer.Email = dBConnection.dr.GetString(2);
                customer.FirstName = dBConnection.dr.GetString(3);
                customer.LastName = dBConnection.dr.GetString(4);
                customer.CreatedOn = dBConnection.dr.GetDateTime(5);

                if (dBConnection.dr.GetBoolean(6)) 
                {
                    customer.IsActive = true;
                }
                else
                {
                    customer.IsActive = true;
                }
                data.Add(customer);
            }
            
            dBConnection.dr.Close();
            return data;
        }

        public void UpdateCustomer(Customer customer, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "update Customer set UserName=@UserName,Email=@Email,FirstName=@FirstName,LastName=@LastName,CreatedOn=@CreatedOn,IsActive=@IsActive where UserId = @UserId";
            dBConnection.cmd.Parameters.AddWithValue("@UserId", customer.UserId);
            dBConnection.cmd.Parameters.AddWithValue("@UserName", customer.UserName);
            dBConnection.cmd.Parameters.AddWithValue("@Email", customer.Email);
            dBConnection.cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            dBConnection.cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            dBConnection.cmd.Parameters.AddWithValue("@CreatedOn", customer.CreatedOn);
            dBConnection.cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);
            dBConnection.cmd.ExecuteNonQuery();
        }

        public bool CustomerExists(Guid CustomerId, DBConnection dBConnection)
        {
            bool IsExists= false;
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "select * from Customer where UserId='"+CustomerId+"'";
            dBConnection.dr = dBConnection.cmd.ExecuteReader();

            while (dBConnection.dr.Read()) 
            {
                if (dBConnection.dr.HasRows)
                {
                    IsExists = true;
                }
            }

            dBConnection.dr.Close();
            return IsExists;
        }
    }
}
