using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zero.Core.xUnitTest.Extensions;

namespace Zero.Core.xUnitTest
{
    [TestCaseOrderer(
    CustomTestCaseOrderer.TypeName,
    CustomTestCaseOrderer.AssembyName)]
    public class TestClassBase
    {
        
    }
}
