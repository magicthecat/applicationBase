using Xunit;
using Moq;
using baseApplication;

namespace baseApplication.Tests
{
    public class TitleManagerTests
    {
        [Fact]
        public void UpdateTitle_WithNoCurrentFileName_ShouldSetUntitled()
        {
            // Arrange
            var titleMock = new Mock<ITitle>();
            var fileManagerMock = new Mock<IFileManager>();
            fileManagerMock.Setup(f => f.CurrentFileName).Returns((string)null);
            var titleManager = new TitleManager(titleMock.Object, fileManagerMock.Object);

            // Act
            titleManager.UpdateTitle();

            // Assert
            titleMock.VerifySet(t => t.Title = "Untitled*");
        }

        [Fact]
        public void UpdateTitle_WithCurrentFileNameAndIsSaved_ShouldSetFileName()
        {
            // Arrange
            var titleMock = new Mock<ITitle>();
            var fileManagerMock = new Mock<IFileManager>();
            fileManagerMock.Setup(f => f.CurrentFileName).Returns("example.txt");
            fileManagerMock.Setup(f => f.IsSaved).Returns(true);
            var titleManager = new TitleManager(titleMock.Object, fileManagerMock.Object);

            // Act
            titleManager.UpdateTitle();

            // Assert
            titleMock.VerifySet(t => t.Title = "example.txt");
        }

        [Fact]
        public void UpdateTitle_WithCurrentFileNameAndIsNotSaved_ShouldSetFileNameWithAsterisk()
        {
            // Arrange
            var titleMock = new Mock<ITitle>();
            var fileManagerMock = new Mock<IFileManager>();
            fileManagerMock.Setup(f => f.CurrentFileName).Returns("example.txt");
            fileManagerMock.Setup(f => f.IsSaved).Returns(false);
            var titleManager = new TitleManager(titleMock.Object, fileManagerMock.Object);

            // Act
            titleManager.UpdateTitle();

            // Assert
            titleMock.VerifySet(t => t.Title = "example.txt*");
        }
    }
}