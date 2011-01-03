This folder contains code copied from Trails plugin. Up to date for SVN rev 218
The same files/structure is used in Matrix and GPS-Running Overlay/UniqueRoutes, to some extent ActivityPicture

Some changes from Trails, using #ifdef
 * PluginMain.GetApplication() -> MatrixPlugin.MatrixPlugin.GetApplication(), GpsRunningPlugin.Plugin.GetApplication()
 * Properties.Resources -> MatrixPlugin.Properties.Resources, GpsRunningPlugin.Plugin

Affects: TrailPointsProvider.cs, TrailMapPolyline.cs, CommonIcons.cs

CommonIcons.cs is customized compared to Trails, but identical to Matrix (using #ifdef).

TrailResult.cs is special for this plugin, but shared Overlay/UniqueRoutes #ifdef.

Integration handled similarily.
