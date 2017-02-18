namespace Ex02_Othelo
{
    public struct Point
    {
        private int m_AxisXValue;
        private int m_AxisYValue;

        public Point(int i_initX, int i_initY)
        {
            m_AxisXValue = i_initX;
            m_AxisYValue = i_initY;
        }

        public static bool operator >=(Point i_LeftPoint, Point i_RightPoint)
        {
            bool f_result = false;
            if (i_LeftPoint.AxisXValue >= i_RightPoint.AxisXValue && i_LeftPoint.AxisYValue >= i_RightPoint.AxisYValue)
            {
                f_result = true;
            }

            return f_result;
        }

        public static bool operator <=(Point i_LeftPoint, Point i_RightPoint)
        {
            bool f_result = false;
            if (i_LeftPoint.AxisXValue <= i_RightPoint.AxisXValue && i_LeftPoint.AxisYValue <= i_RightPoint.AxisYValue)
            {
                f_result = true;
            }

            return f_result;
        }

        public static bool operator <(Point i_LeftPoint, Point i_RightPoint)
        {
            bool f_result = false;
            if (i_LeftPoint.AxisXValue < i_RightPoint.AxisXValue && i_LeftPoint.AxisYValue < i_RightPoint.AxisYValue)
            {
                f_result = true;
            }

            return f_result;
        }

        public static bool operator >(Point i_LeftPoint, Point i_RightPoint)
        {
            bool f_result = false;
            if (i_LeftPoint.AxisXValue > i_RightPoint.AxisXValue && i_LeftPoint.AxisYValue > i_RightPoint.AxisYValue)
            {
                f_result = true;
            }

            return f_result;
        }

        public int AxisXValue
        {
            get
            {
                return m_AxisXValue;
            }

            set
            {
                m_AxisXValue = value;
            }
        }

        public int AxisYValue
        {
            get
            {
                return m_AxisYValue;
            }

            set
            {
                m_AxisYValue = value;
            }
        }

        public override string ToString()
        {
            return string.Format("point({0},{1})", m_AxisXValue, m_AxisYValue);
        }
    }
}