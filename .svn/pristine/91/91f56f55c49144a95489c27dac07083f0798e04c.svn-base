//*********************************************************************
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

namespace CMPE2800Tank {
    /// <summary>
    /// the partical
    /// </summary>
    class Particle {
        /// <summary>
        /// its current position
        /// </summary>
        public PointF pos;
        /// <summary>
        /// the vector of travel
        /// </summary>
        public PointF vel;
        public byte life;
        public Brush color;
        /// <summary>
        /// move it along the vector
        /// </summary>
        public void Tick() {
            pos = Utility.AddVectors(pos, vel);
            life--;
        }
    }
}
