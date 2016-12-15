using Xunit;
using System;
using System.Collections.Generic;
using Library.Objects;
using System.Data;
using System.Data.SqlClient;

namespace Library.Test
{
  public class CopyTest :IDisposable
  {
    public CopyTest()
    {
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-GC3DC7B\\SQLEXPRESS;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void AddCopy_AddsCopyToDB_1()
    {
      Book newBook = new Book("It");
      newBook.Save();
      Copy newCopy = new Copy(newBook.GetId());
      newCopy.Save();
      int result = newBook.GetCopies().Count;
      Assert.Equal(1, result);
    }

    [Fact]
    public void DeleteCopy_RemoveCopyFromDB_0()
    {
      Book newBook = new Book("It");
      newBook.Save();
      Copy newCopy = new Copy(newBook.GetId());
      newCopy.Save();
      newCopy.Delete();
      int result = newBook.GetCopies().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void DeleteCopies_RemoveAllRelevantCopiesOnBookDeletion_0()
    {
      Book newBook = new Book("It");
      newBook.Save();
      Copy newCopy = new Copy(newBook.GetId());
      newCopy.Save();
      newBook.Delete();
      int result = newBook.GetCopies().Count;
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      Copy.DeleteAll();
      Book.DeleteAll();
    }
  }
}
