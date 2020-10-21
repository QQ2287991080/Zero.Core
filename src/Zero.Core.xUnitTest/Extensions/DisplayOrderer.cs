using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using Zero.Core.xUnitTest.Attributes;

namespace Zero.Core.xUnitTest.Extensions
{
    public class DisplayOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            string assemblyName = typeof(OrderAttribute).AssemblyQualifiedName!;
            List<TestCaseOrder<TTestCase>> tests = new List<TestCaseOrder<TTestCase>>();
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
            foreach (TTestCase testCase in testCases)
            {
                int order = testCase.TestMethod.Method
                    .GetCustomAttributes(assemblyName)
                    .FirstOrDefault()
                    ?.GetNamedArgument<int>(nameof(OrderAttribute.Order)) ?? 0;
                 GetOrCreate(sortedMethods, order).Add(testCase);
            }
            foreach (TTestCase testCase in
                sortedMethods.Keys.OrderBy(ob => ob).SelectMany(
                    priority => sortedMethods[priority].OrderByDescending(
                        testCase => testCase.TestMethod.Method.Name)))
            {
                yield return testCase;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(
           IDictionary<TKey, TValue> dictionary, TKey key)
           where TKey : struct
           where TValue : new() =>
           dictionary.TryGetValue(key, out TValue result)
               ? result
               : (dictionary[key] = new TValue());
    }

    //public class DisplayOrderer : ITestCaseOrderer
    //{
    //    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    //    {
    //        string assemblyName = typeof(OrderAttribute).AssemblyQualifiedName!;

    //        //var result = testCases.ToList();

    //        //result.Sort((x, y) =>
    //        //{
    //        //    var xOrder = x.TestMethod.Method
    //        //    .GetCustomAttributes(assemblyName)?
    //        //    .FirstOrDefault()?
    //        //    .GetNamedArgument<int>(nameof(TestOrderDispalyAttribute.Order)) ?? 0;
    //        //    var yOrder = y.TestMethod.Method.GetCustomAttributes(assemblyName)?
    //        //    .FirstOrDefault()?
    //        //    .GetNamedArgument<int>(nameof(TestOrderDispalyAttribute.Order)) ?? 0;

    //        //    //按照Order标签上的Sort属性，从小到大的顺序执行
    //        //    return xOrder - yOrder;
    //        //});
    //        //return result;

    //        List<TestCaseOrder<TTestCase>> tests = new List<TestCaseOrder<TTestCase>>();
    //        var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
    //        foreach (TTestCase testCase in testCases)
    //        {
    //            int order = testCase.TestMethod.Method
    //                .GetCustomAttributes(assemblyName)
    //                .FirstOrDefault()
    //                ?.GetNamedArgument<int>(nameof(OrderAttribute.Order)) ?? 0;
    //            GetOrCreate(sortedMethods, order).Add(testCase);
    //            //tests.Add(new TestCaseOrder<TTestCase>
    //            //{
    //            //    TestCase = testCase,
    //            //    Order = order
    //            //});
    //        }
    //        //return tests.OrderBy(ob => ob.Order).Select(s => s.TestCase);

    //        foreach (TTestCase testCase in
    //            sortedMethods.Keys.OrderBy(ob => ob).SelectMany(
    //                priority => sortedMethods[priority].OrderByDescending(
    //                    testCase => testCase.TestMethod.Method.Name)))
    //        {
    //            yield return testCase;
    //        }
    //    }

    //    private static TValue GetOrCreate<TKey, TValue>(
    //       IDictionary<TKey, TValue> dictionary, TKey key)
    //       where TKey : struct
    //       where TValue : new() =>
    //       dictionary.TryGetValue(key, out TValue result)
    //           ? result
    //           : (dictionary[key] = new TValue());
    //}

    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach (TTestCase testCase in testCases)
            {
                int priority = 0;

                foreach (IAttributeInfo attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(OrderAttribute).AssemblyQualifiedName)))
                    priority = attr.GetNamedArgument<int>("Order");

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
            {
                list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                foreach (TTestCase testCase in list)
                    yield return testCase;
            }
        }

        static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue result;

            if (dictionary.TryGetValue(key, out result)) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }

    public class TestCaseOrder<TTestCase>
    {
        public int Order { get; set; }
        public TTestCase TestCase { get; set; }
    }
}
