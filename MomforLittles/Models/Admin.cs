namespace MomforLittles.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int ADMIN_ID { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[a-zA-Z\x20]*$", ErrorMessage = "Only Alphabets allowed.")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string ADMIN_NAME { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string ADMIN_EMAIL { get; set; }


        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 8 characters required")]
        public string ADMIN_PASSWORD { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d{11}", ErrorMessage = "Please enter 11 digit Mobile No.")]
        public string ADMIN_CONTACT { get; set; }

        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        public string ADMIN_ADDRESS { get; set; }

        [StringLength(100)]
        public string ADMIN_PROFILE { get; set; }

        [NotMapped]
        public HttpPostedFileBase PRO_PIC { get; set; }
    }
}
