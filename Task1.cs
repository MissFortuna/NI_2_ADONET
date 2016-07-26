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
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Fill table 
            string sql1 = "insert into Companies (ID, Title, Country, AddedDate) values (1,'Microsoft',Ukraine, 2014-21-04)";
            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            command1.ExecuteNonQuery();

            string sql2 = "insert into Companies (ID, Title, Country, AddedDate) values (2,'Apple',Ukraine, 2014-21-04)";
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
            command2.ExecuteNonQuery();

            string sql3 = "insert into Companies (ID, Title, Country, AddedDate) values (3,'NewtonIdeas',Ukraine, 2015-21-04)";
            SQLiteCommand command3 = new SQLiteCommand(sql3, m_dbConnection);
            command3.ExecuteNonQuery();

            string sql4 = "insert into Companies (ID, Title, Country, AddedDate) values (4,'Necros',Ukraine, 2014-27-04)";
            SQLiteCommand command4 = new SQLiteCommand(sql4, m_dbConnection);
            command4.ExecuteNonQuery();

            string sql5 = "insert into Companies (ID, Title, Country, AddedDate) values (5,'Nike',Ukraine, 2014-21-04)";
            SQLiteCommand command5 = new SQLiteCommand(sql5, m_dbConnection);
            command5.ExecuteNonQuery();

            string sql6 = "insert into Companies (ID, Title, Country, AddedDate) values (6,'Prometheus',Canada, 2014-21-04)";
            SQLiteCommand command6 = new SQLiteCommand(sql6, m_dbConnection);
            command6.ExecuteNonQuery();

            string sql7 = "insert into Companies (ID, Title, Country, AddedDate) values (7,'Linux',France, 2014-21-04)";
            SQLiteCommand command7 = new SQLiteCommand(sql7, m_dbConnection);
            command7.ExecuteNonQuery();

            string sql8 = "insert into Companies (ID, Title, Country, AddedDate) values (8,'Twitter',Canada, 2014-21-04)";
            SQLiteCommand command8 = new SQLiteCommand(sql8, m_dbConnection);
            command8.ExecuteNonQuery();

            string sql9 = "insert into Companies (ID, Title, Country, AddedDate) values (9,'Yahoo',USA, 2014-21-04)";
            SQLiteCommand command9 = new SQLiteCommand(sql9, m_dbConnection);
            command9.ExecuteNonQuery();

            string sql10 = "insert into Companies (ID, Title, Country, AddedDate) values (10,'Apple',USA, 2014-21-04)";
            SQLiteCommand command10 = new SQLiteCommand(sql10, m_dbConnection);
            command10.ExecuteNonQuery();

            // Select company title with max ID (write to console ID and Title)
            string sqlMaxID="select ID, Title from Companies where ID= (select MAX(ID) from Companies)";
            SQLiteCommand commandMXID = new SQLiteCommand(sqlMaxID, m_dbConnection);
            ExecuteQueryAndPrint(m_dbConnection, commandMXID);

            // Update command (with parameters): set country 'USA' for all companies with country 'Ukraine'
            string sqlUpdate = "update Companies set Country='USA' WHERE Country='Ukraine'";
            SQLiteCommand commandUpdate = new SQLiteCommand(sqlUpdate, m_dbConnection);

            // Delete all companies with country 'USA'
            string sqlDeleteUSA = "delete from Companies where Country='USA'";
            SQLiteCommand commandDeleteUSA = new SQLiteCommand(sqlDeleteUSA, m_dbConnection);

            // Select number of records in 'Companies' table (write to console)
            ExecuteQueryAndPrint(m_dbConnection, "SELECT COUNT(*) FROM Companies");

            // Read all records from 'Companies' table with data reader
            string sql = "select * from Companies order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            Console.WriteLine("ID: " + reader["ID"] + "\tTitle: " + reader["Title"]+ "\tCountry:" + reader["Country"]+"Date:"+reader["AddedDate"]);
            Console.ReadLine();
        }
    }
}
