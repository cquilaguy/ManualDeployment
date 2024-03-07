using DeploymentManualAPI.Bussines;
using DeploymentManualAPI.Controllers;
using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Response.Models;
using DeploymentManualAPI.Servicios;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ManualDeploymentTesting
{
    public class ChangeControllerTesting
    {
      private readonly Mock<IEnvironmentBll> _mockRepositoryBll = new Mock<IEnvironmentBll>();
      private readonly Mock<MessageResponse> _mockMsgProvider = new Mock<MessageResponse>();
      private readonly Mock<ILogger<ChangeController>> _mockLogger = new Mock<ILogger<EnvironmentController>>();
      private readonly EnvironmentController _enviorementController;

      public EnvironmentControllerTesting()
      {
          _environmentController = new EnvironmentController(_mockRepositoryBll.Object, _mockMsgProvider.Object, _mockLogger.Object);
      }

      [Fact]
      public async Task GetEnvironment_ReturnsOkObjectResult_WithValidId()
      {
          int id = 1;
          var expectedEnvironmentDto = new EnvironmentDTO(); // Assuming you have a proper ChangeDTO object

          _mockRepositoryBll.Setup(repo => repo.GetEnvironment(id))
              .ReturnsAsync(new ServiceResponse { Body = new Body { Result = expectedChangeDto }, MsgRsHdr = new MsgRsHdr { Error = new Error { Status = new List<DeploymentManualAPI.Response.Models.Status>
              { new DeploymentManualAPI.Response.Models.Status { StatusCode = (int)HttpStatusCode.OK } } } } });

          // Act
          var result = await _environmentController.GetEnvironment(id);

          // Assert
          var okResult = Assert.IsType<OkObjectResult>(result);
          Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
          Assert.Equal(expectedChangeDto, (okResult.Value as ServiceResponse)?.Body?.Result);
      }

      [Fact]
      public async Task Get_EnvironmentReturnsBadRequest_WithInvalidId()
      {
          // Arrange
          int id = 0;
          var expectedEnvironmentDto = new EnvironmentDTO(); // Assuming you have a proper ChangeDTO object

          _mockRepositoryBll.Setup(repo => repo.GetEnvironment(id))
              .ReturnsAsync(new ServiceResponse
              {
                  Body = new Body { Result = expectedEnvironmentDto },
                  MsgRsHdr = new MsgRsHdr
                  {
                      Error = new Error
                      {
                          Status = new List<DeploymentManualAPI.Response.Models.Status>
              { new DeploymentManualAPI.Response.Models.Status { StatusCode = (int)HttpStatusCode.BadRequest } }
                      }
                  }
              });

          // Act
          var result = await _environmentController.GetChange(id);

          // Assert
          var okResult = Assert.IsType<BadRequestObjectResult>(result);
          Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

      }
      
    }

  
}
