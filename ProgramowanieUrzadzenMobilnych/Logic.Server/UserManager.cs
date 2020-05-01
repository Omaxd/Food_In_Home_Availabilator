using System;
using System.Collections;
using System.Collections.Generic;

namespace Logic.Server
{
    public class UserManager
    {
        private IList<int> _activeUsers;

        public UserManager()
        {
            _activeUsers = new List<int>();
        }

        public bool AddActiveUser(int userId)
        {
            // Check if user is active
            if (_activeUsers.Contains(userId))
                return false;

            if (!CheckIfUserExistInDatabase(userId))
                return false;

            _activeUsers.Add(userId);

            return true;
        }

        public bool RemoveActiveUser(int userId)
        {
            // Check if user is active
            if (!_activeUsers.Contains(userId))
                return false;

            _activeUsers.Remove(userId);

            return true;
        }

        private bool CheckIfUserExistInDatabase(int userId)
        {
            // TODO: Add connection with repository
            return false;
        }
    }
}
