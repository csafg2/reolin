using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Web.Security.Jwt;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JwtTests
{
    [TestClass]
    public class RedisJwtStoreTest
    {
        IConnectionMultiplexer mux;
        IJwtStore store;

        public RedisJwtStoreTest()
        {
            ProcessStartInfo info = new ProcessStartInfo()
            {
                CreateNoWindow = false,
                FileName = @"D:\tools\redis\Redis-x64-3.2.100\redis-start.cmd"
            };
            
            Process.Start(info);
            this.mux = ConnectionMultiplexer.Connect("localhost");
            this.store = new RedisJwtStore(mux);
        }

        [TestMethod]
        public void RedisStore_AddRemoveJwt()
        {
            string userName = "hassan";
            string token = Guid.NewGuid().ToString();
            string secondToken = Guid.NewGuid().ToString();

            store.AddAsync(userName, token).Wait();
            store.AddAsync(userName, secondToken).Wait();

            // check to see if above two token are stored
            Assert.IsTrue(store.HasToken(userName, token));
            Assert.IsTrue(store.HasToken(userName, secondToken));

            //check if tokens are stored correctly
            Assert.IsFalse(store.HasToken("wrong", token));

            store.RemoveAsync(userName, token).Wait();

            // make sture that token is removed
            bool result = store.HasTokenAsync(userName, token).Result;
            Assert.IsFalse(result);
        }
    }
}
