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

        public async Task OnGetAsync(int? page)
        {
            if (_context.Assets != null)
            {
                
                if (Asset == null)
                {
                    Asset = await _context
                                   .Assets
                                   .Include(a => a.Category)
                                   .ToListAsync();
                }
                ViewData["page"] = 1;
                ViewData["status"] = 0;
                if (Asset.Count % 3 == 0)
                {
                    ViewData["totalPage"] = Asset.Count() / 3;
                }
                else
                {
                    ViewData["totalPage"] = Asset.Count() / 3 + 1;
                }
                Asset = await _context
                                   .Assets
                                   .Include(a => a.Category)
                                   .Skip(0)
                                   .Take(5)
                                   .ToListAsync();
            }
        }
        public async Task OnPostAsync(String stat, int? page)
        {
            if (_context.Assets != null)
            {
                Asset = await _context.Assets
                .Include(a => a.Category).ToListAsync();
                if (stat.Equals("0"))
                {
                    Asset = await _context
                                  .Assets
                                  .Include(a => a.Category)
                                  .Skip((int)((page - 1) * 3))
                                  .Take(5)
                                  .ToListAsync();
                    var allAsset = await _context
                                  .Assets
                                  .Include(a => a.Category)                             
                                  .ToListAsync();
                    //Total
                    if (allAsset.Count % 3 == 0)
                    {
                        ViewData["totalPage"] = allAsset.Count() / 3;
                    }
                    else
                    {
                        ViewData["totalPage"] = allAsset.Count() / 3 + 1;
                    }
                    ViewData["page"] = page;
                    ViewData["status"] = 0;
                }
                else
                {
                    bool status;
                    int stats;
                    if (stat.Equals("1"))
                    {
                        status = true;
                        stats = 1;
                    }
                    else
                    {
                        status = false;
                        stats = 2;
                    }
                    Asset = await _context
                                 .Assets
                                 .Include(a => a.Category)
                                 .Skip((int)((page - 1) * 3))
                                 .Take(5)
                                 .Where(a => a.Status == status)
                                 .ToListAsync();
                    var allAsset = await _context
                                 .Assets
                                 .Include(a => a.Category)                                 
                                 .Where(a => a.Status == status)
                                 .ToListAsync();
                    ViewData["page"] = page;
                    ViewData["status"] = stats;
                    if (allAsset.Count % 3 == 0)
                    {
                        ViewData["totalPage"] = allAsset.Count() / 3;
                    }
                    else
                    {
                        ViewData["totalPage"] = allAsset.Count() / 3 + 1;
                    }
                }
            }
        }
    }
}
