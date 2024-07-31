using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Domain
{
    public class Supplier
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean IsActive { get; set; }
    }

    public class InputSupplier
    {
        public string SupplierName { get; set; }
    }
}
