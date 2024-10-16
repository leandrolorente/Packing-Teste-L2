using Application.Commands.ProcessarPedido;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Responses;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Application.Commands
{
    public class ProcessarPedidoHandlerTests
    {
        [Fact]
        public async Task Handle_WhenCalled_ReturnsCorrectResponse()
        {
            // Arrange
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(x => x.GetPedidosAsync())
                .ReturnsAsync(new List<Pedido>());

            var pedidoPackingServiceMock = new Mock<IPedidoPackingService>();
            pedidoPackingServiceMock.Setup(x => x.ProcessarPedidosAsync(It.IsAny<List<Pedido>>()))
                .ReturnsAsync(new ProcessarPedidoResponse());

            var handler = new ProcessarPedidoHandler(pedidoPackingServiceMock.Object);

            var command = new ProcessarPedidoCommand(new List<Pedido>());

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotNull(result);
        }
    }
}
