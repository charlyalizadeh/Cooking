using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Cooking.SQL
{
    public class SQLBasic : SQLlink
    {
       #region Méthodes

        #region GetValue
        /// <summary>
        /// Get all the tuples of a table
        /// </summary>
        /// <param name="table">table name</param>
        /// <returns>a list which containes string array, one array is one tuple</returns>
        public List<string[]> GetAllTuples(string table)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT * FROM {0}", table));
            List<string[]> tuples = new List<string[]>();
            while (reader.Read())
            {
                List<string> tupleBuffer = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    tupleBuffer.Add(reader.GetString(i));
                }
                tuples.Add(tupleBuffer.ToArray());
            }
            reader.Close();
            return tuples;
        }
        /// <summary>
        /// Get all the value of a specific column of a table
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="column">column name</param>
        /// <returns>an array of string which contains the column values</returns>
        public string[] GetAllValues(string table, string column)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT {0} FROM {1}", column, table));
            List<string> values = new List<string>();
            while (reader.Read())
            {
                values.Add(reader.GetString(0));
            }
            reader.Close();
            return values.ToArray();
        }
        /// <summary>
        /// Get all the values of a specific column of a table for all row
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<string[]> GetAllTupleValues(string table,string[] columns)
        {
            string commande = "SELECT ";
            for (int i = 0; i < columns.Length; i++)
            {
                commande += columns[i];
                if (i != columns.Length - 1)
                    commande += ',';
            }
            commande +=  " FROM " + table;
            MySqlDataReader reader = ReadCommand(commande);
            List<string[]> values = new List<string[]>();
            while(reader.Read())
            {
                List<string> temp = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    temp.Add(reader.GetString(i));
                values.Add(temp.ToArray());
            }
            reader.Close();
            return values;
        }
        /// <summary>
        /// Get the value of a column in function of its primary key
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="column">column name</param>
        /// <param name="pKey">primary key value</param>
        /// <param name="pColumn">primary key column name</param>
        /// <returns>a string which contains the value</returns>
        public string GetOneValue(string table, string column, string pKey, string pColumn)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT {0} FROM {1} WHERE {2} = {3}", column, table, pColumn, pKey));
            reader.Read();
            string value = "";
            if (reader.HasRows)
                value = reader.GetString(0);
            reader.Close();
            return value;
        }
        /// <summary>
        /// Get specifics columns values of one row
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="columns">columns name</param>
        /// <param name="pKey">primary key</param>
        /// <param name="pColumn">primary key column name</param>
        /// <returns></returns>
        public string[] GetOneValues(string table,string[] columns,string pKey,string pColumn)
        {
            string commande = "SELECT ";
            for(int i = 0;i<columns.Length;i++)
            {
                commande += columns[i];
                if (i != columns.Length - 1)
                    commande += ',';
            }
            commande += String.Format(" FROM {0} WHERE {1} = {2}",table, pColumn, pKey);
            MySqlDataReader reader = ReadCommand(commande);
            List<string> values = new List<string>();
            reader.Read();
            for(int i = 0;i<reader.FieldCount;i++)
            {
                values.Add(reader.GetString(i));
            }
            reader.Close();
            return values.ToArray();
        }
        public string[] GetOneTuple(string table,string pKey,string pColumn)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT * FROM {0} WHERE {1} = {2}", table, pColumn, pKey));
            reader.Read();
            string[] infos = new string[reader.FieldCount];
            for(int i = 0;i<infos.Length;i++)
            {
                infos[i] = reader.GetString(i);
            }
            reader.Close();
            return infos;
        }
        public int GetCount(string table,string column)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT count({0}) FROM {1}", column, table));
            reader.Read();
            int count = reader.GetInt32(0);
            reader.Close();
            return count;
        }
        #endregion

        #region SetValue
        /// <summary>
        /// Change the value of one row
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="column">column name</param>
        /// <param name="value">new value</param>
        /// <param name="pKey">primary key</param>
        /// <param name="pColumn">primary key column name</param>
        public void UpdateValue(string table,string column,string value,string pKey,string pColumn)
        {
            ExecuteCommand(String.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", table, column, value, pColumn, pKey));
        }
        /// <summary>
        /// Change the values of multiple row
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="columns">columns name</param>
        /// <param name="values">new values name</param>
        /// <param name="pKey">primary key name</param>
        /// <param name="pColumn">primary key column name</param>
        public void UpdateMultipleValue(string table,string[] columns,string[] values,string pKey,string pColumn)
        {
            if (columns.Length != values.Length)
                throw new System.Exception("The columns and values array need to be the same size");
            string commande = String.Format("UPDATE {0} SET", table);
            for(int i = 0;i<columns.Length;i++)
            {
                commande += columns[i] + '=' + values[i];
                if (i != columns.Length - 1)
                    commande += ',';
            }
            commande += String.Format("WHERE {0} = {1}", pColumn, pKey);
            ExecuteCommand(commande);
        }
        /// <summary>
        /// Add a row with specific values
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="columns">columns name</param>
        /// <param name="values">values name</param>
        public void AddRowSpecific(string table,string[] columns,string[] values)
        {
            if (columns.Length != values.Length)
                throw new System.Exception("The columns and values array need to be the same size");
            string commande = String.Format("INSERT INTO {0}  ", table);
            string columnPart = "(";
            string valuePart = "(";
            for (int i = 0; i < columns.Length; i++)
            {
                columnPart += columns[i];
                valuePart += values[i];
                if(i!=columns.Length-1)
                {
                    columnPart += ',';
                    valuePart += ',';
                }
            }
            columnPart += ')';
            valuePart += ')';
            commande += String.Format("{0} VALUES {1}", columnPart, valuePart);
            ExecuteCommand(commande);
        }
        /// <summary>
        /// Add a row by specifying all columns values
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="values">values name</param>
        public void AddRow(string table,string[] values)
        {
            string commande = String.Format("INSERT INTO {0}  ", table);
            string valuePart = "(";
            for (int i = 0; i < values.Length; i++)
            {
                valuePart += values[i];
                if (i != values.Length - 1)
                    valuePart += ',';
            }
            valuePart += ')';
            commande += String.Format("VALUES {0}",valuePart);
            ExecuteCommand(commande);
        }
        /// <summary>
        /// Delete a row 
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="pColumn">primary key column name</param>
        /// <param name="pKey">primary key name</param>
        public void DeleteRow(string table,string pColumn,string pKey)
        {
            ExecuteCommand(String.Format("DELETE FROM {0} WHERE {1} = {2}", table, pColumn, pKey));
        }
        #endregion

        #endregion
    }
}
