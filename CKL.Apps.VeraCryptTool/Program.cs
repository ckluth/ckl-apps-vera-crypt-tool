using CKL.Apps.VeraCryptTool.Commands;
using CKL.Libs.ResultPattern;

namespace CKL.Apps.VeraCryptTool;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            return PrintUsageAndFail();
        }

        return args[0] switch
        {
            "create-keyfile" => RunCreateKeyFile(args),
            "mount" => RunMount(args),
            _ => PrintUsageAndFail(),
        };
    }

    private static int RunCreateKeyFile(string[] args)
    {
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: create-keyfile <keyFilePath> <pin> <strongPassword>");
            return 1;
        }

        var result = CreateKeyFileCommand.Execute(args[1], args[2], args[3]);
        return ReportResult(result);
    }

    private static int RunMount(string[] args)
    {
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: mount <volumeFilePath> <driveLetter> <keyFilePath>");
            return 1;
        }

        Console.Write("KeyFile PIN: ");
        var pin = PinReader.ReadPin();

        var result = MountCommand.Execute(args[1], args[2], args[3], pin);
        return ReportResult(result);
    }

    private static int ReportResult(Result result)
    {
        if (result.Succeeded)
        {
            Console.WriteLine("OK.");
            return 0;
        }

        Console.Error.WriteLine(result.ErrorMessage);
        return 1;
    }

    private static int PrintUsageAndFail()
    {
        Console.Error.WriteLine("Usage:");
        Console.Error.WriteLine("  create-keyfile <keyFilePath> <pin> <strongPassword>");
        Console.Error.WriteLine("  mount <volumeFilePath> <driveLetter> <keyFilePath>");
        return 1;
    }
}
