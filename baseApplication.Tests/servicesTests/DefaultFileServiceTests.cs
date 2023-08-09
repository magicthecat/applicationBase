using System;
using System.IO;
using Xunit;    
    
    public class DefaultFileServiceTests
    {
        private readonly DefaultFileService _fileService;
        private readonly string _testFilePath;

        public DefaultFileServiceTests()
        {
            _fileService = new DefaultFileService();
            _testFilePath = Path.Combine(Path.GetTempPath(), "testFile.txt");
        }

        [Fact]
        public void CanWriteAndReadBackFile()
        {
            string testContent = "Hello World";

            _fileService.WriteFile(_testFilePath, testContent);

            var readContent = _fileService.ReadFile(_testFilePath);

            Assert.Equal(testContent, readContent);
        }

        public void Dispose()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }
    }