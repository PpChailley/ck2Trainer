using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class ParsingSemanticsTest
    {
        private SaveFile _file;

        [SetUp]
        public void SetUp()
        {
            _file = new SaveFile(ParsingFileTest.SHORT_FILE);
            _file.Parse();
        }


        [Test]
        public void ReadPlayerId()
        {
            var playerId = _file.PlayerId;
            Assert.That(playerId, Is.Not.EqualTo(0));
        }




    }
}