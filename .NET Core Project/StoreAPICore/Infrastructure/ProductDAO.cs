using StoreAPICore.Common;
using StoreAPICore.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Infrastructure
{
    public interface ProductDAO
    {
        void InsertProduct(Product product,DBConnection dBConnection);
        bool ProductExists(string ProductName, Guid SupplierId, DBConnection dBConnection);
        bool ProductExistsById(Guid ProductId, DBConnection dBConnection);
    }

    public class ProductDAOImpl : ProductDAO
    {
        public void InsertProduct(Product product, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "insert into Product (ProductId,ProductName,UnitPrice,SupplierId,CreatedOn,IsActive) values (@ProductId,@ProductName,@UnitPrice,@SupplierId,@CreatedOn,@IsActive)";
            dBConnection.cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
            dBConnection.cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            dBConnection.cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            dBConnection.cmd.Parameters.AddWithValue("@SupplierId", product.SupplierId);
            dBConnection.cmd.Parameters.AddWithValue("@CreatedOn", product.CreatedOn);
            dBConnection.cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
            dBConnection.cmd.ExecuteNonQuery();
        }

        public bool ProductExists(string ProductName,Guid SupplierId ,DBConnection dBConnection)
        {
            bool IsExists = false;
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "select * from Product where ProductName='" + ProductName + "' and SupplierId ='"+SupplierId+"'";
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

        public bool ProductExistsById(Guid ProductId, DBConnection dBConnection)
        {
            bool IsExists = false;
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "select * from Product where ProductId='" + ProductId + "'";
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
