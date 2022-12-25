using ClashAttackBreakdown.Models;
using MvvmHelpers;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClashAttackBreakdown.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewData : ContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            theListView.ItemsSource = await App.Database.GetAllAttacksCombinedAsync();
        }
        public ViewData()
        {
            InitializeComponent();
        }

        
        private async void theListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            AttacksCombined player;
            player = e.SelectedItem as AttacksCombined;
            await Navigation.PushAsync(new ViewAttackCombinedDetail(player));
        }

        private async void TableView_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TableViewOfAttacksCombined());
        }
    }
}