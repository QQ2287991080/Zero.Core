using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Zero.Core.Common.Redis
{
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
        public static ConnectionMultiplexer Instance
        {

            get
            {

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
        public static ConnectionMultiplexer CreateConnection(string connectionString = null)
        {
            //连接字符串
            string connectStr = connectionString ?? "127.0.0.1:6379";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectStr);
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
