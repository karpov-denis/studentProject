using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : TabbedPage
    {
        public Main()
        {

            InitializeComponent();
            ((Tab2)this.Children[1]).PageWithList = ((MainPage)this.Children[0]);
            ((MainPage)this.Children[0]).PageWithScreens=((Tab2)this.Children[1]) ;
            ((Tab2)this.Children[1]).MinuteUpdate();
        }
    }
}