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
    [Fact]
    public void GetOverDueBooks_ReturnsOverdueBooks_True()
    {
      Book book1 = new Book("The Old Man and the Sea");
      book1.Save();
      Book book2 = new Book("Monkey Wrench Gang");
      book2.Save();
      Copy copyBook1 = new Copy(book1.GetId());
      copyBook1.Save();
      Copy copyBook2 = new Copy(book2.GetId());
      copyBook2.Save();
      Patron newPatron = new Patron("Daniel", "Munger");
      newPatron.Save();
      //Act
      copyBook1.Checkout(newPatron.GetId());
      copyBook2.Checkout(newPatron.GetId());
      List<Copy> overdueBooks = Copy.GetOverDueBooks();
      List<Copy> expected = new List<Copy> {copyBook1, copyBook2};

      Assert.Equal(expected, overdueBooks);
    }

    public void Dispose()
    {
      Copy.DeleteAll();
      Book.DeleteAll();
    }
  }
}
