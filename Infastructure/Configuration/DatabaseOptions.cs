using System.Collections.Generic;

namespace Infrastructure.Configuration
{
    public class DatabaseOptions
    {
        public string ConnectionStringTemplate { get; set; }

        public Dictionary<string, string> ConnectionStrings { get; set; }
    }
}
