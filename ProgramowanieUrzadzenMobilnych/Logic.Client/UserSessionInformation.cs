using Common.Constants;
using System;

namespace Logic.Client
{
    public class UserSessionInformation
    {
        public int LoggedUserId { get; private set; }
        public UserRole UserRole { get; private set; }
    }
}
