using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Objects
{
  public class Copy
  {

    private int _id;
    private int _bookId;
    private DateTime? _dueDate;
    private bool _checkedOut;

    public Copy(int BookId, int Id = 0, DateTime? dueDate = null, bool checkedOut = false)
    {
      _id = Id;
      _bookId = BookId;
      _dueDate = dueDate;
      _checkedOut = checkedOut;
    }

    public int GetBookId()
    {
      return _bookId;
    }
    public int GetId()
    {
      return _id;
    }

    public override bool Equals(Object otherBook)
    {
      if (!(otherBook is Copy))
      {
        return false;
      }
      else
      {
        Copy newCopy = (Copy) otherBook;
        bool IdEquality = (_id == newCopy.GetId());
        bool BookIdEquality = (_bookId == newCopy.GetBookId());

        return (IdEquality && BookIdEquality);
      }
    }
    public override int GetHashCode()
    {
      return _bookId.GetHashCode();
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO copies (book_id, checked_out) OUTPUT INSERTED.id VALUES (@BookId, @CheckedOut);", conn);
      cmd.Parameters.AddWithValue("@BookId", this.GetBookId());
      cmd.Parameters.AddWithValue("@CheckedOut", false);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();
    }
    public List<Copy> Find(int Id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", Id);
      SqlDataReader rdr = cmd.ExecuteReader();

      int copyId = 0;
      int bookId = 0;
      DateTime? dueDate = null;
      bool checkedOut = false;
      List<Copy> foundCopies = new List<Copy>{};
      while(rdr.Read())
      {
        copyId = rdr.GetInt32(0);
        bookId = rdr.GetInt32(1);
        dueDate = rdr.GetDateTime(2);
        checkedOut = rdr.GetBoolean(3);
      }
      Copy foundCopy = new Copy(bookId, copyId, dueDate, checkedOut);
      foundCopies.Add(foundCopy);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundCopies;
    }
    public void Checkout(int patronId)
    {
      TimeSpan CheckoutLength = new TimeSpan(14, 0, 0, 0);
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO checkouts (copy_id, patron_id) VALUES (@copyId, @PatronId);", conn);
      cmd.Parameters.AddWithValue("@copyId", this.GetId());
      cmd.Parameters.AddWithValue("@PatronId", patronId);
      cmd.ExecuteNonQuery();
      SqlCommand checkoutCmd = new SqlCommand("UPDATE copies SET checked_out = 'true', due_date = @dueDate WHERE id = @copyId;", conn);
      checkoutCmd.Parameters.AddWithValue("@copyId", this.GetId());
      checkoutCmd.Parameters.AddWithValue("@dueDate", DateTime.Today + CheckoutLength);
      checkoutCmd.ExecuteNonQuery();
      if (conn != null) conn.Close();
    }

    // public static List<Copy> GetAll()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM copies;", conn);
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //   List<Copy> inventory = new List<Copy>{};
    //   while(rdr.Read())
    //   {
    //     int id = rdr.GetInt32(0);
    //     int bookId = rdr.GetInt32(1);
    //     DateTime? dueDate = rdr.GetDateTime(2);
    //     bool checkedOut = rdr.GetBoolean(3);
    //     Copy foundCopy = new Copy(bookId, id, dueDate, checkedOut);
    //     inventory.Add(foundCopy);
    //   }
    //   if (rdr != null) rdr.Close();
    //   if (conn != null) conn.Close();
    //   return inventory;
    // }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM copies WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", this.GetId());
      cmd.ExecuteNonQuery();
      if(conn!=null) conn.Close();
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM copies;", conn);
      cmd.ExecuteNonQuery();
      if(conn!=null) conn.Close();
    }
  }
}
