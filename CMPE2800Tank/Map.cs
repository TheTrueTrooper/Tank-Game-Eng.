﻿//*********************************************************************
//Program:     Lab – Tank Game
//Author:      Angelo Sanches and Whilow Schock
//class:       CMPE2800
//Date:        Oct sometime
//*******************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace CMPE2800Tank
{
    /*
     * 
     * it is very important to let certain things throw till the proper level here so allow it plz
     * this lets us try to contuine if posible
     * 
     */
    #region light wiegh objects for half load of XML
    /// <summary>
    /// a light wieght rep single space or tile
    /// </summary>
    class Space
    {
        /// <summary>
        /// different types of spaces
        /// </summary>
        public enum SpaceType
        {
            Wall,
            Open
        };
        /// <summary>
        /// the items, objects, actors, or entities that can ocupie
        /// </summary>
        public enum Item
        {
            PlayerSpawn,
            HeathPickUp,
            SpeedPickUp
        };
        /// <summary>
        /// its location on X
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// its location on Y
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// its Type
        /// </summary>
        public SpaceType Type { get; private set; }
        /// <summary>
        /// the actors contained in
        /// </summary>
        public List<Item> Items { get; private set; }
        /// <summary>
        /// builds the space from strings and items let throw
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        /// <param name="iItems"></param>
        public Space(string x, string y, string type, List<Item> iItems)
        {
            
            switch (type)
            {
                //if type is any thing but throw a miss mach expt
                default:
                    throw new Exception("Type Mismach");
                    //if wall or open set acording
                case "Wall":
                    Type = SpaceType.Wall;
                    break;
                case "Open":
                    Type = SpaceType.Open;
                    break;
            }
            // set x , y and allow throws so we can log
            X = int.Parse(x);
            Y = int.Parse(y);
            //grab a ref to items
            Items = iItems;
        }
    }
    /// <summary>
    /// a light wieght level storer 
    /// </summary>
    class Level
    {
        /// <summary>
        /// the spaces
        /// </summary>
        public List<Space> Spaces { get; private set; }
        /// <summary>
        /// the background loaction
        /// </summary>
        public string BackGround { get; private set; }
        /// <summary>
        /// the levels tile size on x
        /// </summary>
        public int sizeX { get; private set; }
        /// <summary>
        /// the levels tile size on y
        /// </summary>
        public int sizeY { get; private set; }
        /// <summary>
        /// the constructor let throw on creations
        /// </summary>
        /// <param name="spaces"></param>
        /// <param name="iBackGround"></param>
        /// <param name="isizeX"></param>
        /// <param name="isizeY"></param>
        public Level(List<Space> spaces, string iBackGround, string isizeX, string isizeY)
        {
            Spaces = spaces;
            BackGround = iBackGround;
            sizeX = int.Parse(isizeX);
            sizeY = int.Parse(isizeY);
        }
    }
    #endregion

    #region Map Full Objects to give to form (full load)
    /// <summary>
    /// a map object holds real full data to be shared with form
    /// </summary>
    class Map
    {
        ///<summary>
        ///the backgrounds loaction
        ///</summary>
        public string BackGround { get; private set; }
        ///<summary>
        //the objects actors or entitys to give back to the form
        ///</summary>
        public List<Entity> Objects { get; private set; }
        ///<summary>
        //our tile sizeX
        ///</summary>
        public int sizeX { get; private set; }
        ///<summary>
        //our tile sizeY
        ///</summary>
        public int sizeY { get; private set; }
        /// <summary>
        /// stores the objs in proper seats for us
        /// </summary>
        /// <param name="Entities"></param>
        /// <param name="isizeX"></param>
        /// <param name="isizeY"></param>
        /// <param name="iBackGround"></param>
        internal Map(List<Entity> Entities, int isizeX, int isizeY, string iBackGround = null)
        {
            Objects = Entities;
            BackGround = iBackGround;
            if (!File.Exists(Environment.CurrentDirectory + "\\..\\..\\LevelBackGround\\" + BackGround))
                BackGround = null;
            sizeX = isizeX;
            sizeY = isizeY;
        }
    }
    #endregion

    #region The Loader that loads both light wieght and Full
    static class MapLoader
        {
            // a const tiles size
            public const int TileSize = 32;
            // the xml to load froms path
            static readonly string MapsXMLPath = Environment.CurrentDirectory + "\\..\\..\\Level.txt";
            // the error loggers path
            static readonly string ErrorTxtPath = Environment.CurrentDirectory + "\\Error.txt";
            // the list of lightwieght maps to be used
            static readonly List<Level> Levels;
            #region the xml Parser stuff and light weight builder
            /// <summary>
            /// build a static set of static templates we can load from from the xml
            /// </summary>
        static MapLoader()
        {
                // make  a list to add to
            Levels = new List<Level>();
            //a string we will read to
            string XMLRaw;

            ///DO NOT MOVE XML FILE FROM SOURCE files <---------------------!!!! but you can move your folder for the project
            // try and read the file to a string
            try
            {
                    // grab the xml from file
                using (StreamReader SR = File.OpenText(MapsXMLPath))
                {
                    XMLRaw = SR.ReadToEnd();
                }

                //start a parser
                using (XmlReader XR = XmlReader.Create(new StringReader(XMLRaw)))
                {
                        //read for all note closing brases will be delt with at every function level to allow us to leave 
                    while (XR.Read())
                    {
                            //read a node till we can start a level
                        switch (XR.NodeType)
                        {
                            case XmlNodeType.Element:
                                    if (XR.Name == "Level")
                                        try
                                        {
                                            Levels.Add(ReadForLevel(XR));//parse level to the list
                                        }
                                        catch(Exception e)// else contuine
                                        {
                                            //if ther is a parse error log it and and hope we have on level
                                            using (StreamWriter SW = new StreamWriter(File.OpenWrite(ErrorTxtPath)))
                                                SW.WriteLine("Error on Map load - " + e);
                                        }
                                break;

                        }
                    }
                }
            }
            catch (IOException e)
            {// if it is a file load expt just throw it out to next
                    throw e;
            }
            catch (Exception e)
            {
                    //if ther is a parse error log it and and hope we have on level
                using (StreamWriter SW = new StreamWriter(File.OpenWrite(ErrorTxtPath)))
                    SW.WriteLine("Error on Map load - " + e);
            }
                // if there isnot even one level just throw
            if (Levels.Count < 1)
                throw new Exception("Failed to Parse a single map. Check error log for more\n" + ErrorTxtPath);

        }

        // I have Staged out the parsing to make keeping track easyer
        // Every stage is for a collection

        /// <summary>
        /// parse out for the level
        /// </summary>
        /// <param name="XR"></param>
        /// <returns></returns>
        static Level ReadForLevel(XmlReader XR)
        {
                //some self explanitory stuff
            string Background = null;
            string sizeX = null;
            string sizeY = null;
            List<Space> Spaces = new List<Space>();
            //will read to end
            while (XR.Read())
            {
                switch (XR.NodeType)
                {
                        //for each node type store aproprate
                    case XmlNodeType.Element:
                        switch (XR.Name)
                        {
                            case "sizeX":
                                sizeX = XR.ReadInnerXml();
                                break;
                            case "sizeY":
                                sizeY = XR.ReadInnerXml();
                                break;
                            case "Background":
                                string temp = XR.ReadInnerXml();
                                Background = temp.Length > 0 ? temp : null;
                                break;
                            case "Space":
                                    try
                                    {
                                        Spaces.Add(ReadForSpace(XR));//parse for space items
                                    }
                                    catch(Exception e)// else contuine
                                    {
                                        //if ther is a parse error log it and and hope we have on level
                                        using (StreamWriter SW = new StreamWriter(File.OpenWrite(ErrorTxtPath)))
                                            SW.WriteLine("Error on Map load - " + e);
                                    }
                                break;
                        }
                        break;
                        //if we have hit the end of the element brake out and return
                    case XmlNodeType.EndElement:
                        if (XR.Name == "Level")
                                return new Level(Spaces, Background, sizeX, sizeY);
                        break;
                }
            }
            // if hits the end and we have not closed there is a problem with the file
            throw new Exception("Error : unexpected End of File at Space");
        }

        static Space ReadForSpace(XmlReader XR)
        {
                //another self expanitory vars
            List<Space.Item> Items = new List<Space.Item>();
            string X = null;
            string Y = null;
            string Type = null;
            //pases eadch node in here till end
            while (XR.Read())
            {
                switch(XR.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (XR.Name)
                        {
                            case "x":
                                X = XR.ReadInnerXml();
                                break;
                            case "y":
                                Y = XR.ReadInnerXml();
                                break;
                            case "Type":
                                Type = XR.ReadInnerXml();
                                break;
                            case "Items":
                                    try
                                    {
                                        Items = ParseForItems(XR);// parse for items
                                    }
                                    catch(Exception e)// else contuine
                                    {
                                        //if ther is a parse error log it and and hope we have on level
                                        using (StreamWriter SW = new StreamWriter(File.OpenWrite(ErrorTxtPath)))
                                            SW.WriteLine("Error on Map load - " + e);
                                    }
                                    break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                            if (XR.Name == "Space")
                                try
                                { // if ended make  an space obj
                                    return new Space(X, Y, Type, Items);
                                }
                                catch (Exception e)// else contuine
                                {
                                    //if ther is a parse error log it and and hope we have on level
                                    using (StreamWriter SW = new StreamWriter(File.OpenWrite(ErrorTxtPath)))
                                        SW.WriteLine("Error on Map load - " + e);
                                }
                            break;
                }
            }
                // if hits the end and we have not closed there is a problem with the file
                throw new Exception("Error : unexpected End of File at Space");
        }

        static List<Space.Item> ParseForItems(XmlReader XR)
        {
            List<Space.Item> Items = new List<Space.Item>();
            while (XR.Read())
            {
                switch (XR.NodeType)
                {
                    case XmlNodeType.Element:
                        if (XR.Name == "ItemName")
                                switch (XR.ReadInnerXml())
                                {// identify the items and set it in list based on type specified
                                    case "Spawn":
                                        Items.Add(Space.Item.PlayerSpawn);
                                        break;
                                    case "HeathPickUp":
                                        Items.Add(Space.Item.HeathPickUp);
                                        break;
                                    case "SpeedPickUp":
                                        Items.Add(Space.Item.SpeedPickUp);
                                        break;
                                }
                        break;
                    case XmlNodeType.EndElement:
                        if (XR.Name == "Items")
                                return Items;
                        break;
                }
            }
                // if hits the end and we have not closed there is a problem with the file
                throw new Exception("Error : unexpected End of File at Space");
        }
            #endregion

            #region Map and level object loading methods
            /// <summary>
            /// simply picks a random form the static lightwight maps
            /// </summary>
            /// <returns></returns>
            static Level NextMap()
        {
            return Levels[Form1.rng.Next(0, Levels.Count)];
        }

            /// <summary>
            /// load will build map from light wieght templates
            /// </summary>
            /// <returns></returns>
        public static Map LoadMap()
        {
            ///<summary>
            ///the level as a template
            ///</summary>
            Level Template = MapLoader.NextMap();
            /// <summary>
            ///all the objects to be loaded in
            /// </summary>
            List<Entity> Objects = new List<Entity>();
            //for each tempated space read the template for type first
            foreach (Space S in Template.Spaces)
            {
                switch (S.Type)
                {
                    // if it is a wall well i guess we have a wall
                    case Space.SpaceType.Wall:
                        Objects.Add(new Wall(new PointF(S.X * TileSize + TileSize / 2, S.Y * TileSize + TileSize / 2), 0, Color.Red));
                        break;
                        //if it is open well the fun starts and we can place items
                    case Space.SpaceType.Open:
                        for (int i = 0; i < S.Items.Count(); i++)
                        {
                            //grab each item and make  an item based on
                            Space.Item temp = S.Items[i];
                            switch (temp)
                            {
                                case Space.Item.PlayerSpawn:
                                    Objects.Add(new Spawn(new PointF(S.X * TileSize + TileSize / 2, S.Y * TileSize + TileSize / 2), 0, Color.Red));
                                    break;
                                case Space.Item.HeathPickUp:
                                    Objects.Add(new PUHealth(new PointF(S.X * TileSize + TileSize / 2, S.Y * TileSize + TileSize / 2), 0, Color.Green));
                                    break;
                                case Space.Item.SpeedPickUp:
                                    Objects.Add(new PUSpeed(new PointF(S.X * TileSize + TileSize / 2, S.Y * TileSize + TileSize / 2), 0, Color.Yellow));
                                    break;
                            }
                        }
                        break;
                }
            }

            // return a map
            return new Map(Objects, Template.sizeX, Template.sizeY, Template.BackGround);
        }
            #endregion


        }
    #endregion








}