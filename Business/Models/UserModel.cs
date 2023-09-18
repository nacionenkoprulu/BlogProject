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
        #region Entity Referans Özelliklerine Karşılık Kullanacağımız Özellikler

        public RoleModel Role { get; set; } = new RoleModel();
        public UserDetailModel UserDetail { get; set; } // kullanıcı detaylarını tek yerden yönetebilmek için hem burada hem de
                                                        // AccountRegisterModel'da referans özelliği olarak kullanıyoruz,
        #endregion


    }
}
