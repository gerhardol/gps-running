
2.1.3 2015-09-
Overlay, HighScore, UniqueRoutes, PerformancePredictor
  * Updated version numbering after move to GitHub
  * Updated URL for documentation links to github from Google Code
  * Updated Spanish translation
  * Updated some layout, to work better with translations like Spanish
  * Merge from Trails: Custom pace/speed where distance was other than 1 (like min/100m) was not calc correctly

2.0.467 2015-01-30 (Plugin Catalog)
PerformancePredictor:
  * Add [Elinder](http://github.com/gerhardol/gps-running/wiki/PerformancePredictor#elinder) predicting. Works very well, especially for activities longer than 2h.
  * Predicting on many activities no longer requires High Score Plugin, it uses the "best" activity to predict.
  * Possibility to override the time/distance directly in the toolbar (this was possible in the Extrapolate - Ideal tab previously).
  * Show all predictor models in the table. The selected model is shown first and used in training suggestions etc.
  * Reenable chart, show pace/speed in addition to time over distance.
  * Riegel factor is configurable - possibly usable for other activities than running
  * Many minor updates and bugfixes.


2.0.419 2014-10-10 (Plugin Catalog)

HighScore:
  * Selection settings are available in Settings, in addition to the Activity page. This is to simplify use from other plugins, like using Trails as a viewer for results.
  * A one time popup when selecting the activity page, that informs about Trails integration.
  * Something like "Minimum Time over Time" does not make sense, no results can be displayed. The plugin now changes the "the other changed value" to Distance or Time instead to simplify handling.

UniqueRoutes:
  * Make category selector available in the Settings (in addition to the Activity page). This is to simplify use from other plugins, like using Trails as a viewer for results.
  * A one time popup when selecting the activity page, that informs about Trails integration.

Overlay:
  * Compare heart rate and distance differences correctly, when a track is not starting at the same time as the activity.
> Note:
> > - Differences over time still has the offset problem.
> > - Offsets are always over time, also when Xaxis is Distance.
  * A one time popup when selecting the activity page, that informs about Trails plugin.

2.0.411 2014-09-14

Overlay:
  * Tracks that did not start at activity starts did not display offset in charts.
> > Still an issue in diffs.

2.0.410 2013-09-22 (Plugin Catalog)

PerformancePredictor:
  * Utopia tab renamed to Ideal
  * Integration with Trails: Ideal prediction for trails
  * Swedish translation update.
  * Save Selected tab (Prediction, Training or Extrapolation)

2.0.395 2013-06-22 (Plugin Catalog)

PerformancePredictor:
  * Age grading as PredictionModel. See the updated documentation: [WAVA](http://github.com/gerhardol/gps-running/wiki/PerformancePredictor#wava).
  * ExtraPolation new tabs. The effects of Shoes, Age and Utopia (everything combined) can be seen. [Extrapolation](http://github.com/gerhardol/gps-running/wiki/PerformancePredictor#extrapolation).

Other changes:
  * Do not show Weight results below lowest possible "Ideal" weight at 18.5. (The actual limit is likely higher).
  * The "closest" row was not always marked for temperature
  * Extrapolate temperature was always set to 15 degrees instead of using the actual temperature
  * When selecting several activities, the start distance and pace was incorrectly displayed.

  * Overlay: Exception occurred if "Select with UniqueRoutes" menu item was selected but UR was not installed.

  * HighScore: Activities without elevation did not display any data.

2.0.366 2013-01-01 (Plugin Catalog)
PerformancePredictor updates - Google Code [issue 28](https://github.com/gerhardol/gps-running/issues/28)
  * Disable Analyze menu item for multiple activities if HS is not installed
  * Show HS label if multiple activities and HS is not installed
  * Training and Extrapolate views could only be seen after switching activities
  * Avoid null exceptions in Temperature view in certain situations

2.0.360 2012-10-28 (Plugin Catalog)
  * HS: Provide more than one result to Trails

2.0.359 2012-08-04 (Plugin Catalog)
  * Overlay: Integration with Compare Planned plugin

2.0.358 2012-08-04 (Plugin Catalog)
  * PP, HS: Prepared for integration with Trails plugin - Popup based on selected trail results. This has required many internal changes to the plugins.
  * Some speedup in HS. Previously, all results were always calculated. This has been changed to only calculate the selected result. This decreases the time to see a result for many activities significantly. In addition, there are some internal speedups.
  * Save HS popup size
  * PP: Interface for Trails, to get Vdot and predict distances in Trails list.
  * UR: PP popup was normally disabled
  * PP, HS, Overlay: Popup from UR did not always work, some risk for null exceptions. (Problem has not been reproduced in tests though, but could teorethically occur.)

2.0.336 2012-03-21
  * HS: Min Grade could not be set to negative values

2.0.332 2011-12-13
  * PP: SpeedZone settings update
  * UR, Overlay: Minor Italian translation update
  * HS, PP: Show ProgressBar when calculating in popup and after activity selection update.
  * UR: If not SelectedTime was set selection did not work (so if other plugins set some other, CommonStretches did not work)

2.0.322 2011-07-30
  * All: Updated German, Italian, French and Spanish translations
  * AS: Visual updates
  * HS, PP: Minor fixes

2.0.314 2011-04-08
  * Released UniqueRoutes, Overlay, HighScore and PerformancePredictor as 2.0. No updates are planned for AccumulatedSummary and Trimp.
  * A few copy list issues in HS/PP

1.9.311 2011-04-07
  * HighScore: Updated User interface, using SportTracks tables

1.9.307 2011-04-05
  * PerformancePredictor: Updated User interface
    * Action banner with menu, Sidebar can be hidden
    * Using SportTracks tables
    * Split Training view in Training/Extrapolate

1.9.289 2011-03-22
  * Overlay, UniqueRoutes, PerformancePredictor: Updated French translation

1.9.286 2011-03-19
  * All: Release resources when popup is closed. This could give a crash when editing an activity after or slower performance. See [issue 24](https://github.com/gerhardol/gps-running/issues/24).
  * Overlay using Track start time when displaying charts, to handle misformatted activities.

1.9.271 2011-02-19
  * UR: Direction check also for route snippets
  * UR: GPS Radius display should be displayed as twice the prev distance
  * UR: Longitude internal grid was smaller than latitude grid away from the equator - plugin could fail to find a few routes (low probability).
  * Some GUI cleanups

1.9.262 2011-02-17
  * Overlay: Stopped time excluded in list summary

1.9.258 2011-02-03
  * Overlay: Spanish translation update

1.9.257 2011-01-29
  * Italian translation

1.9.256 2011-01-22
  * HighScore: [issue 20](https://github.com/gerhardol/gps-running/issues/20) Default value for min grade

1.9.252 2011-01-18
  * HighScore: GUID fix

1.9.247 2011-01-15
  * HighScore: Integration in Trails - High Scores with charts and compare
  * HighScore: Possibility to set "min grade" to accept for High scores - to avoid that all (shorter) high scores are down hill
    * Limited number of default values at settings reset (and for new installations).

1.9.244 2011-01-13
  * PerformancePredictor: Click in table to see part that HighScore integration used to predict performance.

1.9.240 2011-01-12
  * HighScore: Click in table to show where the HighScore occurs

1.9.237 2011-01-11
  * UR, Overlay: Add ColorSelector
  * Slightly darker marked tracks

  * Overlay can use CommonStretches in Unique Routes to set Offset
  * Overlay: Zoom to all activities at activty change
  * Overlay: Show current reference activity in the menues
  * Overlay: At startup, the chart bar was always shown independently on the settings.
  * Overlay: Add list colums: Category, FastestSpeedMetersPerSecond, MaxPace, MaximumHeartRate, MaximumCadence, MaximumPower

  * Unique Routes: Improved Common Stretches, enable upcoming support in Trails

# Changes in SportTracks 3 before 1.9.224 #
Very brief summary, see the specific plugin documentation for details.

## All plugins ##
  * Support for multiple activities in Activity Pages
  * Corrections and UI updates

## Overlay ##
  * Overlay has a redesigned UI, better confirming with ST interface.
  * Show activities on the Route view
  * Many new features like diff charts
  * Offset in Graphs

## Unique Routes ##
  * Show activities on the Route
  * Route Snippets: Find parts of routes in other activities
  * Improved Common Stretches algorithm
  * Updated UI, using ST lists

## SportTracks 2 ##
  * SportTracks 2 is now no longer supported. ST2 has been supported when possible also for some new functions mentioned. There are builds from the end of December for ST2 users. ST2 support will also be removed from the code over time.

## TRIMP ##
  * TRIMP is not in the official ST3 plugin catalog but has so far been updated. This will no longer be done.

## Changes from Kristian Bisgaard Lassen's version to initial ST3 version ##

### Background ###
Kristian Helkjaer Lassen (Kristian Bisgaard Lassen before he got married) has put up the code for his plugin here at Google Code. I enjoy Kristian's plugins very much, they have worked really well for me. Most of the issues posted for the plugins has been that the download site is down or that there are exceptions when installing in other locations. It is likely that some work is required for next major ST version and Kristian has indicated that he not really has time/interest to maintain the plugins right now. Every thing has it it time. Open sourcing the code lets everyone take over the maintenance. Right now, ST user gerhard has access rights to the code and has started to do some work with the plugins.

Kristian has added me as a committer on Google Code, I have done some work with the plugins (a lot of the changes were started when I was home with a cold but not had to stay in bed all day). There are not many visible changes. See the Changes page on Google Code for details.

### Details ###
  * Plugin settings are now stored in the ST file Preferences.System.xml file instead of in individual preferences.xml files. The plugin will currently try to parse the old settings file if no information is found in the Preferences.System.xml.
  * Plugins are adapted to .st3plugin/.st2plugin format. The plugins were created before the format existed and required some changes to work. Note that most exceptions reported in the forums seem to occur when users have repackaged the plugins in .st2plugin format.
  * Works in Mono/Linux, except AccumulatedSummary that uses browser integration. There are no specific changes to achieve this, just the change of storage for preferences was necessary.
  * Cleaned up labels in graphs/tables and table format. Using the translation information built in to SportTracks for common information like speed/pace etc. This decreases the job required to make translations, both for making the translations, but even more to handle the strings when building. Another benefit is that this makes the user experience more consistent (always the same format for something like km/h). Note that this information was not available in ST when the plugins were first written, it has been added in a later update.
  * More localization, following the ST settings for distance, temperature etc.
  * Input of distance, time can be done in the ST way, i.e. "5km" is handled correctly even if you have the distance set to Miles. For Speed this not available. For Pace, the time is always per "distance unit" and can be entered as standard time (like 5:30) or in minutes (5.5 or 5,5 depending on your DecimalSeparator settings).
  * Documentation is just transferred from the old site at gpsrunning.nicolajsen.nl
  * UniqueRoutes, HighScore: Double click on activity to go to it. UR saves sorting of tables.
  * UniqueRoutes tries to match "Common Stretches" with the reference activity (visible as tool tips).

### Bugfixes ###
  * Various update issues, like ST restart was required to see changed settings for UniqueRoutes.
  * Some unit display issues, like Elevation summaries in AccumulatedSummary were always in meters, not in the elevation unit settings.

# Future plans #
If you want to participate, please join. gerhard and staffann currently drives development, the current job is to only minor changes to the functionality, just updating the structure. There will likely be more additions over time. Some thoughts:

  * UniqueRoutes "hints" to other plugins where matches were found, using the "Common Stretches". This is for Overlay and DotRacing to match graphs better.
  * PerformancePredictor: Age tables?

  * TRIMP plugin: No plans for it, I would like recommend users to use Training Load instead.
  * Accumulated Summary: No immediate plans.

### Related plugins ###
The Unique Routes plugin is integrated in other plugins.
MapOverlay/DotRacing should work without changes.
Matrix plugin requires an update.

### Migration ###
  * The old plugins under %ProgramFiles%\Zone Five Software\SportTracks 2.1\Plugins must be removed manually.
  * The settings files can be removed after first startup (but there is no harm keeping them if you delete the Preferences.System.xml file, without a backup).
```
%APPDATA%\
  AccumlatedSummaryPlugin (always empty)
  HighScorePlugin
  OverlayPlugin
  PerformancePredictorPlugin
  TRIMPPlugin
  UniqueRoutesPlugin
```

# Feedback #
For patches, bug reports or feature suggestions, use the Google Code issue list.
For other feedback please use the SportTracks forum or this wiki.