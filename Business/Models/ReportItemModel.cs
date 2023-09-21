#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class ReportItemModel
    {
        #region Listeleme için olan özellikler

        [DisplayName("Blog Title")]
        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }

        [DisplayName("Create Date")]
        public string BlogCreateDate { get; set; }

        [DisplayName("Update Date")]
        public string BlogUpdateDate { get; set; }

        public double? Score { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("User Active")]
        public string Active { get; set; }

        [DisplayName("Role")]
        public string RoleName { get; set; }

        public string Tag { get; set; }

        [DisplayName("Tag Popular")]
        public string Popular { get; set; }

        public bool? IsPopular { get; set; }

        [DisplayName("User")]
        public int UserId { get; set; }

        #endregion

        #region Filtreleme için
        public DateTime? BlogCreateDateInput { get; set; }

        public DateTime? BlogUpdateDateInput { get; set; } 
        #endregion





    }
}
