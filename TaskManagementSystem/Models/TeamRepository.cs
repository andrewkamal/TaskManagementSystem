

namespace TaskManagementSystem.Models
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TMSDbContext DB;

        public TeamRepository(TMSDbContext DB)
        {
            this.DB = DB;
        }

        public Team AddTeam(Team team)
        {
            DB.Team.Add(team);
            DB.SaveChanges();
            return team;
        }

        public Team DeleteTeam(int id)
        {
            var team = DB.Team.Find(id);
            if (team != null)
            {
                DB.Team.Remove(team);
                DB.SaveChanges();
            }
            return team;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var teams = DB.Team.ToList();
            return teams;
        }

        public Team GetTeam(int id)
        {
            return DB.Team.Find(id);
        }

        public Team UpdateTeam(Team teamChanges)
        {
            var team = DB.Team.Attach(teamChanges);
            team.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            DB.SaveChanges();
            return teamChanges;
        }
    }
}
