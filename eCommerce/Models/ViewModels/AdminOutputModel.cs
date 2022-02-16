namespace eCommerce.Models.ViewModels
{
    public class AdminOutputModel
    {
        public AdminOutputModel()
        {

        }

        public AdminOutputModel(int id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DisplayName = $"{firstName} {lastName}";
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
