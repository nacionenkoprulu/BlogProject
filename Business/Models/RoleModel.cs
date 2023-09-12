#nullable disable

using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class RoleModel : RecordBase
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }



    }
}
