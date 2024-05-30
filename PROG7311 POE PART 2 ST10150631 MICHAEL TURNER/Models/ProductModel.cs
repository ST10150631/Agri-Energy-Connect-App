namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public DateOnly ProductionDate { get; set; }
        public string SupplierName { get; set; }
        public Decimal ProductPrice { get; set; }
        public byte[] ProductImage { get; set; }
    }
}
