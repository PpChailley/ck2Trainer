﻿using System;
using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class ParseCk2File
    {
        private Ck2SaveFile _file;

        [SetUp]
        public void SetUp()
        {
            _file = new Ck2SaveFile(ReadFileTest.SHORT_FILE);
            _file.Parse();
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadInfoBeforeParsing()
        {
            var f = new Ck2SaveFile(ReadFileTest.SHORT_FILE);
            var playerId = f.PlayerId;
        }


        [Test]
        public void ReadPlayerId()
        {
            var playerId = _file.PlayerId;
            Assert.That(playerId, Is.Not.EqualTo(0));
        }



    }
}