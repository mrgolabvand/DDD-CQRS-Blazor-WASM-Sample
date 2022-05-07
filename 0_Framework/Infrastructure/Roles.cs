using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure
{
    public class Roles
    {
        public const string User = "0";
        public const string Blogger = "1";
        public const string Admin = "2";

        public static string GetRoleBy(long roleId)
        {
            switch (roleId)
            {
                case 2:
                    return "مدیر";
                case 1:
                    return "مقاله نویس";
                case 0:
                    return "کاربر";
                default:
                    return "نامشخص";
            }
        }
    }
}
