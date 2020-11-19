using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public class MockCommandAPIRepo : ICommandAPIRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boil and egg", Line="Boil water", Platform="Kettle and pan"},
                new Command{Id=1, HowTo="Slice bread", Line="Get a knife", Platform="Knife and chopping board"},
                new Command{Id=2, HowTo="Make cup of tea", Line="Place tea bag in cup", Platform="Kettle and cup"},
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id=0, HowTo="Boil and egg", Line="Boil water", Platform="Kettle and pan"};
        }
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
        
        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

    }
}