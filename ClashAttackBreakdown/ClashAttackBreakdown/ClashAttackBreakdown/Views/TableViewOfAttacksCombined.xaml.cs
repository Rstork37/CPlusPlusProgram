using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashAttackBreakdown;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClashAttackBreakdown.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ClashAttackBreakdown.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableViewOfAttacksCombined : ContentPage
    {
        public ObservableRangeCollection<AttacksCombined> list;
        List<AttacksCombined> attacksCombined = new List<AttacksCombined>();
        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    attacksCombined = await App.Database.GetAllAttacksCombinedAsync();
        //    //list = new ObservableRangeCollection<AttacksCombined>(attacksCombined as List<AttacksCombined>);
        //    //BindableLayout.SetItemsSource(GridInfo,list);
        //}
        public TableViewOfAttacksCombined()
        {
            InitializeComponent();
        }
    }
}

//< ListView x: Name = "GridListView" >
//            < ListView.Header >
//                < Grid >
//                    < Grid.ColumnDefinitions >
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                    </ Grid.ColumnDefinitions >
//                    < Label Text = "Name" Grid.Column = "0" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Total Stars" Grid.Column = "1" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Triples" Grid.Column = "2" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Singles" Grid.Column = "3" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Total Destruction" Grid.Column = "4" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Average Destruction" Grid.Column = "5" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                </ Grid >
//            </ ListView.Header >
//            < ListView.ItemTemplate >
//                < DataTemplate >
//                    < ViewCell >
//< Grid >
//    < Grid.ColumnDefinitions >
//        < ColumnDefinition Width = "100" />
//        < ColumnDefinition Width = "100" />
//        < ColumnDefinition Width = "100" />
//        < ColumnDefinition Width = "100" />
//        < ColumnDefinition Width = "100" />
//        < ColumnDefinition Width = "100" />
//    </ Grid.ColumnDefinitions >
//        < Label Grid.Column = "0" Text = "{Binding name}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//        < Label Grid.Column = "1" Text = "{Binding totalStars}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//        < Label Grid.Column = "2" Text = "{Binding triples}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//        < Label Grid.Column = "3" Text = "{Binding singles}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//        < Label Grid.Column = "4" Text = "{Binding totalDestruction}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//        < Label Grid.Column = "5" Text = "{Binding AverageDestruction}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//</ Grid >
//                    </ ViewCell >
//                </ DataTemplate >
//            </ ListView.ItemTemplate >
//        </ ListView >




//< StackLayout >
//                < Grid >
//                    < Grid.ColumnDefinitions >
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                    </ Grid.ColumnDefinitions >
//                    < Label Text = "Name" Grid.Column = "0" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Total Stars" Grid.Column = "1" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Triples" Grid.Column = "2" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Singles" Grid.Column = "3" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Total Destruction" Grid.Column = "4" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Text = "Average Destruction" Grid.Column = "5" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                </ Grid >


//                < Grid  x: Name = "GridInfo" >
//                    < Grid.ColumnDefinitions >
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                        < ColumnDefinition Width = "100" />
//                    </ Grid.ColumnDefinitions >
//                    < Label Grid.Column = "0" Text = "{Binding name}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Grid.Column = "1" Text = "{Binding totalStars}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Grid.Column = "2" Text = "{Binding triples}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Grid.Column = "3" Text = "{Binding singles}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Grid.Column = "4" Text = "{Binding totalDestruction}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                    < Label Grid.Column = "5" Text = "{Binding AverageDestruction}" HorizontalOptions = "Fill" BackgroundColor = "White" TextColor = "{StaticResource StandardTextColor}" HorizontalTextAlignment = "Center" WidthRequest = "100" />
//                </ Grid >



//            </ StackLayout >