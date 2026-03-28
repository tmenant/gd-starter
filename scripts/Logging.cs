using System.Collections.Generic;
using System.Linq;
using Godot;

public enum LoggingLevel : byte
{
    DEBUG,
    INFO,
    WARNING,
    ERROR,
    NONE,
    ANY,
}

public class Logging
{
    public class Logger
    {
        private static readonly Dictionary<LoggingLevel, string> colorMapping = new()
        {
            { LoggingLevel.DEBUG,   "#606469" },
            { LoggingLevel.INFO,    "NONE" },
            { LoggingLevel.WARNING, "#FFC000" },
            { LoggingLevel.ERROR,   "#FF1414" },
        };

        private static readonly Dictionary<LoggingLevel, string> levelMapping = new()
        {
            { LoggingLevel.DEBUG,   "DBG" },
            { LoggingLevel.INFO,    "INF" },
            { LoggingLevel.WARNING, "WRN" },
            { LoggingLevel.ERROR,   "ERR" },
        };

        public LoggingLevel loggingLevel = LoggingLevel.DEBUG;

        public string loggerName = "root";

        public Logger(string name, LoggingLevel level = LoggingLevel.DEBUG)
        {
            loggerName = name;
            loggingLevel = level;
        }

        public void Log(string message, bool toConsole = true)
        {
            var text = message.Replace("\r\n", "\n");

            GD.PrintRich(text);

            if (toConsole)
            {
                ConsoleManager.Log(text);
            }
        }

        public void Log(LoggingLevel level, params object[] objects)
        {
            if (loggingLevel > level)
                return;

            string message = string.Join(" ", objects.Select(obj => obj?.ToString()));
            string levelName = levelMapping[level];
            string color = colorMapping[level];

            Log($"[color={color}]{levelName} [{loggerName}] {message}[/color]");
        }

        public void Debug(params object[] objects)
        {
            Log(LoggingLevel.DEBUG, objects);
        }

        public void Info(params object[] objects)
        {
            Log(LoggingLevel.INFO, objects);
        }

        public void Warning(params object[] objects)
        {
            Log(LoggingLevel.WARNING, objects);
        }

        public void Error(params object[] objects)
        {
            Log(LoggingLevel.ERROR, objects);
        }
    }

    public static readonly LoggingLevel loggingLevel = LoggingLevel.DEBUG;

    private static readonly Logger root = new Logger("root", loggingLevel);

    public static Logger CreateLogger(string name, LoggingLevel level = LoggingLevel.DEBUG)
    {
        return new Logger(name, level);
    }

    public static Logger CreateLogger<T>(LoggingLevel level = LoggingLevel.DEBUG)
    {
        return new Logger(typeof(T).Name, level);
    }

    public static void Debug(params object[] objects)
    {
        root.Debug(objects);
    }

    public static void Info(params object[] objects)
    {
        root.Info(objects);
    }

    public static void Warning(params object[] objects)
    {
        root.Warning(objects);
    }

    public static void Error(params object[] objects)
    {
        root.Error(objects);
    }
}
