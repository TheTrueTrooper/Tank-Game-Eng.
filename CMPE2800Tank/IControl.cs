﻿//*********************************************************************
//Program:     Lab – Tank Game
//Author:      Angelo Sanches and Whilow Schock
//class:       CMPE2800
//Date:        Oct sometime
//*******************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPE2800Tank
{
    interface IControl
    {
        /// <summary>
        /// a bool to move forward
        /// </summary>
        bool MoveForward { get; }
        /// <summary>
        /// a bool to move back
        /// </summary>
        bool MoveBackward { get; }
        /// <summary>
        /// a bool to rotate left
        /// </summary>
        bool RotateLeft { get; }
        /// <summary>
        /// a bool to rotate right
        /// </summary>
        bool RotateRight { get; }
        /// <summary>
        /// a bool to rotate the cannon right
        /// </summary>
        bool CannonRight { get; }
        /// <summary>
        /// a bool to rotate the cannon left
        /// </summary>
        bool CannonLeft { get; }
        /// <summary>
        /// a bool to fire the cannon or charge
        /// </summary>
        bool FireCannon { get; }
        /// <summary>
        /// a player number
        /// </summary>
        int PlayerNumber { get; set; }
        /// <summary>
        /// a player color to set 
        /// </summary>
        Color PlayerColor { get; }
        /// <summary>
        /// a score to display
        /// </summary>
        byte PlayerScore { get; }
    }
}
