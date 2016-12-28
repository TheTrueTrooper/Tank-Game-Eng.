//*********************************************************************
//Program:     Lab – Tank Game
//Author:      Angelo Sanches and Whilow Schock
//class:       CMPE2800
//Date:        Oct sometime
//*******************************************************************
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
/// <summary>
/// I (Angelo) will not be resposible for commenting this
/// </summary>
namespace CMPE2800Tank {
    class AI : IControl {
        public System.Drawing.Color PlayerColor { get{ return System.Drawing.Color.Yellow; } }
        /// dummy bools
        public bool CannonLeft { get; private set; }
        public bool CannonRight { get; private set; }
        public bool FireCannon { get; private set; }
        public bool MoveBackward { get; private set; }
        public bool MoveForward { get; private set; }
        public bool RotateLeft { get; private set; }
        public bool RotateRight { get; private set; }
        public bool IsConnected { get; private set; }
        public byte PlayerScore { get { return 0; } }
        // return -1 for AI and leave a dummy set
        public int PlayerNumber {
            get { return -1; }
            set { PlayerNum = (PlayerIndex)255; }
        }

        public PlayerIndex PlayerNum { get; set; }

        public void Update(List<Entity> entities, Tank self) {
            //find nearest enemy
            var enemies = from e in entities where e is Tank where !e.Equals(self) select e;
            Entity closest = enemies.ToList().OrderBy(o => o.position.Distance(self.position)).First();
            float targetAngle = self.position.AngleTo(closest.position);

            //aim at target
            if (Math.Abs(self.cannonAngle - targetAngle) > 2 ) {
                CannonLeft = (targetAngle - self.cannonAngle < 0 || targetAngle - self.cannonAngle > 180);
                //CannonRight = (targetAngle - self.cannonAngle > 0 && targetAngle - self.cannonAngle > 360);
                CannonRight = !CannonLeft;
            } else {
                CannonLeft = false;
                CannonRight = false;
            }

            //move towards target if too far away
            if (closest.position.Distance(self.position) > 200) {
                //face target
                if (Math.Abs(self.rotation - targetAngle) > 2) {
                    RotateLeft = (targetAngle - self.rotation < 0 || targetAngle - self.rotation > 180);
                    RotateRight = !RotateLeft;
                } else {
                    RotateLeft = false;
                    RotateRight = false;
                }
                //move towards target
                MoveForward = Math.Abs(self.rotation - targetAngle) < 90;
                MoveBackward = !MoveForward;
            } else {
                MoveForward = false;
                MoveBackward = false;
                RotateLeft = false;
                RotateRight = false;
            }

        }
    }
}
