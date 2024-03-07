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
    public class BlueprintControllerTesting
    {
      private readonly Mock<IBlueprintBll> _mockRepositoryBll = new Mock<IBlueprintBll>();
      private readonly Mock<MessageResponse> _mockMsgProvider = new Mock<MessageResponse>();
      private readonly Mock<ILogger<BlueprintController>> _mockLogger = new Mock<ILogger<BlueprintController>>();
      private readonly BlueprintController _BlueprintController;

      public BlueprintControllerTesting()
      {
          _blueprintController = new BlueprintController(_mockRepositoryBll.Object, _mockMsgProvider.Object, _mockLogger.Object);
      }
      [Fact]
      public async Task GetBlueprint_ReturnsOkObjectResult_WithValidId()
      {
          int id = 1;
          var expectedBlueprintDto = new BlueprintDTO(); // Assuming you have a proper BlueprintDTO object

          _mockRepositoryBll.Setup(repo => repo.GetBlueprint(id))
              .ReturnsAsync(new ServiceResponse { Body = new Body { Result = expectedBlueprintDto }, MsgRsHdr = new MsgRsHdr { Error = new Error { Status = new List<DeploymentManualAPI.Response.Models.Status>
              { new DeploymentManualAPI.Response.Models.Status { StatusCode = (int)HttpStatusCode.OK } } } } });

          // Act
          var result = await _BlueprintController.GetBlueprint(id);

          // Assert
          var okResult = Assert.IsType<OkObjectResult>(result);
          Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
          Assert.Equal(expectedBlueprintDto, (okResult.Value as ServiceResponse)?.Body?.Result);
      }

      [Fact]
      public async Task GetBlueprint_ReturnsBadRequest_WithInvalidId()
      {
          // Arrange
          int id = 0;
          var expectedBlueprintDto = new BlueprintDTO(); // Assuming you have a proper BlueprintDTO object

          _mockRepositoryBll.Setup(repo => repo.GetBlueprint(id))
              .ReturnsAsync(new ServiceResponse
              {
                  Body = new Body { Result = expectedBlueprintDto },
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
          var result = await _blueprintController.GetBlueprint(id);

          // Assert
          var okResult = Assert.IsType<BadRequestObjectResult>(result);
          Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

      }

      [Fact]
      public async Task Postblueprint_ReturnsOkWhenBlueprintIsValid()
      {
        // Arrange
        BlueprintDTO blueprint = new BlueprintDTO(); // Provide necessary values for BlueprintDTO

        _mockRepositoryBll.Setup(repo => repo.ValidateAsync(blueprint))
            .ReturnsAsync(true);

        _mockRepositoryBll.Setup(repo => repo.Postblueprint(blueprint))
            .ReturnsAsync(new ServiceResponse
            {
                Body = new Body { Result = blueprint },
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
        var result = await _blueprintController.Postblueprint(blueprint);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

      }

      

      public async Task Postblueprint_ReturnBadRequest_ValidationFails()
      {
        BlueprintDTO blueprint = new BlueprintDTO(); // Proporciona los valores necesarios para BlueprintDTO
        var errorMessage = "Error en los datos enviados";

        _mockRepositoryBll.Setup(repo => repo.ValidateAsync(blueprint))
            .ReturnsAsync(false);

        _mockRepositoryBll.Setup(repo => repo.Postblueprint(blueprint))
            .ReturnsAsync(new ServiceResponse
            {
                MsgRsHdr = new MsgRsHdr
                {
                    Error = new Error
                    {
                        Status = new List<DeploymentManualAPI.Response.Models.Status>
                        {
                    new DeploymentManualAPI.Response.Models.Status
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        StatusDesc = errorMessage
                    }
                        }
                    }
                }
            });

        // Act
        var result = await _blueprintController.Postblueprint(blueprint);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);

      }

      [Fact]
      public async Task PutBlueprint_ReturnsOk_WhenBlueprintIsValid()
      {
          // Arrange
          BlueprintDTO blueprint = new BlueprintDTO(); // Provide necessary values for BlueprintDTO


          _mockRepositoryBll.Setup(repo => repo.ValidateAsync(blueprint))
              .ReturnsAsync(true);

          _mockRepositoryBll.Setup(repo => repo.PutBlueprint(blueprint))
              .ReturnsAsync(new ServiceResponse
              {
                  Body = new Body { Result = blueprint },
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
          var result = await _blueprintController.PutBlueprint(blueprint);

          // Assert
          var okResult = Assert.IsType<OkObjectResult>(result);
          Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

      }

      [Fact]
      public async Task Putblueprint_ReturnsBadRequest_WhenValidationFails()
      {
          // Arrange
          BlueprintDTO blueprint = new BlueprintDTO(); // Proporciona los valores necesarios para BlueprintDTO
          var errorMessage = "Error en los datos enviados";

          _mockRepositoryBll.Setup(repo => repo.ValidateAsync(blueprint))
              .ReturnsAsync(false);

          _mockRepositoryBll.Setup(repo => repo.Putblueprint(blueprint))
              .ReturnsAsync(new ServiceResponse
              {
                  MsgRsHdr = new MsgRsHdr
                  {
                      Error = new Error
                      {
                          Status = new List<DeploymentManualAPI.Response.Models.Status>
                          {
                      new DeploymentManualAPI.Response.Models.Status
                      {
                          StatusCode = (int)HttpStatusCode.BadRequest,
                          StatusDesc = errorMessage
                      }
                          }
                      }
                  }
              });

          // Act
          var result = await _blueprintController.PostBlueprint(blueprint);

          // Assert
          var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
          Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);

      }
      
    }
  }