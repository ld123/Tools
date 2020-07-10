using NLog;

namespace Common.Extensions
{
    public static class NLogExtension
    {
        public static Logger GetLogger<T>(this T obj)
        {
            var type = obj.GetType();
            var baseType = type.BaseType;
            var name = type.FullName;
            if (baseType != null && baseType != type)
            {
                name = string.Intern($"{type.FullName}:{baseType.FullName}");
            }

            return LogManager.GetLogger(name);
        }
    }
}