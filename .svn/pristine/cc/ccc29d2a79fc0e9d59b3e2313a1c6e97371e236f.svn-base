//*********************************************************************
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
    class PUSpeed : PickUpss
    {
        //make a static shape
        readonly static GraphicsPath model;

        static PUSpeed()
        {
            model = new GraphicsPath();
            model.AddRectangle(new RectangleF(-16, -16, 32, 32));
        }
        /// <summary>
        /// set type to speed and call base cons
        /// </summary>
        /// <param name="Pos"></param>
        /// <param name="Rot"></param>
        /// <param name="Colour"></param>
        /// <param name="value"></param>
        public PUSpeed(PointF Pos, float Rot, Color Colour, float? value = default(float?)) : base(Pos, Rot, Colour, value)
        {
            Type = PickUPType.Speed;
        }
        /// <summary>
        /// draw a box and transform
        /// </summary>
        /// <returns></returns>
        public override GraphicsPath GetPath()
        {
            GraphicsPath graphicsPath = (GraphicsPath)model.Clone();
            Matrix matrix = new Matrix();
            matrix.Translate(position.X, position.Y);
            graphicsPath.Transform(matrix);

            return graphicsPath;
        }
        /// <summary>
        /// do nothing
        /// </summary>
        internal override void Tick()
        {
            return;
        }
    }
}
