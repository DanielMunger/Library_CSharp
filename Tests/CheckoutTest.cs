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
    [Fact]
    public void CheckoutHistory_ReturnsHistory_True()
    {
      //Arrange
      Book book1 = new Book("The Old Man and The Sea");
      Book book2 = new Book("For Whom the Bell Tolls");
      book1.Save();
      book2.Save();
      Author newAuthor = new Author("Ernest", "Hemmingway");
      newAuthor.Save();
      book1.AddAuthor(newAuthor.GetId());
      book2.AddAuthor(newAuthor.GetId());

      Copy copyBook1 = new Copy(book1.GetId());
      copyBook1.Save();
      Copy copyBook2 = new Copy(book2.GetId());
      copyBook2.Save();
      Patron newPatron = new Patron("Jim", "McDonald");
      newPatron.Save();
      //Act
      copyBook1.Checkout(newPatron.GetId());
      copyBook2.Checkout(newPatron.GetId());
      List<Copy> expected = new List<Copy>{copyBook1, copyBook2};
      List<Copy> result = newPatron.GetHistory();
      //Assert
      Assert.Equal(expected, result);


    }


    public void Dispose()
    {
      Copy.DeleteAll();
      Patron.DeleteAll();
      Book.DeleteAll();
    }

  }
}
