using ClashAttackBreakdown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClashAttackBreakdown.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewAttackCombinedDetail : ContentPage
	{
		private AttacksCombined individual;
		public ViewAttackCombinedDetail (AttacksCombined player)
		{
			InitializeComponent ();
			individual = player;
			name.Text = "Name: " + player.name.ToString();
			totalStars.Text= "Total Stars: " + player.totalStars.ToString();
			numberOfOffensiveAttacks.Text = "Number of Offensive Attacks: " + player.numberOfAttacks.ToString();
			triples.Text= "Triples: " + player.triples.ToString();
			singles.Text= "One Stars: " + player.singles.ToString();
			totalDestruction.Text = "Total Destruction: " + player.totalDestruction.ToString();
			averageDestruction.Text = "Average Offensive Destruction: " + player.AverageDestruction.ToString("0.00") + "%";
			defensiveStars.Text = "Defensive Stars: " + player.defensiveStars.ToString();
			numberOfDefensiveAttacks.Text = "Number of Defenses: " + player.numberOfDefAttacks.ToString();
			defensiveTriples.Text = "Defensive Triples: " + player.defensiveTriples.ToString();
			totalDefensiveDestruction.Text = "Total Defensive Destruction: " + player.defensiveDestruction.ToString();
			averageDefensiveDestruction.Text = "Average Defensive Destruction: " + player.defensiveAvgDestruction.ToString("0.00") + "%";
			defenseSingles.Text = "Defensive Singles: " + player.defensiveSingles.ToString();
			defenseZeroStars.Text = "Defensive Zero Stars: " + player.defensive0Stars.ToString();
			netStars.Text = "Net Stars: " + player.netStars.ToString();
			netPercent.Text = "Net Percent: " + player.netPercent.ToString();
		}

        private async void ToolbarItemGraph_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new ViewGraph(individual.tag));
        }
    }
}