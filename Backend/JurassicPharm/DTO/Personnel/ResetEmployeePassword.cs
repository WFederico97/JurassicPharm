using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JurassicPharm.DTO.Personnel
{
    public class ResetEmployeePassword : UpdatePersonnelDTO
    {
        public string NewPassword { get; set; }
    }
}