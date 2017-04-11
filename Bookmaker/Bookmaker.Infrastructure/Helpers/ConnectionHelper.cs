using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.Helpers
{
    public static class ConnectionHelper
    {
        public static string ConnectionString { get; private set; }

        public static void SetConnectionString(string cnn)
        {
            ConnectionString = cnn;
        }
    }
}
