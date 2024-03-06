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
        private readonly Mock<IChangeBll> _mockRepositoryBll = new Mock<IChangeBll>();
        private readonly Mock<MessageResponse> _mockMsgProvider = new Mock<MessageResponse>();
        private readonly Mock<ILogger<ChangeController>> _mockLogger = new Mock<ILogger<ChangeController>>();
        private readonly ChangeController _changeController;

        public ChangeControllerTesting()
        {
            _changeController = new ChangeController(_mockRepositoryBll.Object, _mockMsgProvider.Object, _mockLogger.Object);
        }
        /// <summary>
        /// Metodo que hace testing en el metodo GetChange por id cuando es valido
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetChange_ReturnsOkObjectResult_WithValidId()
        {
            int id = 1;
            var expectedChangeDto = new ChangeDTO(); // Assuming you have a proper ChangeDTO object

            _mockRepositoryBll.Setup(repo => repo.GetChange(id))
                .ReturnsAsync(new ServiceResponse { Body = new Body { Result = expectedChangeDto }, MsgRsHdr = new MsgRsHdr { Error = new Error { Status = new List<DeploymentManualAPI.Response.Models.Status>
                { new DeploymentManualAPI.Response.Models.Status { StatusCode = (int)HttpStatusCode.OK } } } } });

            // Act
            var result = await _changeController.GetChange(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(expectedChangeDto, (okResult.Value as ServiceResponse)?.Body?.Result);
        }
        /// <summary>
        /// Metodo que hace testing en el metodo GetChange por id cuando no es valido
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetChange_ReturnsBadRequest_WithInvalidId()
        {
            // Arrange
            int id = 0;
            var expectedChangeDto = new ChangeDTO(); // Assuming you have a proper ChangeDTO object

            _mockRepositoryBll.Setup(repo => repo.GetChange(id))
                .ReturnsAsync(new ServiceResponse
                {
                    Body = new Body { Result = expectedChangeDto },
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
            var result = await _changeController.GetChange(id);

            // Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
            
        }
        /// <summary>
        /// Metodo que hace testing en el metodo PostChange cuando la validación no es correcta
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostChange_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            ChangeDTO change = new ChangeDTO(); // Proporciona los valores necesarios para ChangeDTO
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
            var result = await _changeController.PostChange(change);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
            
        }
        /// <summary>
        /// Metodo que hace testing en el metodo PostChange cuando la validación es correcta
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostChange_ReturnsOk_WhenChangeIsValid()
        {
            // Arrange
            ChangeDTO change = new ChangeDTO(); // Provide necessary values for ChangeDTO
            
            _mockRepositoryBll.Setup(repo => repo.ValidateAsync(change))
                .ReturnsAsync(true);

            _mockRepositoryBll.Setup(repo => repo.PostChange(change))
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
            var result = await _changeController.PostChange(change);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            
        }
        /// <summary>
        /// Metodo que hace testing en el metodo PutChange cuando la de las llaves no es correcta
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PutChange_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            ChangeDTO change = new ChangeDTO(); // Proporciona los valores necesarios para ChangeDTO
            var errorMessage = "Error en los datos enviados";

            _mockRepositoryBll.Setup(repo => repo.ValidateAsync(change))
                .ReturnsAsync(false);

            _mockRepositoryBll.Setup(repo => repo.PutChange(change))
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
            var result = await _changeController.PostChange(change);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);

        }
        /// <summary>
        /// Metodo que hace testing en el metodo PutChange cuando la validación de las llaves es correcta
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PutChange_ReturnsOk_WhenChangeIsValid()
        {
            // Arrange
            ChangeDTO change = new ChangeDTO(); // Provide necessary values for ChangeDTO
            

            _mockRepositoryBll.Setup(repo => repo.ValidateAsync(change))
                .ReturnsAsync(true);

            _mockRepositoryBll.Setup(repo => repo.PutChange(change))
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
            var result = await _changeController.PutChange(change);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            
        }

        [Fact]
        public async Task GetChangeS_ReturnsOk_WithValidChangeNumber()
        {
            // Arrange
            string changeNumber = "123"; // Proporciona un número de cambio válido
            var expectedResult = new ChangeObj(); // Proporciona el resultado esperado

            _mockRepositoryBll.Setup(repo => repo.GetChangeS(changeNumber))
               .ReturnsAsync(new ServiceResponse
               {
                   Body = new Body { Result = expectedResult },
                   MsgRsHdr = new MsgRsHdr
                   {
                       Error = new Error
                       {
                           Status = new List<DeploymentManualAPI.Response.Models.Status>
               { new DeploymentManualAPI.Response.Models.Status { StatusCode = (int)HttpStatusCode.OK } }
                       }
                   }
               });

            // Act
            var result = await _changeController.GetChangeS(changeNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(expectedResult, (okResult.Value as ServiceResponse)?.Body?.Result);
        }

        [Fact]
        public async Task GetFilteredChanges_ReturnsOk_WithValidFilter()
        {
            // Arrange
            FilterDTO filter = new FilterDTO(); // Proporciona un filtro válido
            var expectedResult = new FilterDTO(); // Proporciona el resultado esperado

            _mockRepositoryBll.Setup(repo => repo.GetFilteredChanges(filter))
                .ReturnsAsync(new ServiceResponse
                {
                    Body = new Body { Result = expectedResult },
                    MsgRsHdr = new MsgRsHdr
                    {
                        Error = new Error
                        {
                            Status = new List<DeploymentManualAPI.Response.Models.Status>
                            {
                        new DeploymentManualAPI.Response.Models.Status
                        {
                            StatusCode = (int)HttpStatusCode.OK
                        }
                            }
                        }
                    }
                });

            // Act
            var result = await _changeController.GetFilteredChanges(filter);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(expectedResult, (okResult.Value as ServiceResponse)?.Body?.Result);
        }

    }
}
