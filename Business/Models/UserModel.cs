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
    public class UserModel : RecordBase
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }



        //Gösterim için eklenenler
        public RoleModel Role { get; set; } = new RoleModel();



    }
}
