namespace eCommerce.Models.ViewModels
{
    public class ContactOutputModel
    {
        public ContactOutputModel(string phone, string phoneWork, string organization)
        {
            Phone = phone;
            PhoneWork = phoneWork;
            Organization = organization;
        }

        public string Phone { get; set; }
        public string PhoneWork { get; set; }
        public string Organization { get; set; }
    }
}
