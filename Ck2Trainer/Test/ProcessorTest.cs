using System;
using System.Security.Cryptography.X509Certificates;
using Ck2.Save;
using Ck2.Save.Test;
using Ck2.Trainer.Processors;
using NUnit.Framework;

namespace Ck2.Trainer.Test
{
    [TestFixture]
    public class ProcessorTest
    {
        private SaveFile _file;

        [TestFixtureSetUp]
        public void Setup()
        {
            _file = new SaveFile(ReadFileTest.SHORT_FILE);
            _file.Parse(CallerContext.Empty);
        }

        [Test]
        public void SmokeTest()
        {
            
        }

        [Test]
        public void IntegrateProcessor()
        {
            var activator = (ICk2Processor) Activator.CreateInstance(typeof(NoActionProcessor));
            var changeset = activator.ApplyToNode(_file.RootBlock);

            // Assert.That(changeset.Count, Is.EqualTo(0));
        }

        [Test]
        public void NoActionProcessor()
        {
            var activator = (ICk2Processor)Activator.CreateInstance(typeof(NoActionProcessor));
            var changeset = activator.ApplyToNode(_file.RootBlock);

            Assert.That(changeset.Count, Is.EqualTo(0));
        }

    }
}
