

# How does it work? #

Have you ever wondered how fast your fastest 5k really was? Most often your fastest 5k was run at a 5k race, so you would be able to find it simply by sorting the activities in your logbook by distance, and then find the fastest 5k by the 5k with the shortest time. But what if your fastest 5k was actually run during e.g. an 8k race? Or, what if you are not interested in your fastest 5k in all you activities but only your half marathon activities?

You may think what is actually involved in solving this problem with the 5k race. Well in the case of finding the fastest 5k during one of your half marathon it may not suffice just to look at e.g. the first 5k of the half marathon since the fastest 5k may actually be in an interval that do not start at the beginning of the race but e.g. from 12.2k to 17.2k. So you see, this would indeed by a quite tedious task to find this these results in SportTracks by hand, and I suspect that no one would actually do.

High Score plugin solves these types of problems. Actually the plugin is more general than just being able to solve the 5k problem we started out by looking at, it can actually tackle a range of problems similar to the 5k problem. What identify these problems is that they can be formulated in one of these two forms:
  * Find activity with <b>minimal</b> <i>X</i> per <i>some specified Y</i>.
  * Find activity with <b>maximal</b> <i>X</i> per <i>some specified Y</i>.

Here <i>X</i> can be time, distance, and elevation, and <i>Y</i> may be time, distance, elevation, heart rate zone, pace/speed zone, and heart rate zone and pace/speed zone.

I do admit they the two forms may look a bit cryptic at first but the they may be more understandable if we look at some examples:

<dl>
<dt>Find activity with <b>minimal</b> <i>time</i> per <i>5k</i></dt>
<dd>This is actually a reformulation of our initial example, so we can ask the plugin to find the activity to find the fastest 5k in some activities.</dd>
<dt>Find activity with <b>maximal</b> <i>distance</i> per <i>140 BPM - 160 BPM</i></dt>
<dd>This finds the activity where you have traveled furthest while staying inside the heart rate zone from 140 BPM to 160 BPM.</dd>
<dt>Find activity with <b>minimal</b> <i>elevation</i> per <i>10 minutes</i></dt>
<dd>This somewhat weird problem, will make the plugin find the activity with the largest descend in 10 minutes. An example of a result would be an activity with a descend of 100 meters in 10 minutes.</dd>
</dl>

These specified values I used in the examples, 5k , 140 BPM - 160 BPM, and 10 minutes, are all values that you can specify in the settings for the plugin, and that will then be used when solving the <i>find activity</i> problems  you propose.

As a result of solving the problem, the plugin does not only return the activity in question, but also tells you which part of the route the record was set. For example, for the problem <q>find activity with <b>minimal</b> <i>elevation</i> per <i>10 minutes</i></q>, the result would include a start and end GPS point which indicate how far you had traveled in the 10 minutes, where on the route the record started and where it ended.

The plugin will work both by selecting a collection of activities (e.g. all your half marathons) or on a single activity, and it will work equivalently either way. If you use it one a collection of activities the plugin will find the activities that a best in relation to the <i>find activity</i> problem, and in the case of just one activity it will not find any other activities, but simply report on the best solutions to the problems.

You don't need to specify each problem and then get the plugin to calculate it for you. The plugin automatically solves all the problems for you and present you with results that you can then browse through. If we look at how many different problems it actually solves it is the combination of <b>minimal</b>/<b>maximal</b>, <i>X</i>s, and <i>Y</i>s, so it is 2 `*` 3 `*` 6 = 36; in other words there are 36 different kinds of problems you can browse through after you applied the plugin. In addition, you can specify a grade filter, to avoid that all the high scores are displayed for downhills.

# How do I use it? #
## Introduction ##
  * Select the activities you are interested in (Daily Activity or Reports).
  * Select activity page (or popup from Analyze menu).

![http://gps-running.googlecode.com/svn/wiki/images/plugins/highscore/highscore.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/highscore/highscore.png)

## Suggested Use ##
The suggested usage is from [HighScore Trail](http://code.google.com/p/trails/wiki/Features#HighScore_Trail). The quick guide:
  * Select the activities you are interested in (Daily Activity or Reports).
  * Select Trails activity page
  * Select the HighScore Trail in the dropdown list.
The Trails plugin has many more features than the simple viewer in HighScore and offers for instance graphs, display on the map and a top list per result.

The settings described in next section applies to the HighScore trail too.

## Details ##
A good place to start when using this plugin is by looking at the settings. These can be found in the settings view under the <i>High Score</i> entry. Here you will see the five different categories of things (distance, time, elevation, heart rate, pace/speed zone) that the plugin uses to find the best solutions. By default the plugin comes with a wide range of settings which you may remove or add parts to if they do not suit you, and the plugin will use these updated values instead. Below is the settings panel.

![http://gps-running.googlecode.com/svn/wiki/images/plugins/highscore/Settings.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/highscore/Settings.png)

It is possible to see high scores for a single activity in the activity detail view or for collection of activities in the activity report view. The picture below show how to use the High Score plugin in the activity detail view: press the menu that lets you select splits, pace, elevation and so on and select the entry High Score.

![http://gps-running.googlecode.com/svn/wiki/images/plugins/highscore/ActivityDetailSummary.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/highscore/ActivityDetailSummary.png)

When you have selected the High Score plugin in the activity
detail view you will see something similar the picture below. At the top you can select if you want to see the maximum or minimum of either distance, time, or elevation over various values that is specified in the settings. In the picture below the setting is "Show maximal distance per specified speed zone. Show pace". This means that High Score plugin will find the longest distances where you where in a particular speed zone. The final part "Show pace" means that the speed is shown as time/length. If the settings was "Show speed" it would be shown as length/time. High Score Plugin show tooltips on each row in the table that tells which goal the result in the row corresponds to.
If you click the table, the corresponding part of the GPS track is marked.

To use the High Score plugin on multiple activities go to the activity report view and select some activities. Then open the activity page or select Analyze -> High Score for a popup. See following [link](http://gps-running.googlecode.com/svn/wiki/images/plugins/highscore/ActivityReport.png) for selecting the menu.

The popup dialog looks similar to the activity page.

# Trails Integration #
If [Trails plugin](http://code.google.com/p/trails/wiki/Features) is installed, HighScores can be viewed with detailed information in charts too. In addition, Trails can show HighScore for up to 10 activities for each goal.

# Caveats #
  * I have experienced that the GPS points measured by my Forerunner 305 may be off by a lot if on small distances. For example, I found that I once ran 200 meters in 9 seconds (which, if you are in doubt, did not happen). I expect other GPS devices have the same weakness on measuring short distances, so you may find that high scores for small distances, time periods as well, are not that reliable.
  * The plugin use the GPS points for an activity, and do not use manually entered distance.

# Feedback #
For patches, bugreports or feature suggestions, use the Google Code issue list.
For feedback please use the thread for this plugin at SportTracks  [forum](http://www.zonefivesoftware.com/SportTracks/Forums/viewforum.php?f=33).