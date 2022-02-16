using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace eCommerce.Models.Entities
{
    public class CustomerEntity
    {
        public CustomerEntity(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public CustomerEntity(string firstName, string lastName, string email, ContactEntity contact)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Contact = contact;
        }

        public CustomerEntity(int id, string firstName, string lastName, string email, AddressEntity address, ContactEntity contact)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Contact = contact;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required]
        public byte[] Hash { get; private set; }
        [Required]
        public byte[] Salt { get; private set; }

        [Required]
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; }

        public int ContactId { get; set; }
        public ContactEntity Contact { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }

        public void CreateSecurePassword(string password)
        {
            using var hmac = new HMACSHA512();
            Salt = hmac.Key;
            Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool CompareSecurePassword(string password)
        {
            using (var hmac = new HMACSHA512(Salt))
            {
                var _hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));


                {
                    for (int i = 0; i < _hash.Length; i++)
                    {
                        if (_hash[i] != Hash[i])
                            return false;
                    }
                }
            }

            return true;
        }
    }
}
