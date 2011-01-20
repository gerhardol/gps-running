/*
Copyright (C) 2009 Brendan Doherty

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;

using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Forms;

//using TrailsPlugin.Data;

namespace GenericLineChart {
	public partial class GenericLineChart : UserControl {
        private GenericChartDataSeries m_refDataSeries = null;
        private IList<GenericChartDataSeries> m_dataSeries = new List<GenericChartDataSeries>();
        private XAxisValue m_XAxisReferential = XAxisValue.Time;
        private IList<LineChartTypes> m_YAxisReferentials = new List<LineChartTypes>();
        private LineChartTypes m_defaultYAxisReferential = LineChartTypes.Speed;
        private Color m_chartFillColor = Color.WhiteSmoke;
        private Color m_chartLineColor = Color.LightSkyBlue;
        private Color m_chartSelectedColor = Color.AliceBlue;
        private ITheme m_visualTheme;
//        private ActivityDetailPageControl m_page = null;
//        private MultiChartsControl m_multiple = null;
        private bool m_visible = false;
        private bool m_updateInProgress = false;
        private bool m_alwaysUseSeriesColor = true;
        //TODO: Add possibility to set units from outside of this class
        private Length.Units m_lengthUnit = Length.Units.Kilometer;
        private Length.Units m_elevationUnit = Length.Units.Meter;
        private Length.Units m_speedDistanceUnit = Length.Units.Kilometer;
        private Length.Units m_paceDistanceUnit = Length.Units.Kilometer;

        private IList<DateTime> m_timeSplitsPoints = new List<DateTime>();

        //TODO: Add possibility to set precision
        private int m_distancePrecision = 0;
        private int m_heartRateBPMPrecision = 0;
        private int m_speedPrecision = 1;
        private int m_powerPrecision = 0;
        private int m_cadencePrecision = 0;
        private int m_elevationPrecision = 0;


        public GenericLineChart()
        {
            m_distancePrecision = Length.DefaultDecimalPrecision(m_lengthUnit);
            m_elevationPrecision = Length.DefaultDecimalPrecision(m_elevationUnit);;
            InitializeComponent();
            InitControls();
        }

        void InitControls()
        {
            this.MainChart.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            MainChart.YAxis.SmartZoom = true;
            MainChart.XAxis.SmartZoom = true;
            copyChartMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentCopy16;
            copyChartMenuItem.Visible = false;
            saveImageMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Save16;
            selectChartsMenuItem.Visible = false;
            fitToWindowMenuItem.Image = GpsRunningPlugin.Properties.Resources.ZoomToContent;
        }

        //public void SetControl(ActivityDetailPageControl page, MultiChartsControl multiple)
        //{
        //    m_page = page;
        //    m_multiple = multiple;
        //}

        public void ThemeChanged(ITheme visualTheme)
        {
            m_visualTheme = visualTheme;
            MainChart.ThemeChanged(visualTheme);
            ButtonPanel.ThemeChanged(visualTheme);
            ButtonPanel.BackColor = visualTheme.Window;
        }

        public void UICultureChanged(CultureInfo culture)
        {
            copyChartMenuItem.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;
            saveImageMenuItem.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionSaveImage;
            fitToWindowMenuItem.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionRefresh;
            SetupAxes();
            ZoomToData();
        }

        public bool ShowPage
        {
            get
            {
                return m_visible;
            }
            set
            {
                m_visible = value;
                if (value)
                {
                    SetupAxes();
                    SetupDataSeries();
                }
            }
        }
        public enum XAxisValue
        {
			Time,
			Distance
		}

        
        //No simple way to dynamically translate enum
        //The integer (raw) value is stored as defaults too
        public static string XAxisValueString(XAxisValue XAxisReferential)
        {
            string xAxisLabel="";
            switch (XAxisReferential)
            {
                case XAxisValue.Distance:
                    {
                        xAxisLabel = CommonResources.Text.LabelDistance;
                        break;
                    }
                case XAxisValue.Time:
                    {
                        xAxisLabel = CommonResources.Text.LabelTime;
                        break;
                    }
                default:
                    {
                        Debug.Assert(false);
                        break;
                    }
            }
            return xAxisLabel;
        }

        public static IList<LineChartTypes> DefaultLineChartTypes()
        {
            return new List<LineChartTypes>{
                LineChartTypes.SpeedPace, LineChartTypes.Elevation,
                LineChartTypes.HeartRateBPM, LineChartTypes.Cadence};
        }
        public static string ChartTypeString(LineChartTypes x)
        {
            return GenericLineChart.LineChartTypesString((LineChartTypes)x);
        }
        public static string LineChartTypesString(LineChartTypes YAxisReferential)
        {
            string yAxisLabel="";
			switch (YAxisReferential) {
				case LineChartTypes.Cadence: {
						yAxisLabel = CommonResources.Text.LabelCadence;
						break;
					}
				case LineChartTypes.Elevation: {
						yAxisLabel = CommonResources.Text.LabelElevation;
						break;
					}
				case LineChartTypes.HeartRateBPM: {
						yAxisLabel = CommonResources.Text.LabelHeartRate;
						break;
					}
				case LineChartTypes.HeartRatePercentMax:
                case LineChartTypes.DiffHeartRateBPM:
                    {
						yAxisLabel = CommonResources.Text.LabelHeartRate;
						break;
					}
				case LineChartTypes.Power: {
						yAxisLabel = CommonResources.Text.LabelPower;
						break;
					}
				case LineChartTypes.Speed: {
						yAxisLabel = CommonResources.Text.LabelSpeed;
						break;
					}
				case LineChartTypes.Pace: {
						yAxisLabel = CommonResources.Text.LabelPace;
						break;
					}
                case LineChartTypes.SpeedPace:
                    {
                        yAxisLabel = CommonResources.Text.LabelSpeed + CommonResources.Text.LabelPace;
                        break;
                    }
                case LineChartTypes.Grade:
                    {
                        yAxisLabel = CommonResources.Text.LabelGrade;
                        break;
                    }
                case LineChartTypes.DiffTime:
                    {
                        yAxisLabel = CommonResources.Text.LabelTime;
                        break;
                    }
                case LineChartTypes.DiffDist:
                    {
                        yAxisLabel = CommonResources.Text.LabelDistance;
                        break;
                    }
                case LineChartTypes.Time:
                    {
                        yAxisLabel = CommonResources.Text.LabelTime;
                        break;
                    }
                case LineChartTypes.Distance:
                    {
                        yAxisLabel = CommonResources.Text.LabelDistance;
                        break;
                    }
                default:
                    {
						Debug.Assert(false);
						break;
					}
            }
            return yAxisLabel;
        }

        /********************************************/ 
		private void SaveImageButton_Click(object sender, EventArgs e) 
        {
            SaveImageDialog dlg = new SaveImageDialog();
            if (m_visualTheme != null)
            {
                dlg.ThemeChanged(m_visualTheme);
            }
            dlg.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + Path.DirectorySeparatorChar + "Trails";
            dlg.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
			if (dlg.ShowDialog() == DialogResult.OK) 
            {
				Size imgSize = dlg.CustomImageSize;

                if (dlg.ImageSize != SaveImageDialog.ImageSizeType.Custom)
                {
					imgSize = dlg.ImageSizes[dlg.ImageSize];
				}
				MainChart.SaveImage(imgSize, dlg.FileName, dlg.ImageFormat);
			}

			MainChart.Focus();
		}

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            MainChart.ZoomOut();
            MainChart.Focus();
        }
        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            MainChart.ZoomIn();
            MainChart.Focus();
        }

        private void ZoomToContentButton_Click(object sender, EventArgs e)
        {
			this.ZoomToData();
		}

 		public void ZoomToData() 
        {
            MainChart.AutozoomToData(true);
			MainChart.Refresh();
		}

        void copyChartMenuItem_Click(object sender, EventArgs e)
        {
            //Not visible menu item
            //MainChart.CopyTextToClipboard(true, System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
        }

        void MainChart_SelectData(object sender, ZoneFiveSoftware.Common.Visuals.Chart.ChartBase.SelectDataEventArgs e)
        {
        }
#if false
        void MainChart_SelectData(object sender, ZoneFiveSoftware.Common.Visuals.Chart.ChartBase.SelectDataEventArgs e)
        {
            if (e != null && e.DataSeries != null && m_page != null)
            {
                //Get index for dataseries - same as for result
                int i = -1;
                if (MainChart.DataSeries.Count==2 &&
                    m_dataSeries.Count==1)
                {
                    //Match the result, the first is the fill chart
                    i = 0;
                }
                else
                {
                    for (int j = 0; j < MainChart.DataSeries.Count; j++)
                    {
                        if (e.DataSeries.Equals(MainChart.DataSeries[j]))
                        {
                            i = j;
                            break;
                        }
                    }
                }
                if(i>=0)
                {
                    TrailResult tr = m_trailResults[i];
                    IList<float[]> regions;
                    e.DataSeries.GetSelectedRegions(out regions);

                    IList<Data.TrailResultMarked> results = new List<Data.TrailResultMarked>();
                    if (XAxisReferential == XAxisValue.Time)
                    {
                        IValueRangeSeries<DateTime> t = new ValueRangeSeries<DateTime>();
                        foreach (float[] at in regions)
                        {
                            t.Add(new ValueRange<DateTime>(
                                tr.getActivityTime(at[0]),
                                tr.getActivityTime(at[1])));
                        }
                        results.Add(new Data.TrailResultMarked(tr, t));
                    }
                    else
                    {
                        IValueRangeSeries<double> t = new ValueRangeSeries<double>();
                        foreach (float[] at in regions)
                        {
                            t.Add(new ValueRange<double>(
                                tr.getActivityDist(Utils.Units.SetDistance(at[0], m_refTrailResult.Activity)),
                                tr.getActivityDist(Utils.Units.SetDistance(at[1], m_refTrailResult.Activity))));
                        }
                        results.Add(new Data.TrailResultMarked(tr, t));
                    }
                    this.MainChart.SelectData -= new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase.SelectDataHandler(MainChart_SelectData);
                    const int MaxSelectedSeries = 5;
                    bool markAll=(MainChart.DataSeries.Count <= MaxSelectedSeries);
                    //Mark route track, but not chart
                    m_page.MarkTrack(results, false);
                    m_page.EnsureVisible(new List<Data.TrailResult>{tr}, false);

                    if (markAll)
                    {
                        m_multiple.SetSelectedRange(regions);
                    }
                    else
                    {
                        //Assumes that not single results are set
                        m_multiple.SetSelectedRange(i, regions);
                    }
                    this.MainChart.SelectData += new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase.SelectDataHandler(MainChart_SelectData);
                }
            }
        }

        public void SetSelectedRange(IList<float[]> regions)
        {
            for (int i = 0; i < MainChart.DataSeries.Count; i++ )
            {
                MainChart.DataSeries[i].ClearSelectedRegions();
                //For "single result" only select first series
                if (m_trailResults.Count>1 || i==0)
                //if (MainChart.DataSeries[i].ChartType != ChartDataSeries.Type.Fill)
                {
                    SetSelectedRange(i, false, regions);
                }
            }
        }

        public void SetSelectedRange(int i, bool clear, IList<float[]> regions)
        {
            if (m_visible)
            {
                this.MainChart.SelectData -= new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase.SelectDataHandler(MainChart_SelectData);
                if (clear)
                {
                    foreach (ChartDataSeries t in MainChart.DataSeries)
                    {
                        //Note: This is not clearing ranges
                        t.ClearSelectedRegions();
                    }
                }
                if (MainChart.DataSeries != null && MainChart.DataSeries.Count > i)
                {
                    if (!clear)
                    {
                        MainChart.DataSeries[i].ClearSelectedRegions();
                    }
                    if (regions != null && regions.Count > 0)
                    {
                        //foreach (float[] at in regions)
                        //{
                        //    //s.AddSelecedRegion(at[0], at[1]);
                        //}
                        MainChart.DataSeries[i].SetSelectedRange(regions[0][0], regions[regions.Count - 1][1]);
                    }
                }
                this.MainChart.SelectData += new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase.SelectDataHandler(MainChart_SelectData);
            }
        }

        //No TrailResult - use all possible matches
        public void SetSelectedRange(IList<IItemTrackSelectionInfo> asel)
        {
            if (MainChart != null && MainChart.DataSeries != null &&
                    MainChart.DataSeries.Count > 0 &&
                m_trailResults.Count > 0 && ShowPage)
            {
                //This is used in single activity mode, when selected on the route
                Data.TrailsItemTrackSelectionInfo sel = new Data.TrailsItemTrackSelectionInfo();
                foreach (IItemTrackSelectionInfo trm in asel)
                {
                    sel.Union(trm);
                }

                //Set the matching time distance for the activity
                for (int i = 0; i < m_trailResults.Count; i++)
                {
                        MainChart.DataSeries[i].ClearSelectedRegions();
                        //The "fill" chart is 0, line is 1
                        if (i == 0 && m_trailResults.Count == 1 &&
                                    MainChart.DataSeries.Count > 1)
                        {
                             MainChart.DataSeries[1].ClearSelectedRegions();
                        }
                    IList<float[]> l = GetSelection(i, sel);
                    if (l != null && l.Count > 0)
                    {
                        //Only one range can be selected
                        float x1 = l[0][0];
                        float x2 = l[l.Count - 1][1];
                        MainChart.DataSeries[i].ClearSelectedRegions();
                        //The "fill" chart is 0, line is 1
                        if (i == 0 && m_trailResults.Count == 1 &&
                            MainChart.DataSeries.Count > 1)
                        {
                            MainChart.DataSeries[1].ClearSelectedRegions();
                        }
                        //Ignore ranges outside current range and malformed scales
                        if (x1 < MainChart.XAxis.MaxOriginFarValue &&
                            MainChart.XAxis.MinOriginValue > float.MinValue &&
                            x2 > MainChart.XAxis.MinOriginValue &&
                            MainChart.XAxis.MaxOriginFarValue < float.MaxValue)
                        {
                            x1 = Math.Max(x1, (float)MainChart.XAxis.MinOriginValue);
                            x2 = Math.Min(x2, (float)MainChart.XAxis.MaxOriginFarValue);
                            MainChart.DataSeries[i].SetSelectedRange(x1, x2);
                        }
                    }
                }
            }
        }

        //only mark in chart, no range/summary
        public void SetSelectedRegions(IList<TrailResultMarked> atr)
        {
            if (MainChart != null && MainChart.DataSeries != null &&
                    MainChart.DataSeries.Count > 0 &&
                m_trailResults.Count > 0 && ShowPage)
            {
                foreach(ChartDataSeries c in MainChart.DataSeries)
                {
                    c.ClearSelectedRegions();
                }
                foreach (TrailResultMarked trm in atr)
                {
                    //Set the matching time distance for the activity
                    for (int i = 0; i < m_trailResults.Count; i++)
                    {
                        TrailResult tr = m_trailResults[i];
                        if (trm.trailResult == tr)
                        {
                            foreach (float[] ax in GetSelection(i, trm.selInfo))
                            {
                                //Ignore ranges outside current range and malformed scales
                                if (ax[0] < MainChart.XAxis.MaxOriginFarValue &&
                                    MainChart.XAxis.MinOriginValue > float.MinValue &&
                                    ax[1] > MainChart.XAxis.MinOriginValue &&
                                    MainChart.XAxis.MaxOriginFarValue < float.MaxValue)
                                {
                                    ax[0] = Math.Max(ax[0], (float)MainChart.XAxis.MinOriginValue);
                                    ax[1] = Math.Min(ax[1], (float)MainChart.XAxis.MaxOriginFarValue);
                                    MainChart.DataSeries[i].AddSelecedRegion(ax[0], ax[1]);
                                }
                            }
                        }
                    }
                }
            }
        }

        float[] GetSingleSelection(TrailResult tr, IValueRange<DateTime> v)
        {
            double t1 = 0, t2 = 0;
            DateTime d1 = DateTime.MinValue, d2 = DateTime.MinValue;
            d1 = tr.getTimeFromActivity(v.Lower);
            d2 = tr.getTimeFromActivity(v.Upper);
            if (XAxisReferential != XAxisValue.Time)
            {
                t1 = tr.getDistFromActivity(d1);
                t2 = tr.getDistFromActivity(d2);
            }
            return GetSingleSelection(tr, t1, t2, d1, d2);
        }
        float[] GetSingleSelection(TrailResult tr, IValueRange<double> v)
        {
            double t1 = 0, t2 = 0;
            DateTime d1 = DateTime.MinValue, d2 = DateTime.MinValue;
            t1 = tr.getDistFromActivity(v.Lower);
            t2 = tr.getDistFromActivity(v.Upper);
            if (XAxisReferential == XAxisValue.Time)
            {
                d1 = tr.getTimeFromActivity(t1);
                d2 = tr.getTimeFromActivity(t2);
            }
            return GetSingleSelection(tr, t1, t2, d1, d2);
        }
        float[] GetSingleSelection(TrailResult tr, double t1, double t2, DateTime d1, DateTime d2)
        {
            float x1 = float.MaxValue, x2 = float.MinValue;
            //Convert to distance display unit, Time is always in seconds
            if (XAxisReferential == XAxisValue.Time)
            {
                x1 = (float)(tr.getSeconds(d1));
                x2 = (float)(tr.getSeconds(d2));
            }
            else
            {
                x1 = Utils.Units.GetDistance(t1, m_refTrailResult.Activity);
                x2 = Utils.Units.GetDistance(t2, m_refTrailResult.Activity);
            }
            return new float[] { x1, x2 };
        }

        private IList<float[]> GetSelection(int i, IItemTrackSelectionInfo sel)
        {
            IList<float[]> result = new List<float[]>();

            TrailResult tr = m_trailResults[i]; 
            //Currently only one range but several regions can be selected
            if (sel.MarkedTimes != null)
            {
                foreach (IValueRange<DateTime> v in sel.MarkedTimes)
                {
                    result.Add(GetSingleSelection(tr, v));
                }
            }
            else if (sel.MarkedDistances != null)
            {
                foreach (IValueRange<double> v in sel.MarkedDistances)
                {
                    result.Add(GetSingleSelection(tr, v));
                }
            }
            else if (sel.SelectedTime != null)
            {
                result.Add(GetSingleSelection(tr, sel.SelectedTime));
            }
            else if (sel.SelectedDistance != null)
            {
                result.Add(GetSingleSelection(tr, sel.SelectedDistance));
            }
            return result;
        }
#endif
        public void EnsureVisible(IList<NumericTimeDataSeries> serArray)
        {
            if (ShowPage)
            {
                foreach (NumericTimeDataSeries series in serArray)
                {
                    for (int i = 0; i < MainChart.DataSeries.Count; i++)
                    {
                        MainChart.DataSeries[i].ClearSelectedRegions();
                        //For "single result" only select first series
                        if (i < m_dataSeries.Count &&
                            m_dataSeries[i].Equals(series) &&
                            (m_dataSeries.Count > 1 || i == 0))
                        {
                            MainChart.DataSeries[i].AddSelecedRegion(
                                MainChart.DataSeries[i].XMin, MainChart.DataSeries[i].XMax);
                        }
                    }
                }
            }
        }

        //Find if the chart has any data
        private bool? hasValues=null;
        public bool HasValues()
        {
            if (hasValues == null)
            {
                foreach (ChartDataSeries t in MainChart.DataSeries)
                {
                    foreach (KeyValuePair<float, PointF> v in t.Points)
                    {
                        if (v.Value.Y != 0)
                        {
                            hasValues = true;
                            return true;
                        }
                    }
                }
                hasValues = false;
            }
            return (bool)hasValues;
        }

        virtual protected void SetupDataSeries()
        {
			MainChart.DataSeries.Clear();
            MainChart.XAxis.Markers.Clear();
            if (m_visible && !m_updateInProgress)
            {
                hasValues = null;

                // Add main data. We must use 2 separate data series to overcome the display
                //  bug in fill mode.  The main data series is normally rendered but the copy
                //  is set in Line mode to be displayed over the fill

                foreach (GenericChartDataSeries GenSeriesEntry in m_dataSeries)
                {
                    if (GenSeriesEntry.DataSeries != null)
                    {
                        INumericTimeDataSeries graphPoints = GenSeriesEntry.DataSeries;
                        IAxis axis = FindAxis(GenSeriesEntry.LineChartType);
                        if (graphPoints.Count <= 1)
                        {
                            //Add empty - Dataseries index must match results. Also required for axis to be shown.
                            MainChart.DataSeries.Add(new ChartDataSeries(MainChart, axis));
                        }
                        else
                        {
                            Color chartFillColor = ChartFillColor;
                            Color chartLineColor = ChartLineColor;
                            Color chartSelectedColor = ChartSelectedColor;
                            if (m_dataSeries.Count > 1 || m_alwaysUseSeriesColor)
                            {
                                chartFillColor = GenSeriesEntry.LineColor;
                                chartLineColor = chartFillColor;
                                chartSelectedColor = chartFillColor;
                            }

                            ChartDataSeries dataFill = null;

                            // If there is only one line, fill the area below the line
                            if (m_dataSeries.Count == 1 && !m_alwaysUseSeriesColor)
                            {
                                dataFill = new ChartDataSeries(MainChart, axis);
                                MainChart.DataSeries.Add(dataFill);

                                dataFill.ChartType = ChartDataSeries.Type.Fill;
                                dataFill.FillColor = chartFillColor;
                                dataFill.LineColor = chartLineColor;
                                dataFill.SelectedColor = chartSelectedColor;
                                dataFill.LineWidth = 2;
                            }
                            ChartDataSeries dataLine = new ChartDataSeries(MainChart, axis);
                            MainChart.DataSeries.Add(dataLine);

                            dataLine.ChartType = ChartDataSeries.Type.Line;
                            dataLine.LineColor = chartLineColor;
                            dataLine.SelectedColor = chartSelectedColor;

                            if (XAxisReferential == XAxisValue.Time)
                            {
                                float oldElapsedSeconds = -1;
                                foreach (ITimeValueEntry<float> entry in graphPoints)
                                {
                                    float value = ConvertUnit(entry.Value, GenSeriesEntry.LineChartType);
                                    if (oldElapsedSeconds != entry.ElapsedSeconds)
                                    {
                                        if (null != dataFill)
                                        {
                                            dataFill.Points.Add(entry.ElapsedSeconds, new PointF(entry.ElapsedSeconds, value));
                                        }
                                        dataLine.Points.Add(entry.ElapsedSeconds, new PointF(entry.ElapsedSeconds, value));
                                    }
                                    oldElapsedSeconds = entry.ElapsedSeconds;
                                }
                            }
                            else
                            {
                                IDistanceDataTrack distanceTrack = GenSeriesEntry.xAxisDistanceSeries;
                                float oldElapsedSeconds = -1;
                                foreach (ITimeValueEntry<float> dtEntry in distanceTrack)
                                {
                                    float elapsedSeconds = dtEntry.ElapsedSeconds;
                                    if(elapsedSeconds <= graphPoints.TotalElapsedSeconds)
                                    {
                                        ITimeValueEntry<float> valueEntry = graphPoints.GetInterpolatedValue(graphPoints.StartTime.Add(new TimeSpan(0,0,(int)elapsedSeconds)));
                                        float value = ConvertUnit(valueEntry.Value, GenSeriesEntry.LineChartType);
                                        float distanceValue = ConvertUnit(dtEntry.Value, LineChartTypes.Distance);

                                        if (oldElapsedSeconds != elapsedSeconds)
                                        {
                                            if (null != dataFill)
                                            {
                                                dataFill.Points.Add(elapsedSeconds, new PointF(distanceValue, value));
                                            }
                                            dataLine.Points.Add(elapsedSeconds, new PointF(distanceValue, value));
                                        }
                                        oldElapsedSeconds = elapsedSeconds;
                                    }
                                }
                            }
                        }
                    }
                }
                
                // Set split markers
                GenericChartDataSeries refSeries;
                //If only one result is used, it can be confusing if the trail points are set for ref
                if (m_dataSeries.Count == 1 ||
                    m_dataSeries.Count > 0 && m_refDataSeries == null)
                {
                    
                    refSeries = m_dataSeries[0];
                }
                else if (m_refDataSeries != null)
                {
                    refSeries = m_refDataSeries;
                }
                else
                {
                    refSeries = null;
                }

                if (m_timeSplitsPoints.Count != 0 && refSeries != null && refSeries.DataSeries != null)
                {
                    Image icon = null; // new Bitmap(TrailsPlugin.CommonIcons.fileCircle(11, 11));
                    IDistanceDataTrack distanceTrack = refSeries.xAxisDistanceSeries; 
                    foreach (DateTime t in m_timeSplitsPoints)
                    {
                        AxisMarker a;
                        if (XAxisReferential == XAxisValue.Time)
                        {
                            a = new AxisMarker(t.Subtract(refSeries.DataSeries.StartTime).TotalSeconds, icon);
                            a.Line1Style = System.Drawing.Drawing2D.DashStyle.Solid;
                            a.Line1Color = Color.Goldenrod;
                            MainChart.XAxis.Markers.Add(a);
                        }
                        else
                        {
                            ITimeValueEntry<float> entry = refSeries.DataSeries.GetInterpolatedValue(t);
                            if (entry != null)
                            {
                                ITimeValueEntry<float> distEntry = distanceTrack.GetInterpolatedValue(distanceTrack.StartTime.Add(new TimeSpan(0, 0, (int)entry.ElapsedSeconds)));
                                a = new AxisMarker(ConvertUnit(distEntry.Value, LineChartTypes.Distance), icon);
                                a.Line1Style = System.Drawing.Drawing2D.DashStyle.Solid;
                                a.Line1Color = Color.Black;
                                MainChart.XAxis.Markers.Add(a);
                            }
                        }
                    }
                }
                ZoomToData();
            }
		}

        private void SetupAxes()
        {
            if (m_visible && !m_updateInProgress)
            {
                // X axis
                switch (XAxisReferential)
                {
                    case XAxisValue.Distance:
                        {
                            MainChart.XAxis.Formatter = new Formatter.General(m_distancePrecision);
                            MainChart.XAxis.Label = CommonResources.Text.LabelDistance + " (" +
                                                    Length.LabelAbbr(m_lengthUnit) + ")";
                            break;
                        }
                    case XAxisValue.Time:
                        {

                            MainChart.XAxis.Formatter = new Formatter.SecondsToTime();
                            MainChart.XAxis.Label = CommonResources.Text.LabelTime;
                            break;
                        }
                    default:
                        {
                            Debug.Assert(false);
                            break;
                        }
                }

                // Y axis
                MainChart.YAxisRight.Clear();
                m_YAxisReferentials.Clear();
                if (m_dataSeries.Count == 0)
                {
                    CreateAxis(m_defaultYAxisReferential, true);
                }
                else
                {
                    bool boFirst = true;
                    foreach (GenericChartDataSeries series in m_dataSeries)
                    {
                        IAxis axis = FindAxis(series.LineChartType);
                        if (axis == null)
                        {
                            CreateAxis(series.LineChartType, boFirst);
                            boFirst = false;
                        }
                    }
                }
            }
        }

        private IAxis FindAxis(LineChartTypes lineChartType)
        {
            IAxis axis = null;
            bool boAxisTypeExists = false;
            int i;
            for (i = 0; i < m_YAxisReferentials.Count; i++)
            {
                if (lineChartType == m_YAxisReferentials[i])
                {
                    boAxisTypeExists = true;
                    break;
                }
            }
            if (boAxisTypeExists)
            {
                if (i == 0)
                {
                    axis = MainChart.YAxis;
                }
                else
                {
                    axis = MainChart.YAxisRight[i - 1];
                }
            }
            return axis;
        }
        
        private void CreateAxis(LineChartTypes axisType, bool left)
        {
            m_YAxisReferentials.Add(axisType);
            
            IAxis axis;
            if (left)
            {
                axis = MainChart.YAxis;
            }
            else
            {
                axis = new RightVerticalAxis(MainChart);
                axis.SmartZoom = true;
                MainChart.YAxisRight.Add(axis);
            }

            axis.Formatter = new Formatter.General();
            switch (axisType)
            {
                case LineChartTypes.Cadence:
                    {
                        axis.Formatter = new Formatter.General(m_cadencePrecision);
                        axis.Label = CommonResources.Text.LabelCadence + " (" +
                                                CommonResources.Text.LabelRPM + ")";
                        break;
                    }
                case LineChartTypes.Grade:
                    {
                        axis.Formatter = new Formatter.Percent();
                        axis.Label = CommonResources.Text.LabelGrade + " (%)";
                        break;
                    }
                case LineChartTypes.Elevation:
                    {
                        axis.Formatter = new Formatter.General(m_elevationPrecision);
                        axis.Label = CommonResources.Text.LabelElevation + " (" +
                                                   Length.LabelAbbr(m_elevationUnit) + ")";
                        break;
                    }
                case LineChartTypes.HeartRateBPM:
                case LineChartTypes.DiffHeartRateBPM:
                    {
                        axis.Formatter = new Formatter.General(m_heartRateBPMPrecision);
                        axis.Label = CommonResources.Text.LabelHeartRate + " (" +
                                                CommonResources.Text.LabelBPM + ")";
                        break;
                    }
                case LineChartTypes.HeartRatePercentMax:
                    {
                        axis.Label = CommonResources.Text.LabelHeartRate + " (" +
                                                CommonResources.Text.LabelPercentOfMax + ")";
                        break;
                    }
                case LineChartTypes.Power:
                    {
                        axis.Formatter = new Formatter.General(m_powerPrecision);
                        axis.Label = CommonResources.Text.LabelPower + " (" +
                                                CommonResources.Text.LabelWatts + ")";
                        break;
                    }
                case LineChartTypes.Speed:
                    {
                        axis.Formatter = new Formatter.General(m_speedPrecision);
                        axis.Label = CommonResources.Text.LabelSpeed + " (" +
                                                Speed.Label(Speed.Units.Speed, new Length(1, m_speedDistanceUnit)) + ")";
                        break;
                    }
                case LineChartTypes.Pace:
                    {
                        axis.Formatter = new Formatter.SecondsToTime();
                        axis.Label = CommonResources.Text.LabelPace + " (" +
                                                Speed.Label(Speed.Units.Pace, new Length(1, m_paceDistanceUnit)) + ")";
                        break;
                    }
                case LineChartTypes.DiffTime:
                case LineChartTypes.Time:
                    {
                        axis.Formatter = new Formatter.SecondsToTime();
                        axis.Label = CommonResources.Text.LabelTime;
                        break;
                    }
                case LineChartTypes.DiffDist:
                case LineChartTypes.Distance:
                    {

                        axis.Formatter = new Formatter.General(m_distancePrecision);
                        axis.Label = CommonResources.Text.LabelDistance + " (" +
                                                Length.LabelAbbr(m_lengthUnit) +")";
                        break;
                    }
                default:
                    {
                        Debug.Assert(false);
                        break;
                    }
            }
        }


        private float ConvertUnit(float value, LineChartTypes valueType)
        {
            float result;
            switch (valueType)
            {
                case LineChartTypes.Cadence:
                case LineChartTypes.HeartRateBPM:
                case LineChartTypes.DiffHeartRateBPM:
                case LineChartTypes.Power:
                case LineChartTypes.Grade:
                    {
                        result = value;
                        break;
                    }
                case LineChartTypes.Elevation:
                    {
                        result = (float)Length.Convert(value, Length.Units.Meter, m_elevationUnit);
                        break;
                    }
                case LineChartTypes.Speed:
                    {
                        result = (float)Length.Convert(value, Length.Units.Meter, m_speedDistanceUnit);
                        if (m_speedDistanceUnit == Length.Units.Kilometer || m_speedDistanceUnit == Length.Units.Mile)
                        {
                            result = result * 3600; // Convert to length unit per hour
                        }
                        break;
                    }
                case LineChartTypes.Pace:
                    {
                        result = (float)Length.Convert(value, Length.Units.Meter, m_paceDistanceUnit);
                        // Do not converet from seconds - the axis formatter does that
                        result = 1 / result; // Convert speed to pace
                        break;
                    }
                case LineChartTypes.Time:
                case LineChartTypes.DiffTime:
                    {
                        // Do not converet from seconds - the axis formatter does that
                        result = value;
                        break;
                    }
                case LineChartTypes.Distance:
                case LineChartTypes.DiffDist:
                    {
                        result = (float)Length.Convert(value, Length.Units.Meter, m_lengthUnit);
                        break;
                    }
                default:
                {
                    throw new Exception("Unit conversion not implemented for the used member of LineChartTypes in GenericLineChart");
                }


            }
            return result;
        }
        //private INumericTimeDataSeries ConvertSeriesUnit(Data.TrailResult result) {
        //    // Fail safe
        //    INumericTimeDataSeries track = new NumericTimeDataSeries();

        //    switch (YAxisReferential) {
        //        case LineChartTypes.Cadence: {
        //                track = result.CadencePerMinuteTrack;
        //                break;
        //            }
        //        case LineChartTypes.Elevation: {
        //                INumericTimeDataSeries tempResult = result.ElevationMetersTrack;

        //                // Value is in meters so convert to the right unit
        //                track = new NumericTimeDataSeries();
        //                foreach (ITimeValueEntry<float> entry in tempResult) {
        //                    float temp = Utils.Units.GetElevation(entry.Value, m_refTrailResult.Activity); 

        //                    track.Add(tempResult.EntryDateTime(entry), (float)temp);
        //                }
        //                break;
        //            }
        //        case LineChartTypes.HeartRateBPM: {
        //                track = result.HeartRatePerMinuteTrack;
        //                break;
        //            }
        //        /*
        //                        case LineChartTypes.HeartRatePercentMax: {
        //                                track = new NumericTimeDataSeries();

        //                                IAthleteInfoEntry lastAthleteEntry = PluginMain.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(Activity.StartTime);

        //                                // Value is in BPM so convert to the % max HR if we have the info
        //                                if (!float.IsNaN(lastAthleteEntry.MaximumHeartRatePerMinute)) {
        //                                    INumericTimeDataSeries tempResult = activityInfo.SmoothedHeartRateTrack;

        //                                    foreach (ITimeValueEntry<float> entry in tempResult) {
        //                                        double temp = (entry.Value / lastAthleteEntry.MaximumHeartRatePerMinute) * 100;

        //                                        track.Add(tempResult.EntryDateTime(entry), (float)temp);
        //                                    }
        //                                }
        //                                break;
        //                            }
        //        */
        //        case LineChartTypes.Power: {
        //                track = result.PowerWattsTrack;
        //                break;
        //            }
        //        case LineChartTypes.Grade: {
        //                track = result.GradeTrack;
        //                break;
        //            }

        //        case LineChartTypes.Speed: {
        //                INumericTimeDataSeries tempResult = result.SpeedTrack;

        //                track = new NumericTimeDataSeries();
        //                foreach (ITimeValueEntry<float> entry in tempResult) {
        //                    track.Add(tempResult.EntryDateTime(entry), entry.Value);
        //                }
        //                break;
        //            }

        //        case LineChartTypes.Pace:
        //            {
        //                INumericTimeDataSeries tempResult = result.PaceTrack;

        //                track = new NumericTimeDataSeries();
        //                foreach (ITimeValueEntry<float> entry in tempResult)
        //                {
        //                    track.Add(tempResult.EntryDateTime(entry), entry.Value);
        //                }
        //                break;
        //            }

        //        case LineChartTypes.DiffTime:
        //            {
        //                INumericTimeDataSeries tempResult = result.DiffTimeTrack(m_refTrailResult);

        //                track = new NumericTimeDataSeries();
        //                foreach (ITimeValueEntry<float> entry in tempResult)
        //                {
        //                    track.Add(tempResult.EntryDateTime(entry), entry.Value);
        //                }
        //                break;
        //            }
        //        case LineChartTypes.DiffDist:
        //            {
        //                INumericTimeDataSeries tempResult = result.DiffDistTrack(m_refTrailResult);

        //                track = new NumericTimeDataSeries();
        //                foreach (ITimeValueEntry<float> entry in tempResult)
        //                {
        //                    track.Add(tempResult.EntryDateTime(entry), Utils.Units.GetDistance(entry.Value,m_refTrailResult.Activity));
        //                }
        //                break;
        //            }

        //        default:
        //            {
        //                Debug.Assert(false);
        //                break;
        //            }

        //    }

        //    return track;
        //}

		[DisplayName("X Axis value")]
		public XAxisValue XAxisReferential {
			get { return m_XAxisReferential; }
			set {
				m_XAxisReferential = value;
			}
		}

        //[DisplayName("Y Axis value")]
        //public LineChartTypes YAxisReferential
        //{
        //    get { return m_YAxisReferential; }
        //    set
        //    {
        //        m_YAxisReferential = value;
        //    }
        //}

        //[DisplayName("Y Axis value, right")]
        //public IList<LineChartTypes> YAxisReferential_right
        //{
        //    get { return m_YAxisReferential_right; }
        //    set
        //    {
        //        m_YAxisReferential_right = value;
        //    }
        //}

        public Color ChartFillColor
        {
			get { return m_chartFillColor; }
			set {
				if (m_chartFillColor != value) {
					m_chartFillColor = value;

					foreach (ChartDataSeries dataSerie in MainChart.DataSeries) {
						dataSerie.FillColor = ChartFillColor;
					}
				}
			}
		}

		public Color ChartLineColor {
			get { return m_chartLineColor; }
			set {
				if (ChartLineColor != value) {
					m_chartLineColor = value;

					foreach (ChartDataSeries dataSerie in MainChart.DataSeries) {
						dataSerie.LineColor = ChartLineColor;
					}
				}
			}
		}

		public Color ChartSelectedColor {
			get { return m_chartSelectedColor; }
			set {
				if (ChartSelectedColor != value) {
					m_chartSelectedColor = value;

					foreach (ChartDataSeries dataSerie in MainChart.DataSeries) {
						dataSerie.SelectedColor = ChartSelectedColor;
					}
				}
			}
		}

        public bool AlwaysUseDataSeriesColor
        {
            get { return m_alwaysUseSeriesColor; }
            set
            {
                m_alwaysUseSeriesColor = value;
                SetupAxes();
                SetupDataSeries();
            }
        }

        public void SetReference(GenericChartDataSeries refSeries)
        {
            m_refDataSeries = refSeries;
            SetupAxes();
            SetupDataSeries();
        }

        public void SetDataSeries(IList<GenericChartDataSeries> dataSeriesList)
        {
            m_dataSeries = dataSeriesList;
            SetupAxes();
            SetupDataSeries();
        }

        public void AddDataSeries(GenericChartDataSeries dataSeries)
        {
            m_dataSeries.Add(dataSeries);
            SetupAxes();
            SetupDataSeries();
        }

        public void ClearDataSeries()
        {
            m_dataSeries.Clear();
            SetupAxes();
            SetupDataSeries();
        }

        public void SetTimeSplits(IList<DateTime> timeSplitsPoints)
        {
            m_timeSplitsPoints = timeSplitsPoints;
            SetupAxes();
            SetupDataSeries();
        }

        public void AddTimeSplit(DateTime timeSplit)
        {
            m_timeSplitsPoints.Add(timeSplit);
            SetupAxes();
            SetupDataSeries();
        }

        public void ClearTimeSplits()
        {
            m_timeSplitsPoints.Clear();
            SetupAxes();
            SetupDataSeries();
        }

        // Could [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] work???
        //[Browsable(false)]
        //public GenericChartDataSeries ReferenceDataSeries
        //{
        //    get
        //    {
        //        return m_refDataSeries;
        //    }
        //    set
        //    {

        //        if (value == null)
        //        {
        //            m_refDataSeries = value;
        //        }
        //        else if (!m_refDataSeries.Equals(value))
        //        {                    
        //            m_refDataSeries = value;
        //            SetupAxes();
        //            SetupDataSeries();
        //        }
        //    }
        //}

        // Could [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] work???
        //[Browsable(false)]
        //public IList<GenericChartDataSeries> DataSeries
        //{
        //    get
        //    {
        //        return m_dataSeries;
        //    }
        //    set
        //    {
        //        if (m_dataSeries != value)
        //        {
        //            if (value == null)
        //            {
        //                m_dataSeries = new List<GenericChartDataSeries>();
        //            }
        //            else
        //            {
        //                m_dataSeries = value;
        //            }
        //            SetupAxes();
        //            SetupDataSeries();
        //        }
        //    }
        //}

        public bool ShowChartToolBar
        {
            set
            {
                   this.chartTablePanel.RowStyles[0].Height = value ? 25 : 0;
            }
        }

		public bool BeginUpdate() 
        {
            m_updateInProgress = true;
            return MainChart.BeginUpdate();
		}

		public void EndUpdate() 
        {
            m_updateInProgress = false;
            SetupAxes();
            SetupDataSeries();
            MainChart.EndUpdate();
		}
    }

    public enum LineChartTypes
    {
        Cadence,
        Elevation,
        HeartRateBPM,
        HeartRatePercentMax,
        Power,
        Grade,
        Speed,
        Pace,
        SpeedPace,
        DiffTime,
        DiffDist,
        DiffHeartRateBPM,
        Time,
        Distance
    }

    public class GenericChartDataSeries
    {
        private INumericTimeDataSeries m_dataSeries = new NumericTimeDataSeries(); // Data series to be displayed
        private LineChartTypes m_lineChartType = LineChartTypes.Speed;
        private Color m_lineColor = Color.Blue;
        private IDistanceDataTrack m_xAxisDistanceSeries = new DistanceDataTrack(); // Used when the line is plotted against distance

        public GenericChartDataSeries(INumericTimeDataSeries dataSeries,
            LineChartTypes lineChartType, Color lineColor, IDistanceDataTrack xAxisDistanceSeries)
        {
            m_dataSeries = dataSeries;
            m_lineChartType = lineChartType;
            m_lineColor = lineColor;
            m_xAxisDistanceSeries = xAxisDistanceSeries;
        }

        public INumericTimeDataSeries DataSeries
        {
            get { return m_dataSeries; }
        }

        public LineChartTypes LineChartType
        {
            get { return m_lineChartType; }
        }

        public Color LineColor
        {
            get { return m_lineColor; }
        }

        public IDistanceDataTrack xAxisDistanceSeries
        {
            get { return m_xAxisDistanceSeries; }
        }
    } 


}

