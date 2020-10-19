using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.xUnitTest.Redis
{
    public class RedisTest
    {

    }



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
            get {
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
        public static bool StringSet(string key, string value,TimeSpan? expiry=null)
        {
            return Do(d => d.StringSet(key, value, expiry));
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
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        public static bool KeyDelete(string key)
        {
            return Do(d => d.KeyDelete(key));
        }

        public static bool sStringSet<T>(string key,T value,TimeSpan? expiry=null)
        {
            string json = Serialize(value);
            return Do(d => d.StringSet(key, json, expiry));
        }

        public static T StringGet<T>(string key, T value, TimeSpan? expiry = null)
        {
            string json = Do(d => d.StringGet(key)).ToString();
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
                string json = JsonConvert.SerializeObject(value);
                Do(d => d.StringSet(key, json));
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
            string json = Do(d => d.StringGet(key));
            if (!string.IsNullOrEmpty(json))
            {
                var value = JsonConvert.DeserializeObject<List<T>>(json);
                return value;
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
        /// 根据索引删除元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        public static int RemoveAt(string key,int index)
        {
            return (int)Do(d => d.ListRemove(key, index));
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
        /// 判断对象是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains<T>(string key,T value)
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
                if (Do(d => d.ListGetByIndex(key, i).ToString().Equals(json)))
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
        #endregion
    }

    public class RedisList<T> : IList<T>
    {
        private static ConnectionMultiplexer _cnn;
        private string key;
        public RedisList(string key)
        {
            this.key = key;
            _cnn = ConnectionMultiplexer.Connect("localhost");
        }
        private IDatabase GetRedisDb()
        {
            return _cnn.GetDatabase();
        }
        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        private T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        public void Insert(int index, T item)
        {
            var db = GetRedisDb();
            var before = db.ListGetByIndex(key, index);
            db.ListInsertBefore(key, before, Serialize(item));
        }
        public void RemoveAt(int index)
        {
            var db = GetRedisDb();
            var value = db.ListGetByIndex(key, index);
            if (!value.IsNull)
            {
                db.ListRemove(key, value);
            }
        }
        public T this[int index]
        {
            get
            {
                var value = GetRedisDb().ListGetByIndex(key, index);
                return Deserialize<T>(value.ToString());
            }
            set
            {
                Insert(index, value);
            }
        }
        public void Add(T item)
        {
            GetRedisDb().ListRightPush(key, Serialize(item));
        }
        public void Clear()
        {
            GetRedisDb().KeyDelete(key);
        }
        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (GetRedisDb().ListGetByIndex(key, i).ToString().Equals(Serialize(item)))
                {
                    return true;
                }
            }
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            GetRedisDb().ListRange(key).CopyTo(array, arrayIndex);
        }
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (GetRedisDb().ListGetByIndex(key, i).ToString().Equals(Serialize(item)))
                {
                    return i;
                }
            }
            return -1;
        }
        public int Count
        {
            get { return (int)GetRedisDb().ListLength(key); }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(T item)
        {
            return GetRedisDb().ListRemove(key, Serialize(item)) > 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return Deserialize<T>(GetRedisDb().ListGetByIndex(key, i).ToString());
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return Deserialize<T>(GetRedisDb().ListGetByIndex(key, i).ToString());
            }
        }
    }
    public class RedisConnectionHelper
    {

        /// <summary>
        /// 连接redis server 类
        /// </summary>
        public static ConnectionMultiplexer _connection;
        public readonly static object _locker = new object();

        /// <summary>
        /// 缓存连接实例
        /// </summary>
        public static readonly Dictionary<string, ConnectionMultiplexer> _connectionCache =
            new Dictionary<string, ConnectionMultiplexer>();
        /// <summary>
        /// 连接实例  (单例模式)
        /// </summary>
        public static ConnectionMultiplexer Instance {

            get {

                if (_connection == null)
                {
                    lock (_locker)
                    {
                        if (_connection == null || !_connection.IsConnected)
                        {
                            _connection = CreateConnection();
                        }
                    }
                }
                return _connection;
            }
        }
        /// <summary>
        /// https://stackexchange.github.io/StackExchange.Redis/Basics
        /// </summary>
        /// <param name="connectionString"></param>
        public static ConnectionMultiplexer CreateConnection(string connectionString=null)
        {
            //连接字符串
            string connectStr = connectionString ?? "127.0.0.1:6379";
            ConnectionMultiplexer redis =  ConnectionMultiplexer.Connect(connectionString);
            //redis 事件注册
            redis.ConnectionFailed += Redis_ConnectionFailed;
            return redis;
        }

        public static ConnectionMultiplexer GetConnectionMultiplexer(string connectionString)
        {
            if (!_connectionCache.ContainsKey(connectionString))
            {
                _connectionCache[connectionString] = CreateConnection(connectionString);
            }
            return _connectionCache[connectionString];
        }
        /// <summary>
        /// redis 服务连接失败事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Redis_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine(e.Exception);
        }
    }
}
