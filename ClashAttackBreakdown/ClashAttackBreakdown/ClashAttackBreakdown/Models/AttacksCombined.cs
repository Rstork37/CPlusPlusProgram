using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClashAttackBreakdown.Models
{
    public class AttacksCombined
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        public string name { get; set; }
        public int totalStars { get; set; }
        public int triples { get; set; }
        public int singles { get; set; }
        public int totalDestruction { get; set; }
        public decimal AverageDestruction { get; set; }
        public int defensiveStars { get; set; }
        public int defensiveTriples { get; set; }
        public int defensiveDestruction { get; set; }
        public decimal defensiveAvgDestruction { get; set; }
        public int defensiveSingles { get; set; }
        public int defensive0Stars { get; set; }
        public int netStars { get; set; }
        public int netPercent { get; set; }
        public int dips { get; set; }
        public int adjustedStars { get; set; }
        public int numberOfAttacks { get; set; }
        public int numberOfDefAttacks { get; set; }
        public int uphitCount { get; set; }
        public string tag { get; set; }
    }
}
