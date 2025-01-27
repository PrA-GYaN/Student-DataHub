using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Student_Management_System
{
    public partial class ManageCourseForm : Form
    {
        CourseClass course = new CourseClass();
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            showTable();

        }
        // show  course data
        private void showTable()
        {
            // Bind data to the DataGridView_courses (not DataGridView_student)
            DataGridView_courses.DataSource = course.GetCourse(new MySqlCommand("SELECT * FROM `courses`"));

            // Ensure DataGridView_courses is not null and ThemeStyle is available
            if (DataGridView_courses != null && DataGridView_courses.ThemeStyle != null)
            {
                // Set alternating row style
                DataGridView_courses.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
                DataGridView_courses.ThemeStyle.AlternatingRowsStyle.Font = null;
                DataGridView_courses.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
                DataGridView_courses.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
                DataGridView_courses.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
                DataGridView_courses.ThemeStyle.BackColor = System.Drawing.Color.DimGray;
                DataGridView_courses.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
                DataGridView_courses.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
                DataGridView_courses.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
                DataGridView_courses.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                DataGridView_courses.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
                DataGridView_courses.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                DataGridView_courses.ThemeStyle.HeaderStyle.Height = 24;
                DataGridView_courses.ThemeStyle.ReadOnly = false;
                DataGridView_courses.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
                DataGridView_courses.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
                DataGridView_courses.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                DataGridView_courses.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
                DataGridView_courses.ThemeStyle.RowsStyle.Height = 80;
                DataGridView_courses.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
                DataGridView_courses.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            }

            // Apply image layout for the image column (if necessary, but make sure column index is correct)
            if (DataGridView_courses.Columns.Count > 7)
            {
                DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)DataGridView_courses.Columns[7];
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        private void button_clearcourse_Click(object sender, EventArgs e)
        {
            textBox_courseID.Clear();
            textbox_coursename.Clear();
            textbox_coursedur.Clear();
            textBox_coursedes.Clear();
        }

        private void button_updatecourse_Click(object sender, EventArgs e)
        {
            if (textbox_coursename.Text == "" || textbox_coursedur.Text == "" || textBox_courseID.Text.Equals(""))
            {
                MessageBox.Show("Please Insert Course Details", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_courseID.Text);
                string Cname = textbox_coursename.Text;
                int dur = Convert.ToInt32(textbox_coursedur.Text);
                string desc = textBox_coursedes.Text;

                if (course.UpdateCourse(id, Cname, dur, desc))
                {
                    showTable();
                    button_clearcourse.PerformClick();
                    MessageBox.Show("Course Updated Succefully", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error Updating Course", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_deletecourse_Click(object sender, EventArgs e)
        {
            if (textBox_courseID.Text.Equals(""))
            {
                MessageBox.Show("Please Insert Course ID", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(textBox_courseID.Text);
                    string Cname = textbox_coursename.Text;
                    int dur = Convert.ToInt32(textbox_coursedur.Text);
                    string desc = textBox_coursedes.Text;

                    if (course.deleteCourse(id))
                    {
                        showTable();
                        button_clearcourse.PerformClick();
                        MessageBox.Show("Course Deleted Succefully", "Delete Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }catch(Exception ex)

                {
                    MessageBox.Show(ex.Message, "Delete Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridView_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_courseID.Text = DataGridView_courses.CurrentRow.Cells[0].Value.ToString();
            textbox_coursename.Text = DataGridView_courses.CurrentRow.Cells[1].Value.ToString();
            textbox_coursedur.Text = DataGridView_courses.CurrentRow.Cells[2].Value.ToString();
            textBox_coursedes.Text = DataGridView_courses.CurrentRow.Cells[3].Value.ToString();

        }

        private void button_search_Click(object sender, EventArgs e)
        {

        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
