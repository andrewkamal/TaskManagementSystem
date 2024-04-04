namespace TaskManagementSystem.Models
{
    public interface IUserTeamRepository
    {
        UserTeam GetUserTeam(string userId, int teamId);
        IEnumerable<UserTeam> GetUserTeams(string userId);
        IEnumerable<UserTeam> GetTeamUsers(int teamId);
        UserTeam AddUserTeam(UserTeam userTeam);
        UserTeam UpdateUserTeam(UserTeam userTeamChanges);
        UserTeam DeleteUserTeam(string userId, int teamId);
        bool IsUserInTeam(string id, int teamId);
    }
}
