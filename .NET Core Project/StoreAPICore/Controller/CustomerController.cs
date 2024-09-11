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
        DataTable GetAllCustomers();

    }

    public class CustomerControllerImpl : CustomerController
    {
        DBConnection conn;
        CustomerDAO customerDAO = DAOFactory.CreateCustomerDAO();
        string message;
        public string CreateCustomer(InputCustomer customer)
        {
            List<Customer> customerList = new List<Customer>();

            try
            {
                conn = new DBConnection();

                //Check Customer Already created by mail address
                customerList = customerDAO.GetAllCustomers(conn);

                bool flagMail = false;
                bool flagUserName = false;

                //Check User Name Already Exists 
                foreach (Customer cus in customerList) 
                {
                    if (cus.Email == customer.Email)
                    {
                        flagMail = true;
                    }

                    if (cus.UserName == customer.UserName) 
                    {
                        flagUserName = true;
                    }
                }

                if (flagMail) 
                {
                    message = "Mail Address is already Exists..";
                }
                else if (flagUserName) 
                {
                    message = "User name is already Exists..";
                }
                else
                {
                    Guid cusId = Guid.NewGuid();

                    Customer user = new Customer();
                    user.UserId = cusId;
                    user.UserName = customer.UserName;
                    user.Email = customer.Email;
                    user.FirstName = customer.FirstName;
                    user.LastName = customer.LastName;
                    user.CreatedOn = DateTime.Now;
                    user.IsActive = true;
                    customerDAO.CreateCustomer(user, conn);

                    message = "Customer Account Created Successfully.Your Customer Id is " + cusId;
                }
                
                return message;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                return exp.ToString();
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

                if (customerDAO.CustomerExists(customerId,conn))
                {
                    customerDAO.DeleteCustomer(customerId, conn);
                    message = "Customer Account deleted Successfully.";
                }
                else
                {
                    message = "Customer Account does not exists...";
                }

                
                return message;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                return exp.ToString();
            }
            finally
            {
                if (conn.con.State == System.Data.ConnectionState.Open)
                {
                    conn.Commit();
                }
            }
        }

        public DataTable GetAllCustomers()
        {

            List<Customer> customerList = new List<Customer>();
            DataTable dataTable = new DataTable();
            
            try
            {
                dataTable.Columns.Add("Data", typeof(List<Customer>));
                dataTable.Columns.Add("Message",typeof(string));

                conn = new DBConnection();
                customerList = customerDAO.GetAllCustomers(conn);
                
                dataTable.Rows.Add(customerList,"Data Successfully retrieved..");
                return dataTable;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                dataTable.Rows.Add(null,exp.ToString());
                return null;
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

                if (customerDAO.CustomerExists(customer.UserId, conn))
                {
                    customerDAO.UpdateCustomer(customer, conn);
                    message = "Customer Account updated Successfully.";
                }
                else
                {
                    message = "Customer Account does not exists...";
                }
                
                return message;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                return exp.ToString();
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
