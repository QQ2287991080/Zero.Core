using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Zero.Core.Common.Redis;
using Zero.Core.xUnitTest.Attributes;

namespace Zero.Core.xUnitTest.Redis
{
    [TestCaseOrderer("Zero.Core.xUnitTest.Extensions.DisplayOrderer", "Zero.Core.xUnitTest")]
    public class RedisTest
    {
        readonly ITestOutputHelper _output;
        public RedisTest(ITestOutputHelper helper)
        {
            _output = helper;
        }
        //[Fact, Order(1)]
        //public void Test_StringGet()
        //{
        //    var result = RedisHelper.StringGet("k1");
        //    _output.WriteLine("Test_StringGet" + result);
        //    Assert.True(true);
        //}
        //[ Fact, Order(1)]
        //public void TestStringSet()
        //{
        //    var result = RedisHelper.StringSet("k1", "Zero", TimeSpan.FromMinutes(2));
        //    Assert.True(true);
        //}
        //[Fact, Order(3)]
        //public void Test_StringSetObj()
        //{
        //    var result = RedisHelper.StringSet("k2", 10);
        //    Assert.True(true);
        //}

        //[Fact, Order(4)]
        //public void Test_StringGetObj()
        //{
        //    var result = RedisHelper.StringGet<int>("k2");
        //    _output.WriteLine("Test_StringGetObj" + result);
        //    Assert.True(true);
        //}

        //[Fact, Order(5)]
        //public void Test_SetList()
        //{
        //    var list = RedisList.Create();
        //    RedisHelper.SetList("k3", list);
        //    Assert.True(true);
        //}

        //[Fact, Order(6)]
        //public void Test_GetList()
        //{
        //    var list = RedisHelper.GetList<RedisList>("k3");
        //    foreach (var item in list)
        //    {
        //        _output.WriteLine("Test_GetList List  age:{0},name:{1}", item.Age, item.Name);
        //    }
        //    Assert.True(true);
        //}
        //[Fact, Order(7)]
        //public void Test_RemoveFirst()
        //{
        //    var result = RedisHelper.RemoveFirst<RedisList>("k3");
        //    _output.WriteLine("Test_RemoveFirst——Age:{0}，Name:{1}", result.Age, result.Name);
        //    Assert.True(true);
        //}

        //[Fact, Order(8)]
        //public void Test_RemoveLast()
        //{
        //    var result = RedisHelper.RemoveLast<RedisList>("k3");
        //    _output.WriteLine("Test_RemoveLast——Age:{0}，Name:{1}", result.Age, result.Name);
        //    Assert.True(true);
        //}

        //[Fact, Order(9)]
        //public void Test_ListRange()
        //{
        //    var result = RedisHelper.ListRange<RedisList>("k3");
        //    foreach (var item in result)
        //    {
        //        _output.WriteLine("Age:{0},Name:{1}", item.Age, item.Name);
        //    }
        //    Assert.True(true);
        //}

        //[Fact, Order(10)]
        //public void Test_RemoveAt()
        //{
        //    var result = RedisHelper.RemoveAt("k3", new RedisList()
        //    {
        //        Age = -1,
        //        Name = "Zero-1"
        //    });
        //    _output.WriteLine("Test_RemoveAt:" + result);
        //    Assert.True(true);
        //}

        //[Fact, Order(12)]
        //public void Test_AddFirst()
        //{
        //    var redislist = new RedisList()
        //    {
        //        Age = -1,
        //        Name = "Zero-1"
        //    };
        //    RedisHelper.AddFirst("k3", redislist);
        //    Assert.True(true);
        //}
        //[Fact, Order(13)]
        //public void Test_AddLast()
        //{
        //    var redislist = new RedisList()
        //    {
        //        Age = 100,
        //        Name = "Zero100"
        //    };
        //    RedisHelper.AddLast("k3", redislist);
        //    Assert.True(true);
        //}

        //[Fact, Order(14)]
        //public void Test_Contains()
        //{
        //    var redislist = new RedisList()
        //    {
        //        Age = 100,
        //        Name = "Zero100"
        //    };
        //    var isExists = RedisHelper.Contains("k3", redislist);
        //    Assert.True(isExists);
        //}

        //[Fact, Order(15)]
        //public void Test_Get()
        //{
        //    var result = RedisHelper.Get<RedisList>("k3", 1);
        //    _output.WriteLine("Test_Get——Age:{0}，Name:{1}", result.Age, result.Name);
        //    Assert.True(true);
        //}
        //[Fact, Order(16)]
        //public void Test_IndexOf()
        //{
        //    var redislist = new RedisList()
        //    {
        //        Age = 100,
        //        Name = "Zero100"
        //    };
        //    var result = RedisHelper.IndexOf("k3", redislist);
        //    _output.WriteLine("Test_IndexOf:" + result);//4
        //    Assert.True(true);
        //}
        //[Fact, Order(100)]
        //public void Test_Count()
        //{
        //    var count = RedisHelper.Count("k3");
        //    _output.WriteLine("Test_Count:" + count);
        //}
    }

    public class RedisList
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public static List<RedisList> Create()
        {
            List<RedisList> lists = new List<RedisList>()
          {
           new RedisList{ Age=1, Name="zero" },
           new RedisList{ Age=2, Name="zero2" },
           new RedisList{ Age=3, Name="zero3" },
           new RedisList{ Age=4, Name="zero4" },
           new RedisList{ Age=5, Name="zero5" }
          };
            return lists;
        }
    }
}
