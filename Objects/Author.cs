using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Objects
{
  public class Author
  {

    private int _id;
    private string _firstName;
    private string _lastName;

    public Author(string FirstName, string LastName, int Id = 0)
    {
      _id = Id;
      _firstName = FirstName;
      _lastName = LastName;
    }

    public string GetFirstName()
    {
      return _firstName;
    }
    public string GetLastName()
    {
      return _lastName;
    }
    public int GetId()
    {
      return _id;
    }

    public override bool Equals(Object otherAuthor)
    {
      if (!(otherAuthor is Author))
      {
        return false;
      }
      else
      {
        Author newAuthor = (Author) otherAuthor;
        bool authorIdEquality = (_id == newAuthor.GetId());
        bool authorFirstNameEquality = (_firstName == newAuthor.GetFirstName());
        bool authorLastNameEquality = (_lastName == newAuthor.GetLastName());
        return (authorIdEquality && authorFirstNameEquality && authorLastNameEquality);
      }
    }
    public override int GetHashCode()
    {
      return _firstName.GetHashCode();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO authors (first_name, last_name) OUTPUT INSERTED.id VALUES (@FirstName, @LastName);", conn);

      cmd.Parameters.AddWithValue("@FirstName", this.GetFirstName());
      cmd.Parameters.AddWithValue("@LastName", this.GetLastName());
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Author> GetAll()
    {
      List<Author> allAuthors = new List<Author>{};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM authors;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int AuthorId = rdr.GetInt32(0);
        string AuthorFirstName = rdr.GetString(1);
        string AuthorLastName = rdr.GetString(2);
        Author newAuthor = new Author(AuthorFirstName, AuthorLastName, AuthorId);
        allAuthors.Add(newAuthor);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allAuthors;
    }

    public static Author Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT first_name, last_name FROM authors WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      string firstName = null;
      string lastName = null;
      while(rdr.Read())
      {
        firstName = rdr.GetString(0);
        lastName = rdr.GetString(1);
      }
      Author foundAuthor = new Author(firstName, lastName, id);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundAuthor;
    }
    public void Update(string firstName, string lastName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE authors SET first_name = @FirstName, last_name = @LastName WHERE id = @AuthorId;", conn);
      cmd.Parameters.AddWithValue("@FirstName", firstName);
      cmd.Parameters.AddWithValue("@LastName", lastName);
      cmd.Parameters.AddWithValue("@AuthorId", this.GetId());

      cmd.ExecuteNonQuery();
      this._firstName = firstName;
      this._lastName = lastName;
      if (conn != null) conn.Close();
    }
//
//     public static List<TEMPLATE> Sort()
//     {
//       List<Task> allTEMPLATE = new List<Task>{};
//
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("SELECT * FROM template ORDER BY TEMPLATEdate;", conn);
//       SqlDataReader rdr = cmd.ExecuteReader();
//
//       while(rdr.Read())
//       {
//         int TEMPLATEId = rdr.GetInt32(0);
//         string TEMPLATEDescription = rdr.GetString(1);
//         TEMPLATE newTEMPLATE = new TEMPLATE(TEMPLATEDescription, TEMPLATEId);
//         allTEMPLATE.Add(newTEMPLATE);
//       }
//
//       if (rdr != null)
//       {
//         rdr.Close();
//       }
//       if (conn != null)
//       {
//         conn.Close();
//       }
//
//       return allTEMPLATE;
//     }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM authors WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", this.GetId());
      cmd.ExecuteNonQuery();
      if(conn!=null) conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM authors;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
