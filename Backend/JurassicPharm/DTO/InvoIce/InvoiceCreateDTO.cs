
using System.ComponentModel.DataAnnotations;
using JurassicPharm.DTO.InvoIce;
using JurassicPharm.DTO.InvoiceDetail;

namespace JurassicPharm.DTO.Invoice
{
    public class InvoiceCreateDTO : InvoiceUpdateDTO
    {
        public List<DetailDTO> Details { get; set; } = new List<DetailDTO>();
    }
}