using BooksBorrow.Common.Utils;
using BooksBorrow.InjectFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Common.Logger
{
    public static class Logger
    {
        public static void LogInfo(Type type, string message)
        {
            var logger = GetLogHelper();

            logger.WriteInfo(type, message);
        }

        public static void LogInfo(object instance, string message)
        {
            var logger = GetLogHelper();

            logger.WriteInfo(instance, message);
        }

        public static void LogDebug(Type type, string message)
        {
            var logger = GetLogHelper();

            logger.WriteDebug(type, message);
        }

        public static void LogDebug(object instance, string message)
        {
            var logger = GetLogHelper();

            logger.WriteDebug(instance, message);
        }

        public static void LogError(Type type, Exception ex)
        {
            string errorMessage = Utility.GetExceptionContent(ex);

            LogError(type, errorMessage);
        }

        public static void LogError(Type type, string message)
        {
            var logger = GetLogHelper();

            logger.WriteError(type, message);
        }

        public static void LogError(object instance, Exception ex)
        {
            string errorMessage = Utility.GetExceptionContent(ex);

            LogError(instance, errorMessage);
        }

        public static void LogError(object instance, string message)
        {
            var logger = GetLogHelper();

            logger.WriteError(instance, message);
        }

        private static ILogHelper GetLogHelper()
        {
            return InjectContainer.GetInstance<ILogHelper>();
        }
    }
}
