namespace API_RoleBased_Swagger.Services
{
    /*  *** Referências Factory - Mesma interface multiplas instâncias ***
    The Factory Pattern In .NET Core: https://dotnetcoretutorials.com/2019/10/15/the-factory-pattern-in-net-core/

    Injecting a Factory Service in ASP.NET Core: https://espressocoder.com/2018/10/08/injecting-a-factory-service-in-asp-net-core/

    StackOverflow: Fluxo de criação de instâncias e exposição do Factory: https://stackoverflow.com/questions/39174989/how-to-register-multiple-implementations-of-the-same-interface-in-asp-net-core

    Registering multiple implementations of the same interface in ASP.NET Core: https://dejanstojanovic.net/aspnet/2018/december/registering-multiple-implementations-of-the-same-interface-in-aspnet-core/
    */
    public enum PrivilegeTypeEnum : byte
    {
        Admin = 1,
        User = 2
    }
    public interface IPrivilegeService
    {
        PrivilegeTypeEnum PrivilegeType { get; }
        string ShowMyInfo();
    }
    public class PrivilegeServiceAdmin : IPrivilegeService
    {
        public PrivilegeTypeEnum PrivilegeType => PrivilegeTypeEnum.Admin;

        public string ShowMyInfo() => PrivilegeType + ": Mega privilégios";
    }
    public class PrivilegeServiceUser : IPrivilegeService
    {
        public PrivilegeTypeEnum PrivilegeType => PrivilegeTypeEnum.User;

        public string ShowMyInfo() => PrivilegeType + ": Poucos privilégios";
    }
}