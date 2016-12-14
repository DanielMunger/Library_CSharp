using Xunit;
using System;
using System.Collections.Generic;
using Library.Objects;
using System.Data;
using System.Data.SqlClient;

namespace Library.Test
{
  public class AuthorBookTest :IDisposable
  {
    public AuthorBookTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void AddAuthor_AddsBookToAuthor_1()
    {
      Book newBook = new Book("It");
      Author newAuthor = new Author("Stephen", "King");
      newBook.Save();
      newAuthor.Save();
      newBook.AddAuthor(newAuthor.GetId());
      int result = newBook.FindAuthors().Count;

      Assert.Equal(1, result);
    }


    public void Dispose()
    {
      Author.DeleteAll();
      Book.DeleteAll();
    }

  }
}
