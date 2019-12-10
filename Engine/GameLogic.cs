using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
namespace Engine
{
    /// <summary>
    /// Class containing the full Logical operations of a Tic Tac Toe Game
    /// </summary>
    public class GameLogic
    {
        #region Default variables

        /// <summary>
        /// Size of board. Eg. 3x3
        /// </summary>
        public int BoardSize { get; set; } = 3;

        /// <summary>
        /// The current "Winner" of the game
        /// </summary>
        public string Winner { get; set; }

        /// <summary>
        /// Game Over variable
        /// </summary>
        public bool Over { get; set; }

        /// <summary>
        /// Options
        /// </summary>
        public bool AutomaticWinLose { get; set; }
        public bool Delays { get; set; }

        /// <summary>
        /// The players in the game
        /// </summary>
        public Player[] Players { get; set; }     
        
        /// <summary>
        /// ID of the player currently set to play. "In turn"
        /// </summary>
        public int PlayerTurn { get; set; }
                
        /// <summary>
        /// Drawing object
        /// </summary>
        private Drawing drawing = new Drawing();

        /// <summary>
        /// How many moves have passed. ( If board is full, and no winner results in a draw )
        /// </summary>
        private int moveCount { get; set; }
        
        // empty board for creating a default game, and restoring it to default state ( Every tile to empty )
        public GameBoard Gameboard;
        #endregion

        #region Player functions
        /// <summary>
        /// Switch the turn from one player to another
        /// </summary>
        public void SwitchPlayerTurn()
        {
            PlayerTurn = (PlayerTurn == 0) ? 1 : 0;
        }

        /// <summary>
        /// Creates an array of 2 empty players
        /// </summary>
        public void CreateDefaultPlayers()
        {
            Players = (new Player[] { Player.CreateDefaultPlayer(), Player.CreateDefaultPlayer() });
        }
        #endregion

        #region Game functions
        /// <summary>
        /// Creates a default game object
        /// </summary>
        public void CreateDefaultGame()
        {
            // add a empty board
            Gameboard = new GameBoard(BoardSize);
           
            // Game is not over
            Over = false;

            // default options
            Delays = true;
            AutomaticWinLose = false;

            // 0 moves played so far
            moveCount = 0;

            // create empty players
            CreateDefaultPlayers();
            PlayerTurn = 0;

            // assign tile values for both players
            Players[0].PlayerTile = Tile.TileValue.X;
            Players[1].PlayerTile = Tile.TileValue.O;
        }

        public void NewBoard()
        {
            Gameboard = new GameBoard(BoardSize);
        }
        /// <summary>
        /// Resets the game.
        /// <para> empties the board, and puts values to 0 </para>
        /// </summary>
        public void ResetGame()
        {
            moveCount = 0;
            Winner = null;
            
            // reset the tiles
            foreach (Tile tile in Gameboard.getboard())
            {
                tile.Value = Tile.TileValue.NaN;
                //tile.Panel.Dispose();
            }
            
            Over = false;
            // add a empty board
            // Gameboard = new GameBoard(BoardSize);

        }
        #endregion

        #region Boards' functions
        /// <summary>
        /// Set the tile value at [X, Y] to given value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetBoardState(int x, int y, Tile.TileValue value)
        {
            bool Winner = false;
            int n = Gameboard.getboard().GetLength(0);
            moveCount++;

            Gameboard.SetBoardState(x, y, value);

            //Game Win-conditions ->

            //check column
            for (int i = 0; i < n; i++)
            {
                if (Gameboard.getboard()[x, i].Value != value)
                    break;

                // if hasn't triggered break so far, means all horizontal tiles have been of same value so far...
                if (i == n - 1)
                {
                    // -> Means we have a WINNER !
                    Winner = true;

                    // Add all current panels to a list
                    List<System.Windows.Forms.Panel> WinningPanels = new List<System.Windows.Forms.Panel>();
                    for (int p = 0; p < n; p++)
                    {
                        WinningPanels.Add(Gameboard.getboard()[x, p].Panel);
                    }

                    // send the list as array and draw a line showing what line won
                    drawing.DrawHorizontalLine(WinningPanels.ToArray());
                }
            }

            //check row
            for (int i = 0; i < n; i++)
            {
                if (Gameboard.getboard()[i, y].Value != value)
                    break;
                if (i == n - 1)
                {
                    Winner = true;
                    List<System.Windows.Forms.Panel> WinningPanels = new List<System.Windows.Forms.Panel>();
                    for (int p = 0; p < n; p++)
                    {
                        WinningPanels.Add(Gameboard.getboard()[p, y].Panel);
                    }
                    drawing.DrawVerticalLine(WinningPanels.ToArray());
                }
            }

            //check diagonal
            if (x == y)
            {
                for (int i = 0; i < n; i++)
                {
                    if (Gameboard.getboard()[i, i].Value != value)
                        break;
                    if (i == n - 1)
                    {
                        Winner = true;
                        List<System.Windows.Forms.Panel> WinningPanels = new List<System.Windows.Forms.Panel>();
                        for (int p = 0; p < n; p++)
                        {
                            WinningPanels.Add(Gameboard.getboard()[p, p].Panel);
                        }
                        drawing.DrawDiagonalLine(WinningPanels.ToArray());
                    }
                }
            }

            // check antidiagonal (the other way than regular diagonal)
            if (x + y == n - 1)
            {
                for (int i = 0; i < n; i++)
                {
                    // Thanks to a helper in stackoverflow for the -> [i, (n - 1) - i]
                    if (Gameboard.getboard()[i, (n - 1) - i].Value != value)
                        break;
                    if (i == n - 1)
                    {
                        Winner = true;
                        List<System.Windows.Forms.Panel> WinningPanels = new List<System.Windows.Forms.Panel>();
                        for (int p = 0; p < n; p++)
                        {
                            WinningPanels.Add(Gameboard.getboard()[p, (n - 1)-p].Panel);
                        }
                        drawing.DrawAntiDiagonalLine(WinningPanels.ToArray());
                    }
                }
            }


            //check draw, if movecount is same as for eg. 3^2 = 9, and winner is not true
            if (moveCount == (Math.Pow(n, 2)) && Winner != true)
            {
                this.Winner="Draw";
                Over = true;
            }

            // check if winner is true
            if (Winner == true)
            {
                Over = true;               

                // set the winner as the winning tile value
                this.Winner = Players.FirstOrDefault(X => X.PlayerTile == value).PlayerTile.ToString();

                // add 5 experience for the winner, remove 5 from loser
                Players.FirstOrDefault(X => X.PlayerTile == value).AddExperiencePoints(5);
                Players.FirstOrDefault(X => X.PlayerTile != value).AddExperiencePoints(-5);

                SaveGame(Players.FirstOrDefault(X => X.PlayerTile == value).Name, Players.FirstOrDefault(X => X.PlayerTile != value).Name);
            }
        }

        #region Move functions
        /// <summary>
        /// Smart function that returns true
        /// <para> if a winning move is present on the current layout </para>
        /// <para> if a losing move is present on the current layout </para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckWinningMove(int x, int y, Tile.TileValue value)
        {
            // same code as in the setboardstate function, apart from removed saves
            bool Winner = false;
            int n = 3;

            Tile[,] board = Gameboard.getboard();

            if (board[x, y].Value == Tile.TileValue.NaN)
                board[x, y].Value = value;

            for (int i = 0; i < n; i++)
            {
                if (board[x, i].Value != value)
                    break;
                if (i == n - 1)
                    Winner = true;
            }

            for (int i = 0; i < n; i++)
            {
                if (board[i, y].Value != value)
                    break;
                if (i == n - 1)
                    Winner = true;
            }

            if (x == y)
                for (int i = 0; i < n; i++)
                {
                    if (board[i, i].Value != value)
                        break;
                    if (i == n - 1)
                        Winner = true;
                }

            if (x + y == n - 1)
                for (int i = 0; i < n; i++)
                {
                    if (board[i, (n - 1) - i].Value != value)
                        break;
                    if (i == n - 1)
                        Winner = true;
                }

            //IMPORTANT!!!! revert changes
            //Because when dealing with a 2D array, the copy is only a "shallow" copy, so whenever you modify the contents of copy
            //it also modifies the contents of original array
            board[x, y].Value = Tile.TileValue.NaN;
            return Winner;
        }
        /// <summary>
        /// Performs a random move, from a list of legal moves
        /// </summary>
        public void RANDOM_MOVE()
        {
            Random r = new Random();

            // list of possible legal moves
            List<Tile> legalMoves = new List<Tile>();
            foreach (Tile tile in Gameboard.getboard())
            {
                if (tile.Value == Tile.TileValue.NaN)
                {
                    legalMoves.Add(tile);
                }

            }
            // select a random move from legals
            Tile randomTile = legalMoves[r.Next(0, legalMoves.Count - 1)];
            if (randomTile.CheckTileState())
            {
                MakeMove(randomTile.X, randomTile.Y, Players[PlayerTurn].PlayerTile);
            }
        }

        /// <summary>
        /// Checks if a winning / losing move is possible, and executes / prevents it
        /// </summary>
        public void PredictMove()
        {
            List<Tile> legalMoves = new List<Tile>();
            foreach (Tile tile in Gameboard.getboard())
            {
                if (tile.Value == Tile.TileValue.NaN)
                {
                    legalMoves.Add(tile);
                }
            }

            bool winningmove = false;
            bool opponentwinningmove = false;
            int MoveX = 0;
            int MoveY = 0;

            // Executes a winning move if one exists, otherwise executes a move that doesn't allow the opponent to win (unless opponent has a position with 2 or more winning tiles)
            for (int i = 0; i < legalMoves.Count; i++)
            {
                winningmove = CheckWinningMove(legalMoves[i].X, legalMoves[i].Y, Players[PlayerTurn].PlayerTile);
                opponentwinningmove = CheckWinningMove(legalMoves[i].X, legalMoves[i].Y, Players[PlayerTurn == 1 ? 0 : 1].PlayerTile);
                if (winningmove)
                {
                    MoveX = legalMoves[i].X;
                    MoveY = legalMoves[i].Y;
                    break;
                }
                if (opponentwinningmove)
                {
                    MoveX = legalMoves[i].X;
                    MoveY = legalMoves[i].Y;
                    break;
                }
            }
            if (winningmove)
            {
                if (Gameboard.getboard()[MoveX, MoveY].CheckTileState())
                {
                    // save function for machine learning
                    SaveMove(Players[PlayerTurn].PlayerTile.ToString(), Gameboard.getboard()[MoveX, MoveY].ID, Gameboard.getboard()); 
                    MakeMove(MoveX, MoveY, Players[PlayerTurn].PlayerTile);
                }
            }
            else if (opponentwinningmove)
            {
                if (Gameboard.getboard()[MoveX, MoveY].CheckTileState())
                {
                    // save function for machine learning
                    SaveMove(Players[PlayerTurn].PlayerTile.ToString(), Gameboard.getboard()[MoveX, MoveY].ID, Gameboard.getboard());
                    MakeMove(MoveX, MoveY, Players[PlayerTurn].PlayerTile);
                }
            }

        }
        /// <summary>
        /// Makes the move and draws a shape, depending which player made the move
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="value"></param>
        public void MakeMove(int X, int Y, Tile.TileValue value)
        {
            // if the game hasnt been won yet...
            if(Winner == null)
            {
                // who is in turn
                if (PlayerTurn == 0)
                {
                    drawing.DrawCross(Gameboard.getboard()[X, Y].Panel);
                }
                else
                {
                    drawing.DrawCircle(Gameboard.getboard()[X, Y].Panel);
                }

                // set the state of board
                SetBoardState(X, Y, value);

                // switch players
                SwitchPlayerTurn();
                
                if(AutomaticWinLose)
                PredictMove();
            }
        }

        #endregion

        #endregion

        #region Saving to file / Reading from file region
        /// <summary>
        /// returns a CSV file containing the data of executed moves ( used for ML )
        /// </summary>
        /// <param name="TilePlayed"></param>
        /// <param name="TileID"></param>
        /// <param name="Gameboard"></param>
        /// <returns></returns>
        public string MoveToCSVString(string TilePlayed, int TileID, Tile[,] Gameboard)
        {
            string MainData = "";
            string boardData = "";
            for (int y = 0; y < Gameboard.GetLength(0); y++)
            {
                for (int x = 0; x < Gameboard.GetLength(1); x++)
                {
                    Tile tile = Gameboard[x, y];
                    boardData += "," + tile.Value;
                }
            }
            MainData += TilePlayed + "," + TileID + boardData + Environment.NewLine;
            return MainData;
        }

        /// <summary>
        /// Saves the current game at the end of a CSV file (Gamedata) (used for ML)
        /// </summary>
        /// <param name="Winner"></param>
        /// <param name="Gameboard"></param>
        public void SaveGame(string Winner, string Loser)
        {
            List<SaveGame> items = new List<SaveGame>();
            if (File.Exists("Games.json"))
            {
                using (StreamReader r = new StreamReader("Games.json"))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<SaveGame>>(json);
                }
            }

            items.Add(new SaveGame()
            {
                Winner = Winner,
                Loser = Loser
            });

            //open file stream
            using (StreamWriter file = File.CreateText("Games.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, items);
            }
           
        }

        /// <summary>
        /// Saves the move at the end of a CSV file (Moves) (used for ML)
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="TileID"></param>
        /// <param name="Gameboard"></param>
        public void SaveMove(string Player, int TileID, Tile[,] Gameboard)
        {
            string savedata = MoveToCSVString(Player, TileID, Gameboard);

            if (!File.Exists("MoveData.csv"))
            {
                string clientHeader = "TilePlayed,TileID,tile1,tile2,tile3,tile4,tile5,tile6,tile7,tile8,tile9" + Environment.NewLine;

                File.WriteAllText("MoveData.csv", clientHeader);
            }

            File.AppendAllText("MoveData.csv", savedata);
        }

        #region XML region
        /// <summary>
        /// Saves the current options to a XML file
        /// </summary>
        /// <param name="Delays"></param>
        /// <param name="AutomaticWinLose"></param>
        /// <param name="players"></param>
        public void SaveOptions(bool Delays, bool AutomaticWinLose, int BoardSize, Player[] players)
        {
            string options = Options(Delays, AutomaticWinLose, BoardSize, players);
            File.WriteAllText("Options.xml", options);
        }

        /// <summary>
        /// Reads the options from a XML file to the current game
        /// </summary>
        /// <returns></returns>
        public static GameLogic LoadOptions()
        {
            GameLogic game = new GameLogic();
            if (File.Exists("Options.xml"))
                game = CreateGameFromXmlString(File.ReadAllText("Options.xml"));
            else
                game.CreateDefaultGame();

            return game;
        }

        /// <summary>
        /// sets the games players and options from a XML file
        /// </summary>
        /// <param name="xmlGameData"></param>
        /// <returns></returns>
        private static GameLogic CreateGameFromXmlString(string xmlGameData)
        {
            try
            {
                // create a xml document
                XmlDocument gameData = new XmlDocument();

                // load the data
                gameData.LoadXml(xmlGameData);

                // get the values for options
                bool Delays = Convert.ToBoolean(gameData.SelectSingleNode("/Game/Options/Delays").InnerText);
                bool AutomaticWinLose = Convert.ToBoolean(gameData.SelectSingleNode("/Game/Options/AutomaticWinLose").InnerText);
                int BoardSize = Convert.ToInt32(gameData.SelectSingleNode("/Game/Options/BoardSize").InnerText);

                // create a default game object
                GameLogic game = new GameLogic();
                game.BoardSize = BoardSize;
                game.CreateDefaultGame();

                // then set the options
                game.Delays = Delays;
                game.AutomaticWinLose = AutomaticWinLose;
             

                int i = 0;
                foreach (XmlNode node in gameData.SelectNodes("/Game/Players/Player"))
                {
                    // set the players
                    game.Players[i].AddExperiencePoints(Convert.ToInt32(node.Attributes["Experience"].Value));
                    game.Players[i].Name = node.Attributes["Name"].Value;
                    game.Players[i].IsCPU = Convert.ToBoolean(node.Attributes["IsCPU"].Value);
                    i++;
                }

                // return the created game object
                return game;
            }
            catch
            {
                GameLogic defaultgame = new GameLogic();
                defaultgame.CreateDefaultGame();

                // If there was an error with the XML data, return a default game object
                return defaultgame;
            }
        }
        /// <summary>
        /// returns a XML document from given game data
        /// </summary>
        /// <param name="Delays"></param>
        /// <param name="AutomaticWinLose"></param>
        /// <param name="players"></param>
        /// <returns></returns>
        public string Options(bool Delays, bool AutomaticWinLose, int BoardSize, Player[] players)
        {
            XmlDocument GameData = new XmlDocument();

            // Create the top-level XML node
            XmlNode Game = GameData.CreateElement("Game");
            GameData.AppendChild(Game);

            // Create the "Stats" child node to hold the statistics nodes
            XmlNode Options = GameData.CreateElement("Options");
            Game.AppendChild(Options);

            // Create the child nodes for the "Stats" node
            CreateNewChildXmlNode(GameData, Options, "Delays", Delays);
            CreateNewChildXmlNode(GameData, Options, "AutomaticWinLose", AutomaticWinLose);
            CreateNewChildXmlNode(GameData, Options, "BoardSize", BoardSize);

            // Create the "GameBoard" child node to hold each Tile node
            XmlNode Players = GameData.CreateElement("Players");
            Game.AppendChild(Players);
            foreach (Player player in players)
            {
                XmlNode Player = GameData.CreateElement("Player");

                AddXmlAttributeToNode(GameData, Player, "Experience", player.ExperiencePoints);
                AddXmlAttributeToNode(GameData, Player, "Name", player.Name);
                AddXmlAttributeToNode(GameData, Player, "IsCPU", player.IsCPU);
                Players.AppendChild(Player);
            }
            return GameData.InnerXml;
        }

        /// <summary>
        /// Creates a Child XML node
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parentNode"></param>
        /// <param name="elementName"></param>
        /// <param name="value"></param>
        private void CreateNewChildXmlNode(XmlDocument document, XmlNode parentNode, string elementName, object value)
        {
            XmlNode node = document.CreateElement(elementName);
            node.AppendChild(document.CreateTextNode(value.ToString()));
            parentNode.AppendChild(node);
        }

        /// <summary>
        /// Adds a attribute to a XML node
        /// </summary>
        /// <param name="document"></param>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        private void AddXmlAttributeToNode(XmlDocument document, XmlNode node, string attributeName, object value)
        {
            XmlAttribute attribute = document.CreateAttribute(attributeName);
            attribute.Value = value.ToString();
            node.Attributes.Append(attribute);
        }
        #endregion
        #endregion
    }
}
