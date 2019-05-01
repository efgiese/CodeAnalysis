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
<RuleSet Name="Rules for Hello World project" Description="These rules focus on critical issues for the Hello World app." ToolsVersion="10.0">
  <Localization ResourceAssembly="Microsoft.VisualStudio.CodeAnalysis.RuleSets.Strings.dll" ResourceBaseName="Microsoft.VisualStudio.CodeAnalysis.RuleSets.Strings.Localized">
    <Name Resource="HelloWorldRules_Name" />
    <Description Resource="HelloWorldRules_Description" />
  </Localization>
  <Rules AnalyzerId="Microsoft.Analyzers.ManagedCodeAnalysis" RuleNamespace="Microsoft.Rules.Managed">
    <Rule Id="CA1001" Action="Warning" />
    <Rule Id="CA1009" Action="Warning" />
    <Rule Id="CA1016" Action="Warning" />
    <Rule Id="CA1033" Action="Warning" />
  </Rules>
  <Rules AnalyzerId="Microsoft.CodeQuality.Analyzers" RuleNamespace="Microsoft.CodeQuality.Analyzers">
    <Rule Id="CA1802" Action="Error" />
    <Rule Id="CA1814" Action="Info" />
    <Rule Id="CA1823" Action="None" />
    <Rule Id="CA2217" Action="Warning" />
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