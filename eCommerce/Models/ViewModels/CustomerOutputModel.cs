namespace eCommerce.Models.ViewModels
{
    public class CustomerOutputModel
    {
        public CustomerOutputModel()
        {

        }

        public CustomerOutputModel(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public CustomerOutputModel(int id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public CustomerOutputModel(int id, string firstName, string lastName, string email, AddressOutputModel address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
        }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public AddressOutputModel Address { get; set; }
    }

}
