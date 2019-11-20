using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Engine
{
    public class Params
    {
        public int LineWidth { get; set; } = 3;
        public int Margin { get; set; } = 5;
        public int Size { get; set; } = 47 + 3;

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
        public string ToXmlString(string Winner, string Loser, Tile[,]Gameboard)
        {
            XmlDocument GameData = new XmlDocument();

            // Create the top-level XML node
            XmlNode Game = GameData.CreateElement("Game");
            GameData.AppendChild(Game);

            // Create the "Stats" child node to hold the statistics nodes
            XmlNode stats = GameData.CreateElement("Stats");
            Game.AppendChild(stats);

            // Create the child nodes for the "Stats" node
            CreateNewChildXmlNode(GameData, stats, "Winner", Winner);
            CreateNewChildXmlNode(GameData, stats, "Loser", Loser);

            // Create the "GameBoard" child node to hold each Tile node
            XmlNode GameBoard = GameData.CreateElement("GameBoard");
            Game.AppendChild(GameBoard);

            // Create an "Tile" node for each Tile in the Gameboard
            foreach (Tile tile in Gameboard)
            {
                XmlNode Tile = GameData.CreateElement("Tile");

                AddXmlAttributeToNode(GameData, Tile, "ID", tile.ID);
                AddXmlAttributeToNode(GameData, Tile, "X", tile.X);
                AddXmlAttributeToNode(GameData, Tile, "Y", tile.Y);
                AddXmlAttributeToNode(GameData, Tile, "Value", tile.Value);

                GameBoard.AppendChild(Tile);
            }

            return GameData.InnerXml;
        }
        public string ToCSVString(string Winner, Tile[,] Gameboard)
        {
            string MainData = "";
            string boardData = "";
            int occupied = 0;
            for (int y = 0; y < Gameboard.GetLength(0); y++)
            {
                for (int x = 0; x < Gameboard.GetLength(1); x++)
                {
                    Tile tile = Gameboard[x, y];
                    boardData += "," + tile.Value;
                    if (tile.Value != Tile.TileValue.NaN)
                        occupied++;
                }
            }
            MainData += Winner + boardData + "," + occupied + Environment.NewLine;
            return MainData;
        }
        public string MoveToCSVString(string TilePlayed, int TileID, Tile[,] Gameboard)
        {
            string MainData = "";
            string boardData = "";
            for(int y = 0; y < Gameboard.GetLength(0); y++)
            {
                for (int x = 0; x < Gameboard.GetLength(1); x++)
                {
                    Tile tile = Gameboard[x, y];
                    boardData += "," + tile.Value;
                }
            }
            MainData += TilePlayed +","+ TileID + boardData + Environment.NewLine;
            return MainData;
        }
    }
}
