using StoreAPICore.Common;
using StoreAPICore.Domain;
using StoreAPICore.Infrastructure;

namespace StoreAPICore.Controllers
{
    public interface SupplierController
    {
        string CreateSupplierAccount(InputSupplier supplier);
    }

    public class SupplierControllerImpl : SupplierController
    {
        DBConnection conn;
        string message;
        SupplierDAO supplierDAO =DAOFactory.CreateSupplierDAO();
        public string CreateSupplierAccount(InputSupplier supplier)
        {
            try
            {
                conn = new DBConnection();

                if (supplierDAO.SupplierExistsByName(supplier.SupplierName, conn))
                {
                    message = "Supplier name already exists..";
                }
                else
                {
                    Guid cusId = Guid.NewGuid();

                    Supplier user = new Supplier();
                    user.SupplierId = cusId;
                    user.SupplierName = supplier.SupplierName;
                    user.CreatedOn = DateTime.Now;
                    user.IsActive = true;
                    supplierDAO.CreateSupplier(user, conn);

                    message = "Supplier Account Created Successfully.Your Supplier Id is " + cusId;
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
    }
}
