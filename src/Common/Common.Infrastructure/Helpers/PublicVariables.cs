namespace Common.Infrastructure.Helpers;

public static class PublicVariables
{
    public static string RabbitmqHost { get; set; }

    public static string RabbitmqUserName { get; set; }

    public static string RabbitmqPassword { get; set; }

    public static string ProductConnectionString { get; set; }
    public static string BasketConnectionString { get; set; }

    public static string StoreConnectionString { get; set; }
    public static string OrderConnectionString { get; set; }


    public static string JwtSecret { get; set; }

    public static string JwtIssuer { get; set; }
    
    public static string JwtAudience { get; set; }
}