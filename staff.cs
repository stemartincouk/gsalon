using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace gsalon
{
	public class staff
	{
		public string fName;
		public string lName;
		public string sex;
		
		
		public staff ()
		{
		}
		
		public List<staff> search_staff(string searchTerm)
		{
			List<staff>searchResults = new List<staff> ();
			string connectionString =
          	"Server=localhost;" +
          	"Database=gsalon;" +
          	"User ID=root;" +
          	"Password=Lilley1!;" +
          	"Pooling=false";
	  
       		IDbConnection dbcon;
       		dbcon = new MySqlConnection(connectionString);
			try 
			{
				dbcon.Open();
				Console.WriteLine("connected");
			}
			catch (MySqlException ex)
			{
				Console.WriteLine( ex.ToString());
			}
		
			IDbCommand dbcmd = dbcon.CreateCommand();
       		string sql =
           "SELECT fname, lname " +
           "FROM Staff WHERE MATCH (fname,lname) AGAINST('"+searchTerm+"*' IN BOOLEAN MODE)";
       		dbcmd.CommandText = sql;
			try 
			{	
       			IDataReader reader = dbcmd.ExecuteReader();
       			while(reader.Read()) 
				{
					
					staff newStaff = new staff ();
            		newStaff.fName = (string) reader["fname"];
            		newStaff.lName = (string) reader["lname"];
				    searchResults.Add(newStaff);
					
					
	       			
       			}
				reader.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return searchResults;
		}
		
		public List<staff> get_all_staff_members()
		{
			List<staff> staffMembers = new List<staff>();
			string connectionString =
          	"Server=localhost;" +
          	"Database=gsalon;" +
          	"User ID=root;" +
          	"Password=Lilley1!;" +
          	"Pooling=false";
	  
       		IDbConnection dbcon;
       		dbcon = new MySqlConnection(connectionString);
			try 
			{
				dbcon.Open();
				Console.WriteLine("connected");
			}
			catch (MySqlException ex)
			{
				Console.WriteLine( ex.ToString());
			}
		
			IDbCommand dbcmd = dbcon.CreateCommand();
       		string sql =
           "SELECT fname, lname " +
           "FROM Staff";
       		dbcmd.CommandText = sql;
			try 
			{	
       			IDataReader reader = dbcmd.ExecuteReader();
       			while(reader.Read()) 
				{
					staff newStaff = new staff ();
            		newStaff.fName = (string) reader["fname"];
            		newStaff.lName = (string) reader["lname"];
					staffMembers.Add(newStaff);
					
					
	       			
       			}
				reader.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			
	       dbcmd.Dispose();
	       dbcmd = null;
	       dbcon.Close();
	       dbcon = null;	
		return staffMembers;
					
		}
		
		public bool add_staff_member()
		{
			return false;
		}
		public bool remove_staff_member()
		{
			return false;
		}
	}
}

