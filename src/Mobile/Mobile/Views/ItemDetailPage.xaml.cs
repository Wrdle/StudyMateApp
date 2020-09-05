using System.ComponentModel;
using Xamarin.Forms;
using Mobile.ViewModels;

namespace Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}