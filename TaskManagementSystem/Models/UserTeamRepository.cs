
using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Models
{
    public class UserTeamRepository: IUserTeamRepository
    {
        private readonly TMSDbContext DB;
        public UserTeamRepository(TMSDbContext DB)
        {
            this.DB = DB;
        }

        public UserTeam AddUserTeam(UserTeam userTeam)
        {
            var userTeamExists = DB.UserTeam.Find(userTeam.UserId, userTeam.TeamId);
            if (userTeamExists == null)
            {
                DB.UserTeam.Add(userTeam);
                DB.SaveChanges();
            }
            return userTeam;
        }

        public UserTeam DeleteUserTeam(string userId, int teamId)
        {
            UserTeam userTeam = DB.UserTeam.Find(userId, teamId);
            if (userTeam != null)
            {
                DB.UserTeam.Remove(userTeam);
                DB.SaveChanges();
            }
            return userTeam;
        }

        public IEnumerable<UserTeam> GetTeamUsers(int teamId)
        {
            return DB.UserTeam.Include(ut => ut.User).Where(ut => ut.TeamId == teamId).ToList(); ;
        }

        public UserTeam GetUserTeam(string userId, int teamId)
        {
            return DB.UserTeam.Find(userId, teamId);
        }

        public IEnumerable<UserTeam> GetUserTeams(string userId)
        {
            return DB.UserTeam.Where(ut => ut.UserId == userId).ToList();
        }

        public bool IsUserInTeam(string id, int teamId)
        {
            return DB.UserTeam.Any(ut => ut.UserId == id && ut.TeamId == teamId);
        }

        public UserTeam UpdateUserTeam(UserTeam userTeamChanges)
        {
            var userTeam = DB.UserTeam.Attach(userTeamChanges);
            userTeam.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            DB.SaveChanges();
            return userTeamChanges;
        }
    }
}
