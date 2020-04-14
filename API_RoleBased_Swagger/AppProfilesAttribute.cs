using System;

namespace API_RoleBased_Swagger
{
    [Flags]
    public enum EnumAppProfiles
    {
        Public = 0,
        Users = 1,
        Forecast = 2
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AppProfilesAttribute : Attribute
    {
        public AppProfilesAttribute(EnumAppProfiles profiles) => Prifiles = profiles;
        public EnumAppProfiles Prifiles { get; set; }
    }
}