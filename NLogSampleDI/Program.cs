
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Logging;

using NLogSampleDI;

var logger = LogManager.GetCurrentClassLogger();
try
{
    // appsettings.jsonの読み込み
    var config = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .Build();
    LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

    // DI
    using var servicesProvider = new ServiceCollection()
        .AddTransient<Runner>() // Runner.cs には実際の処理が書かれている。Transientスコープでインスタンス化する
        .AddLogging(loggingBuilder =>
        {
            // configure Logging with NLog
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            loggingBuilder.AddNLog(config);
        }).BuildServiceProvider();

    var runner = servicesProvider.GetRequiredService<Runner>();
    runner.DoAction("Action1"); // 実処理関数を呼び出す

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