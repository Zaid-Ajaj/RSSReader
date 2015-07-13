using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSSReader.UI
{
    /// <summary>
    /// Interaction logic for ChannelListItem.xaml
    /// </summary>
    public partial class ChannelListItem : UserControl
    {
        public ChannelListItem()
        {
            InitializeComponent();
            Grid.MouseEnter += Grid_MouseEnter;
            Grid.MouseLeave += Grid_MouseLeave;
        }

        void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            var mouseLeaveAnim = Resources["MouseLeaveAnimation"] as Storyboard;
            mouseLeaveAnim.Begin();
        }

        void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            var mouseEnterAnim = Resources["MouseEnterAnimation"] as Storyboard;
            mouseEnterAnim.Begin();
        }
    }
}
