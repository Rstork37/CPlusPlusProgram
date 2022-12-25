using ClashAttackBreakdown.Models;
using Microcharts.Forms;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClashAttackBreakdown.Services
{
    public class Database
    {
        private readonly SQLiteAsyncConnection theDatabase;
        
        
        public Database(string dbpath)
        {
            theDatabase= new SQLiteAsyncConnection(dbpath);
            theDatabase.CreateTableAsync<AttacksCombined>();
            theDatabase.CreateTableAsync<EachAttack>();
            theDatabase.CreateTableAsync<dboAttackPull>();
        }


        public Task<List<AttacksCombined>> GetAllAttacksCombinedAsync()
        {
            return theDatabase.Table<AttacksCombined>().ToListAsync();
        }

        public Task<int> InsertAllPlayerAsync(List<AttacksCombined> players)
        {
            return theDatabase.InsertAllAsync(players);
        }


        public Task<int> UpdatePlayerAsync(AttacksCombined player)
        {
            return theDatabase.UpdateAsync(player);
        }

        public Task<int> ClearAttacksCombinedDatabaseAsync()
        {
            return theDatabase.DeleteAllAsync<AttacksCombined>();

        }






        public Task<int> InsertAllAttacksAsync(List<EachAttack> attacks)
        {
            return theDatabase.InsertAllAsync(attacks);
        }

        public Task<int> ClearEachAttackDatabaseAsync()
        {
            return theDatabase.DeleteAllAsync<EachAttack>();
        }
        
        public Task<int> InsertEachAttackAsync(EachAttack attack)
        {
            return theDatabase.InsertAsync(attack);
        }

        public Task<List<EachAttack>> GetAttacksForGraph()
        {
            return theDatabase.Table<EachAttack>().ToListAsync();
        }


        public Task<List<dboAttackPull>> GetDatabaseAttacksAsync()
        {
            return theDatabase.Table<dboAttackPull>().ToListAsync();
        }

        public Task<int> InsertAllDBAttacks(List<dboAttackPull> attacks)
        {
            return theDatabase.InsertAllAsync(attacks);
        }

        public Task<int> ClearAllDBAttacks()
        {
            return theDatabase.DeleteAllAsync<dboAttackPull>();
        }
    }

    //<ContentPage.Content>
    //    <StackLayout>
    //        <Label Text = "Welcome to Xamarin.Forms!"
    //            VerticalOptions="CenterAndExpand" 
    //            HorizontalOptions="CenterAndExpand" />
    //    </StackLayout>
    //</ContentPage.Content>

        //    <Grid>
        //<forms:ChartView x:Name="Chart1"
        //                 BackgroundColor="Transparent"
        //                 Grid.Row="0"/>
        //<forms:ChartView x:Name="Chart2"
        //                 BackgroundColor="Transparent"
        //                 Grid.Row="1"/>
        //</Grid>
}
