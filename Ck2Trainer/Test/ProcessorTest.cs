using System;
using System.Collections.Generic;
using System.Linq;
using Ck2.Save;
using Ck2.Save.File;
using Ck2.Save.Model;
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
            _file = new SaveFile(ReadFileTest.TEST_FILE);
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
            var changeset = activator.ApplyToNode(_file.Map);

            // Assert.That(changeset.Count, Is.EqualTo(0));
        }

        [Test]
        public void NoActionProcessor()
        {
            var activator = (ICk2Processor)Activator.CreateInstance(typeof(NoActionProcessor));
            var changeset = activator.ApplyToNode(_file.Map);

            Assert.That(changeset.Count, Is.EqualTo(0));
        }


        [Test]
        public void AllMoraleToZeroProcessor()
        {
            var activator = (ICk2Processor)Activator.CreateInstance(typeof(AllMoraleToZeroProcessor));
            var changeset = activator.ApplyToNode(_file.Map);

            Assert.That(changeset.Count, Is.EqualTo(_file.Map.AllArmies.Count()));
            foreach (var unit in _file.Map.AllArmies)
            {
                Assert.That(unit.Morale, Is.EqualTo(0));
            }
        }

        [Test]
        public void PlayerMoraleToFullProcessor()
        {
            var activator = (ICk2Processor)Activator.CreateInstance(typeof(PlayerMoraleToFullProcessor));
            var changeset = activator.ApplyToNode(_file.Map);

            Assert.That(changeset.Count, Is.EqualTo(_file.Map.AllArmies.Count()));
            foreach (var unit in _file.Map.AllArmies.Where(u => u.Owner.Id == _file.Map.PlayerId))
            {
                Assert.That(unit.Morale, Is.EqualTo(0));
            }
        }

    }
}
