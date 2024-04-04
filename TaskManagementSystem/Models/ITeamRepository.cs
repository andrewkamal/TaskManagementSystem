namespace TaskManagementSystem.Models
{
    public interface ITeamRepository
    {
        Team GetTeam(int id);
        IEnumerable<Team> GetAllTeams();
        Team AddTeam(Team team);
        Team UpdateTeam(Team teamChanges);
        Team DeleteTeam(int id);
    }
}
