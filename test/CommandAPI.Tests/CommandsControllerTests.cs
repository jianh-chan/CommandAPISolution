using Xunit;
using CommandAPI.Controllers;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using CommandAPI.Models;
using CommandAPI.Data;
using CommandAPI.Profiles;
using CommandAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }

        [Fact]
        public void GetAllCommands_ReturnsZeroItems_WhenDBIsEmpty()
        {
            // Arrange           
            mockRepo.Setup(repo => repo.GetAllCommands())
                    .Returns(GetCommands(0));

            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            // Arrange           
            mockRepo.Setup(repo => repo.GetAllCommands())
                    .Returns(GetCommands(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;

            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Returns200OK_WhenDBHasOneResource()
        {
            // Arrange           
            mockRepo.Setup(repo => repo.GetAllCommands())
                    .Returns(GetCommands(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            // Arrange           
            mockRepo.Setup(repo => repo.GetAllCommands())
                    .Returns(GetCommands(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }

        [Fact]
        public void GetCommandById_Returns200OK_WhenValidIDProvided()
        {
            // Arrange
            int id = 1;
            var controller = new CommandsController(mockRepo.Object, mapper);
            mockRepo.Setup(repo => repo.GetCommandById(id))
                    .Returns(new Command
                    {
                        Id = id,
                        HowTo = "Mock",
                        Line = "Mock",
                        Platform = "Mock"
                    });

            // Act
            var result = controller.GetCommandById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandById_ReturnsCorrectType_WhenValidIdProvided()
        {
            // Arrange
            int id = 1;
            var controller = new CommandsController(mockRepo.Object, mapper);
            mockRepo.Setup(repo => repo.GetCommandById(id))
                    .Returns(new Command
                    {
                        Id = id,
                        HowTo = "Mock",
                        Line = "Mock",
                        Platform = "Mock"
                    });

            // Act
            var result = controller.GetCommandById(id);

            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public void GetCommandById_Returns404NotFound_WhenNonExistentIdProvided()
        {
            // Arrange
            int id = 0;
            mockRepo.Setup(repo => repo.GetCommandById(id))
                    .Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetCommandById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    Line = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".NET Core EF"
                });
            }
            return commands;
        }
    }
}