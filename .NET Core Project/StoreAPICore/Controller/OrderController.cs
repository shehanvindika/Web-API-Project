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
                Order item = new Order();
                item.OrderId = orderId;
                item.ProductId = order.ProductId;
                item.OrderStatus = order.OrderStatus;
                item.OrderType = order.OrderType;

                item.OrderBy = order.OrderBy;
                item.OrderedOn = order.OrderedOn;
                item.ShippedOn = order.ShippedOn;
                item.IsActive = true;

                if (orderDAO.OrderExists(item, conn))
                {
                    message = "Order is already inserted.";
                }
                else
                {
                    if (customerDAO.CustomerExists(order.OrderBy, conn) && order.OrderBy.ToString().Length == 36)
                    {
                        if (productDAO.ProductExistsById(order.ProductId, conn) && order.ProductId.ToString().Length == 36)
                        {
                            
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
            DataTable data = new DataTable();

            try
            {
                conn = new DBConnection();
                
                data = orderDAO.GetActiveOrdersByCustomer(customerId,conn);
                return data;
            }
            catch (Exception exp)
            {
                conn.Rollback();
                message = exp.ToString();
                data.Columns.Add("Error",typeof(string));
                data.Rows.Add(message);
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
