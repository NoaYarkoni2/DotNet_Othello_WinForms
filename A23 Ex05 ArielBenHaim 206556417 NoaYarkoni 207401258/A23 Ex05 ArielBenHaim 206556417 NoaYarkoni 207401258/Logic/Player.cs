using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Player
    {
        private string m_Name;
        private string m_Sign;
        private bool m_IsHuman;
        private int m_WinsCounter;

        public Player(string i_Name, string i_Sign, bool i_IsHuman)
        {
            m_Name = i_Name;
            m_Sign = i_Sign;
            m_IsHuman = i_IsHuman;
            m_WinsCounter = 0;
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public bool IsHuman
        {
            get { return m_IsHuman; }
            set { m_IsHuman = value; }
        }

        public int WinsCounter
        {
            get { return m_WinsCounter; }
            set { m_WinsCounter = value; }
        }

    }
}
