using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = Anonymous;
        [Required]
        public string LastName { get; set; } = Anonymous;

        private const string Anonymous = "Anonymous";
    }
}
