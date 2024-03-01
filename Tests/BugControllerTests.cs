using AutoMapper;
using Bugzilla.Api.Controllers;
using Bugzilla.Api.Repository.IRepository;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests
{
    public class BugControllerTests
    {
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IBugRepository> _bugRepositoryMock = new Mock<IBugRepository>();
        private readonly BugController _controller;

       
        public BugControllerTests()
        {
            _controller = new BugController(_mapperMock.Object, _bugRepositoryMock.Object);
        }

        [Fact]
        public async Task Add_ValidBug_ReturnsOkResult()
        {
            // Arrange
            var bugDto = new BugDTO(); // Provide valid BugDTO
            _mapperMock.Setup(m => m.Map<Bug>(It.IsAny<BugDTO>())).Returns(new Bug());

            // Act
            var result = await _controller.Add(bugDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Add_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var bugDto = new BugDTO(); // Provide invalid BugDTO
            _controller.ModelState.AddModelError("key", "error message");

            // Act
            var result = await _controller.Add(bugDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Add_NullBugDto_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.Add(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Add_ExceptionThrown_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var bugDto = new BugDTO(); // Provide valid BugDTO
            _mapperMock.Setup(m => m.Map<Bug>(It.IsAny<BugDTO>())).Returns(new Bug());
            _bugRepositoryMock.Setup(repo => repo.Add(It.IsAny<Bug>())).Throws(new Exception("Some error message"));

            // Act
            var result = await _controller.Add(bugDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error message", badRequestResult.Value);
        }
    }
}