namespace eCommerce.Models.UpdateModels
{
    public class ContactUpdateModel
    {
        private string phone;
        private string phoneWork;
        private string organization;

        public string Phone { get { return phone; } set { phone = value.Trim().Replace("-", "").Replace(" ", "").Substring(0,10); } }
        public string PhoneWork { get { return phoneWork; } set { phoneWork = value.Trim().Replace("-", "").Replace(" ", ""); } }
        public string Organization { get { return organization; } set { organization = value.Trim(); } }
    }
}
