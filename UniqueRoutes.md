

Note: This wiki is only partly updated to the features in this plugin.

![http://gps-running.googlecode.com/svn/wiki/images/uniqueroutes.png](http://gps-running.googlecode.com/svn/wiki/images/uniqueroutes.png)

# What does it do? #
When you run outside it really matters what route you take if want to make a personal record. This is why it does not always make sense to compare, e.g., two 5k run on two different routes if one route is very hilly and the other is not. And worse, it may not make sense to compare two activities that cover the same route but are in the opposite direction. For example a route such as A -> B -> C (the route from A to C via B) may be more difficult than a route that cover the same points but are in the opposite direction; C -> B -> A. For example, running a route with a long slow ascend that ends with a sharp descent are often (that is what I experience) different in difficulty than the route that start with a sharp ascend and ends with a long slow descend. Therefore, manually trying weeding out all the routes that are similar to some activity that you are considering in SportTracks is - and I think you agree - very tedious.

So what is that this plugin does? It finds, for some activity, any activities with routes that are similar to that activity. This way you can get an overview of all the activities that you consider.

The way it goes about evaluating if two activities are similar it tests if the two respective GPS tracks are close to each other. You may wonder what is meant by <i>close to each other</i>. Since GPS devices are not perfect we may expect that two routes, even though we know they are similar, may be dissimilar locally, due to error GPS measurements. Therefore it is possible to state in the settings for the plugin that two routes are similar even though some <i>X</i> percent of the routes are not.

# Search for Similar Routes #
The plugin can be used in several ways, also from other plugins.

The suggested usage is from [UniqueRoutes Trail](http://code.google.com/p/trails/wiki/Features#UniqueRoutes_Trail). The quick guide:
  * Select the activity.
  * Select Trails activity page
  * Select the UniqueRoutes Trail in the dropdown list.
The Trails plugin has many more features than the simple viewer in UniqueRoutes and offers for instance graphs and display on the map.

The plugin specific activity page is activated by selecting the [activity detail page](http://gps-running.googlecode.com/svn/wiki/images/plugins/uniqueroutes/ActivityDetailView.png), both in Daily Activity View and Activity Reports view.

## Activities searched ##
### Single Activity ###
If a single activity is selected, all activities matching the configured category is used.
The single activity is always used as the reference activity.

### Multiple Activities ###
If several activities are selected, these activities are used. The category is not used as a filter. So if you select a month in Daily Activity, those activities are used in the search.

## Reference selection ##
### Reference activity ###
The normal way to use the plugin is to select a single activity and find activities similar to it. The activity used can be changed in the list context menu (right click the list or the refresh button).

### Selected Route Snippet ###
Instead of using a complete activity, the plugin can use a part of an activity, or a route "snippet".
Select part of a track (right click and drag), then press refresh button to use a "snippet" of an activity.
In this case, the complete route snippet (with a percent outside) must be included in the tested activities, the begin/end settings are not used.

## From Other Plugins ##
Several other plugins has implemented support for directly using the Unique Routes plugin. Other plugins use the same settings as in Unique Routes (Radius, Category filter etc).

  * Search for similar activities:  [Overlay](http://www.zonefivesoftware.com/sporttracks/plugins/?p=overlay), [Matrix](http://www.zonefivesoftware.com/sporttracks/plugins/?p=matrix), [Dot Racing](http://www.zonefivesoftware.com/sporttracks/plugins/?p=dot-racing)
  * Search for similar trails (route snippets/parts of activities): [Trails](http://www.zonefivesoftware.com/sporttracks/plugins/?p=trails) - allows viewing the results in Graphs too. See [UniqueRoutes Trail](http://code.google.com/p/trails/wiki/Features#UniqueRoutes_Trail) and [Add similar with UniqueRoutes](http://code.google.com/p/trails/wiki/Features#Add_similar_with_UniqueRoutes) for more features.
  * Mark [Common Stretches](UniqueRoutes#Common_Stretches.md) to mark the parts matching the reference result. [Trails](http://www.zonefivesoftware.com/sporttracks/plugins/?p=trails)
  * Use [Common Stretches](UniqueRoutes#Common_Stretches.md) to set offset. [Overlay](http://www.zonefivesoftware.com/sporttracks/plugins/?p=overlay)

# Features in the activity page #
## Map related ##
  * Click activities (ctrl-click or shift-click for multiple activities) in the list, and they will show on the map.
  * Click on an activity in the list to mark [Common Stretches](UniqueRoutes#Common_Stretches.md). If a single activity is shown (the standard SportTracks track in the Route panel) the corresponding parts in the reference activity is shown too.

## Send to other plugins ##
  * Search plugin: Select activity results, then right-click in list, Analyze -> Select marked results in activity list.

Unique Routes can send results for analyze directly to other GPS-Running plugins:
  * [Overlay](http://www.zonefivesoftware.com/sporttracks/plugins/?p=overlay)
  * [High Score](http://www.zonefivesoftware.com/sporttracks/plugins/?p=high-score)
  * [Performance Predictor](http://www.zonefivesoftware.com/sporttracks/plugins/?p=performance-predictor)
  * [Accumulated Summary](http://www.zonefivesoftware.com/sporttracks/plugins/?p=accumulated-summary)
  * TRIMP - depreciated plugin

## CommonStretches ##
The Unique Route plugin is relative forgiving in how routes are considered identical. A configurable part of points can differ, there is also possibilities to skip start and end of activities.

The Common Stretches functionality will try to match activities to the reference activity, so each snippet corresponds to the reference activity.
This functionality is still experimental.

# Settings #
The settings for the plugin are shown below, and it is here you specify when the plugin should identify two routes as being identical.

There are also some configurations from the list context menu.

![http://gps-running.googlecode.com/svn/wiki/images/plugins/uniqueroutes/Settings.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/uniqueroutes/Settings.png)

# Internals #
The plugin generally works fine detecting similar routes, but due to optimizations it can detect routes that are in the same area, but but can be considered to follow other areas. Using the Common Stretches approach will be very slow with many activities.

The plugin works by setting up a matrix with twice the radius for the quadrant size. If the GPS points for the reference activity and the tested activity is in the same boxes, the activities are considered the same (there is a special trick for direction too). So the plugin will not work well in the following situations for instance:
  * If the radius is close to or shorter to the distance between GPS points.
  * As the grid lines are not related from the reference activity, the plugin could in rare situations not find activities if the grid line is between the reference and the tested activity.
  * Zig-zag may confuse the algorithm.
  * Direction check can be exclude activities for out and back activities where the path is very alike.

A general recommendation is therefore to set the radius higher "than what you would expect".

# Feedback #
For patches, bug reports or feature suggestions, use the Google Code issue list.
For feedback please use the thread for this plugin at SportTracks forum [here](http://www.zonefivesoftware.com/SportTracks/Forums/viewforum.php?f=32).