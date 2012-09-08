using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.Model
{
    public class XYStringToInt
    {
        public string strXY;



        public int X;
        public int Y;


        public XYStringToInt()
        {
            strXY = "";
            X = 0;
            Y = 0;
        }

        public XYStringToInt(string curStrXY)
        {
            strXY = curStrXY;

            if (strXY.Contains(":") && strXY.Contains(","))
            {
                string[] strArryXY = strXY.Split(',');

                //X:383,Y:198;X:539,Y:353

                string strXData = strArryXY[0];
                string strYData = strArryXY[1];

                string[] strArryX = strXData.Split(':');
                string[] strArryY = strYData.Split(':');

                if (Model.Utilities.IsNumeric(strArryX[1]))
                    X = Convert.ToInt32(strArryX[1]);
                else
                    X = 0;


                if (Model.Utilities.IsNumeric(strArryY[1]))
                    Y = Convert.ToInt32(strArryY[1]);
                else
                    Y = 0;

            }
            else
            {
                X = 0;
                Y = 0;
            }
        }


    }
}
