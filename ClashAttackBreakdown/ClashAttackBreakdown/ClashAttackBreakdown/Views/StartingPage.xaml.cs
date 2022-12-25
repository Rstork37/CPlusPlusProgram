using ClashAttackBreakdown.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;

namespace ClashAttackBreakdown.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartingPage : ContentPage
    {

        private List<EachAttack> attackList = new List<EachAttack>();
        private List<AttacksCombined> listForData= new List<AttacksCombined>();


        public StartingPage()
        {
            InitializeComponent();

        }

        private async void BtnLoadFile_Clicked(object sender, EventArgs e)
        {

            try
            {
                var file = await FilePicker.PickAsync();
                if (file != null)
                {
                    if (file.FileName.EndsWith("csv", StringComparison.OrdinalIgnoreCase))
                    {
                        attackList.Clear();
                        listForData.Clear();
                        string fileName = file.FullPath;
                        await App.Database.ClearAttacksCombinedDatabaseAsync();
                        await App.Database.ClearEachAttackDatabaseAsync();
                        using (StreamReader sr = new StreamReader(fileName))
                        {
                            string lineFromFile = sr.ReadLine();
                            while (!sr.EndOfStream)
                            {
                                lineFromFile = sr.ReadLine();
                                string[] lineSplit = lineFromFile.Split(',');
                                EachAttack singleAttack = new EachAttack
                                {
                                    tag = lineSplit[0],
                                    name = lineSplit[1],
                                    defenderTag = lineSplit[7],
                                    townHallLevel = Int32.Parse(lineSplit[3]),
                                    stars = Int32.Parse(lineSplit[8]),
                                    destruction = Int32.Parse(lineSplit[10]),
                                    defenderName = lineSplit[12],
                                    enemyTownHallLevel = Int32.Parse(lineSplit[14]),
                                    isHomeTeam = Int32.Parse(lineSplit[15]),
                                    enemyClanName = lineSplit[20]
                                };
                                attackList.Add(singleAttack);
                                //await App.Database.InsertEachAttack(singleAttack);
                            }

                            sr.Close();
                        }

                        await App.Database.InsertAllAttacksAsync(attackList);
                        for (int i = 0; i < attackList.Count; i++)
                        {
                            AttacksCombined detailedAttack = new AttacksCombined();
                            bool playerInList = false;
                            int locationFoundAt = 0;
                            for (int j = 0; j < listForData.Count; j++)
                            {
                                if ((listForData[j].tag == attackList[i].tag) || (listForData[j].tag == attackList[i].defenderTag))
                                {
                                    playerInList = true;
                                    locationFoundAt = j;
                                    break;
                                }
                            }
                            if (!playerInList)
                            {
                                if (attackList[i].isHomeTeam == 1)
                                {
                                    detailedAttack.tag = attackList[i].tag;
                                    detailedAttack.name = attackList[i].name;
                                    detailedAttack.totalStars = attackList[i].stars;
                                    if (attackList[i].stars == 3)
                                    {
                                        detailedAttack.triples = 1;
                                        detailedAttack.singles = 0;
                                    }
                                    else if (attackList[i].stars == 1)
                                    {
                                        detailedAttack.triples = 0;
                                        detailedAttack.singles = 1;
                                    }
                                    else
                                    {
                                        detailedAttack.triples = 0;
                                        detailedAttack.singles = 0;
                                    }
                                    detailedAttack.totalDestruction = attackList[i].destruction;
                                    detailedAttack.AverageDestruction = (decimal)attackList[i].destruction;
                                    if (attackList[i].townHallLevel > attackList[i].enemyTownHallLevel)
                                    {
                                        detailedAttack.dips = 1;
                                    }
                                    else
                                    {
                                        detailedAttack.dips = 0;
                                    }
                                    if (attackList[i].townHallLevel < attackList[i].enemyTownHallLevel)
                                    {
                                        detailedAttack.uphitCount = 1;
                                    }
                                    else
                                    {
                                        detailedAttack.uphitCount = 0;
                                    }
                                    detailedAttack.defensive0Stars = 0;
                                    detailedAttack.defensiveAvgDestruction = 0;
                                    detailedAttack.defensiveDestruction = 0;
                                    detailedAttack.defensiveSingles = 0;
                                    detailedAttack.defensiveStars = 0;
                                    detailedAttack.defensiveTriples = 0;
                                    detailedAttack.netStars = attackList[i].stars;
                                    detailedAttack.netPercent = attackList[i].destruction;
                                    detailedAttack.adjustedStars = detailedAttack.totalStars - detailedAttack.dips;
                                    detailedAttack.numberOfAttacks = 1;
                                    detailedAttack.numberOfDefAttacks = 0;
                                    listForData.Add(detailedAttack);
                                }
                                else
                                {
                                    detailedAttack.tag = attackList[i].defenderTag;
                                    detailedAttack.name = attackList[i].defenderName;
                                    detailedAttack.totalStars = 0;
                                    detailedAttack.triples = 0;
                                    detailedAttack.singles = 0;
                                    detailedAttack.totalDestruction = 0;
                                    detailedAttack.AverageDestruction = 0m;
                                    detailedAttack.defensiveStars = attackList[i].stars;
                                    if (attackList[i].stars == 3)
                                    {
                                        detailedAttack.defensiveTriples = 1;
                                        detailedAttack.defensive0Stars = 0;
                                        detailedAttack.defensiveSingles = 0;
                                    }
                                    else if (attackList[i].stars == 1)
                                    {
                                        detailedAttack.defensiveTriples = 0;
                                        detailedAttack.defensive0Stars = 0;
                                        detailedAttack.defensiveSingles = 1;
                                    }
                                    else if (attackList[i].stars == 0)
                                    {
                                        detailedAttack.defensiveTriples = 0;
                                        detailedAttack.defensive0Stars = 1;
                                        detailedAttack.defensiveSingles = 0;
                                    }
                                    else
                                    {
                                        detailedAttack.defensiveTriples = 0;
                                        detailedAttack.defensive0Stars = 0;
                                        detailedAttack.defensiveSingles = 0;
                                    }
                                    detailedAttack.dips = 0;
                                    detailedAttack.uphitCount = 0;
                                    detailedAttack.defensiveDestruction = attackList[i].destruction;
                                    detailedAttack.defensiveAvgDestruction = attackList[i].destruction;
                                    detailedAttack.numberOfAttacks = 0;
                                    detailedAttack.numberOfDefAttacks = 1;
                                    detailedAttack.netStars = attackList[i].stars * (-1);
                                    detailedAttack.netPercent = -1 * attackList[i].destruction;
                                    listForData.Add(detailedAttack);
                                }
                            }
                            else
                            {
                                detailedAttack = listForData[locationFoundAt];
                                if (attackList[i].isHomeTeam == 1)
                                {
                                    detailedAttack.totalStars += attackList[i].stars;
                                    if (attackList[i].stars == 3)
                                    {
                                        detailedAttack.triples += 1;
                                    }
                                    else if (attackList[i].stars == 1)
                                    {
                                        detailedAttack.singles += 1;
                                    }
                                    detailedAttack.numberOfAttacks += 1;
                                    detailedAttack.totalDestruction += attackList[i].destruction;
                                    detailedAttack.AverageDestruction = decimal.Round((decimal)detailedAttack.totalDestruction / detailedAttack.numberOfAttacks, 2);
                                    if (attackList[i].townHallLevel > attackList[i].enemyTownHallLevel)
                                    {
                                        detailedAttack.dips += 1;
                                    }
                                    if (attackList[i].townHallLevel < attackList[i].enemyTownHallLevel)
                                    {
                                        detailedAttack.uphitCount += 1;
                                    }
                                    detailedAttack.netStars += attackList[i].stars;
                                    detailedAttack.netPercent += attackList[i].destruction;
                                    detailedAttack.adjustedStars = detailedAttack.totalStars - detailedAttack.dips + detailedAttack.uphitCount;

                                    listForData[locationFoundAt] = detailedAttack;
                                }
                                else
                                {
                                    detailedAttack.defensiveStars += attackList[i].stars;
                                    if (attackList[i].stars == 3)
                                    {
                                        detailedAttack.defensiveTriples += 1;
                                    }
                                    else if (attackList[i].stars == 1)
                                    {
                                        detailedAttack.defensiveSingles += 1;
                                    }
                                    else if (attackList[i].stars == 0)
                                    {
                                        detailedAttack.defensive0Stars += 1;
                                    }
                                    detailedAttack.numberOfDefAttacks += 1;
                                    detailedAttack.defensiveDestruction += attackList[i].destruction;
                                    detailedAttack.defensiveAvgDestruction = decimal.Round((decimal)detailedAttack.defensiveDestruction / detailedAttack.numberOfDefAttacks, 2);
                                    detailedAttack.netStars += attackList[i].stars * (-1);
                                    detailedAttack.netPercent += ((-1) * attackList[i].destruction);
                                    listForData[locationFoundAt] = detailedAttack;
                                }
                            }
                        }

                        await App.Database.InsertAllPlayerAsync(listForData);
                        lblFileLoad.Text = "File Loaded";
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                ShowErrorOnLabel();
            }
        }

            async Task<FileResult> PickAndShow()
        {
            var result = await FilePicker.PickAsync();
            return result;
        }

        public void ShowErrorOnLabel()
        {
            lblFileLoad.Text = "Load Failed to Initialize";
        }

        private async void BtnClickMe_Clicked(object sender, EventArgs e)
        {
            List<dboAttackPull> DBattackList = new List<dboAttackPull>();
            await App.Database.ClearAllDBAttacks();
            await App.Database.ClearAttacksCombinedDatabaseAsync();
            listForData.Clear();
            try
            {
                string conn = @"Data Source = 98.156.117.206\SQLExpress,4729; User ID = ClashUser; Password = ClashUser; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    int numberOfFilesToLoad = 0;
                    int numberOfFilesTraversed = 0;
                    float progressBarProgression = 0.0f;
                    string countQuery = "SELECT COUNT(*) FROM FSAttacks";
                    using (SqlCommand countCmd = new SqlCommand(countQuery, connection))
                    {
                        numberOfFilesToLoad = (int)countCmd.ExecuteScalar();
                    }
                    string query = "SELECT * FROM FSAttacks";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                numberOfFilesTraversed++;
                                if (numberOfFilesTraversed % 10 == 0)
                                {
                                    progressBarProgression = (float)numberOfFilesTraversed / numberOfFilesToLoad;
                                    //LoadingBar.Progress= progressBarProgression;
                                    await LoadingBar.ProgressTo(progressBarProgression, 200, Easing.Linear);
                                }
                                dboAttackPull readFromSql = new dboAttackPull();
                                readFromSql.clanTag = reader["clanTag"].ToString();
                                readFromSql.clanName = reader["clanName"].ToString();
                                readFromSql.memberTag = reader["tag"].ToString();
                                readFromSql.memberName = reader["name"].ToString();
                                readFromSql.townhallLevel = int.Parse(reader["townhallLevel"].ToString());
                                readFromSql.attackerTag = reader["attackerTag"].ToString();
                                readFromSql.isHomeTeam = int.Parse(reader["isHomeTeam"].ToString());
                                readFromSql.defenderTag = reader["defenderTag"].ToString();
                                readFromSql.defenderName = reader["defenderName"].ToString();
                                readFromSql.defenderClanName = reader["defenderClanName"].ToString();
                                readFromSql.defenderTownhallLevel = int.Parse(reader["defenderTownhallLevel"].ToString());
                                readFromSql.stars = int.Parse(reader["stars"].ToString());
                                readFromSql.destructionPercentage = int.Parse(reader["destructionPercentage"].ToString());
                                readFromSql.duration = int.Parse(reader["duration"].ToString());
                                readFromSql.warEndTime = reader["endTime"].ToString();
                                readFromSql.SCCWL = reader["SCCWL"].ToString();
                                DBattackList.Add(readFromSql);
                            }
                            progressBarProgression = 1.0f;
                            LoadingBar.Progress = progressBarProgression;
                        }
                    }
                    connection.Close();
                    await App.Database.InsertAllDBAttacks(DBattackList);
                    lblFileLoad.Text = "DB Loaded";
                }

                for (int i = 0; i < DBattackList.Count; i++)
                {
                    AttacksCombined detailedAttack = new AttacksCombined();
                    bool playerInList = false;
                    int locationFoundAt = 0;
                    for (int j = 0; j < listForData.Count; j++)
                    {
                        if ((listForData[j].tag == DBattackList[i].attackerTag) || (listForData[j].tag == DBattackList[i].defenderTag))
                        {
                            playerInList = true;
                            locationFoundAt = j;
                            break;
                        }
                    }
                    if (!playerInList)
                    {
                        if (DBattackList[i].isHomeTeam == 1)
                        {
                            detailedAttack.tag = DBattackList[i].attackerTag;
                            detailedAttack.name = DBattackList[i].memberName;
                            detailedAttack.totalStars = DBattackList[i].stars;
                            if (DBattackList[i].stars == 3)
                            {
                                detailedAttack.triples = 1;
                                detailedAttack.singles = 0;
                            }
                            else if (DBattackList[i].stars == 1)
                            {
                                detailedAttack.triples = 0;
                                detailedAttack.singles = 1;
                            }
                            else
                            {
                                detailedAttack.triples = 0;
                                detailedAttack.singles = 0;
                            }
                            detailedAttack.totalDestruction = DBattackList[i].destructionPercentage;
                            detailedAttack.AverageDestruction = (decimal)DBattackList[i].destructionPercentage;
                            if (DBattackList[i].destructionPercentage > DBattackList[i].defenderTownhallLevel)
                            {
                                detailedAttack.dips = 1;
                            }
                            else
                            {
                                detailedAttack.dips = 0;
                            }
                            if (DBattackList[i].townhallLevel < DBattackList[i].defenderTownhallLevel)
                            {
                                detailedAttack.uphitCount = 1;
                            }
                            else
                            {
                                detailedAttack.uphitCount = 0;
                            }
                            detailedAttack.defensive0Stars = 0;
                            detailedAttack.defensiveAvgDestruction = 0;
                            detailedAttack.defensiveDestruction = 0;
                            detailedAttack.defensiveSingles = 0;
                            detailedAttack.defensiveStars = 0;
                            detailedAttack.defensiveTriples = 0;
                            detailedAttack.netStars = DBattackList[i].stars;
                            detailedAttack.netPercent = DBattackList[i].destructionPercentage;
                            detailedAttack.adjustedStars = detailedAttack.totalStars - detailedAttack.dips;
                            detailedAttack.numberOfAttacks = 1;
                            detailedAttack.numberOfDefAttacks = 0;
                            listForData.Add(detailedAttack);
                        }
                        else
                        {
                            detailedAttack.tag = DBattackList[i].defenderTag;
                            detailedAttack.name = DBattackList[i].defenderName;
                            detailedAttack.totalStars = 0;
                            detailedAttack.triples = 0;
                            detailedAttack.singles = 0;
                            detailedAttack.totalDestruction = 0;
                            detailedAttack.AverageDestruction = 0m;
                            detailedAttack.defensiveStars = DBattackList[i].stars;
                            if (DBattackList[i].stars == 3)
                            {
                                detailedAttack.defensiveTriples = 1;
                                detailedAttack.defensive0Stars = 0;
                                detailedAttack.defensiveSingles = 0;
                            }
                            else if (DBattackList[i].stars == 1)
                            {
                                detailedAttack.defensiveTriples = 0;
                                detailedAttack.defensive0Stars = 0;
                                detailedAttack.defensiveSingles = 1;
                            }
                            else if (DBattackList[i].stars == 0)
                            {
                                detailedAttack.defensiveTriples = 0;
                                detailedAttack.defensive0Stars = 1;
                                detailedAttack.defensiveSingles = 0;
                            }
                            else
                            {
                                detailedAttack.defensiveTriples = 0;
                                detailedAttack.defensive0Stars = 0;
                                detailedAttack.defensiveSingles = 0;
                            }
                            detailedAttack.dips = 0;
                            detailedAttack.uphitCount = 0;
                            detailedAttack.defensiveDestruction = DBattackList[i].destructionPercentage;
                            detailedAttack.defensiveAvgDestruction = DBattackList[i].destructionPercentage;
                            detailedAttack.numberOfAttacks = 0;
                            detailedAttack.numberOfDefAttacks = 1;
                            detailedAttack.netStars = DBattackList[i].stars * (-1);
                            detailedAttack.netPercent = -1 * DBattackList[i].destructionPercentage;
                            listForData.Add(detailedAttack);
                        }
                    }
                    else
                    {
                        detailedAttack = listForData[locationFoundAt];
                        if (DBattackList[i].isHomeTeam == 1)
                        {
                            detailedAttack.totalStars += DBattackList[i].stars;
                            if (DBattackList[i].stars == 3)
                            {
                                detailedAttack.triples += 1;
                            }
                            else if (DBattackList[i].stars == 1)
                            {
                                detailedAttack.singles += 1;
                            }
                            detailedAttack.numberOfAttacks += 1;
                            detailedAttack.totalDestruction += DBattackList[i].destructionPercentage;
                            detailedAttack.AverageDestruction = decimal.Round((decimal)detailedAttack.totalDestruction / detailedAttack.numberOfAttacks, 2);
                            if (DBattackList[i].townhallLevel > DBattackList[i].defenderTownhallLevel)
                            {
                                detailedAttack.dips += 1;
                            }
                            if (DBattackList[i].townhallLevel < DBattackList[i].defenderTownhallLevel)
                            {
                                detailedAttack.uphitCount += 1;
                            }
                            detailedAttack.netStars += DBattackList[i].stars;
                            detailedAttack.netPercent += DBattackList[i].destructionPercentage;
                            detailedAttack.adjustedStars = detailedAttack.totalStars - detailedAttack.dips + detailedAttack.uphitCount;

                            listForData[locationFoundAt] = detailedAttack;
                        }
                        else
                        {
                            detailedAttack.defensiveStars += DBattackList[i].stars;
                            if (DBattackList[i].stars == 3)
                            {
                                detailedAttack.defensiveTriples += 1;
                            }
                            else if (DBattackList[i].stars == 1)
                            {
                                detailedAttack.defensiveSingles += 1;
                            }
                            else if (DBattackList[i].stars == 0)
                            {
                                detailedAttack.defensive0Stars += 1;
                            }
                            detailedAttack.numberOfDefAttacks += 1;
                            detailedAttack.defensiveDestruction += DBattackList[i].destructionPercentage;
                            detailedAttack.defensiveAvgDestruction = decimal.Round((decimal)detailedAttack.defensiveDestruction / detailedAttack.numberOfDefAttacks, 2);
                            detailedAttack.netStars += DBattackList[i].stars * (-1);
                            detailedAttack.netPercent += ((-1) * DBattackList[i].destructionPercentage);
                            listForData[locationFoundAt] = detailedAttack;
                        }
                    }
                }
                await App.Database.InsertAllPlayerAsync(listForData);
                lblFileLoad.Text = "DB Inserted";


            }
            catch (Exception ex)
            {
                lblFileLoad.Text = ex.Message.ToString();
            }
            finally
            {
            }

        }

    }
}