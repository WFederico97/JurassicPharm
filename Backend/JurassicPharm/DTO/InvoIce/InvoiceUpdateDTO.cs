using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JurassicPharm.DTO.InvoIce
{
    public class InvoiceUpdateDTO
    {

        public int ClientId { get; set; }

        public int BranchId { get; set; }


        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}