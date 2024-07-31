using StoreAPICore.Common;
using StoreAPICore.Domain;
using StoreAPICore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoreAPICore.Controller
{
    public interface OrderController
    {
        string InsertOrderDetails(InputOrder order);
        DataTable GetActiveOrdersByCustomers(Guid customerId);
    }

    public class OrderControllerImpl : OrderController
    {
        DBConnection conn;
        string message;
        OrderDAO orderDAO = DAOFactory.CreateOrderDAO();
        ProductDAO productDAO = DAOFactory.CreateProductDAO();
        CustomerDAO customerDAO =DAOFactory.CreateCustomerDAO();

        public string InsertOrderDetails(InputOrder order)
        {
            try
            {
                conn = new DBConnection();

                Guid orderId = Guid.NewGuid();

                if (customerDAO.CustomerExists(order.OrderBy, conn) && order.OrderBy.ToString().Length == 36)
                {
                    if (productDAO.ProductExists(order.ProductId, conn) && order.ProductId.ToString().Length == 36)
                    {
                        Order item = new Order();
                        item.OrderId = orderId;
                        item.ProductId = order.ProductId;
                        item.OrderStatus = order.OrderStatus;
                        item.OrderType = order.OrderType;

                        item.OrderBy = order.OrderBy;
                        item.OrderedOn = order.OrderedOn;
                        item.ShippedOn = order.ShippedOn;
                        item.IsActive = true;
                        orderDAO.InsertOrder(item, conn);

                        message = "Order inserted Successfully.Order Id is " + orderId;
                    }
                    else
                    {
                        message = "Please check the Product Id.";
                    }
                }
                else 
                {
                    message = "Please check the customer Id.";
                }
                
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
        public DataTable GetActiveOrdersByCustomers(Guid customerId)
        {
            
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Data", typeof(DataTable));
            dataTable.Columns.Add("Message", typeof(string));
            
            try
            {
                conn = new DBConnection();
                DataTable data = new DataTable();
                data = orderDAO.GetActiveOrdersByCustomer(customerId,conn);
                message = "Successfully retrieved.";

                dataTable.Rows.Add(data,message);
                return dataTable;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                message = exp.ToString();
                dataTable.Rows.Add(null, message);
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
