﻿using Inveon.Core.Interfaces.CacheManagement;
using Inveon.Core.Interfaces.DbConfigs;
using Inveon.Data.DbConfig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inveon.Data.CacheManagement
{
    public class RedisManagement : IRedisManagement
    {
        private readonly IRedisConfiguration _redisServer;
        public RedisManagement(IRedisConfiguration redisServer)
        {
            _redisServer = redisServer;
        }
        public async Task AddAsync(string key, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            await _redisServer.Database.StringSetAsync(key,jsonData,TimeSpan.FromMinutes(100));
        }
        public async Task<bool> AnyAsync(string key)
        {
            return  await _redisServer.Database.KeyExistsAsync(key);
        }
        public async Task Clear()
        {
           await _redisServer.FlushDatabaseAsync();
        }
        public async Task<T> GetAsync<T>(string key)
        {
            if (await AnyAsync(key))
            {
                string jsonData = await _redisServer.Database.StringGetAsync(key);
                var response =JsonConvert.DeserializeObject<T>(jsonData);
                return response;
            }
            return default;
        }
        public async Task<IEnumerable<T>> GetByPatternAsync<T>(string pattern)
        {
            var resultList = new List<T>();

            var keys = _redisServer.Server.KeysAsync(pattern: pattern);
            await foreach (var key in keys)
            {
                if (await AnyAsync(key))
                {
                    string jsonData = await _redisServer.Database.StringGetAsync(key);
                    var response = JsonConvert.DeserializeObject<T>(jsonData);
                    resultList.Add(response);
                }
            }
            return resultList;
        }
        public async Task RemoveAsync(string key)
        {
            await _redisServer.Database.KeyDeleteAsync(key);
        }
        public async Task RemoveByPatternAsync(string pattern)
        {
            var keys=  _redisServer.Server.KeysAsync(pattern: pattern);
            await foreach (var key in keys)
            {
                await _redisServer.Database.KeyDeleteAsync(key);
            }
        }
    
    }
}
