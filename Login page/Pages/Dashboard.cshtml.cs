using Login_page.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Login_page.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public DashboardModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public User CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser == null)
            {
                return Challenge();
            }

            return Page();
        }

        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            if (CurrentUser == null)
            {
                return NotFound("User not found");
            }
            return new JsonResult(new
            {
                Name = CurrentUser.Name,
                ConsumerNumber = CurrentUser.ConsumerNumber,
                MeterSerialNumber = CurrentUser.MeterSerialNumber,
                BalanceAmount = CurrentUser.BalanceAmount,
                Consumption = CurrentUser.Consumption
            });
        }
        public IActionResult OnPostLogout()
        {
            return RedirectToPage("/Login");
        }
    }
}