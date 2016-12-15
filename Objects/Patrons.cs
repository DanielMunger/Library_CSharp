using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Objects
{
  public class Patron
  {
    private int _id;
    private string _firstName;
    private string _lastName;

    public Patron(string FirstName, string LastName, int Id = 0)
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

    public override bool Equals(Object otherPatron)
    {
      if (!(otherPatron is Patron))
      {
        return false;
      }
      else
      {
        Patron newPatron = (Patron) otherPatron;
        bool patronIdEquality = (_id == newPatron.GetId());
        bool patronFirstNameEquality = (_firstName == newPatron.GetFirstName());
        bool patronLastNameEquality = (_lastName == newPatron.GetLastName());
        return (patronIdEquality && patronFirstNameEquality && patronLastNameEquality);
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

      SqlCommand cmd = new SqlCommand("INSERT INTO patrons (first_name, last_name) OUTPUT INSERTED.id VALUES (@FirstName, @LastName);", conn);

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

    public static List<Patron> GetAll()
    {
      List<Patron> allPatrons = new List<Patron>{};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM patrons;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int PatronId = rdr.GetInt32(0);
        string PatronFirstName = rdr.GetString(1);
        string PatronLastName = rdr.GetString(2);
        Patron newPatron = new Patron(PatronFirstName, PatronLastName, PatronId);
        allPatrons.Add(newPatron);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allPatrons;
    }

    public static Patron Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT first_name, last_name FROM patrons WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      string firstName = null;
      string lastName = null;
      while(rdr.Read())
      {
        firstName = rdr.GetString(0);
        lastName = rdr.GetString(1);
      }
      Patron foundPatron = new Patron(firstName, lastName, id);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundPatron;
    }
    public void Update(string firstName, string lastName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE patrons SET first_name = @FirstName, last_name = @LastName WHERE id = @PatronId;", conn);
      cmd.Parameters.AddWithValue("@FirstName", firstName);
      cmd.Parameters.AddWithValue("@LastName", lastName);
      cmd.Parameters.AddWithValue("@PatronId", this.GetId());

      cmd.ExecuteNonQuery();
      this._firstName = firstName;
      this._lastName = lastName;
      if (conn != null) conn.Close();
    }
    public List<Copy> GetHistory()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT copies.* FROM patrons JOIN checkouts ON (patrons.id = checkouts.patron_id) JOIN copies ON(checkouts.copy_id = copies.id) WHERE patrons.id = @PatronId;", conn);
      cmd.Parameters.AddWithValue("@PatronId", this.GetId());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Copy> checkoutHistory = new List<Copy>{};
      while(rdr.Read())
      {
        int copyId = rdr.GetInt32(0);
        int bookId = rdr.GetInt32(1);
        DateTime? dueDate = rdr.GetDateTime(2);
        bool checkedOut = rdr.GetBoolean(3);
        Copy newCopy = new Copy(bookId, copyId, dueDate, checkedOut);
        checkoutHistory.Add(newCopy);
      }
      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();
      return checkoutHistory;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM patrons WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", this.GetId());
      cmd.ExecuteNonQuery();
      if(conn!=null) conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM patrons;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
