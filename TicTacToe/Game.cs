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
using System.Xml;
using Engine;
using TicTacToeML.Model;

namespace TicTacToe
{
    public partial class GameForm : Form
    {
        /// <summary>
        /// GameLogic object
        /// Contains the following:
        /// -Players
        /// -GameBoard -> Tiles -> Panels
        /// -Options
        /// </summary>
        private GameLogic Game;

        // Disables selecting a panel while the game is in a gameover State
        // -> Doesn't allow the user to click on the gameboard while winner is being shown
        bool checking = false;

        /// <summary>
        /// Initializer
        /// </summary>
        public GameForm()
        {
            InitializeComponent();

            //Set focus to a label, so it doesnt select a button for some reason...
            ActiveControl = lblWinner;

            // progressbar settings
            progbarCPU.Maximum = 100;
            progbarCPU.Step = 100;

            // Load game
            LoadGame();
        }

        /// <summary>
        /// Loads the game if a save is found
        /// </summary>
        private void LoadGame()
        {
            // Attempt to load from savefile
            GameLogic LoadedFromSave = GameLogic.LoadOptions();

            Game = new GameLogic();
            Game.CreateDefaultGame();

            if (LoadedFromSave != Game)
            {
                // If saved game is not same as default, take the loaded version
                Game = LoadedFromSave;
            }

            // adds the black lines separating each tile
            AddSeparatorPanels();

            // Add a panel to each tile
            AddPanelsToTiles();            
        }

        /// <summary>
        /// Uses ML.NET library
        /// with the data of approximately 1000 games, and few thousand proven good moves to "predict" a "good" move
        /// </summary>
        private void MachineLearningMove()
        {
            // what was the tile placed previously
            string tileplayed = (Game.PlayerTurn == 1) ? "X" : "O";

            // Add input data
            // The gameboard is a 2D array of size n
            // Needs updating if support for >3 size is added in future
            var input = new ModelInput();
            input.Tile1 = Game.Gameboard.getboard()[0, 0].Value.ToString();
            input.Tile2 = Game.Gameboard.getboard()[1, 0].Value.ToString();
            input.Tile3 = Game.Gameboard.getboard()[2, 0].Value.ToString();
            input.Tile4 = Game.Gameboard.getboard()[0, 1].Value.ToString();
            input.Tile5 = Game.Gameboard.getboard()[1, 1].Value.ToString();
            input.Tile6 = Game.Gameboard.getboard()[2, 1].Value.ToString();
            input.Tile7 = Game.Gameboard.getboard()[0, 2].Value.ToString();
            input.Tile8 = Game.Gameboard.getboard()[1, 2].Value.ToString();
            input.Tile9 = Game.Gameboard.getboard()[2, 2].Value.ToString();
            input.TilePlayed = tileplayed;

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            
            // Get a list of legal moves (tiles that do not have a value)
            List<Tile> legalMoves = new List<Tile>();
            foreach (Tile tile in Game.Gameboard.getboard())
            {
                if(tile.Value == Tile.TileValue.NaN)
                legalMoves.Add(tile);
            }

            // result of prediction
            int ID = Convert.ToInt32(result.Prediction);

            // Find the tile with same ID
            Tile MoveTile = legalMoves.Find(X => X.ID == ID);

            // machine predicted an ID thats not a legal move, give a random move instead...
            if (MoveTile == null)
            {
                Random r = new Random();
                MoveTile = legalMoves[r.Next(0, legalMoves.Count - 1)];
            }
            if (MoveTile.CheckTileState())
            {
                Game.MakeMove(MoveTile.X, MoveTile.Y, Game.Players[Game.PlayerTurn].PlayerTile);
            }
            else
            {
                // Somehow the tile was taken use a random move
                Game.RANDOM_MOVE();
            }
        }

        /// <summary>
        /// Panel click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pnl_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;

            int x = (panel.Location.X - 50) / 50;
            int y = (panel.Location.Y - 50) / 50;

            Tile t = Game.Gameboard.getboard()[x, y];
            if (t.CheckTileState() && Game.Over == false)
            {
                Game.MakeMove(x, y, Game.Players[Game.PlayerTurn].PlayerTile);
            }
            CheckGameOver();
        }
         
        /// <summary>
        /// Removes panels from form
        /// </summary>
        public void DisposeExistingPanels()
        {
            // While form contains panels
            while (true)
            {
                bool MorePanels = false;

                // For all controls on the form
                foreach (Control c in Controls)
                {
                    // if it's a panel
                    if (c is Panel)
                    {
                        // get rid of the panel
                        c.Dispose();

                        // we still had a panel on the form
                        MorePanels = true;
                    }
                }
                // when all panels are gone break loop
                if (MorePanels == false)
                    break;
            }           
        }

        /// <summary>
        /// Adds separator panels for the gameboard ( Between each tile )
        /// </summary>
        public void AddSeparatorPanels()
        {
            // Offset for the longer black lines ( ~ X- Margin ) separator lines
            int offset = 100;

            // separator lines
            for (int i = 0; i < Game.Gameboard.getboard().GetLength(0) - 1; i++)
            {
                for (int j = 0; j < Game.Gameboard.getboard().GetLength(1) - 1; j++)
                {
                    Panel HorizontalLine = new Panel
                    {
                        Size = new Size(Game.Gameboard.getboard().GetLength(0) * 50, 3),
                        Location = new Point(50, 50 * j + offset - 2),
                        BackColor = Color.Black,
                    };
                    Controls.Add(HorizontalLine);
                }
                Panel VerticalLine = new Panel
                {
                    Size = new Size(3, Game.Gameboard.getboard().GetLength(0) * 50),
                    Location = new Point(50 * i + offset - 2, 50),
                    BackColor = Color.Black,
                };
                Controls.Add(VerticalLine);
            }
        }

        /// <summary>
        /// Moves the buttons when resizing form
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveButtons(int x, int y)
        {
            foreach (Control c in Controls)
            {
                if(c is Button)
                {
                    c.Location = new Point(x, c.Location.Y + y);
                }
            }
        }
        /// <summary>
        /// Adds all the required panels on the form
        /// </summary>
        public void AddPanelsToTiles()
        {           
            // Margin from top-left corner
            Point newLoc = new Point(50, 50);

            // Panels for each tile
            for (int y = 0; y < Game.Gameboard.getboard().GetLength(0); y++)
            {
                for (int x = 0; x < Game.Gameboard.getboard().GetLength(1); x++)
                {
                    Game.Gameboard.getboard()[x, y].Panel = new Panel
                    {
                        //Panel size is fixed 50 pixels
                        Size = new Size(50, 50),

                        // location starts from 50,50
                        Location = newLoc,
                        BackColor = Color.Transparent,
                    };
                    // Adds the panel click event for panel
                    Game.Gameboard.getboard()[x, y].Panel.Click += Pnl_Click;

                    // Moves it 50 pixels to the right for next panel
                    newLoc.Offset(50, 0);
                    Controls.Add(Game.Gameboard.getboard()[x, y].Panel);
                }

                // After each row is filled, move the offset back to the start, and drop down 50 pixels for the next row
                newLoc.Offset(-1 * Game.Gameboard.getboard().GetLength(0) * 50, 50);
            }

            // move buttons and resize the form to fit new boardsize
            MoveButtons(Game.Gameboard.getboard().GetLength(0) * 50 + 99, 0);
            Size = new Size(Game.Gameboard.getboard().GetLength(0) * 50 + 250, Game.Gameboard.getboard().GetLength(0) * 50 + 250);
        }

        /// <summary>
        /// Checks if game is over
        /// </summary>
        private void CheckGameOver()
        {
            if (Game.Over == true && checking == false && Game.Winner != null)
            {
                // if it's a draw dont add the "Winner" text at the start...
                if(Game.Winner != "Draw")
                    DisplayWinner("Winner: " + Game.Winner);
                else
                {
                    DisplayWinner(Game.Winner);
                }
            }
            else
            {
                CPUturn();
            }
        }

        /// <summary>
        /// Does a computers turn
        /// </summary>
        private void CPUturn()
        {
            // if the player is a CPU do a move for it
            if (Game.Players[Game.PlayerTurn].IsCPU)
            {
                var t = new Timer
                {
                    Interval = 600
                };
                if (Game.Delays == false)
                    t.Interval = 1;

                // Show the progressbar and start the progressbar
                lblCPUthink.Visible = true;
                progbarCPU.Visible = true;
                progbarCPU.PerformStep();

                // after the interval has finished
                t.Tick += (s, e) =>
                {
                    // use a machine learning move
                    if (Game.Over == false && checking == false)
                    {
                        MachineLearningMove();
                        CheckGameOver();
                    }
                    t.Stop();

                    // Hide the progressbar and reset it
                    progbarCPU.Value = 0;
                    lblCPUthink.Visible = false;
                    progbarCPU.Visible = false;
                };
                // start the timer
                t.Start();
            }
        }

        /// <summary>
        /// Display the winner for X amount of time
        /// </summary>
        /// <param name="winner"></param>
        /// <param name="Interval"></param>
        private void DisplayWinner(string winner, int Interval = 2500)
        {
            // start checking -> disable user input on the forms panels
            checking = true;

            // Show winner
            lblWinner.Text = winner;
            lblWinner.Show();

            // start a timer of set interval
            var t = new Timer();
            t.Interval = Interval;
            if (Game.Delays == false)
                t.Interval = 1;

            // after the interval has finished
            t.Tick += (s, e) =>
            {
                // hide the winner, stop timer
                lblWinner.Hide();
                t.Stop();

                // reset the game
                Game.ResetGame();

                // refresh form. ( Every panel gets cleared from all drawn shapes )
                Refresh();

                //Otherwise it selects a button as focus... for some reason...
                ActiveControl = lblWinner;

                // Disable checking and allow user input
                checking = false;

                lblWinner.Text = "";

                // check if the opponent is a CPU, and do their move if yes
                CPUturn();
            };
            // start the timer
            t.Start();
        }

        private void btnRandom_Move_Click(object sender, EventArgs e)
        {
            // use a random move
            if (Game.Over == false && checking == false)
            {
                Game.RANDOM_MOVE();
                CheckGameOver();
            }         
          
        }

        private void btn_MachineLearningMove_Click(object sender, EventArgs e)
        {
            // use a machine learning move
            if (Game.Over == false && checking == false)
            {
                MachineLearningMove();
                CheckGameOver();
            }            
            
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            // show the options tab
            Options o = new Options(ref Game);            
            o.ShowDialog();

            // Remove old separator panels
            DisposeExistingPanels();

            // reset game
            Game.ResetGame();

            // add new panels
            AddSeparatorPanels();
            AddPanelsToTiles();

            // refresh all controls
            Refresh();
        }

        private void btnHiScores_Click(object sender, EventArgs e)
        {
            HighScores h = new HighScores();
            h.ShowDialog();
        }

        /// <summary>
        /// When form is closed save the game options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }
        void SaveData()
        {
            Game.SaveOptions(Game.Delays, Game.AutomaticWinLose, Game.BoardSize, Game.Players);
        }
    }
}