/*
Copyright (C) 2010 Staffan Nilsson

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

namespace GpsRunningPlugin.Util
{
    public partial class InputDialog : Form
    {
        private bool returnOk = false;
        public bool ReturnOk
        {
            get
            {
                return returnOk;
            }
        }

        private String textResult = "";
        public String TextResult
        {
            get
            {
                return textResult;
            }
        }

        public InputDialog(String formText, String label)
        {
            InitializeComponent();
            this.Text = formText;
            this.inputDescriptionLabel.Text = label;
            this.okButton.Text = CommonResources.Text.ActionOk;
            this.cancelButton.Text = CommonResources.Text.ActionCancel;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            ShowDialog();
        }

        public InputDialog(String formText, String label, String inputText)
        {
            InitializeComponent();
            this.Text = formText;
            this.inputDescriptionLabel.Text = label;
            this.inputTextBox.Text = inputText;
            this.okButton.Text = CommonResources.Text.ActionOk;
            this.cancelButton.Text = CommonResources.Text.ActionCancel;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            returnOk = true;
            textResult = this.inputTextBox.Text;
            Dispose();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            returnOk = false;
            textResult = "";
            Dispose();
        }
    }
}
