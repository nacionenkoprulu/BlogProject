#nullable disable

using Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Areas.Report.Models
{
    public class HomeIndexViewModel
    {

        public IEnumerable<ReportItemModel> Reports { get; set; }

        public FilterItemModel Filter { get; set; }

        public SelectList UserSelectList { get; set; }

        public SelectList RoleSelectList { get; set; }
    }
}
