using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.UI
{
    class Grid
    {
        #region Champs
        string[,] m_mat;
        string[] m_columnName;
        #endregion

        #region Constructeur
        public Grid(string[,] mat,string[] columnName) 
        { 
            m_mat = mat;
            m_columnName = columnName;
        }
        public Grid(List<string[]> mat, string[] columnName)
        {
            if (mat.Count != 0)
            {
                m_mat = new string[mat.Count, mat[0].Length];
                for (int i = 0; i < m_mat.GetLength(0); i++)
                {
                    for (int j = 0; j < m_mat.GetLength(1); j++)
                    {
                        m_mat[i, j] = mat[i][j];
                    }
                }
            }
            else
                m_mat = new string[0, 0];
            m_columnName = columnName;
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            int nbSpace = 15;
            string str = "┌";
            for(int i = 0;i<m_columnName.Length;i++)
            {
                str += new String('─', nbSpace + m_columnName[i].Length - 1);
                if (i != m_columnName.Length - 1)
                    str += '┬';
                else
                    str += '┐';
            }
            str += "\n";
            for (int j = 0; j < m_mat.GetLength(1); j++)
            {
                str += "│" + m_columnName[j];
                str += new String(' ', nbSpace - 1);
            }
            str += "│\n├";
            for (int i = 0; i < m_columnName.Length; i++)
            {
                str += new String('─', nbSpace + m_columnName[i].Length - 1);
                if (i != m_columnName.Length - 1)
                    str += '┼';
                else
                    str += '┤';
            }
            str +='\n';
            for (int i = 0; i < m_mat.GetLength(0); i++)
            {
                for (int j = 0; j < m_mat.GetLength(1); j++)
                {
                    str += "│" + m_mat[i, j];
                    str += new String(' ', nbSpace + m_columnName[j].Length - m_mat[i, j].Length - 1);
                }
                str += "│" + '\n';
            }
            str += '└';
            for (int i = 0; i < m_columnName.Length; i++)
            {
                str += new String('─', nbSpace + m_columnName[i].Length - 1);
                if (i != m_columnName.Length - 1)
                    str += '┴';
                else
                    str += '┘';
            }
            return str;
        }
        public string[] ToStringMenu()
        {
            List<string> lines = new List<string>();
            int nbSpace = 20;
            string str = "┌";
            for (int i = 0; i < m_columnName.Length; i++)
            {
                str += new String('─', nbSpace + m_columnName[i].Length - 1);
                if (i != m_columnName.Length - 1)
                    str += '┬';
                else
                    str += '┐';
            }
            lines.Add(str);
            str = "";
            for (int j = 0; j < m_mat.GetLength(1); j++)
            {
                str += "│" + m_columnName[j];
                str += new String(' ', nbSpace - 1);
            }
            str += "│";
            lines.Add(str);
            str = "├";
            for (int i = 0; i < m_columnName.Length; i++)
            {
                str += new String('─', nbSpace + m_columnName[i].Length - 1);
                if (i != m_columnName.Length - 1)
                    str += '┼';
                else
                    str += '┤';
            }
            lines.Add(str);
            str = "";
            for (int i = 0; i < m_mat.GetLength(0); i++)
            {
                for (int j = 0; j < m_mat.GetLength(1); j++)
                {
                    str += "│" + m_mat[i, j];
                    str += new String(' ', nbSpace + m_columnName[j].Length - m_mat[i, j].Length - 1);
                }
                str += "│";
                lines.Add(str);
                str = "";
            }
            str += '└';
            for (int i = 0; i < m_columnName.Length; i++)
            {
                str += new String('─', nbSpace + m_columnName[i].Length - 1);
                if (i != m_columnName.Length - 1)
                    str += '┴';
                else
                    str += '┘';
            }
            lines.Add(str);
            return lines.ToArray();
        }
        #endregion
    }
}
