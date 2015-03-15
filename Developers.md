

This is not a general SportTracks plugin description. See for instance [here](http://www.zonefivesoftware.com/SportTracks/Forums/viewtopic.php?t=4828) for more information about developing ST plugins.

Some specific notes for gps-running plugins, as well as many other plugins gerhard is involved in. The plugins shares code in addition to the project setup.

### Getting Started ###
The ST plugin API is described on the SportTracks site. Please sign up to the Developer Network before working with ST plugins (legal requirement).
http://www.zonefivesoftware.com/sporttracks/developers/

MS Visual Studio Express 2013 is used, but at the time of conversion versions after 2008 should happily work with the projects. The solution file can be considered as a template only (use your own copy) and is in VSE 2012 format.

This project is adapted to be used without user changes. To achieve this in a simple way, the common paths etc are separated to a common file:
  1. Program paths etc uses environment variables
  1. Some common build actions (a few common files)
  1. Build actions like creating .st3plugin
  1. References to SportTacks libraries etc are dependent on the ST version

The same setup can be used in other plugins too, done in MiscPlugin, !ST2Funbeat, Trails, ActivityPicture, NordicMaps (hitta.se), ApplyRoutes and Matrix (on CodePlex), as well as patches for ExportCrs, HTML Export Plugin and CSV Import. See one of the project files as an example.
Each .csproj contains a few specific settings and the specific files for the plugin only. The common setting files are imported.
The plugin GUID must be added manually. The GlobalSection in the .sln must be updated manually (you probably want to use your own .sln file, copy the file in the project).

Note the following issues with the setup:
  1. At startup, VSE warns that files are imported (can be avoided by adjusting the registry).
  1. There are build warnings that all references cannot be found at builds (if both ST2 and ST3 are supported).
  1. You must adjust the Solution properties when using a configuration the first time, otherwise your project will not be build.

This solution was contributed by old\_man\_biking on the SportTracks forum.

### SportTracks 2 ###
Until December 2010, the plugin supported both ST2 and ST3. Where there was a difference, the code was compiled with "#ifdef ST\_2\_1". Many new functions only works with ST3, but the code base is the same.
The ST2 specific code may be removed. To continue the ST2 builds, use the last ST2 commit or reenable the ST2 build:
  * ST references in Common/Common-References.csproj
  * Re-add ST2 configurations in the .sln file

### Install packages ###
The build event copies the built files to "%ALLUSERSPROFILE%\ZoneFiveSoftware\SportTracks\2.0\Plugins\Update" (similar for ST3) automatically, so you can have ST running while building, just restart when you want to use the new binaries.

The build event also creates .stplugin packages. This should be automatic if you have 7-zip installed in the standard location, otherwise edit the paths. If you have Cygwin with perl installed, the version number is set on the install package too.

### Translations ###
The master copy for translations is done in a Spreadsheet, the .resx files are generated from the source. The master Resources.resx and StringResources.resx files must be updated correctly for each plugin, the i18.pl script described below updates the translations with the info from the spread sheet.

The generally used "master copy" of the spreadsheet is a Google Spreadsheet document.
https://docs.google.com/spreadsheets/d/1gy-zMCf1eyEjX49F5ZcV4eZTDDsGC7qKJIfOMy7082c/edit#gid=0

The spreadsheet contains several plugins, each with a separate sheet.

Note: The i18.pl script is setup to pull the spreadsheet contents automatically. The functionality is not working in a simple way after Google changed the API, there are workarounds. The perl script uses the wget utility, that must be setup for SSL, for instance Cygwin GNUtls package.

Instructions:
  1. Update Resources and StringResources in the plugins correctly.
  1. Add all master strings and the master language to the spreadsheet. All fields in the plugin must be included here, the script below will not change the "master" .resx, each translation will have the same fields as the master. Another way to express this: The English strings in the second column is not pushed to the .resx files at generation. The script will list the differences, if you have KDiff3 installed the differences will be shown char by char.
  1. If the information cannot be pulled with the script:
    1. Save a .xlsx (possibly changed to .ods) in the repository. (several plugins use same spreadsheet) https://docs.google.com/spreadsheets/export?id=1gy-zMCf1eyEjX49F5ZcV4eZTDDsGC7qKJIfOMy7082c&exportFormat=xlsx
    1. Save the file in .csv format, Field separation ",", specify the character format to be in UTF-8 (important!). These settings are default in Google SpreadSheet, but may have to be adjusted in OpenOffice/Excel. For GPS-Running (gid param differs): https://docs.google.com/spreadsheets/export?id=1gy-zMCf1eyEjX49F5ZcV4eZTDDsGC7qKJIfOMy7082c&exportFormat=csv&gid=0
  1. The script that updates translations is written by ST user markw65 (post about this in the [invitational ST developers forum](http://www.zonefivesoftware.com/SportTracks/Forums/viewtopic.php?p=34501#34501) and used in the ApplyRoutes plugin, with additions. The script is written in perl. I use the script in Cygwin, but other perl versions should work too (strawberry Perl is known to be working). The script requires some perl modules like XML::Simple. You will get messages when running it...
  1. The script requires no parameters when pulling the spreadsheet from the net. If the copy is local, check the script util/i18n.pl, to see how to adapt inparameters as paths. Example how I run it from the trunk directory with a local file:
> `$ util/i18n.pl . . .`
> > or

> `$ util/i18n.pl file:g:/Users/go/dev/gc/gps-running/Resources.csv .`
    1. Some features of the script:
      * Pull the files from Google
      * Check that spreadsheet and resx matches. Warn if definitions are completely missing. If the definitions differ, use commandline diff or optionally KDiff3 to display differences.
      * Show where there are .resx files but no translation
      * List number of matching and missing strings when writing the .resx
      * See the strings missing, tab separated so the output can be pasted to a spreadsheet. Run with verbose: `$ env VERBOSE=10 util/i18n.pl . . .`

### Util ###
Some code (and part of the translation) is shared between the plugins. The source files (no libraries)  is put under Common/.
All plugins share the same namespace to get over limitations with a previous setup. To share the .resx files with separate name spaces, something like the following is needed to adjust namespace in the resx. (Another omb tip.)
> 

&lt;LogicalName&gt;

GpsRunning.Resources.Resources.fr.resources

&lt;/LogicalName&gt;



Previous setup: Shared code was not in a separate library but in duplicated code (only the namespace differed originally). A simple script is used to copy and replace namespace. (There is a problem making this to a library, as there is a reference to the application.) A simple shell script (run in Cygwin) that copies .cs and StringResources.resx from a "master" plugin to the others and updates namespace is available:
`$ util/cputil.sh PerformancePredictor`

### Profiling ###
The project has some "built-in" support for profiling. This can be achieved with other means.
For the hobbyists that do not want to minimize costs, the profiler at http://www.eqatec.com/ is free with a limitation of 3 traced files. (I have seen other that offers trials.)

The project configuration "Profile" is similar to "Release" except that the build script copies the plugin files as well as the SportTracks executables to the plugin "Installed" catalog.
Changes done:
http://code.google.com/p/trails/source/detail?r=635

After that, point the profiler to the files in that folder (including SportTracks.exe). Select the ST.exe and primary plugin .dll files, build and run in the profiler, then analyze the bottlenecks.

### Problems ###
???