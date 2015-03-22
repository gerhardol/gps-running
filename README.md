The project contains SportTracks plugins:

  * [High Score](#high-score) 
  * [Unique Routes](#unique-routes) 
  * [Overlay](#overlay) 
  * [Performance Predictor](#performance-predictor) 

Also includes the following plugins, no longer in development:
  * [Accumulated Summary](#accumulated-summary)
  * [TRIMP](#trimp)

See the links below for more information about the plugins.

### [High Score](http://github.com/gerhardol/gps-running/wiki/HighScore) ###
This plugin calculates various high scores using distance, time, elevation and other parameters. It is highly configurable so it is possible to calculate many types of combinations of these parameters such as min distance per distance, max elevation per time, and more. The results can be seen in a table and in a graph.

[SportTracks Plugin Catalog](http://www.zonefivesoftware.com/sporttracks/plugins/?p=high-score)

![](http://github.com/gerhardol/gps-running/wiki/images/plugins/highscore/highscore.png)


### [Unique Routes](http://github.com/gerhardol/gps-running/wiki/UniqueRoutes) ###

This plugin works on a single activity. It calculates all activities that are on that route. Since the route is defined implicitly it is not straightforward to match to routes against each other. Therefore it is possible to select how big a band around each GPS point you accept the other route to be within and also how many points you accept the other route may be outside your route with bands around all points. Also, it is possible to specify that a route has a direction so the plugin will ignore routes that even though they are on the same route are not in the same direction.

[SportTracks Plugin Catalog](http://www.zonefivesoftware.com/sporttracks/plugins/?p=unique-routes)

![](http://github.com/gerhardol/gps-running/wiki/images/plugins/uniqueroutes/uniqueroutes.png)


### [Overlay](http://github.com/gerhardol/gps-running/wiki/Overlay) ###
This plugin lets you overlay two or more heart rate, pace, and/or speed tracks, so you can see how your training is progressing.

[SportTracks Plugin Catalog](http://www.zonefivesoftware.com/sporttracks/plugins/?p=overlay)

![](http://github.com/gerhardol/gps-running/wiki/images/plugins/overlay/overlay.png)


### [Performance Predictor](http://github.com/gerhardol/gps-running/wiki/PerformancePredictor) ###
This plugin lets you predict your performance on various distances by using well-known methods such as Pete Riegel's model. The plugin works on a single activity, and use the distance and time of that activity to extrapolate how you perform on other distances.

[SportTracks Plugin Catalog](http://www.zonefivesoftware.com/sporttracks/plugins/?p=performance-predictor)

![](http://github.com/gerhardol/gps-running/wiki/images/plugins/performancepredictor/performancepredictor-timeprediction.png)
![](http://github.com/gerhardol/gps-running/wiki/images/plugins/performancepredictor/performancepredictor-training-training.png)


### [Accumulated Summary](http://github.com/gerhardol/gps-running/wiki/AccumulatedSummary) ###
The intention of this plugin is simply to allow the user to get some aggregated values of a collection of activities that are selected in the activity report view. The plugin will report combined distance, time, climbs, and more. The idea is that it should give the same type information as the activity view, but for a collection of activities instead.

Last build on Google Code: [SportTracks Plugin Catalog](https://code.google.com/p/gps-running/downloads/detail?name=AccumulatedSummaryPlugin-1.9.321.st3plugin&can=2&q=)
 
![](/gerhardol/gps-running/wiki/images/plugins/accumulatedsummary/accumulatedsummary.png)


### [TRIMP](http://github.com/gerhardol/gps-running/wiki/TRIMP) ###
Plugin calculates TRIMP values for a single activity in the activity detail view, and for multiple activities in activity report view.

![](http://github.com/gerhardol/gps-running/wiki/images/plugins/trimp/trimp.png)
