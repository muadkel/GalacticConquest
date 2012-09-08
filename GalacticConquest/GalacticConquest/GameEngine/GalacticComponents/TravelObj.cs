using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine.GalacticComponents
{
    public class TravelObj
    {
        public bool _inTransit;
        public int _remainingTransitDays;

        public TravelObj()
        {
            _inTransit = false;
            _remainingTransitDays = 0;
        }


        public void setTravelTime(int transitTime)
        {
            _remainingTransitDays = transitTime;
            _inTransit = true;
        }

        public void updateTransitByDay()
        {
            _remainingTransitDays--;
            if (_remainingTransitDays <= 0)
            {
                _remainingTransitDays = 0;
                _inTransit = false;
            }
        }

    }
}
