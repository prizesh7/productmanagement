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
    using System.ComponentModel.DataAnnotations;

    public partial class Retailer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Retailer()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int RetailerId { get; set; }

        [Required()]
        public string Name { get; set; }

        [Required()]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required()]
        [Phone()]
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }

        [Required()]
        public string Address { get; set; }

        [Required()]
        [MinLength(8,ErrorMessage = "Minimum length of password should be 8")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required()]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password And Confirm Password doesn't match")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Contact")]
        [Phone()]
        public string Contact { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
