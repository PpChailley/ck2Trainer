using System;
using System.IO;
using System.Linq;
using ck2.Mapping.Save.Extensions;
using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class WriteFileTest
    {
        private SaveFile _file;

        public const string TEMP_WRITE_DIR = ReadFileTest.DATA_DIR +    @"temp\";
        public const string TEMP_WRITE_FILENAME = TEMP_WRITE_DIR + @"imitation.ck2";
        private const int COMPARE_BUFFER_SIZE = sizeof(long);

        [TestFixtureSetUp]
        public void SetUp()
        {
            new DirectoryInfo(TEMP_WRITE_DIR).Create();

            _file = new SaveFile(ReadFileTest.SHORT_FILE);
            _file.Parse();
        }

        [Test]
        public void ImitationWrite()
        {
            _file.WriteTo(TEMP_WRITE_FILENAME);
            
            //Assert.That(FilesAreEqual(ReadFileTest.SHORT_FILE, TEMP_WRITE_FILENAME), Is.EqualTo(true));

            var src = new FileInfo(ReadFileTest.SHORT_FILE).OpenText().ReadToEnd();
            var copy = new FileInfo(TEMP_WRITE_FILENAME).OpenText().ReadToEnd();

            Assert.That(src.EqualsWithIgnores(copy, new[] {' ', '\t', '\r', '\n'}), Is.EqualTo(0));

            /*

            var srcTrim = src.RemoveAll(new[] { ' ', '\t', '\r', '\n'});
            var copyTrim = copy.RemoveAll(new[] { ' ', '\t', '\r', '\n' });

            Assert.That(srcTrim.Equals(copyTrim, StringComparison.OrdinalIgnoreCase));

            // */
        }

        [Test]
        public void ChangeThenWrite()
        {
            var line =  _file.RootBlock.Children
                .First(data => data is DataLine && ((DataLine) data).AsKeyVal == null);

            ((DataLine) line).AsText = "CHANGED BY TEST: ChangeThenWrite";

            _file.WriteTo(TEMP_WRITE_FILENAME);

            var src = new FileInfo(ReadFileTest.SHORT_FILE).OpenText().ReadToEnd();
            var copy = new FileInfo(TEMP_WRITE_FILENAME).OpenText().ReadToEnd();

            Assert.That(src.EqualsWithIgnores(copy, new[] { ' ', '\t', '\r', '\n' }), Is.EqualTo(1));


        }


        [Test]
        public void AppendThenWrite()
        {
            var extraLine = new DataLine(_file.RootBlock, 1) {AsText = $"AddedByTest - AppendThenWrite" };
            _file.RootBlock.Children.Add(extraLine);

            _file.WriteTo(TEMP_WRITE_FILENAME);

            var src = new FileInfo(ReadFileTest.SHORT_FILE).OpenText().ReadToEnd();
            var copy = new FileInfo(TEMP_WRITE_FILENAME).OpenText().ReadToEnd();

            Assert.That(src.EqualsWithIgnores(copy, new[] { ' ', '\t', '\r', '\n' }), Is.GreaterThan(58700));

        }




        private static bool FilesAreEqual(string filenameA, string filenameB)
        {
            return FilesAreEqual(new FileInfo(filenameA), new FileInfo(filenameB));
        }


        public static bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            int iterations = (int)Math.Ceiling((double)first.Length / COMPARE_BUFFER_SIZE);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[COMPARE_BUFFER_SIZE];
                byte[] two = new byte[COMPARE_BUFFER_SIZE];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, COMPARE_BUFFER_SIZE);
                    fs2.Read(two, 0, COMPARE_BUFFER_SIZE);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }

    }
}