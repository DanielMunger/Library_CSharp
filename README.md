# _TEMPLATE_

#### _Short Description._

#### By _**Daniel Munger and Levi Bibo**_

## Description
_Longer Description_

## Specs
| Description                                                                    | Input                     | Output                               |
|--------------------------------------------------------------------------------|---------------------------|--------------------------------------|
| Program will allow librarian to add a new author                               | Stephen King              | 1                                    |
| Program will allow any user to find author                                     | Stephen King              | Stephen King                         |
| Program will allow librarian to edit author details                            | Steven King: Stephen King | Stephen King                         |
| Program will allow librarian to delete author                                  | remove author             | 0                                    |
| Program will allow librarian to add a new book                                 | It                        | 1                                    |
| Program will allow any user to find book                                       | It                        | It                                   |
| Program will allow librarian to edit book details                              | It:Carrie                 | Carrie                               |
| Program will allow librarian to remove book                                    | remove book               | 0                                    |
| Program will allow librarian to add book to author                             | It: Stephen King          | It: Stephen King                     |
| Program will allow librarian to add a new copy of a given book                 | It: 1 copy                | 1                                    |
| Program will allow librarian to remove copy of book                            | remove copy               | 0                                    |
| Program will allow librarian to remove all copies of book when book is removed | remove book               | 0 copies                             |
| Program will allow librarian to add new patron                                 | Johnny Appleseed          | 1                                    |
| Program will allow librarian to find a patron                                  | Johnny Appleseed          | Johnny Appleseed                     |
| Program will allow any user to checkout a book copy                            | Johnny Appleseed: It copy | Johnny Appleseed checkouts: 1        |
| Program will allow any user to look at checkout history.                       | Johnny Appleseed          | Johnny Appleseed: It, It, It, Carrie |
| Program will allow any user to look up a checkout due date                     | It copy                   | 12/14/2016                           |
| Program will allow librarian to find all overdue book copies                   | Get overdue               | It: Johnny Appleseed                 |

## Setup/Installation Requirements

* _Download the repository [here] "'s TEMPLATE"). Run on Windows with Powershell. In powershell, navigate into the repository directory and use command >dnx kestrel to run. Open up any browser window and enter localhost:5004 into URL search bar. Use command >dnx test to run tests in Test folder._

## Known Bugs

_None known._

## Support and contact details

_Please contact author through GitHub at username: _

## Technologies and Resources Used

_HTML, CSS, C#, Xunit,  Nancy, Razor, Git, GitHub_

### License

*MIT*

Copyright (c) 2016 **_Daniel Munger and Levi Bibo**
