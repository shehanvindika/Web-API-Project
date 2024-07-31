using StoreAPICore.Controller;
using StoreAPICore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Common
{
    public class ControllerFactory
    {
        public static CustomerController CreateCustomerController()
        {
            CustomerController customerController = new CustomerControllerImpl();
            return customerController;
        }

        public static OrderController CreateOrderController()
        {
            OrderController orderController = new OrderControllerImpl();
            return orderController;
        }

        public static SupplierController CreateSupplierController()
        {
            SupplierController supplierController = new SupplierControllerImpl();
            return supplierController;
        }

        public static ProductController CreateProductController()
        {
            ProductController productController = new ProductControllerImpl();
            return productController;
        }
    }
}
