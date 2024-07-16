using MechanicalSyncApp.Publishing.DeliverablePublisher;
using Moq;
using NUnit.Framework.Internal;
using ILogger = Serilog.ILogger;

namespace MechanicalSyncApp_Tests
{
    [TestFixture]
    public class NextDrawingRevisionCalculatorTests
    {
        private Mock<ILogger> mockLogger;
        private string tempDirectory;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger>();
            tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(tempDirectory))
            {
                Directory.Delete(tempDirectory, true);
            }
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenProjectPublishingDirectoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NextDrawingRevisionCalculator(null, mockLogger.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NextDrawingRevisionCalculator(tempDirectory, null));
        }

        [Test]
        public void GetNextRevision_ShouldThrowArgumentNullException_WhenDrawingFileNameWithoutExtensionIsNull()
        {
            var calculator = new NextDrawingRevisionCalculator(tempDirectory, mockLogger.Object);
            Assert.Throws<ArgumentNullException>(() => calculator.GetNextRevision(null));
        }

        [Test]
        public void GetNextRevision_ShouldReturnA_WhenNoFilesExist()
        {
            var calculator = new NextDrawingRevisionCalculator(tempDirectory, mockLogger.Object);
            var result = calculator.GetNextRevision("Drawing1");

            Assert.That(result, Is.EqualTo("A"));
        }

        [Test]
        public void GetNextRevision_ShouldReturnB_WhenOneFileExists()
        {
            var pdfDirectory = Path.Combine(tempDirectory, "PDF");
            Directory.CreateDirectory(pdfDirectory);
            File.Create(Path.Combine(pdfDirectory, "Drawing1.pdf")).Dispose();

            var calculator = new NextDrawingRevisionCalculator(tempDirectory, mockLogger.Object);
            var result = calculator.GetNextRevision("Drawing1");

            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        public void GetNextRevision_ShouldReturnAB_WhenMoreThan26FilesExist()
        {
            var pdfDirectory = Path.Combine(tempDirectory, "PDF");
            Directory.CreateDirectory(pdfDirectory);

            for (int i = 0; i < 26; i++)
            {
                File.Create(Path.Combine(pdfDirectory, $"Drawing1-{(char)('A' + i)}.pdf")).Dispose();
            }

            var calculator = new NextDrawingRevisionCalculator(tempDirectory, mockLogger.Object);
            var result = calculator.GetNextRevision("Drawing1");

            Assert.That(result, Is.EqualTo("AB"));
        }
    }
}