# Technologies #
* .NET
    * ASP.NET MVC 5
    * Mongo C# Driver 1.10
    * Unity for dependency injection
    * Elmah for error logging
    * Newtonsoft JSON for JSON serialization
* AngularJS 1.4
    * UI Bootstrap
    * Angular Route
* Bootstrap 3
* MongoDB
* NuGet for .NET packages
* Bower for JS and CSS packages
* Gulp for bundling and minification

# The VS solution #
* VS version: 2015
* Projects:
    * **`Ssi.TrackTruck.Web`**: MVC-5 project containing the client side and the MVC-5 controllers.
    * **`Ssi.TrackTruck.Bussiness`**: Class library containing the business logics

# Deployment #
1. Clone/pull the release repository to your development machine from https://bitbucket.org/truckmonitoring/ssi-logistics-release.
1. Update the main repository to `test-release` branch.
1. Merge `dev` branch of the main repository with `test-release` branch.
1. Publish the project Ssi.TrackTruck.Web to the location. Use Custom publish and choose *file system* method. **DO NOT** check "Delete all existing files prior to publish".
1. Check if the Web.config file is OK. Specially the App settings `MONGOLAB_URI`.
1. Commit and push the release repository. Please check the previous commit messages and use the same pattern.
1. Clone/pull https://bitbucket.org/truckmonitoring/ssi-logistics-release to the deployment server.
1. Update to the head of default branch.
1. Set the IIS web application to use the root directory of the release repository.
1. In your development machine, update main repository to `dev` branch.
1. Open `AssemblyInfo.cs` file of `Ssi.TrackTruck.Web` project.
1. Increament third part of `AssemblyVersion` and `AssemblyFileVersion`.
1. Commit the main repository with message version updated to x.y.z
1. Push the main repository.