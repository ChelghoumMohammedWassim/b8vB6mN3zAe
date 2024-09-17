using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Tools
{
    public static class Utils
    {
        public static bool UserHaveAccess(List<User?> users, string userId, ApplicationDBContext _context)
        {
            User? accessUser = _context.Users.FirstOrDefault(user => user.ID == userId);

            if (accessUser is not null && accessUser.Role == Models.Enums.Role.Admin)
            {
                return true;
            }

            if (users is null)
            {
                return false;
            }

            foreach (User? user in users)
            {
                if (user is not null && user.ID == userId)
                {
                    return true;
                }
            }

            return false;
        }
    }

}
