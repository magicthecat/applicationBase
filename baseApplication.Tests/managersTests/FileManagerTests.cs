using Xunit;
using Moq;
using baseApplication;

namespace baseApplication.Tests
{
    public class FileManagerTests
    {
        [Fact]
        public void WriteFile_WhenCalled_ShouldUseFileService()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var dialogServiceMock = new Mock<IDialogService>();
            var fileManager = new FileManager(fileServiceMock.Object, dialogServiceMock.Object);

            // Act
            fileManager.WriteFile("testPath", "testContent");

            // Assert
            fileServiceMock.Verify(f => f.WriteFile("testPath", "testContent"), Times.Once);
        }

        [Fact]
        public void OpenFile_WhenDialogReturnsValidPath_ShouldReadFileAndSetProperties()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(f => f.ReadFile("testPath")).Returns("testContent");
            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(d => d.ShowOpenFileDialog()).Returns("testPath");
            var fileManager = new FileManager(fileServiceMock.Object, dialogServiceMock.Object);

            // Act
            var result = fileManager.OpenFile();

            // Assert
            Assert.Equal("testContent", result);
            Assert.True(fileManager.IsSaved);
            Assert.Equal("testPath", fileManager.CurrentFilePath);
        }

        [Fact]
        public void SaveAs_WhenDialogReturnsValidPath_ShouldWriteFile()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(d => d.ShowSaveFileDialog()).Returns("testSavePath");
            var fileManager = new FileManager(fileServiceMock.Object, dialogServiceMock.Object);

            // Act
            var result = fileManager.SaveAs("testSaveContent");

            // Assert
            fileServiceMock.Verify(f => f.WriteFile("testSavePath", "testSaveContent"), Times.Once);
            Assert.True(fileManager.IsSaved);
            Assert.True(result);
            Assert.Equal("testSavePath", fileManager.CurrentFilePath);
        }

    }
}
