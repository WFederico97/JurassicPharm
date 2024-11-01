using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.DTO.InvoiceDetail;

namespace JurassicPharm.DTO.InvoIce
{
    public class InvoiceResponseDTO
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClienLastName { get; set; }
        public DateTime Date { get; set; }
        public string Branch { get; set; }

        public List<InvoiceDetailResponseDTO> Details { get; set; } = new List<InvoiceDetailResponseDTO>();

    }
}