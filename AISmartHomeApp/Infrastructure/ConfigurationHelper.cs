using System.Collections;

namespace Infrastructure;

public class ConfigurationHelper {
    public static void Setup(IDictionary data){
        _data = data;
    }
    protected static IDictionary _data = new Dictionary<string,string>();
    protected static string GetStringValueByKeySafe(string keyName, string defaultValue = "") {
        ArgumentNullException.ThrowIfNull(_data, nameof(_data));        
        if (_data.Contains(keyName)) {
            try {
                return ((string?)_data[keyName]) ?? defaultValue;
            } catch {
                return defaultValue;
            }
        }
        return defaultValue;
    }
    protected static int GetIntValueByKeySafe(string keyName, int defaultValue) {
        try {
            return int.Parse(GetStringValueByKeySafe(keyName));
        } catch {
            return defaultValue;
        }
        
        
    }

    protected static double GetDoubleValueByKeySafe(string keyName, double defaultValue) {
        try {
            return double.Parse(GetStringValueByKeySafe(keyName));
        } catch {
            return defaultValue;
        }
        
    }

    protected static T GetEnumValueByKeySafe<T>(string keyName, T defaultValue) {
        try {
            return (T) Enum.Parse(typeof(T), GetStringValueByKeySafe(keyName));
        } catch {
            return defaultValue;
        }
        
    }
}