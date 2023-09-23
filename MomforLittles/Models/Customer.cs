

namespace MomforLittles.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Feedbacks = new HashSet<Feedback>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public int CUSTOMER_ID { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[a-zA-Z\x20]*$", ErrorMessage = "Only Alphabets allowed.")]
        [DataType(DataType.Text)]
        [StringLength(50)]

        public string CUSTOMER_NAME { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]

        public string CUSTOMER_EMAIL { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]

        public string CUSTOMER_ADDRESS { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d{11}", ErrorMessage = "Please enter 11 digit Mobile No.")]

        public string CUSTOMER_CONTACT { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 8 characters required")]
        [StringLength(50)]
        public string CUSTOMER_PASSWORD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}


