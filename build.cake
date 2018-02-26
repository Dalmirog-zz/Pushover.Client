#tool nuget:?package=NUnit.ConsoleRunner&version=3.7.0
#tool nuget:?package=Cake.CoreCLR&version=0.21.1

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// VARIABLES
///////////////////////////////////////////////////////////////////////////////

var solutionFile = "./source/Pushover.Client.sln";
var clientProjectfile = "./source/Pushover.Client/Pushover.Client.csproj";
var testsProjectfile = "./source/Pushover.Client.Tests/Pushover.Client.Tests.csproj";
var clientOutputDirectory = MakeAbsolute(Directory("./build/Pushover.Client")).ToString();
var testsOutputDirectory = MakeAbsolute(Directory("./build/Pushover.Client.Tests")).ToString();


///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
  .Does(() =>
{
    Information("Cleaning up build");
    CleanDirectories("./source/**/bin");
    CleanDirectories("./source/**/obj");
    CleanDirectories(clientOutputDirectory);
    CleanDirectories(testsOutputDirectory);
});

Task("Restore")
  .IsDependentOn("Clean")
  .Does(() =>
{
    Information("Restoring packages for [{0}]", solutionFile);
    NuGetRestore(solutionFile);
});

Task("DotNetPublish")
  .IsDependentOn("Restore")
  .Does(() =>
{
    var clientNetCoreSettings = new DotNetCorePublishSettings
    {
        Configuration = configuration,
        OutputDirectory = clientOutputDirectory,
        NoRestore = true
    };

    Information("Running dotnet publish for Client project...");
    DotNetCorePublish(clientProjectfile, clientNetCoreSettings);

    // var testsNetCoreSettings = new DotNetCorePublishSettings
    // {
    //     Configuration = configuration,
    //     OutputDirectory = testsOutputDirectory,
    //     NoRestore = true
    // };

    // Information("Running dotnet publish for Tests project...");
    // DotNetCorePublish(testsProjectfile, testsNetCoreSettings);
});

Task("Test")
  .IsDependentOn("DotNetPublish")
  .Does(() =>
{
     var settings = new DotNetCoreTestSettings
     {
         Configuration = "Release",
         ArgumentCustomization = args => args.Append("--logger \"trx;LogFileName=TestResults.xml\""),
         ResultsDirectory = testsOutputDirectory
     };

     DotNetCoreTest(testsProjectfile, settings);
});

// Task("PublishTestResult")
//   .IsDependentOn("Test")
//   .Does(() =>
// {
//      var wc = new System.Net.WebClient();
//      wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit/ssagchqhpfic7dsx","C:/GitHub/Pushover.Client/build/Pushover.Client.Tests/TestResults.xml");
// });

RunTarget("Test");
