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
    public interface SupplierDAO
    {
        void CreateSupplier(Supplier supplier,DBConnection dBConnection);
        bool SupplierExistsById(Guid SupplierId, DBConnection dBConnection);
        bool SupplierExistsByName(string SupplierName, DBConnection dBConnection);
    }

    public class SupplierDAOImpl : SupplierDAO
    {
        public void CreateSupplier(Supplier supplier, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "insert into Supplier (SupplierId,SupplierName,CreatedOn,IsActive) values (@UserId,@UserName,@CreatedOn,@IsActive)";
            dBConnection.cmd.Parameters.AddWithValue("@UserId", supplier.SupplierId);
            dBConnection.cmd.Parameters.AddWithValue("@UserName", supplier.SupplierName);
            dBConnection.cmd.Parameters.AddWithValue("@CreatedOn", supplier.CreatedOn);
            dBConnection.cmd.Parameters.AddWithValue("@IsActive", supplier.IsActive);
            dBConnection.cmd.ExecuteNonQuery();
        }

        public bool SupplierExistsById(Guid SupplierId, DBConnection dBConnection)
        {
            bool IsExists = false;
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "select * from Supplier where SupplierId='" + SupplierId + "'";
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
        public bool SupplierExistsByName(string SupplierName, DBConnection dBConnection)
        {
            bool IsExists = false;
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandType = CommandType.Text;
            dBConnection.cmd.CommandText = "select * from Supplier where SupplierName='" + SupplierName + "'";
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
