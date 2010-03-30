/*
Copyright (C) 2010 splaniden

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZoneFiveSoftware.Common.Visuals;
using System.Drawing.Imaging;
using SportTracksOverlayPlugin.Properties;
using SportTracksOverlayPlugin.Util;
using System.IO;

namespace SportTracksOverlayPlugin.Source
{
	public class OverlaySaveImageInfoPage : Form
	{
		public OverlaySaveImageInfoPage()
		{
			InitializeComponent();

			DateTime d = DateTime.Now;
			txtFilename.Text = String.Format( "{0} {1}",
											"Overlay", d.ToShortDateString());
            char[] cInvalid = Path.GetInvalidFileNameChars();
            for (int i = 0; i < cInvalid.Length; i++)
                txtFilename.Text = txtFilename.Text.Replace(cInvalid[i], '-');

			txtSaveIn.Text = Environment.GetFolderPath( Environment.SpecialFolder.Personal );

			LoadResourceStrings();

            this.comboSize.SelectedIndex = 2;
            this.comboType.SelectedIndex = 2;

            this.isSaveIn.Controls["buttonCombo"].Click += new EventHandler(isSaveInButton_Click);

		}
		private void LoadResourceStrings()
		{
			label1.Text = global::SportTracksOverlayPlugin.Properties.Resources.SaveIn;
			label2.Text = global::SportTracksOverlayPlugin.Properties.Resources.FileName;
			label3.Text = global::SportTracksOverlayPlugin.Properties.Resources.Size;
			label4.Text = global::SportTracksOverlayPlugin.Properties.Resources.Type;
			comboSize.Items.AddRange( new object[] {
				global::SportTracksOverlayPlugin.Properties.Resources.Thumbnail + " (200 x 160)",
				global::SportTracksOverlayPlugin.Properties.Resources.Small + " (400 x 310)",
				global::SportTracksOverlayPlugin.Properties.Resources.Medium + " (800 x 620)",
				global::SportTracksOverlayPlugin.Properties.Resources.Large + " (1400 x 1090)",
				global::SportTracksOverlayPlugin.Properties.Resources.Huge + " (2000 x 1560)",
				global::SportTracksOverlayPlugin.Properties.Resources.Custom} );
			comboType.Items.AddRange( new object[] {
				global::SportTracksOverlayPlugin.Properties.Resources.Bitmap + " (.bmp)",
				global::SportTracksOverlayPlugin.Properties.Resources.JointPhotographicExpertsGroup + " (.jpg)",
				global::SportTracksOverlayPlugin.Properties.Resources.PortableNetworkGraphics + " (.png)",
				global::SportTracksOverlayPlugin.Properties.Resources.TaggedImageFileFormat + " (.tif)"} );
            btnCancel.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCancel;
            btnOK.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionOk;
			Text = global::SportTracksOverlayPlugin.Properties.Resources.SaveImage;
		}

		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private ComboBox comboSize;
		private ComboBox comboType;
		private Label label5;
		private SplitContainer splitContainer1;
		private ZoneFiveSoftware.Common.Visuals.TextBox txtFilename;
		private ZoneFiveSoftware.Common.Visuals.TextBox txtYSize;
		private ZoneFiveSoftware.Common.Visuals.TextBox txtXSize;
		private ZoneFiveSoftware.Common.Visuals.Button btnCancel;
		private ZoneFiveSoftware.Common.Visuals.Button btnOK;
		private System.Windows.Forms.Panel panelCustom;
		private ItemSelectorBase isSaveIn;
		private ZoneFiveSoftware.Common.Visuals.TextBox txtSaveIn;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( OverlaySaveImageInfoPage ) );
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboSize = new System.Windows.Forms.ComboBox();
			this.comboType = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtSaveIn = new ZoneFiveSoftware.Common.Visuals.TextBox();
			this.isSaveIn = new ZoneFiveSoftware.Common.Visuals.ItemSelectorBase();
			this.panelCustom = new System.Windows.Forms.Panel();
			this.txtXSize = new ZoneFiveSoftware.Common.Visuals.TextBox();
			this.txtYSize = new ZoneFiveSoftware.Common.Visuals.TextBox();
			this.txtFilename = new ZoneFiveSoftware.Common.Visuals.TextBox();
			this.btnCancel = new ZoneFiveSoftware.Common.Visuals.Button();
			this.btnOK = new ZoneFiveSoftware.Common.Visuals.Button();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panelCustom.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 20, 13 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 46, 13 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Save in:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point( 20, 40 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 52, 13 );
			this.label2.TabIndex = 1;
			this.label2.Text = "Filename:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point( 20, 67 );
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size( 30, 13 );
			this.label3.TabIndex = 2;
			this.label3.Text = "Size:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point( 20, 94 );
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size( 34, 13 );
			this.label4.TabIndex = 3;
			this.label4.Text = "Type:";
			// 
			// comboSize
			// 
			this.comboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSize.FormattingEnabled = true;
			this.comboSize.Location = new System.Drawing.Point( 105, 64 );
			this.comboSize.Name = "comboSize";
			this.comboSize.Size = new System.Drawing.Size( 226, 21 );
			this.comboSize.TabIndex = 3;
			this.comboSize.SelectedIndexChanged += new System.EventHandler( this.comboSize_SelectedIndexChanged );
			// 
			// comboType
			// 
			this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboType.FormattingEnabled = true;
			this.comboType.Location = new System.Drawing.Point( 105, 91 );
			this.comboType.Name = "comboType";
			this.comboType.Size = new System.Drawing.Size( 226, 21 );
			this.comboType.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point( 52, 3 );
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size( 12, 13 );
			this.label5.TabIndex = 13;
			this.label5.Text = "x";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom ) ) );
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point( -1, 0 );
			this.splitContainer1.Margin = new System.Windows.Forms.Padding( 0 );
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add( this.txtSaveIn );
			this.splitContainer1.Panel1.Controls.Add( this.isSaveIn );
			this.splitContainer1.Panel1.Controls.Add( this.panelCustom );
			this.splitContainer1.Panel1.Controls.Add( this.txtFilename );
			this.splitContainer1.Panel1.Controls.Add( this.label1 );
			this.splitContainer1.Panel1.Controls.Add( this.label4 );
			this.splitContainer1.Panel1.Controls.Add( this.label2 );
			this.splitContainer1.Panel1.Controls.Add( this.label3 );
			this.splitContainer1.Panel1.Controls.Add( this.comboSize );
			this.splitContainer1.Panel1.Controls.Add( this.comboType );
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitContainer1.Panel2.Controls.Add( this.btnCancel );
			this.splitContainer1.Panel2.Controls.Add( this.btnOK );
			this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler( this.splitContainer1Panel2_Paint );
			this.splitContainer1.Size = new System.Drawing.Size( 512, 199 );
			this.splitContainer1.SplitterDistance = 156;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 14;
			this.splitContainer1.TabStop = false;
			// 
			// txtSaveIn
			// 
			this.txtSaveIn.AcceptsReturn = false;
			this.txtSaveIn.AcceptsTab = false;
			this.txtSaveIn.BackColor = System.Drawing.Color.White;
			this.txtSaveIn.BorderColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 123 ) ) ) ), ( (int)( ( (byte)( 114 ) ) ) ), ( (int)( ( (byte)( 108 ) ) ) ) );
			this.txtSaveIn.ButtonImage = null;
			this.txtSaveIn.Location = new System.Drawing.Point( 104, 10 );
			this.txtSaveIn.MaxLength = 32767;
			this.txtSaveIn.Multiline = false;
			this.txtSaveIn.Name = "txtSaveIn";
			this.txtSaveIn.ReadOnly = false;
			this.txtSaveIn.ReadOnlyColor = System.Drawing.SystemColors.Control;
			this.txtSaveIn.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
			this.txtSaveIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtSaveIn.Size = new System.Drawing.Size( 325, 20 );
			this.txtSaveIn.TabIndex = 1;
			this.txtSaveIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// isSaveIn
			// 
			this.isSaveIn.BackColor = System.Drawing.Color.White;
			this.isSaveIn.BorderColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 123 ) ) ) ), ( (int)( ( (byte)( 114 ) ) ) ), ( (int)( ( (byte)( 108 ) ) ) ) );
			this.isSaveIn.ButtonImage = ( (System.Drawing.Image)( resources.GetObject( "isSaveIn.ButtonImage" ) ) );
			this.isSaveIn.ItemRenderer = null;
			this.isSaveIn.Location = new System.Drawing.Point( 104, 10 );
			this.isSaveIn.Name = "isSaveIn";
			this.isSaveIn.PopupItems = null;
			this.isSaveIn.ReadOnlyColor = System.Drawing.SystemColors.Control;
			this.isSaveIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.isSaveIn.SelectedItem = null;
			this.isSaveIn.Size = new System.Drawing.Size( 348, 20 );
			this.isSaveIn.TabIndex = 21;
			this.isSaveIn.TabStop = false;
			// 
			// panelCustom
			// 
			this.panelCustom.Controls.Add( this.label5 );
			this.panelCustom.Controls.Add( this.txtXSize );
			this.panelCustom.Controls.Add( this.txtYSize );
			this.panelCustom.Location = new System.Drawing.Point( 337, 64 );
			this.panelCustom.Name = "panelCustom";
			this.panelCustom.Size = new System.Drawing.Size( 115, 18 );
			this.panelCustom.TabIndex = 18;
			this.panelCustom.Visible = false;
			// 
			// txtXSize
			// 
			this.txtXSize.AcceptsReturn = false;
			this.txtXSize.AcceptsTab = false;
			this.txtXSize.BackColor = System.Drawing.Color.White;
			this.txtXSize.BorderColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 123 ) ) ) ), ( (int)( ( (byte)( 114 ) ) ) ), ( (int)( ( (byte)( 108 ) ) ) ) );
			this.txtXSize.ButtonImage = null;
			this.txtXSize.Location = new System.Drawing.Point( 0, 0 );
			this.txtXSize.MaxLength = 5;
			this.txtXSize.Multiline = false;
			this.txtXSize.Name = "txtXSize";
			this.txtXSize.ReadOnly = false;
			this.txtXSize.ReadOnlyColor = System.Drawing.SystemColors.Control;
			this.txtXSize.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
			this.txtXSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtXSize.Size = new System.Drawing.Size( 49, 19 );
			this.txtXSize.TabIndex = 4;
			this.txtXSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtXSize.Validating += new System.ComponentModel.CancelEventHandler( this.txtXSize_Validating );
			// 
			// txtYSize
			// 
			this.txtYSize.AcceptsReturn = false;
			this.txtYSize.AcceptsTab = false;
			this.txtYSize.BackColor = System.Drawing.Color.White;
			this.txtYSize.BorderColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 123 ) ) ) ), ( (int)( ( (byte)( 114 ) ) ) ), ( (int)( ( (byte)( 108 ) ) ) ) );
			this.txtYSize.ButtonImage = null;
			this.txtYSize.Location = new System.Drawing.Point( 66, 0 );
			this.txtYSize.MaxLength = 5;
			this.txtYSize.Multiline = false;
			this.txtYSize.Name = "txtYSize";
			this.txtYSize.ReadOnly = false;
			this.txtYSize.ReadOnlyColor = System.Drawing.SystemColors.Control;
			this.txtYSize.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
			this.txtYSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtYSize.Size = new System.Drawing.Size( 49, 19 );
			this.txtYSize.TabIndex = 5;
			this.txtYSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtYSize.Validating += new System.ComponentModel.CancelEventHandler( this.txtYSize_Validating );
			// 
			// txtFilename
			// 
			this.txtFilename.AcceptsReturn = false;
			this.txtFilename.AcceptsTab = false;
			this.txtFilename.BackColor = System.Drawing.Color.White;
			this.txtFilename.BorderColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 123 ) ) ) ), ( (int)( ( (byte)( 114 ) ) ) ), ( (int)( ( (byte)( 108 ) ) ) ) );
			this.txtFilename.ButtonImage = null;
			this.txtFilename.Location = new System.Drawing.Point( 104, 37 );
			this.txtFilename.MaxLength = 32767;
			this.txtFilename.Multiline = false;
			this.txtFilename.Name = "txtFilename";
			this.txtFilename.ReadOnly = false;
			this.txtFilename.ReadOnlyColor = System.Drawing.SystemColors.Control;
			this.txtFilename.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
			this.txtFilename.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtFilename.Size = new System.Drawing.Size( 348, 19 );
			this.txtFilename.TabIndex = 2;
			this.txtFilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.BorderColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 100 ) ) ) ), ( (int)( ( (byte)( 40 ) ) ) ), ( (int)( ( (byte)( 50 ) ) ) ), ( (int)( ( (byte)( 120 ) ) ) ) );
			this.btnCancel.CenterImage = null;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.HyperlinkStyle = false;
			this.btnCancel.ImageMargin = 2;
			this.btnCancel.LeftImage = null;
			this.btnCancel.Location = new System.Drawing.Point( 424, 10 );
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.PushStyle = true;
			this.btnCancel.RightImage = null;
			this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TextAlign = System.Drawing.StringAlignment.Center;
			this.btnCancel.TextLeftMargin = 2;
			this.btnCancel.TextRightMargin = 2;
			this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.BorderColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 100 ) ) ) ), ( (int)( ( (byte)( 40 ) ) ) ), ( (int)( ( (byte)( 50 ) ) ) ), ( (int)( ( (byte)( 120 ) ) ) ) );
			this.btnOK.CenterImage = null;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.HyperlinkStyle = false;
			this.btnOK.ImageMargin = 2;
			this.btnOK.LeftImage = null;
			this.btnOK.Location = new System.Drawing.Point( 340, 10 );
			this.btnOK.Name = "btnOK";
			this.btnOK.PushStyle = true;
			this.btnOK.RightImage = null;
			this.btnOK.Size = new System.Drawing.Size( 75, 23 );
			this.btnOK.TabIndex = 7;
			this.btnOK.Text = "OK";
			this.btnOK.TextAlign = System.Drawing.StringAlignment.Center;
			this.btnOK.TextLeftMargin = 2;
			this.btnOK.TextRightMargin = 2;
			this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
			// 
			// OverlaySaveImageInfoPage
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size( 508, 202 );
			this.Controls.Add( this.splitContainer1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OverlaySaveImageInfoPage";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Save Image";
			this.splitContainer1.Panel1.ResumeLayout( false );
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout( false );
			this.splitContainer1.ResumeLayout( false );
			this.panelCustom.ResumeLayout( false );
			this.panelCustom.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		#region Properties
		private string ImageFileExtension
		{
			get
			{
				string sExt;
				switch ( comboType.SelectedIndex )
				{
					case 0:	//BMP
						sExt = "bmp";
						break;
					case 1:	//JPG
						sExt = "jpg";
						break;
					case 2:	//PNG
						sExt = "png";
						break;
					case 3:	//TIFF
						sExt = "tif";
						break;
					default: //JPG
						sExt = "jpg";
						break;
				}
				return sExt;
			}
		}
		public ImageFormat ImageFormat
		{
			get
			{
				ImageFormat imgF;
				switch ( comboType.SelectedIndex )
				{
					case 0:	//BMP
						imgF = ImageFormat.Bmp;
						break;
					case 1:	//JPG
						imgF = ImageFormat.Jpeg;
						break;
					case 2:	//PNG
						imgF = ImageFormat.Png;
						break;
					case 3:	//TIFF
						imgF = ImageFormat.Tiff;
						break;
					default: //JPG
						imgF = ImageFormat.Jpeg;
						break;
				}
				return imgF;
			}
			set
			{
				if ( value == ImageFormat.Bmp )
					comboType.SelectedIndex = 0;
				else if ( value == ImageFormat.Jpeg )
					comboType.SelectedIndex = 1;
				else if ( value == ImageFormat.Png )
					comboType.SelectedIndex = 2;
				else if ( value == ImageFormat.Tiff )
					comboType.SelectedIndex = 3;
				else
					comboType.SelectedIndex = 1;

			}
		}
		public string FileFullPathAndName
		{
			get
			{
				string sPath = Path.GetFileNameWithoutExtension( txtFilename.Text );
				sPath += "." + ImageFileExtension;
				sPath = Path.Combine( txtSaveIn.Text, sPath );
				return sPath;
			}
		}
		public string FileName
		{
			get
			{
				return Path.GetFileNameWithoutExtension( txtFilename.Text );
			}
			set
			{
				txtFilename.Text = value;
			}
		}
		public string FolderName
		{
			get
			{
				return txtSaveIn.Text;
			}
			set
			{
				txtSaveIn.Text = value;
			}
		}
		public Size ImageSize
		{
			get
			{
				Size size = new Size();
				switch ( comboSize.SelectedIndex )
				{
					case 0:	//Thumbnail
						size.Width = 200;
						size.Height = 160;
						break;
					case 1:	//Small
						size.Width = 400;
						size.Height = 310;
						break;
					case 2:	//Medium
						size.Width = 800;
						size.Height = 620;
						break;
					case 3:	//Large
						size.Width = 1400;
						size.Height = 1090;
						break;
					case 4:	//Huge
						size.Width = 2000;
						size.Height = 1560;
						break;
					default:
						Int32 n = 0;
						if ( Int32.TryParse( txtXSize.Text, out n ) ) size.Width = n; else size.Width = 0;
						if ( Int32.TryParse( txtYSize.Text, out n ) ) size.Height = n; else size.Height = 0;
						break;
				}
				return size;
			}
			set
			{
				if ( ( value.Width == 200 ) && ( value.Height == 160 ) )
					comboSize.SelectedIndex = 0;
				else if ( ( value.Width == 400 ) && ( value.Height == 310 ) )
					comboSize.SelectedIndex = 1;
				else if ( ( value.Width == 800 ) && ( value.Height == 620 ) )
					comboSize.SelectedIndex = 2;
				else if ( ( value.Width == 1400 ) && ( value.Height == 1090 ) )
					comboSize.SelectedIndex = 3;
				else if ( ( value.Width == 2000 ) && ( value.Height == 1560 ) )
					comboSize.SelectedIndex = 4;
				else
				{
					comboSize.SelectedIndex = 5;
					txtXSize.Text = value.Width.ToString();
					txtYSize.Text = value.Height.ToString();
				}
			}
		}
		#endregion

		#region "Events"
		void txtXSize_Validating( object sender, CancelEventArgs e )
		{
			int i = 0;
			ZoneFiveSoftware.Common.Visuals.TextBox t = (ZoneFiveSoftware.Common.Visuals.TextBox)sender;
			if ( !int.TryParse( t.Text, out i ) )
				e.Cancel = true;
		}

		void txtYSize_Validating( object sender, CancelEventArgs e )
		{
			int i = 0;
			ZoneFiveSoftware.Common.Visuals.TextBox t = (ZoneFiveSoftware.Common.Visuals.TextBox)sender;
			if ( !int.TryParse( t.Text, out i ) )
				e.Cancel = true;
		}

		void isSaveInButton_Click( object sender, EventArgs e )
		{
			ZoneFiveSoftware.Common.Visuals.DirectoryTreePopup dtp = new DirectoryTreePopup();
			dtp.BackColor = Color.White;
			dtp.Tree.RowHotlightColor = System.Drawing.SystemColors.ControlLight;
			dtp.Tree.RowHotlightColorText = System.Drawing.SystemColors.ControlText;
			dtp.Location = splitContainer1.Panel1.PointToScreen( isSaveIn.Location );
			dtp.Tree.SelectedChanged += new EventHandler( dtpTree_SelectedChanged );
			dtp.Top += isSaveIn.Height;
			dtp.Width = isSaveIn.Width;
			dtp.Tree.SelectedPath = txtSaveIn.Text;
			dtp.ShowDialog();
			txtSaveIn.Text = dtp.Tree.SelectedPath;
		}

		void dtpTree_SelectedChanged( object sender, EventArgs e )
		{
			//Prevent the tree from flickering as it expands the selected item
			//as it's closing the tree
			if ( ( (DirectoryTree)sender ).Visible )
				( (DirectoryTree)sender ).Visible = false;
		}

		void splitContainer1Panel2_Paint( object sender, PaintEventArgs e )
		{
			Rectangle r = new Rectangle( this.ClientRectangle.X,
										this.ClientRectangle.Y,
										this.splitContainer1.Size.Width,
										this.ClientRectangle.Height );
			ControlPaint.DrawBorder( e.Graphics, r, Color.Blue, ButtonBorderStyle.Solid );
		}

		private void comboSize_SelectedIndexChanged( object sender, EventArgs e )
		{
			if ( comboSize.SelectedIndex == 5 )
			{
				panelCustom.Visible = true;
			}
			else
			{
				Size imgSize = this.ImageSize;
				txtXSize.Text = imgSize.Width.ToString();
				txtYSize.Text = imgSize.Height.ToString();
				panelCustom.Visible = false;
			}		
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click( object sender, EventArgs e )
		{
			this.Close();
		}
		#endregion


	}
}