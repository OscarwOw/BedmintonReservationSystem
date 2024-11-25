using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BedmintonReservationSystem.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }


        public IActionResult OnPost()
        {
            Console.WriteLine($"Login attempt: Username = {Username}, Password = {Password}");


            return RedirectToPage("/Index");
        }
    }
}
