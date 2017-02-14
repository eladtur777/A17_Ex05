namespace Ex02_Othelo
{
    public class Cell
    {
        private char m_SignValue = (char)eGameSigns.None;
        private Point? m_Point = null;
       
        public Cell(char i_SignValue, int i_AxisXValue, int i_AxisYValue)
        {
            m_Point = new Point(i_AxisXValue, i_AxisYValue);
        }

        public char SignValue
        {
            get
            {
                return m_SignValue;
            }

            set
            {
                m_SignValue = value;
            }
        }

        public Point? Point
        {
            get { return m_Point; }
        }
    }
}
