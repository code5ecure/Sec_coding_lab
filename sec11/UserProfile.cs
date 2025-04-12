
namespace SerializationSecurity
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public Address UserAddress { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
