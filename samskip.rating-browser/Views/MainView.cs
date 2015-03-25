using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using samskip.rating_browser.Clients;
using samskip.rating_browser.Models;
using System.Threading;
using System.Reflection;

namespace samskip.rating_browser
{
    public partial class MainView : UserControl
    {

        // Ratings API Client for the IT Technical API.
        readonly APIClient _clientAPI = new Clients.APIClient();

        // Working set of Ratings.
        private List<Rating> workRatings = null;

        public MainView()
        {
            InitializeComponent();

            // Lets set the version label, so we know what version we are working with.
            lblVersion.Text = "Version " + Application.ProductVersion.ToString();

        }

        #region Button Events

        private void OnClickQuit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnClickExportExcel(object sender, EventArgs e)
        {
            MessageBox.Show("I'm terribly sorry, but this feature has not been implemented.");
        }

        private async void OnGetAll(object sender, EventArgs e)
        {

            var asyncRatingsObject = _clientAPI.Get<RatingsAPICall>("api/v1.1/rate?key=" + Settings.Default.ITAPIKEY);
            lblStatus.Text = "Loading...";

            try
            {
                await asyncRatingsObject;
                lblStatus.Text = "Loaded ("+DateTime.Now.ToString()+")";
                workRatings = asyncRatingsObject.Result.Data.Ratings;
                dataGridViewResult.DataSource = workRatings; // NOT A POINTER :)

                filterByDateFilter(); // Filter by date filter picker.

            }
            catch (APIException ex)
            {
                lblStatus.Text = "Error loading data: " + ex.Message;
            }

        }

        #endregion

        #region DataGridView Events

        private void OnDateFilterChange(object sender, EventArgs e)
        {
            try
            {
                filterByDateFilter(); // Filter by date filter pickers.
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("The working set is empty. Please load something first.");
            }
        }

        private void onCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridViewResult.Columns[e.ColumnIndex].Name == "Stars")
            {
                if (e.Value != null)
                {
                    int stars = (int)e.Value;
                    if (stars <= 2)
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }

                }
            }
        }

        #endregion

        #region Private Helpers

        private void filterByDateFilter()
        {
            var filtRatings = workRatings.Where(x => x.Created >= dateTimePickerStart.Value && x.Created <= dateTimePickerEnd.Value).ToList();
            dataGridViewResult.DataSource = filtRatings;
            lblRows.Text = "Ratings: " + filtRatings.Count.ToString();
        }

        #endregion

    }
}
