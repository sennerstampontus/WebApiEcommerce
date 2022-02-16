namespace eCommerce.Models.UpdateModels
{
    public class AddressUpdateModel
    {
        private string streetName;
        private string postalCode;
        private string city;
        private string country;

        public string StreetName { get { return streetName; } set { streetName = value.Trim(); } }
        public string PostalCode { get { return postalCode; } set { postalCode = value.Trim().Replace(" ", "").Replace("-", ""); } }
        public string City { get { return city; } set { city = value.Trim(); } }
        public string Country { get { return country; } set { country = value.Trim(); } }
    }
}
