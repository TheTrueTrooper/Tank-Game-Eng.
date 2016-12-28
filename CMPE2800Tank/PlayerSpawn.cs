﻿//*********************************************************************
//Program:     Lab – Tank Game
//Author:      Angelo Sanches and Whilow Schock
//class:       CMPE2800
//Date:        Oct sometime
//*******************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPE2800Tank
{

    class Spawn : Entity
    {/// <summary>
    /// a master list of all avalable spawns
    /// </summary>
        static List<Spawn> Spawns = new List<Spawn>();
        /// <summary>
        /// a lin to the random number generator from form
        /// </summary>
        static Random Rand = Form1.rng;
        /// <summary>
        /// to make a spawn also sets the checkdistance is big
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="col"></param>
        public Spawn(PointF pos, float rot, Color col) : base(pos, rot, col)
        {
            this.CheckAtDistance = 80.0f;
            Spawns.Add(this);
        }
        /// <summary>
        /// do nothing in path
        /// </summary>
        /// <returns></returns>
        public override GraphicsPath GetPath()
        {
            return new GraphicsPath();
        }
        /// <summary>
        /// so nothing in tick
        /// </summary>
        internal override void Tick()
        {
            return;
        }
        /// <summary>
        /// spans a tank using the controller and same inputs as tank
        /// </summary>
        /// <param name="PlayerToSpawn"></param>
        /// <param name="CallBack"></param>
        /// <param name="DangerousTanks"></param>
        /// <returns></returns>
        public static Tank SpawnTank(IControl PlayerToSpawn, Form1.delVoidEntity CallBack, List<Tank> DangerousTanks)
        {
            // should be null till we find one
            Spawn Safe = null;
            int Pos = 0;
            while(Safe == null)
            {
                // try for a random spawn point
                Pos = Rand.Next(0, Spawns.Count);
                // check to se iff there os an tank around
                foreach (Tank t in DangerousTanks)
                if (!(Utility.Distance(t.position, Spawns[Pos].position) < Spawns[Pos].CheckAtDistance + t.CheckAtDistance))
                    {
                        //none around set safe so we can break
                        Safe = Spawns[Pos];
                        break;
                    }
            }
            //Make a tank there and return it
            return new Tank(PlayerToSpawn, CallBack, Safe.position, Safe.rotation, PlayerToSpawn.PlayerColor);
        }
    }
}
