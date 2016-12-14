using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Objects
{
  public class Book
  {

    private int _id;
    private string _title;


    public Book(string Title, int Id = 0)
    {
      _id = Id;
      _title = Title;
    }

    public string GetTitle()
    {
      return _title;
    }
    public int GetId()
    {
      return _id;
    }

    public override bool Equals(Object otherBook)
    {
      if (!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBook;
        bool IdEquality = (_id == newBook.GetId());
        bool titleEquality = (_title == newBook.GetTitle());

        return (IdEquality && titleEquality);
      }
    }
    public override int GetHashCode()
    {
      return _title.GetHashCode();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO books (title) OUTPUT INSERTED.id VALUES (@Title);", conn);
      cmd.Parameters.AddWithValue("@Title", this.GetTitle());

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book>{};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM books;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int BookId = rdr.GetInt32(0);
        string BookTitle = rdr.GetString(1);
        Book newBook = new Book(BookTitle);
        allBooks.Add(newBook);
      }

      if (rdr != null) rdr.Close();
      if (conn != null) rdr.Close();
      return allBooks;
    }

    public static Book Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT title FROM books WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      string title = null;
      while(rdr.Read())
      {
        title = rdr.GetString(0);
      }
      Book foundBook = new Book(title, id);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

      return foundBook;
    }
    public void Update(string Title)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE books SET title = @Title WHERE id = @BookId;", conn);
      cmd.Parameters.AddWithValue("@Title", Title);
      cmd.Parameters.AddWithValue("@BookId", this.GetId());
      cmd.ExecuteNonQuery();
      this._title = Title;
      if (conn != null) conn.Close();
    }
    public List<Author> FindAuthors()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT authors.* FROM books JOIN authors_books ON (books.id = authors_books.book_id) JOIN authors ON (authors_books.author_id = authors.id) WHERE books.id = @BookId;", conn);
      cmd.Parameters.AddWithValue("@BookId", this.GetId());
      SqlDataReader rdr = cmd.ExecuteReader();

      int authorId = 0;
      string firstName = null;
      string lastName = null;
      List<Author> allAuthors = new List<Author>{};
      while(rdr.Read())
      {
        authorId = rdr.GetInt32(0);
        firstName = rdr.GetString(1);
        lastName = rdr.GetString(2);
        Author newAuthor = new Author(firstName, lastName, authorId);
        allAuthors.Add(newAuthor);
      }
      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();
      return allAuthors;
    }
    public void AddAuthor(int AuthorId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO authors_books (book_id, author_id) VALUES (@bookId, @authorId);", conn);
      cmd.Parameters.AddWithValue("@bookId", this.GetId());
      cmd.Parameters.AddWithValue("@authorId", AuthorId);
      cmd.ExecuteNonQuery();
      if (conn != null) conn.Close();
    }
    public List<Copy> GetCopies()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM copies WHERE book_id = @BookId;", conn);
      cmd.Parameters.AddWithValue("@BookId", this.GetId());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Copy> allCopies = new List<Copy>{};
      while(rdr.Read())
      {
        int copyId = rdr.GetInt32(0);
        Copy newCopy = new Copy(this.GetId(), copyId);
        allCopies.Add(newCopy);
      }
      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();
      return allCopies;
    }
//
// //     public static List<TEMPLATE> Sort()
// //     {
// //       List<Task> allTEMPLATE = new List<Task>{};
// //
// //       SqlConnection conn = DB.Connection();
// //       conn.Open();
// //
// //       SqlCommand cmd = new SqlCommand("SELECT * FROM template ORDER BY TEMPLATEdate;", conn);
// //       SqlDataReader rdr = cmd.ExecuteReader();
// //
// //       while(rdr.Read())
// //       {
// //         int TEMPLATEId = rdr.GetInt32(0);
// //         string TEMPLATEDescription = rdr.GetString(1);
// //         TEMPLATE newTEMPLATE = new TEMPLATE(TEMPLATEDescription, TEMPLATEId);
// //         allTEMPLATE.Add(newTEMPLATE);
// //       }
// //
// //       if (rdr != null)
// //       {
// //         rdr.Close();
// //       }
// //       if (conn != null)
// //       {
// //         conn.Close();
// //       }
// //
// //       return allTEMPLATE;
// //     }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM books WHERE id = @Id; DELETE FROM copies WHERE book_id = @Id", conn);
      cmd.Parameters.AddWithValue("@Id", this.GetId());
      cmd.ExecuteNonQuery();
      if(conn!=null) conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM books;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
