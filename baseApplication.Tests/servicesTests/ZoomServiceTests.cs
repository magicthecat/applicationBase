using Moq;
using Xunit;
using baseApplication; 

public class ZoomServiceTests
{
    [Fact]
    public void ZoomIn_CallsZoomInOnZoomableComponent()
    {
        // Arrange
        var mockZoomableComponent = new Mock<IZoomable>();
        var zoomService = new ZoomService();

        // Act
        zoomService.ZoomIn(mockZoomableComponent.Object);

        // Assert
        mockZoomableComponent.Verify(z => z.ZoomIn(), Times.Once);
    }

    [Fact]
    public void ZoomOut_CallsZoomOutOnZoomableComponent()
    {
        // Arrange
        var mockZoomableComponent = new Mock<IZoomable>();
        var zoomService = new ZoomService();

        // Act
        zoomService.ZoomOut(mockZoomableComponent.Object);

        // Assert
        mockZoomableComponent.Verify(z => z.ZoomOut(), Times.Once);
    }
}
