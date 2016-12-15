// using Xunit;
// using System;
// using System.Collections.Generic;
// using Library.Objects;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace Library.Test
// {
//   public class BookTest :IDisposable
//   {
//     public BookTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=DESKTOP-GC3DC7B\\SQLEXPRESS;Initial Catalog=library_test;Integrated Security=SSPI;";
//     }
//     [Fact]
//     public void Save_SavesBookToDB_True()
//     {
//       Book newBook = new Book("It");
//       newBook.Save();
//       List<Book> result = Book.GetAll();
//
//       Assert.Equal(1, result.Count);
//     }
//
//     [Fact]
//     public void Find_GetBookFromDB_EquivalentBook()
//     {
//       Book newBook = new Book("It");
//       newBook.Save();
//       Book result = Book.Find(newBook.GetId());
//
//       Assert.Equal(newBook, result);
//     }
//     [Fact]
//     public void Update_UpdatesBookinDB_True()
//     {
//       Book newBook = new Book("Carrie");
//       newBook.Save();
//       newBook.Update("It");
//       Book foundBook = Book.Find(newBook.GetId());
//       Assert.Equal(newBook,foundBook);
//     }
//
//     [Fact]
//     public void Delete_RemoveOneBookFromDB_0()
//     {
//       Book newBook = new Book("It");
//       newBook.Save();
//       newBook.Delete();
//       List<Book> allBooks = Book.GetAll();
//       Assert.Equal(0, allBooks.Count);
//     }
//
//
//     public void Dispose()
//     {
//       Book.DeleteAll();
//     }
//
//   }
// }
