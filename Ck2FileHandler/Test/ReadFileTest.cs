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
    public class ReadFileTest
    {
        private SaveFile _file;

        public const string DATAFILES_DIR = @"D:\Users\pipo\Dropbox\IsoFiling\Development\ck2Trainer\Data\readonly\";
        public const string TEST_FILE =         DATAFILES_DIR + @"full.ck2";
        public const string SHORT_FILE =        DATAFILES_DIR + @"shortened.ck2";
        public const string VERYSHORT_FILE =    DATAFILES_DIR + @"veryshort.ck2";
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
            f.Parse();
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
            f.Parse();
        }


        [Test]
        public void VeryShortFileParse()
        {
            var file = new SaveFile(VERYSHORT_FILE);
            file.Parse();
            Assert.That(file.RootBlock.Children, Has.Count.GreaterThan(10));
            Assert.That(file.NbReadLines, Is.EqualTo(5338));
        }

        [Test]
        [Ignore("Takes too long, possible stack overflow")]
        public void BigFileParse()
        {
            var file = new SaveFile(TEST_FILE);
            file.Parse();
            Assert.That(file.RootBlock.Children, Has.Count.GreaterThan(10));
            Assert.That(file.NbReadLines, Is.EqualTo(3828711));
        }




        [Test]
        public void ReadFileIntoTextBlocks()
        {
            _file.Parse();
            Assert.That(_file.RootBlock.Children, Has.Count.GreaterThan(50));
        }

        [Test]
        public void ReadLinesCount()
        {
            _file.Parse();
            Assert.That(_file.NbReadLines, Is.EqualTo(58709));
        }




    }

    [TestFixture]
    public class FileContentsTest
    {
        private SaveFile _file;


        [TestFixtureSetUp]
        public void SetUp()
        {
            _file = new SaveFile(ReadFileTest.SHORT_FILE);
            _file.Parse();
        }





        [TestCase("player", 1)]
        [TestCase("flags", 1)]
        [TestCase("dynasties", 1)]
        [TestCase("character", 1)]
        [TestCase("relation", 1)]
        [TestCase("id", 1)]
        [TestCase("religion", 1)]
        [TestCase("provinces", 1)]
        [TestCase("title", 1)]
        [TestCase("nomad", 1)]
        [TestCase("diplomacy", 1)]
        [TestCase("combat", 1)]
        [TestCase("war", 1)]
        [TestCase("coalition", 1)]
        [TestCase("previous_war", 1)]
        [TestCase("disease", 1)]
        [TestCase("income_statistics", 1)]
        [TestCase("character_history", 1)]
        [TestCase("nomad_relation", 1)]
        [TestCase("character_action", 1)]
        public void FindAllMandatoryUniqueBlocks(string name, int expectedCount)
        {
            FindAllDescendants(name, expectedCount, true);
        }

        [TestCase("version", 1, false)]
        [TestCase("date", 1, false)]
        [TestCase("pagan_coa", 1, false)]
        [TestCase("player_realm", 1, false)]
        [TestCase("base_title", 1, false)]
        [TestCase("is_zeus_save", 1, false)]
        [TestCase("unit", 1, false)]
        [TestCase("sub_unit", 1, false)]
        [TestCase("start_date", 1, false)]
        [TestCase("next_outbreak_id", 1, false)]
        [TestCase("vc_data", 1, false)]
        public void FindAllMandatoryUniqueKV(string name, int expectedCount, bool expectedAsBlock = true)
        {
            FindAllDescendants(name, expectedCount, expectedAsBlock);
        }

        private void FindAllDescendants(string name, int expectedCount, bool expectedAsBlock)
        {
            var objects = _file.RootBlock.GetDescendants(name);

            Assert.That(objects, Has.Count.EqualTo(expectedCount));
            Assert.That(objects.First(), Is.InstanceOf(typeof(DataLine)));
            Assert.That((objects.First() as DataLine).IsBlock, Is.EqualTo(expectedAsBlock));
        }
    }

}
