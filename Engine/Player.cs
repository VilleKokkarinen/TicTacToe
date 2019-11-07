using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace Engine
{
    public class Player: User
    {
        private int _experiencePoints;
        private string _name;
        
        public Tile.TileValue PlayerTile { get; set; }

        public event EventHandler<MessageEventArgs> OnMessage;
        
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
        
        private void RaiseMessage(string message, bool addExtraNewLine = false)
        {
            if (OnMessage != null)
            {
                OnMessage(this, new MessageEventArgs(message, addExtraNewLine));
            }
        }
    }
}