//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_Football_Club.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SignUp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SignUp()
        {
            this.Notifications = new HashSet<Notification>();
        }
        [Required(ErrorMessage = "Enter Username :")]
        [Display(Name = "Username  ")]
        [StringLength(maximumLength: 4, MinimumLength = 4, ErrorMessage = "Username length must be 4")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Name :")]
        [Display(Name = " Name  ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Password :")]
        [Display(Name = " Password  ")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Password has to be 6 length")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Email :")]
        [Display(Name = " Email  ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Salary :")]
        [Display(Name = " Salary  ")]
        [Range(0, 200000)]
        public double Salary { get; set; }
        public string Type { get; set; }
        public string ErrorMessage { get; set; }


        public virtual Coach Coach { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual Player Player { get; set; }
    }
}
