using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using MySql.Data.MySqlClient;

namespace Cooking.SQL
{
    public class SQLlink
    {
        #region Champs
        MySqlConnection m_sqlConnection;
        #endregion

        #region Propriétés
        MySqlConnection SqlConnection { get { return m_sqlConnection; } }
        #endregion

        #region Constructeur
        public SQLlink()
        {
            m_sqlConnection = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=cooking;UID=cookingadmin;PASSWORD=cooking;");
            m_sqlConnection.Open();
            ExecuteCommand("SET SQL_SAFE_UPDATES = 0;");
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="commandStr">the command</param>
        /// <returns>A MySqlDataReader which correspond to the command</returns>
        protected MySqlDataReader ReadCommand(string commandStr)
        {
            MySqlCommand command = new MySqlCommand(commandStr, m_sqlConnection);
            return command.ExecuteReader();
        }
        /// <summary>
        /// Execture a query and close the reader immediately after
        /// </summary>
        /// <param name="commandStr"></param>
        protected void ExecuteCommand(string commandStr)
        {
            MySqlCommand command = new MySqlCommand(commandStr, m_sqlConnection);
            command.ExecuteNonQuery();
        }
        #endregion
    }
}
