using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ck2.Save.Model;
using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class MappingTest
    {
        private Ck2SaveFile _file;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var f = ReadFileTest.TEST_FILE;
            int expectedFileSize = Ck2SaveFile.EstimateNbLines(new FileInfo(f));

            _file = new Ck2SaveFile(f);
            _file.Parse(CallerContext.Empty);

            Assert.That(_file.FullyParsed, Is.True);
            Assert.That(_file.Map, Is.Not.Null);
            Assert.That(_file.NbReadLines, Is.EqualTo(expectedFileSize).Within(expectedFileSize * 0.1));
        }

        [Test]
        public void SmokeTest()
        {
            
        }

        [Test]
        public void Property()
        {
            var prop = _file.RootBlock.Property("is_zeus_save");
            Assert.That(prop.Value, Is.EqualTo("yes"));
        }

        [Test]
        public void Properties()
        {
            var prop = _file.RootBlock.Properties("dyn_title").ToArray();
            Assert.That(prop, Has.Count.EqualTo(116));
        }

        [Test]
        public void Value()
        {
            var val = _file.RootBlock.Value("is_zeus_save");
            Assert.That(val, Is.EqualTo("yes"));
        }

        [Test]
        public void Values()
        {
            IEnumerable<string> val = _file.RootBlock.Values("is_zeus_save").ToArray();
            Assert.That(val, Has.Count.EqualTo(1));
        }

        [Test]
        public void Block()
        {
            var b = _file.RootBlock.Block("character");
            Assert.That(b.Children, Has.Count.GreaterThanOrEqualTo(100));
        }

        [Test]
        public void Blocks()
        {
            IEnumerable<DataBlock> blocks = _file.RootBlock.Blocks("active_war").ToArray();
            Assert.That(blocks, Has.Count.EqualTo(50));
        }





        [Test]
        public void Player()
        {
            var player = _file.Map.Player;

            Assert.That(player.BirthName.Value.ToUnindentedString(), Is.EqualTo("Gaya"));
        }


        [Test]
        public void Dynasty()
        {
            var player = _file.Map.Player;
            var dynasty = player.Dynasty;

            Assert.That(dynasty.Name.Value.ToUnindentedString(), Is.EqualTo("Whiteshirt"));
        }

        [Test]
        public void Date()
        {
            var date = _file.Map.Date;
            Assert.That(date.Value.ToUnindentedString(), Is.EqualTo("1042.1.1"));
        }

        [Test]
        public void Armies()
        {
            var armies = _file.Map.AllArmies.ToArray();

            Assert.That(armies.Length, Is.EqualTo(100));
            Assert.That(armies.Count( a => a.Id == 100747), Is.EqualTo(1));
        }

        [Test]
        public void SubUnits()
        {
            var subUnits = _file.Map.AllArmies.Single(a => a.Id == 96222).SubUnits;

            Assert.That(subUnits, Has.Count.EqualTo(12));
        }


        [Test]
        public void SubUnitOwner()
        {
            var armies = _file.Map.AllArmies;
            var subUnits = new List<SubUnit>();
            foreach (var army in armies)
            {
                subUnits.AddRange(army.SubUnits);
            }

            IEnumerable<SubUnit> ownedByPlayer = subUnits.Where(a => a.Owner.Equals(_file.Map.Player));

            Assert.That(ownedByPlayer, Has.Count.EqualTo(subUnits.Count(a => a.Owner.Id == _file.Map.PlayerId)));

            Assert.That(ownedByPlayer, Has.Count.EqualTo(1));
            Assert.That(subUnits.Count(a => a.Owner.Equals(_file.Map.Player)), Is.EqualTo(1));
        }

        [Test]
        public void Characters()
        {
            var characters = _file.Map.Characters.ToArray();

            Assert.That(characters.Count(), Is.EqualTo(100));
            Assert.That(characters.Count(c => c.Id == 605459), Is.EqualTo(1));
            Assert.That(characters.Count(c => c.Equals(_file.Map.Player)), Is.EqualTo(1));
        }



    }

}