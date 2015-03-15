Note: Depreciated. You may want to take a look at the [Training Load](http://www.zonefivesoftware.com/sporttracks/plugins/?p=training-load) plugin instead.
Training Load plugin does not only calculate TRIMP, but also Training Load, that allows for better analysis.

No updates are planned for this plugin.

### What does it do? ###
TRIMP stands for <b>TR</b>aining <b>IMP</b>ulse and it is a simple meaurement of how hard you train. TRIMP is also known as the <b>training exertion factor</b>. It is calculated by adding each product of minutes spend in a heart rate zone together with a factor depending on that zone. The standard setting for this plugin is that we have heart rate zones  50%-60% (zone 1), 60%-70% (zone 2), 70%-80% (zone 3), 80%-90% (zone 4) and 90%-100% (zone 5) of maximal heart rate (you can set the maximal heart rate in the athlete view). Each factor has a corresponding factor which in the default settings are 1 (zone 1), 1.1 (zone 2), 1.2 (zone 3), 2.2 (zone 4), and 4.5 (zone 5). It is, however, <b>possible to change</b> these heart rate zones and/or the factors they are associated with in the settings for the plugin.

Let's look at an example on how the TRIMP value is calculated. Assume we have exercised for one hour and you spend 40 minutes in zone 3 and 20 minutes in zone 4. With the default settings the TRIMP plugin uses the same factors as Polar use in their training software, so it is 1, 1.1, 1.2, 2.2, and 4.5 for their respective zones. This mean that the TRIMP value for the exercise is 40 &#183; 1.2 + 20 &#183; 2.2 = 48 + 44 = 92.

As the above example show the TRIMP value may seem arbitrary but by comparing TRIMP values of exercise session it is possible to <b>detect if you are overtraining</b>.

This plugin allow you to select a single or multiple activities and calculate TRIMP values. For multiple activities the minutes spend in each activity is simply added before calculating the TRIMP value.

Read more about TRIMP <a href='http://www.pponline.co.uk/encyc/training-schedules.html'>here</a>.

### How do I use it? ###
To change the settings go to the settings view and select the TRIMP plugin. Below is an example where I have changed the number of zones to 9, the start zone to 37% and set the factors to 1,2,...,9. Also, in the figure I have annotated which parts are editable.

![http://gps-running.googlecode.com/svn/wiki/images/plugins/trimp/Settings.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/trimp/Settings.png)

You can use the plugin to calculate either in the activity detail view or in the activity report view. Below is shown how the plugin looks:

![http://gps-running.googlecode.com/svn/wiki/images/plugins/trimp/ActivityDetailView.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/trimp/ActivityDetailView.png)

For using the plugin on multiple activities go to the activity report view and select a collection of activities by either holding down control and select some activities individually, or holding down shift and select a list of activities by first select the start activity and second select the end activity. Here is a situation where I've selected some activities by holding down control and selected some activities:

![http://gps-running.googlecode.com/svn/wiki/images/plugins/trimp/ActivityReportView.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/trimp/ActivityReportView.png)

In the case where you have calculated the TRIMP value for a collection of activities it may be nice to see the individual TRIMP value per activity. You do this by selecting the <i>Graph</i> pane in the TRIMP result window as shown below.

![http://gps-running.googlecode.com/svn/wiki/images/plugins/trimp/Graph.png](http://gps-running.googlecode.com/svn/wiki/images/plugins/trimp/Graph.png)

### Feedback ###
For patches, bugreports or feature suggestions, use the Google Code issue list.
For feedback please use the thread for this plugin at SportTracks forum [here](http://www.zonefivesoftware.com/SportTracks/Forums/viewforum.php?f=29).