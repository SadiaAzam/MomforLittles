namespace MomforLittles.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int PRODUCT_ID { get; set; }

        [StringLength(50)]
        public string PRODUCT_NAME { get; set; }

        [StringLength(50)]
        public string DESCRIPTION { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PURCHASEPRICE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SALEPRICE { get; set; }

        [StringLength(100)]
        public string PICTURE { get; set; }

        [NotMapped]
        public HttpPostedFileBase PRO_PIC { get; set; }

        [NotMapped]
        public int PRO_QUANTITY { get; set; }

        public int? CATEGORY_FID { get; set; }

        public int? BRAND_FID { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
