#nullable disable

using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Role : RecordBase
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }


    }
}
