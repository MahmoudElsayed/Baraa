using System;
using System.Collections.Generic;
using System.Linq;

namespace Baraa.BLL.ViewModel
{
    public class UserMenuItem
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public int? Type { get; set; }
        public string Icon { get; set; }
        public List<UserMenuSubItem> SubItems { get; set; }
    }

    public class UserMenuSubItem
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
