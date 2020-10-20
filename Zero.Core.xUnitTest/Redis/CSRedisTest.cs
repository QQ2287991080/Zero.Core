using CSRedis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Zero.Core.xUnitTest.Attributes;
using Zero.Core.xUnitTest.Extension;

namespace Zero.Core.xUnitTest.Redis
{
    public class CSRedisTest
    {

    }
    [TestCaseOrderer("Zero.Core.xUnitTest.Extensions.DisplayOrderer", "Zero.Core.xUnitTest.Redis")]
    public class CsRedisHelper
    {
        readonly ITestOutputHelper _ouput;
        public CsRedisHelper(ITestOutputHelper test)
        {
            _ouput = test;
        }
        //static IRedisClient _client;
        //public CsRedisHelper()
        //{
        //    _client = new RedisClient("127.0.0.1");
        //}
        private static IRedisClient Db()
        {
            return new RedisClient("127.0.0.1");
        }
        
        //[Fact, Order(1)]
        //public void StringSet()
        //{
        //    var list = RedisList.Create();
        //    var obj = SerializeList(list);
        //    foreach (var item in obj)
        //    {
        //        Db().RPush("ki", 0, item);
        //    }
        //}

        //private IEnumerable<string> SerializeList<T>(List<T> list)
        //{
        //    int count = list.Count;
        //    List<string> obj = new List<string>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        var model = JsonConvert.SerializeObject(list[i]);
        //        obj.Add(model);
        //    }
        //    return obj;
        //}
        //[Fact]
        //public void DeleteKey()
        //{
        //    Db().Del("ki");
        //}

        //[Fact]
        //public void GetLength()
        //{
        //    var length = Db().LLen("ki");
        //    _ouput.WriteLine("{0}",length);
        //}
        //[Fact]
        //public void GetList()
        //{
        //    var values = Db().LRange("ki", 0, -1);
        //    var redises = values.Defined<RedisList>();
        //    foreach (var item in redises)
        //    {
        //        _ouput.WriteLine("Age:{0},Name:{1}", item.Age, item.Name);
        //    }
        //}
    }
    public static class Extension
    {
        public static IEnumerable<T> Defined<T>(this IEnumerable<string> source)
        {
            foreach (var item in source)
            {
                yield return JsonConvert.DeserializeObject<T>(item);
            }
        }

    }
}
