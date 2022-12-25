using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClashAttackBreakdown.Models
{
    public class EachAttack
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string tag { get; set; }
        public string defenderTag { get; set; }
        public string name { get; set; }
        public int stars { get; set; }
        public int destruction { get; set; }
        public string defenderName { get; set; }
        public string enemyClanName { get; set; }
        public int isHomeTeam { get; set; }
        public int townHallLevel { get; set; }
        public int enemyTownHallLevel { get; set; }
    }
}
