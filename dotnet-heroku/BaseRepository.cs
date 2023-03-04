using Microsoft.Extensions.Options;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;

    public class BaseRepository
    {
        protected readonly string _connData = null;
        public IConfiguration Configuration { get; }

        public BaseRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=\\LAPTOP-GQHLV798\\Documentos\\NinjaTrader 8\\db\\NinjaTrader.sqlite;");
        }
    }
