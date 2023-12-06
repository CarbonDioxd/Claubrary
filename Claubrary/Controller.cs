using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Claubrary
{
    internal static class Controller
    {
        private static ClaubraryDataContext Context { get; set; } = new ClaubraryDataContext();

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

        public static void VerifyEmployee(string email)
        {
            Context.uspVerifyEmployee(email);
        }

        public static bool IsEmployeVerifiedOrExists(string email)
        {
            return (bool)Context.fnIsEmployeeVerifiedOrExists(email);
        }

        public static void UpdateEmployeeDetails(string firstName, string middleName, string lastName, DateTime birthdate, string contactNo, string address)
        {
            try
            {
                Context.uspUpdateEmployeeDetails(firstName, middleName, lastName, birthdate, contactNo, address);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
    }
}
