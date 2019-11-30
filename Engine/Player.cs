using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Engine
{
    /// <summary>
    /// Player object
    /// </summary>
    public class Player
    {
        // Current amount of experience
        private int _experiencePoints;
        
        // Tile the player is currently playing with X/O
        public Tile.TileValue PlayerTile { get; set; }

        // Experience points
        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                if (_experiencePoints <= 0)
                    _experiencePoints = 0;
            }
        }

        // Level of player
        public int Level
        {
            get { return (ExperiencePoints / 25) + 1; }
        }

        // Name of player
        public string Name { get; set; }
        
        /// <summary>
        /// Create a player object
        /// </summary>
        /// <param name="experiencePoints"></param>
        /// <param name="name"></param>
        private Player(int experiencePoints, string name)
        {
            ExperiencePoints = experiencePoints;
            Name = name;
        }

        /// <summary>
        /// Create a default player
        /// </summary>
        /// <returns></returns>
        public static Player CreateDefaultPlayer()
        {
            Player player = new Player(0, "default");
            return player;
        }
       
        /// <summary>
        /// Add experience to player
        /// </summary>
        /// <param name="experiencePointsToAdd"></param>
        public void AddExperiencePoints(int experiencePointsToAdd)
        {
            ExperiencePoints += experiencePointsToAdd;
        }
    }
}