using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using Newtonsoft.Json;

namespace TicTacToe
{
    class player
    {
        public string name;
        public int wins;
        public int losses;
    }
    public partial class HighScores : Form
    {
        List<SaveGame> AllGames = new List<SaveGame>();
        List<player> Players = new List<player>();
        List<player> TopTen = new List<player>();
        public HighScores()
        {
            InitializeComponent();

            if (File.Exists("Games.json"))
            {
                using (StreamReader r = new StreamReader("Games.json"))
                {
                    string json = r.ReadToEnd();
                    AllGames = JsonConvert.DeserializeObject<List<SaveGame>>(json);

                    SetWinsAndLossesForPlayers();
                    //list.Sort((x, y) => y.CompareTo(x));
                    Players.Sort((x, y) => (y.wins - y.losses).CompareTo(x.wins - x.losses));
                    for (int i = 0; i <= 10 && i < Players.Count; i++)
                    {
                        TopTen.Add(Players[i]);
                    }
                }
            }
        }
        public void SetWinsAndLossesForPlayers()
        {
            foreach(SaveGame game in AllGames)
            {
                if (!Players.Any(x => x.name == game.Winner))
                {
                    Players.Add(new player() { name = game.Winner });
                }
                if (!Players.Any(x => x.name == game.Loser))
                {
                    Players.Add(new player() { name = game.Loser });
                }
                if(Players.Any(x => x.name == game.Winner))
                {
                    player p = Players.First(x => x.name == game.Winner);
                    p.wins += 1;
                }
                if (Players.Any(x => x.name == game.Loser))
                {
                    player p = Players.First(x => x.name == game.Loser);
                    p.losses += 1;
                }
            }
        }
    }
}
