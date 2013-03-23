using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AzureAcres
{
    public static class Clock
    {
        public enum MONTH { JAN = 1, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC };
        private static DateTime _gameDateTime;
        public static DateTime GameDateTime
        {
            get { return Clock._gameDateTime; }
            set { Clock._gameDateTime = value; }
        }
        public static string Season
        {
            get
            {
                if ((_month >= 3 && _day >= 21) && (_month <= 6 && _day <= 20))
                    return "Spring";
                if ((_month >= 6 && _day >= 21) && (_month <= 9 && _day <= 20))
                    return "Summer";
                if ((_month >= 9 && _day >= 21) && (_month <= 12 && _day <= 20))
                    return "Fall";
                if (((_month >= 12 && _day >= 21) || (_month >= 1)) && (_month <= 3 && _day <= 20))
                    return "Winter";
                return "";
            }
        }
        private static int _year = 2012;
        public static int Year
        {
            get { return Clock._year; }
            set { Clock._year = value; }
        }
        private static int _month = 1;
        public static int Month
        {
            get { return Clock._month; }
            set { Clock._month = value; }
        }
        public static string MonthString
        {
            get { MONTH m = (MONTH)Clock.Month; return m.ToString(); }
        }
        private static int _day = 1;
        public static int Day
        {
            get { return Clock._day; }
            set { Clock._day = value; }
        }
        private static int _hours = 0;
        public static int Hours
        {
            get { return Clock._hours; }
            set { Clock._hours = value; }
        }
        private static int _minutes = 0;
        public static int Minutes
        {
            get { return Clock._minutes; }
            set { Clock._minutes = value; }
        }
        private static int _seconds;
        public static int Seconds
        {
            get { return Clock._seconds; }
            set { Clock._seconds = value; }
        }
        private static float _milliseconds = 0;
        public static float Milliseconds
        {
            get { return Clock._milliseconds; }
            set { Clock._milliseconds = value; }
        }
        private static float _realTimeRatio = 240;
        public static float RealTimeRatio
        {
            get { return Clock._realTimeRatio; }
            set { Clock._realTimeRatio = value; }
        }
        private static float _elapsedMilliseconds = 0;
        public static float ElapsedMilliseconds
        {
            get { return Clock._elapsedMilliseconds; }
            set { Clock._elapsedMilliseconds = value; }
        }
        private static float ElapsedSeconds { get { return ElapsedMilliseconds / 1000; } }

        private static float _dayLightAlpha = 1.0f;
        public static float DaylightAlpha
        {
            get
            {
                return _dayLightAlpha;
            }
            set { _dayLightAlpha = value; }
        }

        public static void Update(GameTime gameTime)
        {
            Milliseconds += (float)gameTime.ElapsedGameTime.TotalMilliseconds * _realTimeRatio;
            ElapsedMilliseconds = (float)gameTime.ElapsedGameTime.TotalMilliseconds * _realTimeRatio;

            /// There are 1000 ms in a second. 
            /// if there are more than 1000 then add the floor of milliseconds/1000
            /// then check how many are left over
            if (Milliseconds > 1000)
            {
                float leftOvers = Milliseconds % 1000;
                Seconds += (int)Math.Floor((Milliseconds - leftOvers) / 1000);
                Milliseconds = leftOvers;
            }
            if (Seconds >= 60)
            {
                Minutes++; Seconds = 0;
                if (Hours == 6)
                    _dayLightAlpha -= (_dayLightAlpha - 0.016f < 0.0f) ? 0.0f : 0.016f;
                else if (Hours == 18)
                    _dayLightAlpha += (_dayLightAlpha + 0.016f > 0.5f) ? 0.0f : 0.016f;
                else if (Hours >= 7 && Hours < 18)
                    _dayLightAlpha = 0.0f;
                else
                    _dayLightAlpha = 0.5f;
            }
            if (Minutes >= 60) { Minutes = 0; Hours++; }
            if (Hours >= 24) { Hours = 0; Day++; }
            if (Day > 30) { Month++; Day = 1; }
            if (Month > 12) { Year++; Month = 1; }
            GameDateTime = new DateTime((int)Year, (int)Month, (int)Day, (int)Hours, (int)Minutes, (int)Seconds, (int)Milliseconds);
        }
    }
}
