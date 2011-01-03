This folder contains code copied from Trails plugin. Up to date for SVN rev 218
The same files/structure are planned for other plugins like GPS-Running Overlay/UniqueRoutes/Trails

Some changes from Trails, using #ifdef
 * PluginMain.GetApplication() -> MatrixPlugin.MatrixPlugin.GetApplication(), GpsRunningPlugin.Plugin.GetApplication()
 * Properties.Resources -> MatrixPlugin.Properties.Resources, GpsRunningPlugin.Plugin

Affects: TrailPointsProvider.cs, TrailMapPolyline.cs, CommonIcons.cs

CommonIcons.cs is customized compared to Trails, but identical to Matrix (except for a #ifdef).

TrailResult.cs is special for this plugin, but shared Overlay/UniqueRoutes.

Integration handled similarily.
