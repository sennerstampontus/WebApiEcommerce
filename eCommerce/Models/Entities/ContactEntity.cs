using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.Entities
{
    public class ContactEntity
    {
        public ContactEntity(string phone, string phoneWork, string organization)
        {
            Phone = phone;
            PhoneWork = phoneWork;
            Organization = organization;
        }

        public ContactEntity(int id, string phone, string phoneWork, string organization)
        {
            Id = id;
            Phone = phone;
            PhoneWork = phoneWork;
            Organization = organization;
        }

        [Key]
        public int Id { get; set; }
        public string Phone { get; set; }
        public string PhoneWork { get; set; }
        public string Organization { get; set; }
        public ICollection<CustomerEntity> Customers { get; set; }
    }
}
