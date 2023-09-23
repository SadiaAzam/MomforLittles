namespace MomforLittles.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        public int FEEDBACK_ID { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string FEEDBACK_DETAIL { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string FEEDBACK_EMAIL { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d{11}", ErrorMessage = "Please enter 11 digit Mobile No.")]
        public string FEEDBACK_CONTACT { get; set; }

        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string FEEDBACK_ADDRESS { get; set; }

        public int? CUSTOMER_FID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[a-zA-Z\x20]*$", ErrorMessage = "Only Alphabets allowed.")]
        [DataType(DataType.Text)]
        public string FEEDBACK_NAME { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
