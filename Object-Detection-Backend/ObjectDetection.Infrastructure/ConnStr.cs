using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Infrastructure
{
    public class ConnStr
    {
        private readonly IConfiguration _configuration;

        public ConnStr(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Get()
        {
            var uriString = _configuration["ELEPHANTSQL_URL"];
               // ??  _configuration["LOCAL_URL"];
            var uri = new Uri(uriString);
            var db = uri.AbsolutePath.Trim('/');
            var user = uri.UserInfo.Split(':')[0];
            var passwd = uri.UserInfo.Split(':')[1];
            var port = uri.Port > 0 ? uri.Port : 5432;
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                uri.Host, db, user, passwd, port);
            return connStr;
        }
    }
}