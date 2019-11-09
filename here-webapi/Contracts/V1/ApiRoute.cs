using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace here_webapi.Contracts.V1
{
    public static class ApiRoute
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Universiteler
        {
            public const string GetAll = Base + "/universiteler";
            public const string Get = Base + "/universiteler/{id}";
            public const string Create = Base + "/universiteler";
        }
    }
}
