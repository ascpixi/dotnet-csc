using System.Diagnostics;
using System.Text.RegularExpressions;

var version = Process.Start(new ProcessStartInfo("dotnet", "--version") {
    RedirectStandardOutput = true
})!.StandardOutput.ReadToEnd().Trim();

var sdk =
    Process.Start(
        new ProcessStartInfo("dotnet", "--list-sdks") {
            RedirectStandardOutput = true
        }
    )!
    .StandardOutput.ReadToEnd()
    .Trim()
    .Split(Environment.NewLine)
    .Single(x => x.StartsWith(version));

var sdkRoot = Regex.Match(sdk, @".+\[(.+)]").Groups[1].Value;
var cscPath = Path.Combine(sdkRoot, version, "Roslyn", "bincore", "csc.dll");

if (!File.Exists(cscPath)) {
    Console.Error.WriteLine($"wrapper error: the 'csc.dll' file does not exist (path: {cscPath})");
    return 1;
}

var psi = new ProcessStartInfo {
    FileName = "dotnet",
    ArgumentList = { "exec", cscPath }
};

foreach (var arg in args) {
    psi.ArgumentList.Add(arg);
}

var p = Process.Start(psi);
p!.WaitForExit();
return p.ExitCode;
