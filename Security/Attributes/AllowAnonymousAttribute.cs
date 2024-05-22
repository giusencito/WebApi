namespace WebApi.Security.Attributes
{ 
     [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
