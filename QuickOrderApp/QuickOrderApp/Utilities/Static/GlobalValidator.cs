using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Static
{
    public static class GlobalValidator
    {
        public static bool CheckNullOrEmptyImage (ImageSource imgValue)
        {
            if( imgValue != null )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckNullOrEmptyProperties (string fullname, string email, string username, string password, string confirmpassword, string phone, string adress, string gender)
        {
            if( !string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(adress) && !string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmpassword) && password == confirmpassword )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckNullOrEmptyPropertiesOfListValues (IList<string> Values)
        {
            bool result = false;

            foreach( var item in Values )
            {
                if( !string.IsNullOrEmpty(item) )
                {
                    result = true;
                }
                else
                {
                    return false;
                }
            }

            return result;
        }

        public static bool CheckNullOrEmptyValue (string value)
        {
            if( !String.IsNullOrEmpty(value) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}