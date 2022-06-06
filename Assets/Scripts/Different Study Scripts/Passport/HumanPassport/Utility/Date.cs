namespace Examples.HumanPassport.Utility
{
    public class Date
    {
        private int _year;
        private int _month;
        private int _day;

        public Date(int y, int m, int d)
        {
            _year = y;
            _month = m;
            _day = d;
        }

        public override string ToString() //какой метод мы переопредили?  
        {
            return $"{_day}.{_month}.{_year}";
        }
    }
}