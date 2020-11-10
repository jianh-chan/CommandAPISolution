using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAppCommands();

        Command GetCommandById(int Id);

    }
}