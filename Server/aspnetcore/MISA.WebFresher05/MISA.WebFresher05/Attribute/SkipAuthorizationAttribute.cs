namespace MISA.WebFresher05
{
    /// <summary>
    /// Attribute bỏ qua xác thực
    /// </summary>
    /// Created by: vtahoang (16/09/2023) 
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class SkipAuthorizationAttribute : Attribute
    {
    }
}
