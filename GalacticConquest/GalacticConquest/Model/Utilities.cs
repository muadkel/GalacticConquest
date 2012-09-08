using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Model
{
    public class Utilities
    {


        public static bool IsNumeric(string numberString)
        {
            if (null == numberString)
                return false;

            //Lets remove the junk that makes this function think its a string
            //Trim off any white space and remove all commas.
            //This is all I(Jon Morin) found to cause this to not validate correctly.
            //You may remove other junk the same way I did below.
            numberString = numberString.Trim();
            numberString = numberString.Replace(",", "");

            if (string.IsNullOrEmpty(numberString))
                return false;

            foreach (char c in numberString)
            {
                if (!char.IsNumber(c) && c != '.')
                    return false;
            }
            return true;
        }

        public static int getIntOrN(string numToCheck)
        {
            return getIntOrN(numToCheck, 0);
        }
        public static int getIntOrN(string numToCheck, int N)
        {
            int rtnIntVal = N;

            if (IsNumeric(numToCheck))
            {
                rtnIntVal = Convert.ToInt32(numToCheck);
            }

            return rtnIntVal;
        }



        public static Rectangle getRectFromTexture2D(Texture2D myTexture2D, Vector2 myVector)
        {
            return new Rectangle((int)myVector.X, (int)myVector.Y, myTexture2D.Width, myTexture2D.Height);
        }


        public static float getTextHeight(String text, Model.StaticFonts staticFonts)
        {
            float rtnHeight = 0;

            //string[] arryLineBreaks = text.Split('\n');

            rtnHeight = staticFonts._courierNew.MeasureString(text).Y;

            return rtnHeight;
        }


            //Add /n line breaks to make text look justified. And Cut off any excess text
        public static String parseTextWidthHeight(String text, int textWidth, int textHeight, Model.StaticFonts staticFonts)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            Vector2 fontMeasurments = staticFonts._courierNew.MeasureString(text);


            foreach (String word in wordArray)
            {
                if (staticFonts._courierNew.MeasureString(line + word).Length() > textWidth)
                {

                   

                    
                    returnString = returnString + line + '\n';
                    line = String.Empty;

                    if (staticFonts._courierNew.MeasureString(returnString).Y + 20 > textHeight)
                    {
                        break;
                    }


                    
                }

                line = line + word + ' ';
            }

            returnString = returnString + line;

            float fontHeight = fontMeasurments.Y;
            



            return returnString;
        }


        //Add /n line breaks to make text look justified.
        public static String parseText(String text, int textWidth, Model.StaticFonts staticFonts)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (staticFonts._courierNew.MeasureString(line + word).Length() > textWidth)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }


    }
}
