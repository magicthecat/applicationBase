using System;
using Xunit;
using Moq;

namespace baseApplication.Tests
{
    public class FileManagerTests
    {
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<IDialogService> _dialogServiceMock;
        private readonly FileManager _fileManager;

        public FileManagerTests()
        {
            _fileServiceMock = new Mock<IFileService>();
            _dialogServiceMock = new Mock<IDialogService>();
            _fileManager = new FileManager(_fileServiceMock.Object, _dialogServiceMock.Object);
        }

        [Fact]
        public void WhenWritingFile_MarkAsSavedIsCalled()
        {
            // Arrange
            string filePath = "test.txt";
            string content = "test content";

            // Act
            _fileManager.WriteFile(filePath, content);

            // Assert
            Assert.True(_fileManager.IsSaved);
        }

        [Fact]
        public void WhenOpeningValidFile_FileIsReadAndPathIsSet()
        {
            // Arrange
            string filePath = "test.txt";
            _dialogServiceMock.Setup(ds => ds.ShowOpenFileDialog()).Returns(filePath);
            _fileServiceMock.Setup(fs => fs.ReadFile(filePath)).Returns("test content");

            // Act
            string content = _fileManager.OpenFile();

            // Assert
            Assert.Equal("test content", content);
            Assert.True(_fileManager.IsSaved);
            Assert.Equal(filePath, _fileManager.CurrentFilePath);
        }

        [Fact]
        public void WhenSavingFile_PathIsSetAndContentIsWritten()
        {
            // Arrange
            string filePath = "test.txt";
            string content = "test content";
            _dialogServiceMock.Setup(ds => ds.ShowSaveFileDialog()).Returns(filePath);

            // Act
            bool result = _fileManager.SaveAs(content);

            // Assert
            _fileServiceMock.Verify(fs => fs.WriteFile(filePath, content));
            Assert.True(_fileManager.IsSaved);
            Assert.True(result);
        }

        [Fact]
        public void OnFileNew_InvokesFileNewEvent()
        {
            // Arrange
            bool wasCalled = false;
            _fileManager.FileNew += (sender, e) => wasCalled = true;

            // Act
            _fileManager.OnFileNew();

            // Assert
            Assert.True(wasCalled);
        }

    }
}