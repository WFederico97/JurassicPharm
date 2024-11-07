using JurassicPharm.DTO.Branch;
using JurassicPharm.DTO.InvoiceDetail;

namespace JurassicPharm.DTO.InvoIce
{
    public class InvoiceResponseDTO
    {
        public int InvoiceNumber { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public BranchDTO Branch { get; set; }

        public string ClienLastName { get; set; }
        public DateOnly Date { get; set; }

        public List<InvoiceDetailResponseDTO> Details { get; set; } = new List<InvoiceDetailResponseDTO>();

    }
}