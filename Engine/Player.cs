using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Engine
{
    public class Player: INotifyPropertyChanged
    {
        private Params props = new Params();
        private int _experiencePoints;
        private string _name;
        
        public Tile.TileValue PlayerTile { get; set; }

        public event EventHandler<MessageEventArgs> OnMessage;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                OnPropertyChanged("ExperiencePoints");
                OnPropertyChanged("Level");
            }
        }
        public string PlayerName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("PlayerName");
                OnPropertyChanged("Name");
            }
        }

        public int Level
        {
            get { return ((ExperiencePoints / 100) + 1); }
        }
        public string Name
        {
            get { return PlayerName; }
        }
      
        
        private Player(int experiencePoints, string name)
        {
            ExperiencePoints = experiencePoints;
            PlayerName = name;
        }

        public static Player CreateDefaultPlayer()
        {
            Player player = new Player(0, "default");
            return player;
        }

        public static Player CreatePlayerFromXmlString(string xmlPlayerData)
        {
            try
            {
                XmlDocument playerData = new XmlDocument();

                playerData.LoadXml(xmlPlayerData);

                int experiencePoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/ExperiencePoints").InnerText);
                string name = playerData.SelectSingleNode("/Player/Stats/PlayerName").InnerText;

                Player player = new Player(experiencePoints, name);

                return player;
            }
            catch
            {
                // If there was an error with the XML data, return a default player object
                return CreateDefaultPlayer();
            }
        }
        
        public string ToXmlString()
        {
            XmlDocument playerData = new XmlDocument();

            // Create the top-level XML node
            XmlNode player = playerData.CreateElement("Player");
            playerData.AppendChild(player);

            // Create the "Stats" child node to hold the other player statistics nodes
            XmlNode stats = playerData.CreateElement("Stats");
            player.AppendChild(stats);

            // Create the child nodes for the "Stats" node
            CreateNewChildXmlNode(playerData, stats, "ExperiencePoints", ExperiencePoints);
            CreateNewChildXmlNode(playerData, stats, "PlayerName", PlayerName);


            return playerData.InnerXml; // The XML document, as a string, so we can save the data to disk
        }

      
        private void AddExperiencePoints(int experiencePointsToAdd)
        {
            ExperiencePoints += experiencePointsToAdd;
        }
      
        private void CreateNewChildXmlNode(XmlDocument document, XmlNode parentNode, string elementName, object value)
        {
            XmlNode node = document.CreateElement(elementName);
            node.AppendChild(document.CreateTextNode(value.ToString()));
            parentNode.AppendChild(node);
        }

        private void AddXmlAttributeToNode(XmlDocument document, XmlNode node, string attributeName, object value)
        {
            XmlAttribute attribute = document.CreateAttribute(attributeName);
            attribute.Value = value.ToString();
            node.Attributes.Append(attribute);
        }
        public void SaveGame(string Winner, Tile[,] Gameboard)
        {
            //File.AppendAllText("GameData.xml", props.ToXmlString(Winner.Name, Loser.Name, Gameboard));

            string savedata = props.ToCSVString(Winner, Gameboard);
            //File.AppendAllText("GameData.csv", props.ToCSVString(Winner.Name, Loser.Name, Gameboard));

            if (!File.Exists("GameData.csv"))
            {
                string clientHeader = "Winner,tile1,tile2,tile3,tile4,tile5,tile6,tile7,tile8,tile9,OccupiedTiles" + Environment.NewLine;

                File.WriteAllText("GameData.csv", clientHeader);
            }

            File.AppendAllText("GameData.csv", savedata);
        }
        public void SaveMove(string Player, int TileID, Tile[,] Gameboard)
        {
            string savedata = props.MoveToCSVString(Player, TileID, Gameboard);

            if (!File.Exists("MoveData.csv"))
            {
                string clientHeader = "TilePlayed,TileID,tile1,tile2,tile3,tile4,tile5,tile6,tile7,tile8,tile9" + Environment.NewLine;

                File.WriteAllText("MoveData.csv", clientHeader);
            }

            File.AppendAllText("MoveData.csv", savedata);
        }

        private void RaiseMessage(string message)
        {
            if (OnMessage != null)
            {
                OnMessage(this, new MessageEventArgs(message));
            }
        }
    }
}