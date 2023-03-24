using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Models;

namespace AssetManagement.Pages.Admin.Assets
{
    public class IndexModel : PageModel
    {
        private readonly StockManagemnetContext _context;

        public IndexModel(StockManagemnetContext context)
        {
            _context = context;
        }
        [BindProperty]
        public FilterAsset Filter { get; set; }
        public IList<Asset> Asset { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Filter = new FilterAsset();
            Filter.page = 1;
            Filter.status = 3;
            Filter.keyWord = "";
            if (_context.Assets != null)
            {
                Asset = await _context.Assets
                .Include(a => a.Category).ToListAsync();
            }
        }
    }
}
