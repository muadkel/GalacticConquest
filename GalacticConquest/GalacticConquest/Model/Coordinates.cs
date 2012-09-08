using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GalacticConquest.Model
{
    public class Coordinates
    {
        //Private Variables
        private int _x;
        private int _y;
        private bool _isNull;


	

        //Public Methods
        public bool IsNull
        {
            get { return _isNull; }
            set { _isNull = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }


        //Constructors
        public Coordinates()
        {
            X = -1;
            Y = -1;
            IsNull = true;

        }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
            IsNull = false;
        }

        public Coordinates(string strCoords)
        {
            _isNull = true;

            if (strCoords.Length >= 3)
            {
                if (strCoords.Contains(","))
                {
                    string[] aryStrCoords = strCoords.Split(',');

                    if (aryStrCoords.Length == 2)
                    {
                        if (Model.Utilities.IsNumeric(aryStrCoords[0]))
                        {
                            int x = Convert.ToInt32(aryStrCoords[0]);
                            
                            if (Model.Utilities.IsNumeric(aryStrCoords[1]))
                            {
                                int y = Convert.ToInt32(aryStrCoords[1]);

                                _isNull = false;
                                _x = x;
                                _y = y;

                            }

                        }

                    }

                }

            }

        }

        //Methods
        public void setCoordinates(int x, int y)
        {
            X = x;
            Y = y;
            IsNull = false;
        }

	
    }
}
