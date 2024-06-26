using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Pulumi;
using Nuke.Common.Utilities.Collections;

using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Deploy);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true)] readonly Solution Solution;

    AbsolutePath InfrastructureDirectory => RootDirectory / "iac";
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath PublishDirectory => RootDirectory / "publish";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("*/bin", "*/obj").ForEach(dir => dir.DeleteDirectory());
            PublishDirectory.CreateOrCleanDirectory();
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution.src.GetTenant)
                .SetRuntime("linux-x64"));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
            .SetProjectFile(Solution.src.GetTenant)
            .SetConfiguration(Configuration)
            .SetRuntime("linux-x64")
            .EnableNoRestore());
        });

    Target Publish => _ => _
        .DependsOn(Clean, Compile)
        .Executes(() =>
        {
            DotNetPublish(_ => _
                        .SetProject(Solution.src.GetTenant)
                        .SetConfiguration(Configuration)
                        .SetRuntime("linux-x64")
                        .EnableNoBuild()
                        .SetOutput(PublishDirectory));

            PublishDirectory.ZipTo(ArtifactsDirectory / "bootstrap.zip");
        });

    Target Deploy => _ => _
        .DependsOn(Publish)
        .Executes(() =>
        {
            PulumiTasks.PulumiUp(_ => _
                .SetCwd(InfrastructureDirectory)
                .SetStack("dev")
                .EnableYes());
        });

}