using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class CountryModel : RecordBase
    {

        #region Entity'den Kopyalanan Özellikler
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Country Name")]
        public string Name { get; set; }
        #endregion

    }
}
