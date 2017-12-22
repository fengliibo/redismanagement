using System;
using System.Configuration;
using System.IO;
namespace RedisManagement
{
    public class RedisConfig:ConfigurationSection
    {
        public RedisConfig() { }
        public static RedisConfig GetConfig(){
            RedisConfig section = GetConfig("RedisConfig");

            return section;
        }

        public static RedisConfig GetConfig(string sectionName){
            //RedisConfig section = (RedisConfig)ConfigurationManager.GetSection(sectionName);
            RedisConfig section = new RedisConfig();
            string readServerConStr = ConfigurationManager.AppSettings["ReadServerConStr"];
            string writeServerConstr = ConfigurationManager.AppSettings["WriteServerConstr"];
            section.ReadServerConStr = readServerConStr;
            section.WriteServerConstr = writeServerConstr;
            if(section==null)
            {
                throw new ConfigurationErrorsException("Section " + sectionName + "is not found");
            }
            return section;
            
        }

        [ConfigurationProperty("WriteServerConStr",IsRequired = false)]
        public string WriteServerConstr
        {
            get{
                return (string)base["WriteServerConStr"];
            }
            set{
                base["WriteServerConStr"] = value;
            }
        }

        [ConfigurationProperty("ReadServerConStr", IsRequired = false)]
        public string ReadServerConStr
        {
            get
            {
                return (string)base["ReadServerConStr"];
            }
            set
            {
                base["ReadServerConStr"] = value;
            }
        }
        [ConfigurationProperty("MaxWritePoolSize", IsRequired = false,DefaultValue = 5)]
        public int MaxWritePoolSize
        {
            get
            {
                int _maxWritePoolSize = (int)base["MaxWritePoolSize"];
                return _maxWritePoolSize > 0 ? _maxWritePoolSize : 5;
            }
            set
            {
                base["MaxWritePoolSize"] = value;
            }
        }
        [ConfigurationProperty("MaxReadPoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxReadPoolSize
        {
            get
            {
                int _maxReadPoolSize = (int)base["MaxReadPoolSize"];
                return _maxReadPoolSize > 0 ? _maxReadPoolSize : 5;
            }
            set
            {
                base["MaxReadPoolSize"] = value;
            }
        }

        [ConfigurationProperty("AutoStart", IsRequired = false, DefaultValue = true)]
        public bool AutoStart
        {
            get
            {
                return (bool)base["AutoStart"];
            }
            set
            {
                base["AutoStart"] = value;
            }
        }


        [ConfigurationProperty("LocalCacheTime", IsRequired = false, DefaultValue = 36000)]
        public int LocalCacheTime
        {
            get
            {
                return (int)base["LocalCacheTime"];
            }
            set
            {
                base["LocalCacheTime"] = value;
            }
        }

        [ConfigurationProperty("RecordeLog", IsRequired = false, DefaultValue = false)]
        public bool RecordeLog
        {
            get
            {
                return (bool)base["RecordeLog"];
            }
            set
            {
                base["RecordeLog"] = value;
            }
        }

    }
}
