using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Models;

using CommandAPI.Data;

namespace CommandAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;

        public CommandsController(ICommandAPIRepo repository)
        {
            _repository = repository;
        }
        // private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        
        //GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAppCommands();

            return Ok(commandItems); // the commandItems object is the value to format in the entity body
        }

        //GET api/commands/5
        [HttpGet("{id}")]
        public ActionResult <Command> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            return Ok(commandItem);
        }
    }
}