
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSaga.Adventure.Context.Common.Domain.Model{
    public class BaseRedisRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;

        /// <summary>
        /// The Namespace is the first part of any key created by this Repository, e.g. "location" or "employee"
        /// </summary>
        private readonly string _namespace;

        public BaseRedisRepository(IConnectionMultiplexer redis, string nameSpace)
        {
            _redisConnection = redis;
            _namespace = nameSpace;
        }

        public T Get<T>(Guid id)
        {
            return Get<T>(id.ToString());
        }

        public T Get<T>(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            if (serializedObject.IsNullOrEmpty) throw new ArgumentNullException(); //Throw a better exception than this, please
            return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
        }

        public List<T> GetMultiple<T>(List<Guid> ids)
        {
            var database = _redisConnection.GetDatabase();
            List<RedisKey> keys = new List<RedisKey>();
            foreach (Guid id in ids)
            {
                keys.Add(MakeKey(id));
            }
            var serializedItems = database.StringGet(keys.ToArray(), CommandFlags.None);
            List<T> items = new List<T>();
            foreach (var item in serializedItems)
            {
                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }
            return items;
        }

        public bool Exists(Guid id)
        {
            return Exists(id.ToString());
        }

        public bool Exists(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            return !serializedObject.IsNullOrEmpty;
        }

        public void Save(Guid id, object entity)
        {
            Save(id.ToString(), entity);
        }

        public void Save(string keySuffix, object entity)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            database.StringSet(MakeKey(key), JsonConvert.SerializeObject(entity));
        }

        private string MakeKey(Guid id)
        {
            return MakeKey(id.ToString());
        }

        private string MakeKey(string keySuffix)
        {
            if (!keySuffix.StartsWith(_namespace + ":"))
            {
                return _namespace + ":" + keySuffix;
            }
            else return keySuffix; //Key is already prefixed with namespace
        }
}
}