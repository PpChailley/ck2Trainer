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
        public const string TEST_FILE = @"D:\Users\pipo\Dropbox\IsoFiling\Development\ck2Trainer\Data\readonly\olderautosave.ck2";
        public const string EMPTY_FILE = @"D:\Users\pipo\Dropbox\IsoFiling\Development\ck2Trainer\Data\readonly\empty.ck2";
        public const string INVALID_FILE = @"D:\Users\pipo\Dropbox\IsoFiling\Development\ck2Trainer\Data\readonly\invalid.ck2";


        [SetUp]
        public void SetUp()
        {
            // Nothing yet
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


    }
}
