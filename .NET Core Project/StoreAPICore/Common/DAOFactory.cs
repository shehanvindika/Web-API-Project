using StoreAPICore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Common
{
    public class DAOFactory
    {
        public static CustomerDAO CreateCustomerDAO()
        {
            CustomerDAO customerDAO = new CustomerDAOImpl();
            return customerDAO;
        }

        public static OrderDAO CreateOrderDAO()
        {
            OrderDAO orderDAO = new OrderDAOImpl();
            return orderDAO;
        }

        public static SupplierDAO CreateSupplierDAO()
        {
            SupplierDAO supplierDAO = new SupplierDAOImpl();
            return supplierDAO;
        }

        public static ProductDAO CreateProductDAO()
        {
            ProductDAO productDAO = new ProductDAOImpl();
            return productDAO;
        }
    }
}
