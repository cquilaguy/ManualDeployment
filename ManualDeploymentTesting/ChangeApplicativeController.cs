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
      private readonly Mock<IChangeApplicativeBll> _mockRepositoryBll = new Mock<IChangeApplicativeBll>();
      private readonly Mock<MessageResponse> _mockMsgProvider = new Mock<MessageResponse>();
      private readonly Mock<ILogger<ChangeApplicativeController>> _mockLogger = new Mock<ILogger<ChangeController>>();
      private readonly ChangeApplicativeController _changeApplicativeController;

      public ChangeApplicativeControllerTesting(){
        _changeApplicativeController = new ChangeApplicativeController(_mockRepositoryBll.Object, _mockMsgProvider.Object, _mockLogger.Object);
      }
      [Fact]
      [Fact]
      public async Task GetChangeApplicative_ReturnsOkObjectResult_WithValidId()
      {
          int id = 1;
          var expectedChangeApplicativeDto = new ChangeApplicativeDTO(); // Assuming you have a proper ChangeDTO object

          _mockRepositoryBll.Setup(repo => repo.GetChangeApplicative(id))
              .ReturnsAsync(new ServiceResponse { Body = new Body { Result = expectedChangeApplicativeDto }, MsgRsHdr = new MsgRsHdr { Error = new Error { Status = new List<DeploymentManualAPI.Response.Models.Status>
              { new DeploymentManualAPI.Response.Models.Status { StatusCode = (int)HttpStatusCode.OK } } } } });

          // Act
          var result = await _changeApplicativeController.GetApplicativeChange(id);

          // Assert
          var okResult = Assert.IsType<OkObjectResult>(result);
          Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
          Assert.Equal(expectedChangeDto, (okResult.Value as ServiceResponse)?.Body?.Result);
      }

    [Fact]
    public async Task GetChangeAppicative_ReturnsBadRequest_WithInvalidId()
    {
        // Arrange
        int id = 0;
        var expectedChangeAppicativeDto = new ChangeAppicativeDTO(); // Assuming you have a proper ChangeDTO object

        _mockRepositoryBll.Setup(repo => repo.GetChangeAppicative(id))
            .ReturnsAsync(new ServiceResponse
            {
                Body = new Body { Result = expectedChangeAppicativeDto },
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
        var result = await _changeAppicativeController.GetChangeAppicative(id);

        // Assert
        var okResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    
    [Fact]
    public async Task PostChange_ReturnsOkWhenChangeApplicativeIsValid()
    {
        // Arrange
        ChangeDTO changeApplication = new ChangeApplicationDTO(); // Provide necessary values for ChangeDTO

        _mockRepositoryBll.Setup(repo => repo.ValidateAsync(changeApplication))
            .ReturnsAsync(true);

        _mockRepositoryBll.Setup(repo => repo.PostChange(changeApplication))
            .ReturnsAsync(new ServiceResponse
            {
                Body = new Body { Result = changeApplication },
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
        var result = await _changeApplicationController.PostChange(changeApplication);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

    }

    [Fact]
    public async Task PostChangeApplicative_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        ChangeApplicativeDTO changeApplicative = new ChangeApplicativeDTO(); // Proporciona los valores necesarios para ChangeDTO
        var errorMessage = "Error en los datos enviados";

        _mockRepositoryBll.Setup(repo => repo.ValidateAsync(change))
            .ReturnsAsync(false);

        _mockRepositoryBll.Setup(repo => repo.PostChange(change))
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
        var result = await _changeApplicativeController.PostChangeApplicative(changeApplicative);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);

    }

    [Fact]
    public async Task PutChangeApplicative_ReturnsOk_WhenChangeIsValid()
    {
        // Arrange
        ChangeDTO changeApplicative = new ChangeApplicativeDTO(); // Provide necessary values for ChangeDTO


        _mockRepositoryBll.Setup(repo => repo.ValidateAsync(changeApplicative))
            .ReturnsAsync(true);

        _mockRepositoryBll.Setup(repo => repo.PutChange(changeApplicative))
            .ReturnsAsync(new ServiceResponse
            {
                Body = new Body { Result = change },
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
        var result = await _changeController.PutChangeApplicative(changeApplicative);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

    }
    [Fact]
    public async Task PutChange_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        ChangeDTO changeApplicative = new ChangeApplicativeDTO(); // Proporciona los valores necesarios para ChangeDTO
        var errorMessage = "Error en los datos enviados";

        _mockRepositoryBll.Setup(repo => repo.ValidateAsync(changeApplicative))
            .ReturnsAsync(false);

        _mockRepositoryBll.Setup(repo => repo.PutChangeApplicative(changeApplicative))
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
        var result = await _changeApplicativeController.PostChangeApplicative(changeApplicative);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);

    }
    
    }
}