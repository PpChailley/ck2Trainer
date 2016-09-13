using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ck2.Save;
using Ck2.Save.File;
using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class ReadFileTest
    {
        private SaveFile _file;

        public const string DATA_DIR = @"D:\Users\pipo\Dropbox\IsoFiling\Development\ck2Trainer\Data\";
        public const string DATAFILES_DIR =     DATA_DIR + @"readonly\";
        public const string TEST_FILE =         DATAFILES_DIR + @"full.ck2";
        public const string SHORT_FILE =        DATAFILES_DIR + @"shortened.ck2";
        public const string VERYSHORT_FILE =    DATAFILES_DIR + @"veryshort.ck2";
        public const string ARMIES_FILE =       DATAFILES_DIR + @"armies.ck2";
        public const string EMPTY_FILE =        DATAFILES_DIR + @"empty.ck2";
        public const string INVALID_FILE =      DATAFILES_DIR + @"invalid.ck2";


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

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void EmptyFile()
        {
            var f = new SaveFile(EMPTY_FILE);
            f.Parse(CallerContext.Empty);
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
            f.Parse(CallerContext.Empty);
        }


        [Test]
        public void VeryShortFileParse()
        {
            var file = new SaveFile(VERYSHORT_FILE);
            file.Parse(CallerContext.Empty);
            Assert.That(file.RootBlock.Children, Has.Count.GreaterThan(10));
            Assert.That(file.NbReadLines, Is.EqualTo(5338));
        }

        [Test]
        [Ignore("Takes too long, possible stack overflow")]
        public void BigFileParse()
        {
            var file = new SaveFile(TEST_FILE);
            file.Parse(CallerContext.Empty);
            Assert.That(file.RootBlock.Children, Has.Count.GreaterThan(10));
            Assert.That(file.NbReadLines, Is.EqualTo(3828711));
        }




        [Test]
        public void ReadFileIntoTextBlocks()
        {
            _file.Parse(CallerContext.Empty);
            Assert.That(_file.RootBlock.Children, Has.Count.GreaterThan(50));
        }

        [Test]
        public void ReadLinesCount()
        {
            _file.Parse(CallerContext.Empty);
            Assert.That(_file.NbReadLines, Is.EqualTo(59179));
        }




    }
}
