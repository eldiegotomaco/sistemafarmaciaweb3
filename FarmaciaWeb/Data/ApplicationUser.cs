using Microsoft.AspNetCore.Identity;

namespace FarmaciaWeb.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}