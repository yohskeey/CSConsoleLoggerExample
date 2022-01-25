
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Logging;


var logger = LogManager.GetCurrentClassLogger();
try
{
    // appsettings.jsonの読み込み
    var config = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .Build();
    LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

    logger.Trace("trace");
    logger.Debug("debug");
    logger.Info("info");
    logger.Warn("warn");
    logger.Error("error");
    logger.Fatal("fatal");

    Console.WriteLine("Press ANY key to exit");
    Console.ReadKey();
}
catch (Exception ex)
{
    // NLog: catch any exception and log it.
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // アプリケーションの終了前に、内部タイマやスレッドを確実にフラッシュし、停止すること
    // Linuxでのセグメンテーションフォルトを防止するらしいが、Windowsターゲットでも必要か？
    LogManager.Shutdown();
}