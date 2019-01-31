using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace YoutubeCh9
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = DataFactory.Videos;

            videoList.ItemTapped += async (sender, args) =>
            {
                var vid = videoList.SelectedItem as Video;
                await Browser.OpenAsync("https://m.youtube.com/watch?v=" + vid.ID);
                videoList.SelectedItem = null;
            };
        }
    }
}
