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
    class PUHealth : PickUpss
    {
        /// <summary>
        /// makes a sqare
        /// </summary>
        readonly static GraphicsPath model;

        static PUHealth()
        {
            model = new GraphicsPath();
            model.AddRectangle(new RectangleF(-16, -16, 32, 32));
        }
        /// <summary>
        /// set type to health and call base cons
        /// </summary>
        /// <param name="Pos"></param>
        /// <param name="Rot"></param>
        /// <param name="Colour"></param>
        /// <param name="value"></param>
        public PUHealth(PointF Pos, float Rot, Color Colour, float? value = default(float?)) : base(Pos, Rot, Colour, value)
        {
            Type = PickUPType.Health;
        }
        /// <summary>
        /// get shape and trans form
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
        // do nothing
        internal override void Tick()
        {
            return;
        }
    }
}
