# ConsoleApp

## Integration `Microsoft.CodeAnalysis.FxCopAnalyzers`

[Dokumentation](https://docs.microsoft.com/en-us/visualstudio/code-quality/?view=vs-2017)

### Installation

Installation als NuGet Package

```C#
dotnet add package Microsoft.CodeAnalysis.FxCopAnalyzers --version 2.9.2
```

oder einfach in die `*.csproj` einbinden.

```xml
<ItemGroup>
  ...
  <PackageReference Include="Microsoft.CodeAnalysis.FxCopAnalyzers" Version="2.9.2" PrivateAssets="all"/>
</ItemGroup>
```

### Konfiguration

Zur Konfiguration wird ein `rule set` benötigt. Dies ist eine XML-Datei `project.ruleset`.

[Rule set format](https://docs.microsoft.com/en-us/visualstudio/code-quality/using-rule-sets-to-group-code-analysis-rules?view=vs-2017)

```xml
<?xml version="1.0"?>
<RuleSet Name="All Rules with default action" Description="All Rules with default action. Rules with IsEnabledByDefault = false are disabled." ToolsVersion="15.0">
   <Rules AnalyzerId="Microsoft.CodeAnalysis.VersionCheckAnalyzer" RuleNamespace="Microsoft.CodeAnalysis.VersionCheckAnalyzer">
      <Rule Id="CA9999" Action="Warning" />          <!-- Analyzer version mismatch -->
   </Rules>
   <Rules AnalyzerId="Microsoft.CodeQuality.Analyzers" RuleNamespace="Microsoft.CodeQuality.Analyzers">
      <Rule Id="CA1000" Action="Warning" />          <!-- Do not declare static members on generic types -->
      <Rule Id="CA1008" Action="None" />             <!-- Enums should have zero value -->
      <Rule Id="CA1010" Action="Warning" />          <!-- Collections should implement generic interface -->
      <Rule Id="CA1012" Action="None" />             <!-- Abstract types should not have constructors -->
      <Rule Id="CA1501" Action="Warning" />             <!-- Avoid excessive inheritance -->
      <Rule Id="CA1502" Action="Warning" />             <!-- Avoid excessive complexity -->
      <Rule Id="CA1505" Action="Warning" />             <!-- Avoid unmaintainable code -->
      <Rule Id="CA1506" Action="Warning" />             <!-- Avoid excessive class coupling -->
  </Rules>
   <Rules AnalyzerId="Microsoft.NetCore.Analyzers" RuleNamespace="Microsoft.NetCore.Analyzers">
      <Rule Id="CA1303" Action="None" />             <!-- Do not pass literals as localized parameters -->
      <Rule Id="CA1304" Action="Warning" />          <!-- Specify CultureInfo -->
      <Rule Id="CA1305" Action="Warning" />          <!-- Specify IFormatProvider -->
      <Rule Id="CA1307" Action="Warning" />          <!-- Specify StringComparison -->
  </Rules>
</RuleSet>
```

[Diese Datei kann beliebig angepasst werden.](https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-create-a-custom-rule-set?view=vs-2017) Alternativ kann die Konfiguration auch über eine [.editorconfig-Datei](https://docs.microsoft.com/en-us/visualstudio/code-quality/configure-fxcop-analyzers?view=vs-2017#editorconfig-file) ([siehe auch](https://github.com/dotnet/roslyn-analyzers/blob/master/docs/Analyzer%20Configuration.md)) angepasst werden. Dazu gibt es weitere [Optionen](https://docs.microsoft.com/en-us/visualstudio/code-quality/fxcop-analyzer-options?view=vs-2017).

Sie wird ebenfalls in die `*.csproj` eingebunden

```xml
<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
  <NoWarn>1701;1702;1591</NoWarn>
  <CodeAnalysisRuleSet>versandAPI.ruleset</CodeAnalysisRuleSet>
</PropertyGroup>
```

Warnungen können auch [dediziert unterdrückt](https://docs.microsoft.com/en-us/visualstudio/code-quality/in-source-suppression-overview?view=vs-2017) werden.

### How to: Generate code metrics data

In FxCop sind auch [code metric rules](https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-generate-code-metrics-data?view=vs-2019#command-line-code-metrics) enthalten. Im Standard sind sie jedoch deaktiviert. Sie können im Ruleset aktiviert werden. Die Warnschwellen werden in einer separaten Datei `CodeMetricsConfig.txt` konfiguriert. Diese Datei muss dann wieder eingebunden werden. Zusätzlich muss das Modul `Microsoft.CodeAnalysis.Metrics` installiert sein.

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.CodeAnalysis.Metrics" Version="2.9.2" />
  <AdditionalFiles Include="CodeMetricsConfig.txt" />
</ItemGroup>
```

Durch den Build-Prozess wird die Metric in Form einer XML-Datei erstellt. Dies funktioniert allerdings nur unter Windows, weil die Metrics.exe zur Analyse nur unter Window ausgeführt werden kann. Das Ruleset und die Warnschwellen können trotzdem verwendet werden. Abhängig von den Warnschwellen werden entsprechende Meldungen angezeigt.

```powershell
PS C:\Projects\CSharp\CodeAnalysis\ConsoleApp> dotnet build /t:Metrics
Microsoft (R)-Build-Engine, Version 16.0.450+ga8dc7f1d34 für .NET Core
Copyright (C) Microsoft Corporation. Alle Rechte vorbehalten.

  Wiederherstellung in "83,35 ms" für "C:\Projects\CSharp\CodeAnalysis\ConsoleApp\ConsoleApp.csproj" abgeschlossen.
  ConsoleApp -> C:\Projects\CSharp\CodeAnalysis\ConsoleApp\bin\Debug\netcoreapp2.2\ConsoleApp.dll
  Loading ConsoleApp.csproj...
  Computing code metrics for ConsoleApp.csproj...
  Writing output to 'ConsoleApp.Metrics.xml'...
  Completed Successfully.

Der Buildvorgang wurde erfolgreich ausgeführt.
    0 Warnung(en)
    0 Fehler

Verstrichene Zeit 00:00:19.88
```
