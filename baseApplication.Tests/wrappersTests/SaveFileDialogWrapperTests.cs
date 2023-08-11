using Moq;
using Moq.Protected;
using Xunit;
using System.Windows.Forms;
using baseApplication;

public class SaveFileDialogWrapperTests
{
    [Fact]
    public void GetSavePath_ReturnsCorrectPath()
    {
        // Arrange
        var mockFileDialog = new Mock<ISaveFileDialog>();
        mockFileDialog.Setup(m => m.ShowDialog()).Returns(DialogResult.OK);
        mockFileDialog.Setup(m => m.FileName).Returns("testpath");

        var mockFactory = new Mock<ISaveFileDialogFactory>();
        mockFactory.Setup(m => m.Create()).Returns(mockFileDialog.Object);

        var wrapper = new SaveFileDialogWrapper(mockFactory.Object);

        // Act
        var result = wrapper.GetSavePath("filter", "title");

        // Assert
        Assert.Equal("testpath", result);
    }
}
