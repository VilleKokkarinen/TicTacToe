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
    /// <summary>
    /// HighScores form
    /// </summary>
    public partial class HighScores : Form
    {
        // List for all games results saved in a file
        List<SaveGame> AllGames = new List<SaveGame>();

        // List for current players found in all the saved games
        List<Player> Players = new List<Player>();

        // List for the best players from current players
        List<Player> TopTen = new List<Player>();

        /// <summary>
        /// Initializer
        /// </summary>
        public HighScores()
        {
            InitializeComponent();

            // If saved file exist
            if (File.Exists("Games.json"))
            {
                // start reading data
                using (StreamReader r = new StreamReader("Games.json"))
                {
                    // convert from json to a list
                    string json = r.ReadToEnd();
                    AllGames = JsonConvert.DeserializeObject<List<SaveGame>>(json);

                    // get all the players from games
                    SetWinsAndLossesForPlayers();

                    // sort the best players to top
                    Players.Sort((x, y) => (y.wins - y.losses).CompareTo(x.wins - x.losses));
                    for (int i = 0; i <= 10 && i < Players.Count; i++)
                    {
                        TopTen.Add(Players[i]);
                    }
                }
            }

            // add the list as a datasource
            dgTopTen.DataSource = TopTen;

            // hide unnecessary data
            dgTopTen.Columns["PlayerTile"].Visible = false;
            dgTopTen.Columns["IsCPU"].Visible = false;
        }

        /// <summary>
        /// Adds players, and their experience points from all games
        /// </summary>
        public void SetWinsAndLossesForPlayers()
        {
            // for all games
            foreach(SaveGame game in AllGames)
            {
                // if the player is not already in players list
                if (!Players.Any(x => x.Name == game.Winner))
                {
                    // create a new player and add to list
                    Player p;
                    p = Player.CreateDefaultPlayer();
                    p.Name = game.Winner;
                    Players.Add(p);
                }
                if (!Players.Any(x => x.Name == game.Loser))
                {
                    Player p;
                    p = Player.CreateDefaultPlayer();
                    p.Name = game.Loser;
                    Players.Add(p);
                }
                // if player is in Players list, give him 5 experience for the won game
                if(Players.Any(x => x.Name == game.Winner))
                {
                    Player p = Players.First(x => x.Name == game.Winner);
                    p.wins += 1;
                    p.AddExperiencePoints(5);
                }
                // if player is in Players list, take 5 experience away for the lost game
                if (Players.Any(x => x.Name == game.Loser))
                {
                    Player p = Players.First(x => x.Name == game.Loser);
                    p.losses += 1;
                    p.AddExperiencePoints(-5);
                }
            }
        }
    }
}
