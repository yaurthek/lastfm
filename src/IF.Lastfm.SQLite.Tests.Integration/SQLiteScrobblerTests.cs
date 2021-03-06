﻿using System;
using System.IO;
using System.Net.Http;
using IF.Lastfm.Core.Scrobblers;
using IF.Lastfm.Core.Tests.Scrobblers;

namespace IF.Lastfm.SQLite.Tests.Integration
{
    public class SQLiteScrobblerTests : ScrobblerTestsBase
    {
        private string _dbPath;

        public override void Initialise()
        {
            var dbPath = Path.GetFullPath("test.db");
            File.Delete(dbPath);
            using (File.Create(dbPath))
            {
                
            }
            _dbPath = dbPath;

            base.Initialise();
        }

        public override void Cleanup()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete(_dbPath);

            base.Cleanup();
        }

        protected override ScrobblerBase GetScrobbler()
        {
            var httpClient = new HttpClient(FakeResponseHandler);
            return new SQLiteScrobbler(MockAuth.Object, _dbPath, httpClient);
        }
    }
}
