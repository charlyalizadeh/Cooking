using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;

namespace Cooking.UI
{
    class Menu
    {
        #region Champs
        int m_currentChoice;
        int m_beginIndex, m_endIndex;
        string[] m_text;
        int[] m_indexNotValid;
        int m_longestString;
        #endregion

        #region Constructeur
        public Menu(string[] text, int beginIndex = 1, int endIndex = -1, int[] indexNotValid = null)
        {
            m_endIndex = endIndex == -1 ? text.Length : endIndex;
            m_beginIndex = beginIndex;
            m_currentChoice = beginIndex;
            m_text = text;
            m_longestString = -1;
            SetLongestString();
            m_indexNotValid = indexNotValid;
        }
        #endregion

        #region Propriétés
        public int CurrentIndex { get { return m_currentChoice; } }
        #endregion

        #region Méthodes
        protected void DisplayFirstMenu()
        {
            Console.WriteLine('┌' + new String('─', m_longestString) + '┐');
            string[] temp = m_text[0].Split('\n');
            for(int i = 0;i<temp.Length;i++)
            {
                if (temp[i] != "")
                    Console.Write('│' + temp[i] + new String(' ', m_longestString - temp[i].Length) + "│\n");
            }
            Console.WriteLine('└' + new String('─', m_longestString) + '┘');
            Console.WriteLine('┌' + new String('─', m_longestString) + '┐');
        }
        protected void GoDown()
        {
            m_currentChoice++;
            if (m_indexNotValid != null)
                while (m_indexNotValid.Contains(m_currentChoice))
                    m_currentChoice++;
            if (m_currentChoice == m_endIndex)
                m_currentChoice = m_beginIndex;
        }
        protected void GoUp()
        {
            m_currentChoice--;
            if (m_indexNotValid != null)
                while (m_indexNotValid.Contains(m_currentChoice))
                    m_currentChoice--;
            if (m_currentChoice == m_beginIndex - 1)
                m_currentChoice = m_endIndex - 1;
        }
        protected void DisplayMenu(bool betterUI = true)
        {
            int start = 0;
            if (betterUI)
            {
                DisplayFirstMenu();
                start += 1;
            }
            for (int i = start; i < m_text.Length; i++)
            {
                if (betterUI)
                    Console.Write('│');
                if (i == CurrentIndex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write(m_text[i]);
                if (betterUI)
                    Console.Write(new String(' ', m_longestString - m_text[i].Length));
                if (i == CurrentIndex)
                    Console.ResetColor();
                if (betterUI)
                    Console.Write('│');
                Console.Write('\n');
            }
            if (betterUI)
                Console.WriteLine("└" + new String('─', m_longestString) + "┘");
        }
        protected void DisplayMenuAllBox(bool betterUI = true)
        {
            int start = 0;
            if (betterUI)
            {
                Box.DisplayBox(m_text[0]);
                start += 1;
            }
            for (int i = start; i < m_text.Length; i++)
            {
                if (i == CurrentIndex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Box.DisplayBox(m_text[i]);
                if (i == CurrentIndex)
                    Console.ResetColor();
                Console.Write('\n');
            }
        }

        public int Start(bool displayAllBox = false,bool betterUI = true)
        {
            Console.Clear();
            Console.CursorVisible = false;
            bool stop = false;
            Stopwatch elapsedTime = new Stopwatch();
            double timeSinceLastInput = 0;
            while (!stop)
            {
                elapsedTime.Start();
                if (displayAllBox)
                    DisplayMenuAllBox();
                else
                    DisplayMenu(betterUI);
                if (Keyboard.IsKeyDown(Key.Down) && timeSinceLastInput > 200)
                {
                    timeSinceLastInput = 0;
                    GoDown();
                }
                if (Keyboard.IsKeyDown(Key.Up) && timeSinceLastInput > 200)
                {
                    timeSinceLastInput = 0;
                    GoUp();
                }
                if (Keyboard.IsKeyDown(Key.Enter) && timeSinceLastInput > 200)
                    stop = true;
                if (Keyboard.IsKeyDown(Key.Escape) && timeSinceLastInput > 200)
                {
                    m_currentChoice = -1;
                    stop = true;
                }
                elapsedTime.Stop();
                timeSinceLastInput += elapsedTime.Elapsed.TotalMilliseconds;
                elapsedTime.Reset();
                Console.SetCursorPosition(0, 0);
            }
            Console.Clear();
            return CurrentIndex;
        }
        

        private void SetLongestString()
        {
            List<string> m_textByLine = new List<string>();
            m_longestString = 0;
            for(int i = 0;i<m_text.Length;i++)
            {
                string[] temp = m_text[i].Split('\n');
                for(int j = 0;j<temp.Length;j++)
                {
                    if (temp[j].Length > m_longestString)
                        m_longestString = temp[j].Length;
                }
            }
        }

        #endregion
    }
}
