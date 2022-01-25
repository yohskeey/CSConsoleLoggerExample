using System;

using Serilog;

namespace SerilogExample;

/// <summary>
/// Serilog
/// Serilog.Sinks.Console
/// Serilog.Sinks.File の３つを使ったロガーのサンプル
/// Serilogのチュートリアルのまま
/// https://github.com/serilog/serilog/wiki/Getting-Started#example-application
/// </summary>
class Program
{
    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()  // コンソール出力
            .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Day)   // ファイル出力、日別のファイル(yyyyMMdd)
            .CreateLogger();

        Log.Information("Hello, world!");

        int a = 10, b = 0;
        try
        {
            Log.Debug("Dividing {A} by {B}", a, b);
            Console.WriteLine(a / b);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Something went wrong");
        }
        finally
        {
            // ロガーの開放
            // プログラムの最後に必須
            Log.CloseAndFlush();
        }
    }
}
