using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JurassicPharm.DTO.InvoiceDetail
{
    public class InvoiceDetailResponseDTO
    {
        public string SupplyName { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }
    }
}