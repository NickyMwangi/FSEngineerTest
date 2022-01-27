using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.General
{
    public class ApplicationMenuDto: BaseDto<BaseDtoLine>
    {
        public string Code { get; set; }
        public string MenuTitle { get; set; }
        public string MenuName { get; set; }
        public string ParentId { get; set; }
        public string UrlTitle { get; set; }
        public string RouterLink { get; set; }
        public string Component { get; set; }
        public string RouteData { get; set; }
        public int MenuOrder { get; set; }
        public string MenuIcon { get; set; }
        public int? MenuLevel { get; set; }
        public string Target { get; set; }
        public bool ExpandOnly { get; set; }
        public bool? HasSubMenu { get; set; }
        public bool? IsVisible { get; set; }
    }
}
