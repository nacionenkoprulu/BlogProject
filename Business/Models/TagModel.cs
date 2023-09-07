#nullable disable

using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class TagModel : RecordBase
    {

        #region Entity'den gelenler
        
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        public bool IsPopular { get; set; }

        #endregion

        #region Model için gerekli özellikler

        [DisplayName("Is Popular")]
        public string IsPopularDisplay { get; set; }


        #endregion


    }
}
