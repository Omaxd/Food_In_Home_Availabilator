using Common.Constants;
using System;

namespace Logic.Client
{
    public class UserSessionInformation
    {
        public static int LoggedUserId { get; set; }
        public static string UserName { get; set; }
        public static UserRole UserRole { get; set; }
        public static string ApplicationVersion { get; set; }
    }
}
