using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RSSReader.Data.Models;
using RSSReader.Data;

namespace RSSReader.UI
{
    public partial class MainWindow : Window 
    {
        private readonly string fileName = Environment.CurrentDirectory + "\\data.xml";
        private List<RSSChannel> rssChannels;


        public MainWindow()
        {
            InitializeComponent();
            rssChannels = RSSDataManeger.GetChannels(fileName).ToList();
            ViewChannels();
        }

        void AddChannelToUI(RSSChannel channel)
        {
            var uiChannel = new ChannelListItem { DataContext = channel };
            uiChannel.ContextMenu = new ContextMenu();
            MenuItem deleteButton = new MenuItem { Header = "Delete" };
            deleteButton.Click += delegate
            {
                StackChannels.Children.Remove(uiChannel);
                SaveChanges();
            };
            uiChannel.ContextMenu.Items.Add(deleteButton);
            uiChannel.MouseDoubleClick += delegate
            {
                LoadRSSItems(channel.Link);
            };
            StackChannels.Children.Add(uiChannel);
        }
        void ViewChannels()
        {
            StackChannels.Children.Clear();
            foreach (var channel in rssChannels)
            {
                AddChannelToUI(channel);
            }
            SaveChanges();
        }

        void LoadRSSItems(string link)
        {
            var items = RSSDataManeger.GetItemsFrom(link);
            StackItems.Children.Clear();
            foreach (var item in items)
            {
                var uiItem = new RSSFeedListItem() {DataContext = item};
                uiItem.MouseDoubleClick += delegate { Process.Start(item.Link); };
                StackItems.Children.Add(uiItem);
            }
        }

        private void ShowAddNewChannelWindow(object sender, RoutedEventArgs e)
        {
            var newChannalWin = new NewChannelWindow();
            var result = newChannalWin.ShowDialog();
            if (result.HasValue && result == true)
            {
                AddChannelToUI(newChannalWin.channel);
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            var updatedChannels = new List<RSSChannel>();
            foreach (UIElement element in StackChannels.Children)
            {
                if (element is ChannelListItem)
                {
                    var channel = ((ChannelListItem)element).DataContext as RSSChannel;
                    updatedChannels.Add(channel);
                }
            }
            RSSDataManeger.SaveChannels(updatedChannels, fileName);
        }

        private void ShowAboutWindow(object sender, RoutedEventArgs e)
        {
            var win = new AboutWindow();
            win.ShowDialog();
        }

    }
}
