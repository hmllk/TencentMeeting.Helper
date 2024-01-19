using System;
using System.Collections.Generic;
using System.Text;

namespace TencentMeeting.Util
{
    public static class EncryptHelper
    {

        public static string EncodeBase64(string data)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                var result = Convert.ToBase64String(bytes);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static string DecodeBase64(string data)
        {
            try
            {
                var bytes = Convert.FromBase64String(data);
                var result = Encoding.UTF8.GetString(bytes);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
