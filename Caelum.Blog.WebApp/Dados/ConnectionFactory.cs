using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Caelum.Blog.WebApp.Dados
{
    public class ConnectionFactory
    {
        public IDbConnection CreateOpenedCnx()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration cfg = builder.Build();

            var stringCnx = cfg.GetConnectionString("Blog");
            IDbConnection conexaoBD = new SqlConnection(stringCnx);
            conexaoBD.Open();
            return conexaoBD;
        }
    }
}
