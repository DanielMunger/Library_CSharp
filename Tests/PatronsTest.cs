// using Xunit;
// using System;
// using System.Collections.Generic;
// using Library.Objects;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace Library.Test
// {
//   public class PatronTest :IDisposable
//   {
//     public PatronTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=DESKTOP-GC3DC7B\\SQLEXPRESS;Initial Catalog=library_test;Integrated Security=SSPI;";
//     }
//
//     [Fact]
//     public void Add_AddsPatronToDB_True()
//     {
//       Patron newPatron = new Patron("Johnny", "Appleseed");
//       newPatron.Save();
//       List<Patron> result = Patron.GetAll();
//
//       Assert.Equal(1, result.Count);
//     }
//
//     [Fact]
//     public void Find_GetPatronFromDB_EquivalentPatron()
//     {
//       Patron newPatron = new Patron("Johnny", "Appleseed");
//       newPatron.Save();
//       Patron result = Patron.Find(newPatron.GetId());
//
//       Assert.Equal(newPatron, result);
//     }
//     [Fact]
//     public void Update_UpdatesPatroninDB_True()
//     {
//       Patron newPatron = new Patron("John", "Appleseed");
//       newPatron.Save();
//       newPatron.Update("Johnny", "Appleseed");
//       Patron foundPatron = Patron.Find(newPatron.GetId());
//       Assert.Equal(newPatron,foundPatron);
//     }
//
//     [Fact]
//     public void Delete_RemoveOnePatronFromDB_0()
//     {
//       Patron newPatron = new Patron("Johnny", "Appleseed");
//       newPatron.Save();
//       newPatron.Delete();
//       List<Patron> allPatrons = Patron.GetAll();
//       Assert.Equal(0, allPatrons.Count);
//     }
//
//     public void Dispose()
//     {
//       Patron.DeleteAll();
//     }
//
//   }
// }
