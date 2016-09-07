using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2.Save;
using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class FileTest
    {
        private SaveFile _file;

        public const string DATAFILES_DIR = @"D:\Users\pipo\Dropbox\IsoFiling\Development\ck2Trainer\Data\readonly\";
        public const string TEST_FILE =     DATAFILES_DIR + @"full.ck2";
        public const string SHORT_FILE =    DATAFILES_DIR + @"shortened.ck2";
        public const string EMPTY_FILE =    DATAFILES_DIR + @"empty.ck2";
        public const string INVALID_FILE =  DATAFILES_DIR + @"invalid.ck2";


        [SetUp]
        public void SetUp()
        {
            _file = new SaveFile(SHORT_FILE);

        }

        [Test]
        public void SmokeTest()
        {
            var f = new SaveFile(TEST_FILE);
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void EmptyFile()
        {
            var f = new SaveFile(EMPTY_FILE);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void EmptyFileName()
        {
            var f = new SaveFile(string.Empty);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void InvalidFile()
        {
            var f = new SaveFile(INVALID_FILE);
        }

        [Test]
        public void ReadFileIntoTextBlocks()
        {
            var textBlocks = _file.ReadTextBlocks();
            Assert.That(textBlocks.Children, Has.Count.GreaterThan(100));
        }

        [Test]
        public void ReadLinesCount()
        {
            var textBlocks = _file.ReadTextBlocks();
            Assert.That(SaveFile.NbReadLines, Is.EqualTo(130612));
        }


        [Test]
        public void ReadPlayerId()
        {
            var playerId = _file.PlayerId;
            Assert.That(playerId as object, Is.Not.EqualTo(0));
        }

        [Test]
        public void GetPlayerObject()
        {
            var player = _file.Player;
            var playerId = _file.PlayerId;
            Assert.That(player as object, Is.Not.EqualTo(0));
            Assert.That(player as object, Is.EqualTo(playerId));
        }



    }
}
