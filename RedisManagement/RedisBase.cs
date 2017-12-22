using System;
using ServiceStack.Redis;
namespace RedisManagement
{
    public class RedisBase : IDisposable
    {
        public static IRedisClient Core { get; private set; }
        private bool _dispose = false;
        static RedisBase(){
            Core = RedisManager.GetClient();
        }
        protected virtual void Dispose(bool disposing)
        {
            if(!this._dispose)
            {
                if(disposing)
                {
                    Core.Dispose();
                    Core = null;
                }
            }
            this._dispose = true;
        }
        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);     
        }
        public void Save(){
            Core.Save();
        }
        public void SaveAsync()
        {
            Core.SaveAsync();
        }
    }
}
