// using Xunit;
// using System;
// using System.Collections.Generic;
// using Library.Objects;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace Library.Test
// {
//   public class AuthorTest :IDisposable
//   {
//     public AuthorTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=DESKTOP-GC3DC7B\\SQLEXPRESS;Initial Catalog=library_test;Integrated Security=SSPI;";
//     }
//
//     [Fact]
//     public void Add_AddsAuthorToDB_True()
//     {
//       Author newAuthor = new Author("Stephen", "King");
//       newAuthor.Save();
//       List<Author> result = Author.GetAll();
//
//       Assert.Equal(1, result.Count);
//     }
//
//     [Fact]
//     public void Find_GetAuthorFromDB_EquivalentAuthor()
//     {
//       Author newAuthor = new Author("Stephen", "King");
//       newAuthor.Save();
//       Author result = Author.Find(newAuthor.GetId());
//
//       Assert.Equal(newAuthor, result);
//     }
//     [Fact]
//     public void Update_UpdatesAuthorinDB_True()
//     {
//       Author newAuthor = new Author("Steven", "King");
//       newAuthor.Save();
//       newAuthor.Update("Stephen", "King");
//       Author foundAuthor = Author.Find(newAuthor.GetId());
//       Assert.Equal(newAuthor,foundAuthor);
//     }
//
//     [Fact]
//     public void Delete_RemoveOneAuthorFromDB_0()
//     {
//       Author newAuthor = new Author("Stephen", "King");
//       newAuthor.Save();
//       newAuthor.Delete();
//       List<Author> allAuthors = Author.GetAll();
//       Assert.Equal(0, allAuthors.Count);
//     }
//
//     public void Dispose()
//     {
//       Author.DeleteAll();
//     }
//
//   }
// }
