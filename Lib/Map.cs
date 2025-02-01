﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Map
    {
        private static string mapdes = @"
##################################################
#*******#******#*********************************#
#*p************#*********************************#
#**************#*****************************#***#
#**#***********#****************##***********#***#
#**##**********#****************##***************#
#**##******#**********************#**************#
#**#*******#**********************#**************#
#*********************************************#**#
#************************#********************#**#
#***********************##***********************#
#***************#********#*******#***************#
#**************##****************#***********#***#
#*******###****##************#***#**********###**#
#********#*****##***************#************#***#
#*******************************##***********#***#
#********************************#************#**#
#**************##****************#************#**#
#**************##***************##***************#
##################################################";
        static Map()
        {
            mapdes = mapdes.Trim();
            string[] mapLines = mapdes.Split('\n');
            gameMap = new char[mapLines.Length, mapLines[0].Length];

            for (int i = 0; i < mapLines.Length; i++)
            {
                for (int j = 0; j < mapLines[i].Length; j++)
                {
                    gameMap[i, j] = mapLines[i][j];
                }
            }
        }
        

        private static char[,] gameMap;
        public static char[,] map
        {
            set { gameMap = value; }
            get => gameMap;
        }
    }
}
