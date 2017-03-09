using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CECS_550_Program
{
    public class Menu_Items
    {
        public string Title
        {
            get;
            set;
        }

        public Symbol SymbolIcon
        {
            get;
            set;
        }

        public Type NavigateTo
        {
            get;
            set;
        }
    }
}
