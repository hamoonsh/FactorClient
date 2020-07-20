using System;
using System.Collections.Generic;


namespace FactorClient.Models
{

    public class Invoice
    {
        public long InvoiceID { get; set; }
        public long UserID { get; set; }

        public DateTime RegDate { get; set; }

        public bool IsPayed { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
