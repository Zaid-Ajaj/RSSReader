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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RSSReader.Data.Models;
using RSSReader.Data;


namespace RSSReader.UI
{
    /// <summary>
    /// Interaction logic for NewChannelWindow.xaml
    /// </summary>
    public partial class NewChannelWindow : Window
    {
        public RSSChannel channel;
        public NewChannelWindow()
        {
            InitializeComponent();
            btnAdd.Click += btnAdd_Click;
           
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLink.Text) && !string.IsNullOrEmpty(txtTitle.Text))
            {
                if(RSSDataManeger.IsValidChannel(txtLink.Text))
                {
                    channel = new RSSChannel() { Link = txtLink.Text, Title = txtTitle.Text };
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Channel is invalid and cannot be used!","RSSReader");
                    txtLink.Text = "";
                }
                
            }
            else
            {
                MessageBox.Show("Empty fields are not allowed!","RSSReader");
            }
        }
    }
}
