using System.Collections.Generic;
using System.Linq;
using Ck2.Save.File;
using NUnit.Framework;

namespace Ck2.Save.Test
{
    [TestFixture]
    public class FileContentsTest
    {
        private SaveFile _file;


        [TestFixtureSetUp]
        public void SetUp()
        {
            _file = new SaveFile(ReadFileTest.SHORT_FILE);
            _file.Parse(CallerContext.Empty);
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
        public void FindAllMandatoryUniqueBlocks(string name, int expectedCount)
        {
            FindChildren(name, expectedCount, expectedCount, true);
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
        [TestCase("character_action", 1, false)]
        [TestCase("vc_data", 1, false)]
        public void FindAllMandatoryUniqueKv(string name, int expectedCount, bool expectedAsBlock)
        {
            FindChildren(name, expectedCount, expectedCount, expectedAsBlock);
        }


        [TestCase("dyn_title", 4, 5)]
        [TestCase("delayed_event", 10, 20)]
        [TestCase("active_focus", 6, 6)]
        [TestCase("active_ambition", 6, 6)]
        [TestCase("active_war", 13, 50)]
        public void FindSomeCommonKv(string name, int minCount, int maxCount)
        {
            FindChildren(name, minCount, maxCount, true);
        }


        [TestCase("dynasties", null, 10, 1000)]
        [TestCase("character", null, 10000, 20000)]
        [TestCase("relation", null, 10, 10000)]
        [TestCase("religion", null, 10, 1000)]
        [TestCase("provinces", null, 10, 3000)]
        [TestCase("title", null, 10, 10000)]
        [TestCase("nomad", null, 10, 1000)]
        [TestCase("combat", null, 1000, 10000)]
        [TestCase("nomad_relation", null, 3, 100)]
        public void FindSomeNestedBlocks(string uniqueParentName, string name, int minCount, int maxCount)
        {
            FindChildren(uniqueParentName, name, minCount, maxCount, true, true);
        }



        private void FindChildren(string uniqueParentName, string name, int minCount, int maxCount, bool expectedAsBlock, bool allowRecurse)
        {

            DataBlock startingBlock;

            if (uniqueParentName == null)
                startingBlock = _file.RootBlock;
            else
            {
                var found = _file.RootBlock.Children.Where(data => data.Name.Equals(uniqueParentName));
                //var found = _file.RootBlock.GetDescendants(uniqueParentName);
                var uniqueLine = found.Single();
                Assert.That(uniqueLine, Is.TypeOf<DataLine>());
                startingBlock = uniqueLine.AsBlock;
            }

            Assert.That(startingBlock , Is.TypeOf<DataBlock>());

            IDataElement[] objects = null;
            if (allowRecurse)
                objects = startingBlock.GetDescendants(name).ToArray();
            else
                objects = startingBlock.Children.Where(data => data.Name.Equals(name)).ToArray();

            Assert.That(objects.Length, Is.GreaterThanOrEqualTo(minCount));
            Assert.That(objects.Length, Is.LessThanOrEqualTo(maxCount));

            Assert.That(objects.First(), Is.InstanceOf(typeof(DataLine)));
            Assert.That(objects.First().IsBlock, Is.EqualTo(expectedAsBlock));

        }


        private void FindChildren(string name, int minCount, int maxCount, bool expectedAsBlock, bool recurse = false)
        {
            FindChildren(null, name, minCount, maxCount, expectedAsBlock, recurse);
        }
    }
}