using ShopSphere_API.Entities;
namespace ShopSphere_API.Entities
{
    public class User
    {
        public int  Id { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }
        public string  Password { get; set; }
        public string Role { get; set; }

    }
}
