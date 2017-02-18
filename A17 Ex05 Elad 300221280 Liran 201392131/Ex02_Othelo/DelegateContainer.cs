using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class DelegateContainer
    {
        public delegate void UpdateLegalPictureBoxCellDelegate<T1>(T1 i_Point);

        public delegate void UpdatePictureBoxCellDelegate<T1, T2>(T1 i_SignValue, T2 i_Point);
    }
}
