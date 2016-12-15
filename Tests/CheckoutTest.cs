using Xunit;
using System;
using System.Collections.Generic;
using Library.Objects;
using System.Data;
using System.Data.SqlClient;

namespace Library.Test
{
  public class CheckoutTest :IDisposable
  {
    public CheckoutTest()
    {
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-GC3DC7B\\SQLEXPRESS;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void CheckoutCopy_AddsCopyToPatron_1()
    {
      //Arrange
      Book newBook = new Book("It");
      Copy newCopy = new Copy(newBook.GetId());
      Patron newPatron = new Patron("Johnny", "Appleseed");
      newBook.Save();
      newCopy.Save();
      newPatron.Save();

      //Act
      newCopy.Checkout(newPatron.GetId());
    //  List<Copy> foundCopy = newCopy.Find(newCpoy.GetId());
      List<Copy> checkoutHistory = newPatron.GetHistory();

      //Assert
      Assert.Equal(1, checkoutHistory.Count);
    }


    public void Dispose()
    {
      Copy.DeleteAll();
      Patron.DeleteAll();
      Book.DeleteAll();
    }

  }
}
