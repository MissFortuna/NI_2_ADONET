using System;
using System.Data.SQLite;
using System.Date;
using System.IO;

namespace SQLiteTask
{
    class Program
    {
        // Holds our connection with the database
        SQLiteConnection m_dbConnection;

        static void Main(string[] args)
        {
            Program p = new Program();
        }

        public Program()
        {
            // Creates an empty database file
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            // Creates a connection with database file
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            // Creates a table named 'Companies' with four columns: ID (autoincrement), Title (string), Country (string), AddedDate (Date)
            string sql = "create table Companies (ID int, Title string, Country string, AddedDate Date)";
            SqliteCommand command = new SqliteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Fill table 
            string sql1 = "insert into Companies (ID, Title, Country, AddedDate) values (1,'Microsoft',Ukraine, 2014-21-04)";
            SqliteCommand command1 = new SqliteCommand(sql1, m_dbConnection);
            command1.ExecuteNonQuery();

            string sql2 = "insert into Companies (ID, Title, Country, AddedDate) values (2,'Apple',Ukraine, 2014-21-04)";
            SqliteCommand command2 = new SqliteCommand(sql2, m_dbConnection);
            command2.ExecuteNonQuery();

            string sql3 = "insert into Companies (ID, Title, Country, AddedDate) values (3,'NewtonIdeas',Ukraine, 2015-21-04)";
            SqliteCommand command3 = new SqliteCommand(sql3, m_dbConnection);
            command3.ExecuteNonQuery();

            string sql4 = "insert into Companies (ID, Title, Country, AddedDate) values (4,'Necros',Ukraine, 2014-27-04)";
            SqliteCommand command4 = new SqliteCommand(sql4, m_dbConnection);
            command4.ExecuteNonQuery();

            string sql5 = "insert into Companies (ID, Title, Country, AddedDate) values (5,'Nike',Ukraine, 2014-21-04)";
            SQLiteCommand command5 = new SQLiteCommand(sql5, m_dbConnection);
            command5.ExecuteNonQuery();

            string sql6 = "insert into Companies (ID, Title, Country, AddedDate) values (6,'Prometheus',Canada, 2014-21-04)";
            SqliteCommand command6 = new SqliteCommand(sql6, m_dbConnection);
            command6.ExecuteNonQuery();

            string sql7 = "insert into Companies (ID, Title, Country, AddedDate) values (7,'Linux',France, 2014-21-04)";
            SqliteCommand command7 = new SqliteCommand(sql7, m_dbConnection);
            command7.ExecuteNonQuery();

            string sql8 = "insert into Companies (ID, Title, Country, AddedDate) values (8,'Twitter',Canada, 2014-21-04)";
            SqliteCommand command8 = new SqliteCommand(sql8, m_dbConnection);
            command8.ExecuteNonQuery();

            string sql9 = "insert into Companies (ID, Title, Country, AddedDate) values (9,'Yahoo',USA, 2014-21-04)";
            SqliteCommand command9 = new SqliteCommand(sql9, m_dbConnection);
            command9.ExecuteNonQuery();

            string sql10 = "insert into Companies (ID, Title, Country, AddedDate) values (10,'Apple',USA, 2014-21-04)";
            SqliteCommand command10 = new SqliteCommand(sql10, m_dbConnection);
            command10.ExecuteNonQuery();

            // Select company title with max ID (write to console ID and Title)
            string sqlMaxID = "select ID, Title from Companies where ID= (select MAX(ID) from Companies)";
            SqliteCommand commandMXID = new SqliteCommand(sqlMaxID, m_dbConnection);
            ExecuteQueryAndPrint(m_dbConnection, commandMXID);

            // Update command (with parameters): set country 'USA' for all companies with country 'Ukraine'
            string sqlUpdate = "update Companies set Country='USA' WHERE Country='Ukraine'";
            SqliteCommand commandUpdate = new SqliteCommand(sqlUpdate, m_dbConnection);

            // Delete all companies with country 'USA'
            string sqlDeleteUSA = "delete from Companies where Country='USA'";
            SqliteCommand commandDeleteUSA = new SqliteCommand(sqlDeleteUSA, m_dbConnection);

            // Select number of records in 'Companies' table (write to console)
            string sqlPrint = "SELECT COUNT(*) FROM Companies";
            ExecuteQueryAndPrint(m_dbConnection, sqlPrint);

            // Read all records from 'Companies' table with data reader
            string sql = "select * from Companies order by score desc";
            SqliteCommand command = new SqliteCommand(sql, m_dbConnection);
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("ID: " + reader["ID"] + "\tTitle: " + reader["Title"] + "\tCountry:" + reader["Country"] + "Date:" + reader["AddedDate"]);
            Console.ReadLine();
        }


        public static void ExecuteNonQuery(SqliteConnection sqlConnection, string query)
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                var com = sqlConnection.CreateCommand();
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
        }

        public static void ExecuteQueryAndPrint(SqliteConnection sqlConnection, string query)
        {
            using (sqlConnection)
            {

                sqlConnection.Open();
                var com = sqlConnection.CreateCommand();
                com.CommandText = query;
                using (SqliteDataReader rdr = com.ExecuteReader())
                {
                    StringBuilder sb = new StringBuilder();
                    while (rdr.Read())
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                            sb.Append(rdr.GetValue(i) + "   |   ");
                        sb.Append("\n");
                    }
                    System.Console.WriteLine(sb.ToString());
                }
                sqlConnection.Close();
            }
        }
    }
}
