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

namespace AplicativeControllerTesting{
    public class ApplicativeControllerTesting
    {
      private readonly Mock<IApplicativeBll> _mockRepositoryBll = new Mock<IApplicativeBll>();
      private readonly Mock<MessageResponse> _mockMsgProvider = new Mock<MessageResponse>();
      private readonly Mock<ILogger<ApplicativeController>> _mockLogger = new Mock<ILogger<CApplicativeController>>();
      private readonly ApplicativeController _applicativeController;

      public ApplicativeControllerTesting(){
        _applicativeController = new ApplicativeController(_mockRepositoryBll.Object, _mockMsgProvider.Object, _mockLogger.Object);
      }

      [Fact]
      public async Task GetApplicatives_ReturnsOkObjectResponse()
      {
          var expectedApplicativeDto = new ApplicativeDTO(); // Assuming you have a proper ChangeDTO object

          _mockRepositoryBll.Setup(repo => repo.GetApplicatives())
              .ReturnsAsync(new ServiceResponse {
                Body = new Body { 
                  Response = expectedApplicativeDto },
                MsgRsHdr = new MsgRsHdr { Error = new Error { Status = new List<DeploymentManualAPI.Response.Models.Status>
              { new DeploymentManualAPI.Response.Models.Status { StatusCode = (int)HttpStatusCode.BadRequest } } } } });

          // Act
          var response = await _applicativeController.GetApplicatives();

          // Assert
          var okResponse = Assert.IsType<OkObjectResult>(response);
          Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
          Assert.Equal(expectedApplicativeDto, (okResult.Value as ServiceResponse)?.Body?.Result);
      }


      [Fact]
      public async Task GetApplicatives_ReturnsBadRequest()
      {
          var expectedApplicativeDto = new ApplicativeDTO(); // Assuming you have a proper ChangeDTO object

          _mockRepositoryBll.Setup(repo => repo.GetApplicatives())
              .ReturnsAsync(new ServiceResponse { Body = new Body { Response = expectedApplicativeDto }, MsgRsHdr = new MsgRsHdr { Error = new Error { Status = new List<DeploymentManualAPI.Response.Models.Status>
              { new DeploymentManualAPI.Response.Models.Status { StatusCode = (int)HttpStatusCode.BadRequest } } } } });

          // Act
          var response = await _applicativeController.GetApplicatives();

          // Assert
          var okResponse = Assert.IsType<BadRequestObjectResult>(response);
          Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
          
      }

      
}