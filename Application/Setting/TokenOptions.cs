using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Setting
{
    public class TokenOptions
    {
        public string Key { get; set; } = "THIS_IS_YOUR_SECRET_KEY_123456789";
        public string Issuer { get; set; } = "UniversitySystem";
        public string Audience { get; set; } = "UniversitySystemUsers";
        public int DurationInMinutes { get; set; } = 60;
    }

}
