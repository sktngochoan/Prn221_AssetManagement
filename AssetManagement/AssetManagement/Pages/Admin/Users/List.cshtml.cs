using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Models;

namespace AssetManagement.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly AssetManagement.Models.StockManagemnetContext _context;

        public IndexModel(AssetManagement.Models.StockManagemnetContext context)
        {
            _context = context;
        }
        private static String USER = "userlogin";
        private static String ROLEJSON = "role";
        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users
                .Include(u => u.Role).ToListAsync();
                if(getUserLogged() != null)
                {
                    ViewData["admin"] = getUserLogged();

                }
            }
        }
        User getUserLogged()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(USER);
            string role = session.GetString(ROLEJSON);
            User user = _context.Users.Where(u => u.Id == int.Parse(jsoncart)).FirstOrDefault();
            if (user != null && role.Equals("admin"))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
