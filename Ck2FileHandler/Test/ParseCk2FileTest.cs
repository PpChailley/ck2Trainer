using System;
using System.Linq;
using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class ParseCk2FileTest
    {
        private Ck2SaveFile _file;

        [SetUp]
        public void SetUp()
        {
            _file = new Ck2SaveFile(ReadFileTest.SHORT_FILE);
            _file.Parse(CallerContext.Empty);
        }


        [Test]
        public void RootParent()
        {
            var nested = _file.RootBlock.Children.First();

            Assert.That(nested.RootParent, Is.EqualTo(_file.RootBlock));
        }

    }
}