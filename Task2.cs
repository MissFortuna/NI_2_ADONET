using System;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SQLite;

namespace SQLite
{
 class Program
{

SQLiteConnection m_dbConnection;

static void Main(string[] args)
{
Program p = new Program();
}

public Program()
{
    //open file
    var dirname = Directory.GetCurrentDirectory();
    string connectionString = "Data Source=" + dirname + @"\northwind.db";
    SqliteConnection connection = new SqliteConnection(connectionString);
    
    //tasks
    System.Console.WriteLine("Select all customers whose name starts with letter 'D'");
            string command1 = "SELECT ContactName FROM Customers WHERE ContactName LIKE 'D%'";
            ExecuteQueryAndPrint(connection, command1);

    System.Console.WriteLine("Convert names of all customers to Upper Case");
            string command2 = "SELECT UPPER(contactname) FROM customers";
            ExecuteQueryAndPrint(connection, command2);
           
    System.Console.WriteLine("Select distinct country from Customers");
            string command3 = "SELECT DISTINCT country FROM customers";
            ExecuteQueryAndPrint(connection, command3);
            
    System.Console.WriteLine("Select Contact name from Customers Table from London and title like 'Sales'");
            string command4 = "SELECT contactname FROM customers WHERE contacttitle LIKE 'sales%' AND city IS 'london'";
            ExecuteQueryAndPrint(connection, command4);
        
    System.Console.WriteLine("Select all orders id where was bought 'Tofu'");
            string command5 = "SELECT orderid FROM 'order details' WHERE productid IS (SELECT productid FROM products WHERE productname IS 'tofu')";
            ExecuteQueryAndPrint(connection, command5);
    
    System.Console.WriteLine("Select all product names that were shipped to Germany");
            string command6 = "SELECT productname FROM products WHERE productid IN (SELECT DISTINCT productid FROM 'order details' WHERE orderid IN (SELECT orderid FROM orders WHERE shipcountry IS 'germany'))";
            ExecuteQueryAndPrint(connection, command6);
    
    System.Console.WriteLine("Select all customers that ordered 'Ikura'");
            string command7 = "SELECT contactname FROM customers WHERE customerid IN (SELECT DISTINCT customerid FROM orders WHERE orderid IN (SELECT orderid FROM 'order details' WHERE productid IS (SELECT productid FROM products WHERE productname IS 'ikura')))";
            ExecuteQueryAndPrint(connection, command7);
    
    System.Console.WriteLine("Select all employees and any orders they might have");
            string command8 = "SELECT FirstName, LastName, OrderID FROM Employees LEFT JOIN Orders ON Employees.EmployeeID = Orders.EmployeeID;";
            ExecuteQueryAndPrint(connection, command8);
    
    System.Console.WriteLine("Selects all employees, and all orders");
            string command9 = "SELECT FirstName, LastName, OrderID FROM Employees, Orders WHERE Employees.EmployeeID = Orders.EmployeeID; ";
            ExecuteQueryAndPrint(connection, command9);
    
    System.Console.WriteLine("Select all phones from Shippers and Suppliers");
            string command10 = "SELECT phone FROM shippers UNION SELECT phone FROM suppliers";
            ExecuteQueryAndPrint(connection, command10);
    
    System.Console.WriteLine("Count all customers grouped by city");
            string command11 = "SELECT city, count(city) AS Quantity FROM customers GROUP BY city";
            ExecuteQueryAndPrint(connection, command11);
    
    System.Console.WriteLine("Select all customers that placed more than 10 orders with average Unit Price less than 17");
            string command12 = "SELECT * FROM customers WHERE customerid IN (SELECT DISTINCT customerid FROM orders WHERE orderid IN (SELECT orderid id FROM 'order details' WHERE 17 > (SELECT avg(unitprice) FROM 'order details' WHERE orderid = id GROUP BY orderid) AND 10 < (SELECT sum(quantity) FROM 'order details' WHERE orderid = id GROUP BY orderid) GROUP BY orderid))";
            ExecuteQueryAndPrint(connection, command12);
    
    System.Console.WriteLine("Select all customers with phone that has format (’NNNN-NNNN’)");
            string command13 = "SELECT phone FROM customers WHERE phone glob '[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'";
            ExecuteQueryAndPrint(connection, command13);
    
    System.Console.WriteLine("Select customer that ordered the greatest amount of goods (not price)");
            string command14 = "SELECT CustomerID FROM (SELECT customerid, SUM(od.quantity) AS SUM FROM orders AS O INNER JOIN 'order details' AS OD ON O.orderid = OD.orderid GROUP BY customerid ORDER BY SUM DESC) LIMIT 1";
            ExecuteQueryAndPrint(connection, command14);
    
    System.Console.WriteLine("Select only these customers that ordered the absolutely the same products as customer 'FAMIA'");
            string command15 = "SELECT DISTINCT CustomerID FROM (SELECT OrderD.OrderID, GROUP_CONCAT(ProductID) AS ProductsIDs, Orders.CustomerID FROM 'Order Details' AS OrderD LEFT JOIN Orders ON OrderD.OrderID=Orders.OrderID GROUP BY OrderD.OrderID) WHERE CustomerID != 'FAMIA' AND ProductsIDs IN (SELECT GROUP_CONCAT(ProductID) AS ProductsIDs FROM 'Order Details' AS OrderD LEFT JOIN Orders ON OrderD.OrderID=Orders.OrderID WHERE CustomerID='FAMIA' GROUP BY OrderD.OrderID)";
            ExecuteQueryAndPrint(connection, command15);
    
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