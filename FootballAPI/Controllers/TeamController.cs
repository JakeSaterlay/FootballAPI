using FootballAPI.Domain;
using FootballAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace FootballAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _dbClient;
        private readonly MongoDB.Driver.Linq.IMongoQueryable<Team> _dbList;

        public TeamController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbClient = new MongoClient(_configuration.GetConnectionString("DefaultFootballConnection"));
            _dbList = _dbClient.GetDatabase("Football").GetCollection<Team>("TeamCollection").AsQueryable();
        }

        [HttpGet]
        [Route("api/getteams")]
        public List<TeamModel> GetAllTeams()
        {
            var teamList = new List<TeamModel>();

            foreach(var team in _dbList)
            {
                var teamToAdd = new TeamModel()
                {
                    TeamId = team.TeamId,
                    Name = team.Name,
                    Stadium = new Models.Stadium()
                    {
                        Name = team.Stadium.Name,
                        Capacity = team.Stadium.Capacity
                    }
                };
                teamList.Add(teamToAdd);
            }

            return teamList;
        }

        [HttpGet]
        [Route("api/getteambyid/{id}")]
        public TeamModel GetTeamById(int id)
        {
            var team = _dbList.Where(x => x.TeamId == id).FirstOrDefault();

            if (team == null)
                throw new Exception("No team found");

            TeamModel model = new TeamModel()
            {
                TeamId = team.TeamId,
                Name = team.Name,
                Stadium = new Models.Stadium()
                {
                    Name = team.Stadium.Name,
                    Capacity = team.Stadium.Capacity
                }
            };

            return model;
        }
    }
}