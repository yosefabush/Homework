using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Project
{
    public class InputValidation
    {

        public  InputValidation() { }

        public bool isValidEmail(string email){
            if (!email.Contains("@"))
                return false;
            if (!email.Contains("."))
                return false;
            if (!(email.Split('@')[0].Length > 0))
                return false;
            if (email.Split('@')[0].Contains("@") || email.Split('@')[1].Contains("@"))
                return false;
            if (email.Split('.')[0].Contains(".") || email.Split('.')[1].Contains("."))
                if (!(email.Split('.')[0].Contains("@")))
                    return false;
            if (!(email.Split('.')[1].Length > 0))
                return false;
            if (!(email.Split('.')[0].Length > 0))
                return false;
            if (!(email.Split('@')[0].Length > 0))
                return false;
            if (!(email.Split('@')[1].Length > 0))
                return false;
            return true;

        }

        public bool isValidPassword(string password)
        {
            if (!(password.Length >= 8))
                return false;
            if (!(password.Any(char.IsDigit)))
                return false;
            if (!(password.Any(char.IsLetter)))
                return false;
            return true;
        }

        public bool isValidUsername(string username)
        {
            if (!(username.Length >= 4))
                return false;
            if (!(username.Any(char.IsLetter)))
                return false;
            return true;
        }
    }
}
