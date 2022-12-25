using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using ClashAttackBreakdown.Models;
using System.IO;

namespace ClashAttackBreakdown.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewGraph : ContentPage
	{

        private List<dboAttackPull> attacks;
        private string playerTag;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            attacks = await App.Database.GetDatabaseAttacksAsync();
            List<Microcharts.ChartEntry> eachAttackEntry = new List<Microcharts.ChartEntry>();
            List<Microcharts.ChartEntry> triplePercentEntry = new List<Microcharts.ChartEntry>();
            List<Microcharts.ChartEntry> averageAttackDestructionEntry = new List<Microcharts.ChartEntry>();
            int attackNumber = 0;
            int tripleCount = 0;
            int triplePercent;
            float averageAttackDestruction;
            int totalDestruction = 0;
            foreach (dboAttackPull attack in attacks)
            {
                if (attack.memberTag == playerTag)
                {
                    PlayerName.Text = attack.memberName;
                    attackNumber++;
                    totalDestruction += attack.destructionPercentage;
                    string attackDestructionColor = "#3498db";
                    string averageDestructionColor = "#3498db";
                    string triplePercentColor = "#3498db";
                    if (attack.stars == 3)
                    {
                        tripleCount++;
                    }
                    triplePercent = (tripleCount * 100) / attackNumber;
                    averageAttackDestruction = (float)totalDestruction/ attackNumber;

                    if (attack.destructionPercentage > 90)                                // Set the color for the Each Attack
                    {
                        attackDestructionColor = "00FF00";
                    }
                    else if (attack.destructionPercentage > 85)
                    {
                        attackDestructionColor = "FFFF00";
                    }
                    else
                    {
                        attackDestructionColor = "FF0000";
                    }

                    if (averageAttackDestruction > 90)                                // Set the color of Average Destruction
                    {
                        averageDestructionColor = "00FF00";
                    }
                    else if (averageAttackDestruction > 85)
                    {
                        averageDestructionColor = "FFFF00";
                    }
                    else
                    {
                        averageDestructionColor = "FF0000";
                    }
                    if (triplePercent > 30)                                           // Set the color of Triple Percentage
                    {
                        triplePercentColor = "00FF00";
                    }
                    else if (triplePercent > 20)
                    {
                        triplePercentColor = "FFFF00";
                    }
                    else
                    {
                        triplePercentColor = "FF0000";
                    }
                    ChartEntry singleAttackEntry = new ChartEntry(attack.destructionPercentage)
                    {                                                                   // Chart entry for attack destruction
                        Label = " ",
                        ValueLabel = attack.destructionPercentage.ToString(),

                        Color = SKColor.Parse(attackDestructionColor)
                        
                    };
                    ChartEntry singleAverageAttackkEntry = new ChartEntry(averageAttackDestruction)
                    {                                                                   // Chart entry for Average Attack Destruction
                        Label = " ",
                        ValueLabel = averageAttackDestruction.ToString("0.00"),
                        Color = SKColor.Parse(averageDestructionColor)
                    };
                    ChartEntry singleTriplePercentEntry = new ChartEntry(triplePercent)
                    {                                                                   // Chart entry for Triple Percentage
                        Label = " ",
                        ValueLabel = triplePercent.ToString(),
                        Color = SKColor.Parse(triplePercentColor)
                    };
                    eachAttackEntry.Add(singleAttackEntry);                             // adding the entries to the appropriate chart
                    averageAttackDestructionEntry.Add(singleAverageAttackkEntry);
                    triplePercentEntry.Add(singleTriplePercentEntry);
                }
            }
            
            ChartEachAttackDestruction.Chart = new LineChart { Entries = eachAttackEntry };
            ChartAverageDestruction.Chart = new LineChart { Entries = averageAttackDestructionEntry };
            ChartTriplePercent.Chart = new LineChart { Entries = triplePercentEntry };
        }
        public ViewGraph (string tag)
		{
			InitializeComponent ();
            playerTag = tag;
			
        }

        private void RadioButtonAttack_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {                                                   // Set the Attack Percent Chart to Visible
                ChartEachAttackDestruction.IsVisible = true;
                ChartAverageDestruction.IsVisible = false;
                ChartTriplePercent.IsVisible = false;
            }
        }

        private void RadioButtonDestruction_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {                                                   // Set the Average Destruction Chart to Visible
                ChartEachAttackDestruction.IsVisible = false;
                ChartAverageDestruction.IsVisible = true;
                ChartTriplePercent.IsVisible = false;
            }
        }

        private void RadioButtonTriple_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {                                                   // Set the Triple Percentage Chart to Visible
                ChartEachAttackDestruction.IsVisible = false;
                ChartAverageDestruction.IsVisible = false;
                ChartTriplePercent.IsVisible = true;
            }
        }
    }
}