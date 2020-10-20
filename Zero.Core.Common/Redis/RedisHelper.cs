using Microsoft.VisualBasic;
using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.Redis
{
    public static class RedisHelper
    {
        public static readonly ConnectionMultiplexer _connectin;
        /// <summary>
        /// 数据库id
        /// </summary>
        public static int DbNum { get; set; }
        /// <summary>
        /// 连接方式
        /// </summary>
        public static string ConnectionString { get; set; }

        static RedisHelper()
        {
            DbNum = -1;
            ConnectionString = null;
            _connectin = string.IsNullOrEmpty(ConnectionString) ?
                   RedisConnectionHelper.Instance :
                   RedisConnectionHelper.GetConnectionMultiplexer(ConnectionString);
        }
        /// <summary>
        /// 获取db
        /// </summary>
        public static IDatabase Db
        {
            get
            {
                return _connectin.GetDatabase(DbNum);
            }
        }
        /// <summary>
        /// 执行委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T Do<T>(Func<IDatabase, T> func)
        {
            return func(Db);
        }
        #region string
        /// <summary>
        /// 保存string
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        public static bool StringSet(string key, string value, TimeSpan? expiry = null)
        {
            return Do(d => d.StringSet(key, value,expiry));
        }

        /// <summary>
        /// 异步保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await Do(d => d.StringSetAsync(key, value, expiry));
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string StringGet(string key)
        {
            return Do(d => d.StringGet(key));
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<string> StringGetAsync(string key)
        {
            return await Do(d => d.StringGetAsync(key));
        }
        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        public static bool KeyDelete(string key)
        {
            return Do(d => d.KeyDelete(key));
        }

        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        public static async Task<bool> KeyDeleteAsync(string key)
        {
            return await Do(d => d.KeyDeleteAsync(key));
        }
        /// <summary>
        /// set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool StringSet<T>(string key, T value, TimeSpan? expiry = null)
        {
            string json = Serialize(value);
            return Do(d => d.StringSet(key, json, expiry));
        }

        /// <summary>
        /// set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            string json = Serialize(value);
            return await Do(d => d.StringSetAsync(key, json, expiry));
        }
        /// <summary>
        /// get 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static T StringGet<T>(string key)
        {
            string json = Do(d => d.StringGet(key)).ToString();
            return Deserialize<T>(json);
        }

        /// <summary>
        /// get 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<T> StringGetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            string json = await Do(d => d.StringGetAsync(key));
            return Deserialize<T>(json);
        }
        #endregion
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string Serialize<T>(T value)
        {
            var result = value is string ? value.ToString() : JsonConvert.SerializeObject(value,
                new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd hh:mm:ss"
                });
            return result;
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        private static T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return default;
            return JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 解析redis缓存的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        private static List<T> DeserializeList<T>(RedisValue[] values)
        {
            return values.Select(s => Deserialize<T>(s)).ToList();
        }

        #region List
        /// <summary>
        /// 保存list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetList<T>(string key, List<T> value)
        {
            if (value.Count > 0)
            {
                var values = value.Select(s => (RedisValue)Serialize(s)).ToArray();
                Do(d => d.ListRightPush(key, values));
            }
        }

        /// <summary>
        /// 保存list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static async Task SetListAsync<T>(string key, List<T> value)
        {
            if (value.Count > 0)
            {
                var values = value.Select(s => (RedisValue)Serialize(s)).ToArray();
                await Do(d => d.ListRightPushAsync(key, values));
            }
        }
        /// <summary>
        /// 获取list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(string key)
        {
            var values = Do(d => d.ListRange(key));
            if (values.IsNullOrEmpty())
            {
                return values.Select(s => Deserialize<T>(s)).ToList();
            }
            return default;
        }
        /// <summary>
        /// 获取list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetListAsync<T>(string key)
        {
            var values = await Do(d => d.ListRangeAsync(key));
            if (values.IsNullOrEmpty())
            {
                return values.Select(s => Deserialize<T>(s)).ToList();
            }
            return default;
        }
        /// <summary>
        /// 删除并返回集合的第一个元素
        /// ListLeftPop
        /// </summary>
        /// <param name="key"></param>
        public static T RemoveFirst<T>(string key)
        {
            var json = Do(d => d.ListLeftPop(key));
            return Deserialize<T>(json);
        }
        /// <summary>
        /// 删除并返回集合的第一个元素
        /// ListLeftPop
        /// </summary>
        /// <param name="key"></param>
        public static async Task<T> RemoveFirstAsync<T>(string key)
        {
            var json = await Do(d => d.ListLeftPopAsync(key));
            return Deserialize<T>(json);
        }
        /// <summary>
        /// 删除并返回集合的最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T RemoveLast<T>(string key)
        {
            return Deserialize<T>(Do(d => d.ListRightPop(key)));
        }

        /// <summary>
        /// 删除并返回集合的最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> RemoveLastAsync<T>(string key)
        {
            return Deserialize<T>(await Do(d => d.ListRightPopAsync(key)));
        }
        /// <summary>
        /// 根据索引获取集合 (默认所有)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static List<T> ListRange<T>(string key, long start = 0, long stop = -1)
        {
            var json = Do(d => d.ListRange(key, start, stop));
            return DeserializeList<T>(json);
        }

        /// <summary>
        /// 根据索引获取集合 (默认所有)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static async Task<List<T>> ListRangeAsync<T>(string key, long start = 0, long stop = -1)
        {
            var json = await Do(d => d.ListRangeAsync(key, start, stop));
            return DeserializeList<T>(json);
        }

        /// <summary>
        /// 根据元素删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>返回删除元素数量</returns>
        public static int RemoveAt<T>(string key,T value)
        {
            return (int)Do(d => d.ListRemove(key, Serialize(value)));
        }

        /// <summary>
        /// 根据元素删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>返回删除元素数量</returns>
        public static async Task<int> RemoveAtAsync<T>(string key, T value)
        {
            return (int)await Do(d => d.ListRemoveAsync(key, Serialize(value)));
        }
        /// <summary>
        /// 向集合开头添加一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddFirst<T>(string key, T value)
        {
            Do(d => d.ListLeftPush(key, Serialize(value)));
        }
        /// <summary>
        /// 向集合开头添加一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static async Task AddFirstAsync<T>(string key, T value)
        {
            await Do(d => d.ListLeftPushAsync(key, Serialize(value)));
        }
        /// <summary>
        /// 向集合末尾添加一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddLast<T>(string key, T value)
        {
            Do(d => d.ListRightPush(key, Serialize(value)));
        }
        /// <summary>
        /// 向集合末尾添加一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>

        public static async Task AddLastAsync<T>(string key, T value)
        {
            await Do(d => d.ListRightPushAsync(key, Serialize(value)));
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains<T>(string key, T value)
        {
            //数量
            var length = Count(key);
            //格式话对象
            var json = Serialize(value);
            for (int i = 0; i < length; i++)
            {
                if (Do(d => d.ListGetByIndex(key, i).ToString().Equals(json)))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> ContainsAsync<T>(string key, T value)
        {
            //数量
            var length = await CountAsync(key);
            //格式话对象
            var json = Serialize(value);
            for (int i = 0; i < length; i++)
            {
                var item = await Do(d => d.ListGetByIndexAsync(key, i));
                if (item.ToString().Equals(json))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据索引获取对象
        /// ListGetByIndex
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Get<T>(string key, int index)
        {
            string json = Do(d => d.ListGetByIndex(key, index));
            return Deserialize<T>(json);
        }

        /// <summary>
        /// 根据索引获取对象
        /// ListGetByIndex
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string key, int index)
        {
            string json = await Do(d => d.ListGetByIndexAsync(key, index));
            return Deserialize<T>(json);
        }
        /// <summary>
        /// 根据对象获取索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IndexOf<T>(string key, T value)
        {
            //数量
            var length = Count(key);
            //格式话对象
            var json = Serialize(value);
            for (int i = 0; i < length; i++)
            {
                if ( Do(d => d.ListGetByIndex(key, i).ToString().Equals(json)))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 根据对象获取索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<int> IndexOfAsync<T>(string key, T value)
        {
            //数量
            var length = await CountAsync(key);
            //格式话对象
            var json = Serialize(value);
            for (int i = 0; i < length; i++)
            {
                var item = await Do(d => d.ListGetByIndexAsync(key, i));
                if ( item.ToString().Equals(json))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 获取list的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int Count(string key)
        {
            return (int)Do(d => d.ListLength(key));
        }

        /// <summary>
        /// 获取list的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<int> CountAsync(string key)
        {
            return (int)await Do(d => d.ListLengthAsync(key));
        }
        #endregion
    }
}
