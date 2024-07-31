using StoreAPICore.Common;
using StoreAPICore.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Infrastructure
{
    public interface OrderDAO
    {
        void InsertOrder(Order order,DBConnection dBConnection);
        DataTable GetActiveOrdersByCustomer(Guid CustomerId, DBConnection dBConnection);
    }

    public class OrderDAOImpl : OrderDAO
    {
        public void InsertOrder(Order order, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "insert into OrderTable (OrderId,ProductId,OrderStatus,OrderType,OrderBy,OrderOn,ShippedOn,IsActive) values (@OrderId,@ProductId,@OrderStatus,@OrderType,@OrderBy,@OrderedOn,@ShippedOn,@IsActive)";
            dBConnection.cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
            dBConnection.cmd.Parameters.AddWithValue("@ProductId", order.ProductId);
            dBConnection.cmd.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
            dBConnection.cmd.Parameters.AddWithValue("@OrderType", order.OrderType);
            dBConnection.cmd.Parameters.AddWithValue("@OrderBy", order.OrderBy);
            dBConnection.cmd.Parameters.AddWithValue("@OrderedOn", order.OrderedOn);
            dBConnection.cmd.Parameters.AddWithValue("@ShippedOn", order.ShippedOn);
            dBConnection.cmd.Parameters.AddWithValue("@IsActive", order.IsActive);
            dBConnection.cmd.ExecuteNonQuery();
        }
        public DataTable GetActiveOrdersByCustomer(Guid CustomerId, DBConnection dBConnection)
        {
            DataTable data = new DataTable();
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.StoredProcedure;
            dBConnection.cmd.CommandText = "dbo.GetOrdersByCustomerId";
            dBConnection.cmd.Parameters.AddWithValue("CustomerId", CustomerId);

            
            data.Columns.Add("OrderId",typeof(Guid));
            data.Columns.Add("ProductId", typeof(Guid));
            data.Columns.Add("OrderStatus", typeof(decimal));
            data.Columns.Add("OrderType", typeof(decimal));
            data.Columns.Add("CustomerId", typeof(Guid));
            data.Columns.Add("OrderOn", typeof(DateTime)); 
            data.Columns.Add("ShippedOn", typeof(DateTime));
            data.Columns.Add("OrderIsActive", typeof(Boolean));

            data.Columns.Add("ProductName", typeof(string));
            data.Columns.Add("UnitPrice", typeof(decimal));
            data.Columns.Add("ProductCreatedOn", typeof(DateTime));
            data.Columns.Add("ProductIsActive", typeof(Boolean));

            data.Columns.Add("SupplierId", typeof(Guid));
            data.Columns.Add("SupplierName", typeof(string));
            data.Columns.Add("SupplierCreatedOn", typeof(DateTime));
            data.Columns.Add("SupplierIsActive", typeof(Boolean));
            dBConnection.dr = dBConnection.cmd.ExecuteReader();

            while (dBConnection.dr.Read())
            {
                data.Rows.Add(dBConnection.dr.GetGuid(0),
                              dBConnection.dr.GetGuid(1),
                              dBConnection.dr.GetDecimal(2),
                              dBConnection.dr.GetDecimal(3),
                              dBConnection.dr.GetGuid(4),
                              dBConnection.dr.GetDateTime(5),
                              dBConnection.dr.GetDateTime(6),
                              dBConnection.dr.GetBoolean(7),
                              dBConnection.dr.GetString(8),
                              dBConnection.dr.GetDecimal(9),
                              dBConnection.dr.GetDateTime(10),
                              dBConnection.dr.GetBoolean(11),
                              dBConnection.dr.GetGuid(12),
                              dBConnection.dr.GetString(13),
                              dBConnection.dr.GetDateTime(14),
                              dBConnection.dr.GetBoolean(15));
            }

            dBConnection.dr.Close();
            return data;
        }
    }
}
