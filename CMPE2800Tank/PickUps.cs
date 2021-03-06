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
    /// <summary>
    /// diffenent types of pickups
    /// </summary>
    enum PickUPType
    {
        Health,
        Speed
    }

    /// <summary>
    /// Pick up base class
    /// </summary>
    abstract class PickUpss : Entity
    {
        /// <summary>
        /// the type of pick up we are
        /// </summary>
        public PickUPType Type { get; protected set; }

        /// <summary>
        /// our value to use in tank (scaler)
        /// </summary>
        public float Value { get; protected set; } = 1.5f;
        /// <summary>
        /// Constructor sets value and should set type later
        /// </summary>
        /// <param name="Pos"></param>
        /// <param name="Rot"></param>
        /// <param name="Colour"></param>
        /// <param name="value"></param>
        public PickUpss(PointF Pos, float Rot, Color Colour, float? value= null) : base(Pos, Rot, Colour)
        {
            if (value != null)
                Value = value.Value;

            CheckAtDistance = 24;
        }
        /// <summary>
        /// it is noly a simple check here and let the tank do the rest to keep fast
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gr"></param>
        /// <returns></returns>
        public override bool DetectColish(Entity sender, Graphics gr)
        {
            if (!(sender is Tank))
                return false;
            if (!base.DetectColish(sender, gr))
                return false;

                (sender as Tank).PickUp(this);

                IsMarkedForDeath = true;

            return true;
        }
    }
    
}
