using System.IO;
using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class MappingTest
    {
        private Ck2SaveFile _file;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var f = ReadFileTest.TEST_FILE;
            int expectedFileSize = Ck2SaveFile.EstimateNbLines(new FileInfo(f));

            _file = new Ck2SaveFile(f);
            _file.Parse(CallerContext.Empty);

            Assert.That(_file.FullyParsed, Is.True);
            Assert.That(_file.Map, Is.Not.Null);
            Assert.That(_file.NbReadLines, Is.EqualTo(expectedFileSize).Within(expectedFileSize * 0.1));
        }

        [Test]
        public void SmokeTest()
        {
            
        }

        [Test]
        public void Player()
        {
            var player = _file.Map.Player;

            Assert.That(player.BirthName.Value.ToUnindentedString(), Is.EqualTo("Gaya"));
        }


        [Test]
        public void Dynasty()
        {
            var player = _file.Map.Player;
            var dynasty = player.Dynasty;

            Assert.That(dynasty.Name.Value.ToUnindentedString(), Is.EqualTo("Whiteshirt"));
        }

        [Test]
        public void Date()
        {
            var date = _file.Map.Date;
            Assert.That(date.Value.ToUnindentedString(), Is.EqualTo("1042.1.1"));
        }

    }
}