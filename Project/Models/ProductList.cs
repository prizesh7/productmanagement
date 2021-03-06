//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductList()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public string YourQuantity { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
