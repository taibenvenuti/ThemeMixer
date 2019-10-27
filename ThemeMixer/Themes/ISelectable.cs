using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThemeMixer.Themes
{
    public interface ISelectable
    {
        bool IsSelected(string themeID);
    }
}
