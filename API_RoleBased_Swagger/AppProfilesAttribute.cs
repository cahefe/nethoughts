using System;

namespace API_RoleBased_Swagger
{
    [Flags]
    public enum EnumAppProfiles
    {
        Undefined = 0,
        Public = 1,
        Users = 2,
        Forecast = 4
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class AppProfilesAttribute : Attribute
    {
        public AppProfilesAttribute(EnumAppProfiles profiles) => Prifiles = profiles;
        public EnumAppProfiles Prifiles { get; set; }
    }
}