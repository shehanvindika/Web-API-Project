using StoreAPICore.Common;
using StoreAPICore.Domain;
using StoreAPICore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Controller
{
    public interface CustomerController
    {
        string CreateCustomer(InputCustomer customer);
        string UpdateCustomer(Customer customer);
        string DeleteCustomer(Guid customerId);
        List<Customer> GetAllCustomers();

    }

    public class CustomerControllerImpl : CustomerController
    {
        DBConnection conn;
        CustomerDAO customerDAO = DAOFactory.CreateCustomerDAO();
        string message;
        public string CreateCustomer(InputCustomer customer)
        {
            
            try
            {
                conn = new DBConnection();
                
                Guid cusId = Guid.NewGuid();

                Customer user = new Customer();
                user.UserId = cusId;
                user.UserName = customer.UserName;
                user.Email = customer.Email;
                user.FirstName = customer.FirstName;    
                user.LastName = customer.LastName;
                user.CreatedOn = DateTime.Now;
                user.IsActive = true;
                customerDAO.CreateCustomer(user,conn);

                message = "Customer Account Created Successfully.Your Customer Id is "+ cusId;
                return message;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                return exp.ToString();
                throw;
            }
            finally
            {
                if (conn.con.State == System.Data.ConnectionState.Open) 
                {
                    conn.Commit();
                }
            }
            
        }

        public string DeleteCustomer(Guid customerId)
        {
            
            try
            {
                conn = new DBConnection();

                customerDAO.DeleteCustomer(customerId,conn);
                message = "Customer Account deleted Successfully.";
                return message;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                return exp.ToString();
                throw;
            }
            finally
            {
                if (conn.con.State == System.Data.ConnectionState.Open)
                {
                    conn.Commit();
                }
            }
        }

        public List<Customer> GetAllCustomers()
        {

            List<Customer> customerList = new List<Customer>();

            try
            {
                conn = new DBConnection();
                customerList = customerDAO.GetAllCustomers(conn);
                return customerList;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                return null;
                throw;
            }
            finally
            {
                if (conn.con.State == System.Data.ConnectionState.Open)
                {
                    conn.Commit();
                }
            }
        }

        public string UpdateCustomer(Customer customer)
        {
            
            try
            {
                conn = new DBConnection();
                customerDAO.UpdateCustomer(customer,conn);
                message = "Customer details successfully updated.";
                return message;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                return exp.ToString();
                throw;
            }
            finally
            {
                if (conn.con.State == System.Data.ConnectionState.Open)
                {
                    conn.Commit();
                }
            }
        }
    }
}
