using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Claubrary
{
    internal static class Controller
    {
        public static ClaubraryDataContext Context { get; set; } = new ClaubraryDataContext();

        public static string SendOTP(string email, string password)
        {
            string OTP = "";
            Context.uspSendOTP(email, password, ref OTP);

            return OTP;
        }

        public static bool VerifyOTP(string email, string OTP)
        {
            if ((bool)Context.fnCheckOTP(email, OTP))
            {
                return true;
            }

            return false;
        }

        public static void VerifyEmployee(string email, string password)
        {
            Context.uspVerifyEmployee(email, password);
        }

        public static bool IsEmployeVerifiedOrExists(string email)
        {
            return (bool)Context.fnIsEmployeeVerifiedOrExists(email);
        }

        public static void UpdateEmployeeDetails(string firstName, string middleName, string lastName, DateTime birthdate, string contactNo, string address, string password)
        {
            try
            {
                Context.uspUpdateEmployeeDetails(firstName, middleName, lastName, birthdate, contactNo, address, password);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public static void UpdateEmployeeStatus(string email, bool signIn)
        {
            Context.uspSetEmployeeStatus(email, signIn);
        }

        public static bool SignInEmployee(string email, string password)
        {
           return (bool)Context.fnSignIn(email, password);
        }

        public static bool? BorrowBook(int memberId, int bookId)
        {
            bool? success = false;

            Context.uspBorrowBook(memberId, bookId, DateTime.Now.AddDays(7), Auth.EmployeeEmail, ref success);

            return success;
        }

        public static bool? ReturnBook(int memberId, int bookId)
        {
            bool? success = false;

            Context.uspReturnBook(memberId, bookId, ref success);

            return success;
        }

        public static List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();

            foreach (Book book in Context.Books)
            {
                books.Add(book);
            }

            return books;
        }
        
        public static List<Author> GetAuthors() 
        {
            List<Author> authors = new List<Author>();

            foreach (Author author in Context.Authors)
            {
                authors.Add(author);
            }

            return authors;
        }

        public static List<Series> GetAllSeries()
        {
            List<Series> seriesList = new List<Series>();

            foreach (Series serie in Context.Series)
            {
                seriesList.Add(serie);
            }

            return seriesList;
        }

        public static List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();

            foreach (Genre genre in Context.Genres)
            {
                genres.Add(genre);
            }

            return genres;
        }

        public static List<Publisher> GetPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();

            foreach (Publisher publisher in Context.Publishers)
            {
                publishers.Add(publisher);
            }

            return publishers;
        }

        public static List<Member> GetMembers()
        {
            List <Member> members = new List<Member>();

            foreach (Member member in Context.Members)
            {
                members.Add(member);
            }

            return members;
        }

        public static List<Borrow> GetBorrowsFromMember(Member member)
        {
            List<Borrow> borrows = new List<Borrow>();

            foreach (Borrow borrow in Context.Borrows)
            {
                if (borrow.MemberID == member.MemberID)
                {
                    borrows.Add(borrow);
                }
            }

            return borrows;
        }

        
    }
}
