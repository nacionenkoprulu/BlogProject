#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class FilterItemModel
    {
        [DisplayName("Blog Title")]
        public string BlogTitle { get; set; }

        [DisplayName("Blog Date")]
        public DateTime? CreateDateBegin { get; set; } //Seçmediyse null gelecek filtreleme yapmayacağız. Bu yüzden null tanımlıyoruz.

        public DateTime? CreateDateEnd { get; set; } //Seçmediyse null gelecek filtreleme yapmayacağız. Bu yüzden null tanımlıyoruz.

        public double? ScoreBegin { get; set; } //Seçmediyse null gelecek filtreleme yapmayacağız. Bu yüzden null tanımlıyoruz.

        public double? ScoreEnd { get; set; } //Seçmediyse null gelecek filtreleme yapmayacağız. Bu yüzden null tanımlıyoruz.

        [DisplayName("User")]
        public int? UserId { get; set; }

        [DisplayName("Role")]
        public int? RoleId { get; set; }

    }
}
