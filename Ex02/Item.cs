using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class Item
    {
        private enum eItemStatus
        {
            Red,
            Yellow,
            Green
        }
        private enum eItemLetter
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H
        }
        eItemLetter m_ItemLetter;
        eItemStatus m_ItemStatus;

        public static Item ConvertCharToItem(char i_Letter)
        {
            Item item = new Item
            {
                m_ItemLetter = (eItemLetter)(i_Letter - 'A'),
                m_ItemStatus = eItemStatus.Red
            };
            return item;
        }

        public char ItemLetterToChar()
        {
            return (char)(m_ItemLetter + 'A');
        }

        public char ItemStatusToChar()
        {
            char symbol;
            switch(m_ItemStatus)
            {
                case eItemStatus.Green:
                    {
                        symbol = 'V';
                        break;
                    }
                case eItemStatus.Yellow:
                    {
                        symbol = 'X';
                        break;
                    }
                default:
                    {
                        symbol = ' ';
                        break;
                    }
            }

            return symbol;
        }
    }
}
