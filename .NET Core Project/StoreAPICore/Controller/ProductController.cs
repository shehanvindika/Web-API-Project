using StoreAPICore.Common;
using StoreAPICore.Domain;
using StoreAPICore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Controller
{
    public interface ProductController
    {
        string InsertProduct(InputProduct product);
    }

    public class ProductControllerImpl : ProductController
    {
        DBConnection conn = null;
        string message;
        ProductDAO productDAO = DAOFactory.CreateProductDAO();
        SupplierDAO supplierDAO = DAOFactory.CreateSupplierDAO();

        public string InsertProduct(InputProduct product)
        {   

            try
            {
                conn = new DBConnection();

                Guid prdId = Guid.NewGuid();

                if (supplierDAO.SupplierExists(product.SupplierId, conn) && product.SupplierId.ToString().Length == 36)
                {
                    Product item = new Product();
                    item.ProductId = prdId;
                    item.ProductName = product.ProductName;
                    item.UnitPrice = product.UnitPrice;
                    item.SupplierId = product.SupplierId;
                    item.CreatedOn = DateTime.Now;
                    item.IsActive = true;
                    productDAO.InsertProduct(item, conn);

                    message = "Product inserted Successfully.Product Id is " + prdId;
                }
                else
                {
                    message = "Please Check Supplier Id.";
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
