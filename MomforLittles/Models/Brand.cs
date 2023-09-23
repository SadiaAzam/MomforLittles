namespace MomforLittles.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Brand")]
    public partial class Brand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int BRAND_ID { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[a-zA-Z\x20]*$", ErrorMessage = "Only Alphabets allowed.")]
        [DataType(DataType.Text)]
        public string BRAND_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string BRAND_TYPE { get; set; }

        [StringLength(50)]
        public string CORRESPOND_TITLE { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[a-zA-Z\x20]*$", ErrorMessage = "Only Alphabets allowed.")]
        [DataType(DataType.Text)]
        public string CORRESPOND_NAME { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d{11}", ErrorMessage = "Please enter 11 digit Mobile No.")]
        public string BRAND_PHONE { get; set; }

        [StringLength(50)]
        public string BRAND_TEL { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        public string BRAND_ADDRESS { get; set; }

        [StringLength(50)]
        public string BRAND_LOGO { get; set; }
        [NotMapped]
        public HttpPostedFileBase PRO_PIC { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string BRAND_EMAIL { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 8 characters required")]
        public string BRAND_PASSWORD { get; set; }

        [StringLength(50)]
        public string BRAND_NTN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}