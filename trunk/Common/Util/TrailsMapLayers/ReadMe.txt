This folder contains code copied from Trails plugin. Up to date for SVN rev 209
The same files/structure are planned for other plugins like GPS-Running Overlay

Some changes from Trails:
 * PluginMain.GetApplication() -> MatrixPlugin.MatrixPlugin.GetApplication(), GpsRunningPlugin.Plugin.GetApplication()
 * Properties.Resources -> MatrixPlugin.Properties.Resources, GpsRunningPlugin.Plugin

Affects: TrailPointsProvider.cs, TrailMapPolyline.cs, CommonIcons.cs

CommonIcons.cs is customized compared to Trails, but identical to Matrix (except for plugin references).

 TrailResult.cs is special for this plugin.
