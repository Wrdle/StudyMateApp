using System.ComponentModel;
using Xamarin.Forms;
using App.ViewModels;

namespace App.Views
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