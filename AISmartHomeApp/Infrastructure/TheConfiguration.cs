using System.Collections;

namespace Infrastructure;

public class TheConfiguration : ConfigurationHelper {

    // Strings
    
    public static string CustomString => GetStringValueByKeySafe("CustomString", "key not found");
    public static string FormType => "application/x-www-form-urlencoded";
    public static string JsonType => "application/json";
    
    // Ints 
    public static int MaxFailures => GetIntValueByKeySafe("MaxFailures", 10);
    public static int ServiceFrequency =>  GetIntValueByKeySafe("ServiceFrequency", 5);
    
    public static double BaseLocationLatitude => GetDoubleValueByKeySafe("BaseLocationLatitude", 27.774383);
    public static double BaseLocationLongitude => GetDoubleValueByKeySafe("BaseLocationLongitude", -82.633286);
    // Enums
}   