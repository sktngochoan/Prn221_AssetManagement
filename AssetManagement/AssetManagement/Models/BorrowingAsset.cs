﻿using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public partial class BorrowingAsset
    {
        public int Id { get; set; }
        public DateTime? BorrowDate { get; set; }
        public int? AssetId { get; set; }
        public bool? Status { get; set; }
        public int? Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public int? BorrowerId { get; set; }
        public DateTime? RetrurnDate { get; set; }

        public virtual Asset? Asset { get; set; }
        public virtual User? Borrower { get; set; }
    }
}
