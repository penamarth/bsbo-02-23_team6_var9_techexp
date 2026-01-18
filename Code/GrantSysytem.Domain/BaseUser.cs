using System;

namespace GrantSysytem.Domain
{
    abstract public class BaseUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
    }
}
