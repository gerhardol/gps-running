

# What does it do? #
Ever wondered after running a killer race, how well you would perform at other distances. This plugin does this by using various models. The plugin implements several models to predict your performance, but more models could be added. Note that the current models applies to Running only, not applicable for other types of activities (except for Riegel to some extent).

The prediction models works better in some situations:
  * The distance used to base a prediction on must be comparable to the distance to predict for. So predicting a marathon from a 1km activity will be unreliable, but predicting a marathon from a half marathon is better.
  * You have to be trained for both the activity you base the prediction on and the distance you predict for. So you have to train for a marathon to predict it.
  * The models do not use the physiological effect often seen, that activities longer than 2h will be slower. So for instance to predict a marathon from a half marathon often give a little too optimistic result.

In all, the predictions are often considered a little too optimistic. Still, if you are interested in analysis of your best performances this sort of plugin should definitely  also be a part of your SportTracks plugin setup.

# Models #
The plugin presents all algorithms in the time prediction table and graph, so you can compare them. The selected algoritm is used in some other situations, like scaling interval paces and estimating from other plugins.

## Dave Cameron ##
The Dave Cameron model works as follows:
![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/davecameron.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/davecameron.png)

Here d<sub>old</sub> and t<sub>old</sub> both refer to the race you use as input to the model; i.e., the distance and time of the race. By selecting d<sub>new</sub> at various distances we can calculate the predicted time at these distances t<sub>new</sub>.

## Pete Riegel ##
The Pete Riegel model is similar to the Cameron model:

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/peteriegel.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/peteriegel.png)

The algorithm is designed for the "endurance range" about 5 to 120 minutes. By default the fatigue factor is set for running, but it can be used for some other sports too in the plugin settings: http://www.runscore.com/coursemeasurement/Articles/ARHE.pdf

## WAVA ##
World Master Athletics ([WMA](http://www.world-masters-athletics.org/)), formerly known as the World Association of Veteran Athletes (WAVA) compiles statistics for results for a number of distances. The statistics is adjusted so that 100% should be the same (or higher) than the world record for the sex, age and distance. (There are some exceptions for unique athletes that exceed 100%.)

The values can also be used to predict performances. If you perform at 60% of the world record at a certain age and distance, (with the usual reservations) many perform at 60% at other distances. The benefit of this model is that it takes into account that most runners loose speed before endurance. So for an older runner this model is often predicting better than the theoretical formulas. However all athletes are not aging the same way. See for instance [WikiPedia](http://en.wikipedia.org/wiki/Masters_athletics#Age-graded_tables) for a discussion about limits.

The WAVA algorithm works quite well for activities shorter than 2h, see [PerformancePredictor#Elinder](PerformancePredictor#Elinder.md) for a discussion of longer estimations.

## Elinder ##
Most athletes get a quite distinct performance drop for activities longer than 2 hours. The world records have a distinguished "knee" around 2h. The Elinder model has separate algorithms for activities shorter than the "knee" (infra distances) and longer (ultra distances), the knee is at the break even point.

Most algorithms compares to world records. So for instance for marathons where the world record is just over 2h, the world record is at the limit, most normal athletes will require more time than the estimations. The Elinder algorithm is developed by Fredrik Elinder and presented in a Swedish number of Runners World in May 2009. Some information including tables (in Swedish) are available here: http://www.ultradistans.se/wp-content/uploads/2014/05/FORMLER-F%C3%96R-BER%C3%84KNING-AV-L%C3%96PRESULTAT.pdf

The formula is designed for estimations longer than 2 km.

It is possible to adjust the break even time in the plugin settings. Note however that the implementation has limitations if the break even time is changed, the break even point may not be correctly selected and the prediction may be for a slightly different time than requested. The slope is correct, but the formula may not predict the same time/distance it was based on.
A similar issue may occur if the based time is longer than 2h.

# Training #
Besides predicting times the plugin also give suggestions regards to training. These suggestions are based on Jack Daniels's training philosophies. Currently, the plugin suggests training paces, pace for tempo races and interval split times.

# Extrapolation #
In addition to predicting other distances from a certain activity, the plugin can extrapolate data for the activity based on other types of data.

### Temperature ###
Compensate for higher lower temperatures. Slightly described in [Burton and Edholm 1969](http://coachsci.sdsu.edu/csa/vol36/rushall1.htm).

### Weight ###
The Weight affects performance. Some discussions at  [Runners World](http://www.runnersworld.com/weight-loss/whats-your-ideal-weight). However a too low weight will not improve the performance. The plugin will not present results lower than BMI 18.5 (if you have set your height correctly).

### Shoe ###
Light shoes improves performance, some research by [Jack Daniels](http://runsmartproject.com/coaching/2012/02/06/how-much-does-shoe-weight-affect-performance/). The ideal weight for a shoe is seldom zero, that value is speculative...

The default shoe weight is derived from the activity Equipment, the first device under 1 kg. Note that the plugin presents weight by shoe, so the equipment weight is divided by two.

### Age ###
Extrapolate the result to other ages, to see how you could have performed as younger or may perform as older. The calculation uses the WAVA tables that also can be used for prediction.

### Ideal ###
If the ideal conditions had occurred in your prime age... Your true personal best, when there is no other explanation. This tab has some assumptions about defaults. It is also possible to adjust the time/distance to estimate for other distances, to give an estimate or compare to others.

Something that is not included is Hills. This is included in Trails plugin, using Performance prediction integration.

Another unknown factor is Wind. Some estimations can be done using Weather and Power plugins.

Note: It is possible to change both the actual ("activity") and ideal ("optimized") values. These values are then used for prediction, training and extrapolation.

# How do I use it? #
The plugin uses the "best" prediction for all selected activities. The complete time and distance for the activities is used. If HighScore plugin is used, the fastest time for some distances is used in addition to the complete distances. The distances that HS searches for is 40% (by default) of the distances to predict for. This will give faster predictions if the activity has warmup/cooldowns.

The plugin can be used in both in the [Activity Page](PerformancePredictor#Activity_Page.md) and from [Edit](PerformancePredictor#Edit_menu.md). The result is the same. (The Edit menu remains from ST2 where an Activity page only could select one activity at a time.)

It is possible to manually override the source for the prediction. The time/distance can be manually changed in the activity page. The plugin displays one source for calculation, when manually overridden the font for the time/distance values changes from italics to regular.

Note though that the base activity and the distance to predict for should be similar. Using a 1km activity to predict a marathon is not reliable. To avoid to predict marathons from short runs, the plugin excludes activities shorter than than a certain percentage (default 10%), configurable in Settings.

If you click the prediction table (except when manually overriding), the activity and the corresponding used part of the activity is marked on the Route view. Double clicking the row will open the activity itself.

# Activity Page #
Go to the activity detail view and select the Performance Predictor plugin from the Analyze menu as shown [here](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/ActivityDetailView.png).

The side bar can be hidden, use the title menu (the triangle).

Here is a couple of images of the plugin in action:

## Time Prediction ##

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-timeprediction.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-timeprediction.png)

## Training suggestions ##

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-training-training.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-training-training.png)

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-training-pace.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-training-pace.png)

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-training-interval.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-training-interval.png)

## Extrapolate ##

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-extrapolate-ideal.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-extrapolate-ideal.png)

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-extrapolate-temperature.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-extrapolate-temperature.png)

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-extrapolate-weight.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-extrapolate-weight.png)

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-extrapolate-age.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/performancepredictor-extrapolate-age.png)

# Edit menu #

Select one or more activities in Daily Activity or Report View, go to the Analyze menu and select the "Performance Predictor" item. The information is the same as the Predictor View when invoked as an Activity Page.

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/EditFunction.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/EditFunction.png)

# Settings #

It is possible to add and remove the distances that the plugin use in its calculation by going to the settings panel for the Performance Predictor plugin as shown in this figure:

![http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/Settings.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/performancepredictor/Settings.png)

# Other Plugins #
## Trails ##
Analyze the trails in Performance Predictor.

## Unique Routes ##
Display the selected results.

# Todo #
  * Extrapolation based on more scenarios:
    * Elevation [RunningTimes](http://runningtimes.com/Article.aspx?ArticleID=10507&PageNum=3). See Trails plugin for a solution.
    * Weather?
  * HighScore based on "best estimation", extracting the best part of activities. This can be calculation intensive, without giving much information.
  * Other algorithms like Purdy or Vdot? http://run-down.com/statistics/calcs_explained.php

# Feedback #
For patches, bug reports or feature suggestions, use the Google Code issue list.
For feedback please use the forum for this plugin at SportTracks forum [here](http://www.zonefivesoftware.com/SportTracks/Forums/viewforum.php?f=30).