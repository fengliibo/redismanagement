using System;
using System.Linq;
using ServiceStack.Redis;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace RedisManagement
{
    public class RedisManager
    {
        private static RedisConfig RedisConfig = RedisConfig.GetConfig();
        private static PooledRedisClientManager prcm;
        private static void CreateManager(){
            string[] WriteServerConStr = SplitString(RedisConfig.WriteServerConstr, ",");
            string[] ReadServerConStr = SplitString(RedisConfig.ReadServerConStr, ",");
            prcm = new PooledRedisClientManager(ReadServerConStr, WriteServerConStr,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = RedisConfig.MaxWritePoolSize,
                                 MaxReadPoolSize = RedisConfig.MaxWritePoolSize,
                                 AutoStart = RedisConfig.AutoStart,
                             });
        }

        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }
        static RedisManager()
        {
            CreateManager();
        }
        public static IRedisClient GetClient()
        {
            if (prcm == null)
                CreateManager();

            return prcm.GetClient();
        }
    }
}
