using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zero.Core.xUnitTest.Attributes;
using Zero.Core.xUnitTest.Extensions;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer(
    CustomTestCollectionOrderer.TypeName,
    CustomTestCollectionOrderer.AssembyName)]
namespace Zero.Core.xUnitTest
{
    [TestCaseOrderer("Zero.Core.xUnitTest.Extensions.DisplayOrderer", "Zero.Core.xUnitTest")]
    public class OrderTest: TestClassBase
    {
        [Fact, Order(1)]
        public void xx()
        {
            Assert.True(true);
        }

        [Fact, Order(3)]
        public void xx2()
        {
            Assert.True(true);
        }
        [Fact, Order(2)]
        public void xx3()
        {
            Assert.True(true);
        }
        [Fact, Order(-2)]
        public void xx4()
        {
            Assert.True(true);
        }
    }
}
