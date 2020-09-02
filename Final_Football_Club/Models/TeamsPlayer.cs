using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Football_Club.Models
{
    public class TeamsPlayer
    {
        public string Note { get; set; }

        public Nullable<bool> status { get; set; }

        public string Team { get; set; }

        public int MatchID { get; set; }

        public String[] PlayerUserName { get; set; }

    }
}