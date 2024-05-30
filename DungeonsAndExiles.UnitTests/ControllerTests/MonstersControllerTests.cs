using DungeonsAndExiles.Api.Controllers;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Models.Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DungeonsAndExiles.UnitTests.ControllerTests
{
    public class MonstersControllerTests
    {
        private readonly MonstersController _monstersController;
        private readonly IMonsterRepository _monsterRepository;
        private readonly ILogger<MonstersController> _logger;


        public MonstersControllerTests()
        {
            _monsterRepository = A.Fake<IMonsterRepository>();
            _logger = A.Fake<ILogger<MonstersController>>();
            _monstersController = new MonstersController(_monsterRepository, _logger);
        }

        [Fact]
        public async Task GetMonstersList_ReturnsListOfMonsters_WhenCalled()
        {
            // Arrange
            var sampleMonsters = GetSampleMonsters();
            A.CallTo(() => _monsterRepository.MonstersList()).Returns(Task.FromResult(sampleMonsters));


            // Act
            var result = await _monstersController.GetMonstersList();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<List<Monster>>();
            var returnValue = okResult.Value as List<Monster>;
            returnValue.Should().HaveCount(3);
        }

        private List<Monster> GetSampleMonsters()
        {
            return new List<Monster>
            {
                new Monster { Id = Guid.NewGuid(), Name = "Monster1", Level = 1,Health = 1,Defence = 1, Damage = 10 },
                new Monster { Id = Guid.NewGuid(), Name = "Monster2", Level = 2,Health = 1,Defence = 1, Damage = 30 },
                new Monster { Id = Guid.NewGuid(), Name = "Monster3", Level = 3,Health = 1,Defence = 1, Damage = 50 },
            };
        }
    }
}
