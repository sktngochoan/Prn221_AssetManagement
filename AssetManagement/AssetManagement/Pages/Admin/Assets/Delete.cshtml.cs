﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Models;

namespace AssetManagement.Pages.Assets
{
    public class DeleteModel : PageModel
    {
        private readonly AssetManagement.Models.StockManagemnetContext _context;

        public DeleteModel(AssetManagement.Models.StockManagemnetContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Asset Asset { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FirstOrDefaultAsync(m => m.Id == id);

            if (asset == null)
            {
                return NotFound();
            }
            else 
            {
                Asset = asset;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }
            var asset = await _context.Assets.FindAsync(id);
            var assetId = await _context.BorrowingAssets.FindAsync(id);
            if (assetId != null)
            {
                ModelState.AddModelError(string.Empty, "This asset is currently borrowed and cannot be deleted.");
                return RedirectToPage("./List");
            }
            else
            {
                if (asset != null)
                {
                    Asset = asset;
                    _context.Assets.Remove(Asset);
                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./List");
            }
          
        }
    }
}
