using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
