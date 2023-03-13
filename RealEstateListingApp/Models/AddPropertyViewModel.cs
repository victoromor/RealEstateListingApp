namespace RealEstateListingApp.Models
{
    public class AddPropertyViewModel
    {
        public string PropertyType { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public decimal SquareFootage { get; set; }
        public DateTime ListingDate { get; set; }
    }
}
