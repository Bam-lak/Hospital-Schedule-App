using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class AddScedule : Form
    {
        private DataGridViewRow row;
        private bool v;
        DataTable dtDoctors,dtNurses;
        public AddScedule()
        {
            InitializeComponent();
        }

        public AddScedule(DataGridViewRow row, bool v)
        {
            InitializeComponent();
            this.row = row;
            this.v = v;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DutyScedule scedule;
            dtDoctors = new DataTable();
            dtNurses = new DataTable();
            dtDoctors = ConnectDb.GetDoctorsScedule();
            dtNurses = ConnectDb.GetNursesScedule();
            if (row.Cells["Type"].Value.ToString().Equals("2"))
            {
               if(dtDoctors.Rows.Count>0)
                {
                    var selectedRows = dtDoctors.AsEnumerable()
                .Where(row1 => (row1.Field<DateTime>("DutyDate") == DateTime.Parse(dateTimePicker1.Value.ToString("yyy-MM-dd")))&&(int.Parse(row.Cells["Id"].Value.ToString())==row1.Field<int>("UserId"))).FirstOrDefault();
                    if (selectedRows != null)
                    {
                        MessageBox.Show("Scedule for specified date exists");
                        return;
                    }
                   var selectedRowsNex = dtDoctors.AsEnumerable()
                .Where(row1 => (row1.Field<DateTime>("DutyDate") == DateTime.Parse(dateTimePicker1.Value.AddDays(1).ToString("yyy-MM-dd"))) && (int.Parse(row.Cells["Id"].Value.ToString()) == row1.Field<int>("UserId"))).FirstOrDefault();

                    if (selectedRowsNex != null)
                    {
                        MessageBox.Show("Scedule for next day exists");
                        return;
                    }
                   var selectedRowsPrev = dtDoctors.AsEnumerable()
                .Where(row1 => (row1.Field<DateTime>("DutyDate") == DateTime.Parse(dateTimePicker1.Value.AddDays(-1).ToString("yyy-MM-dd"))) && (int.Parse(row.Cells["Id"].Value.ToString()) == row1.Field<int>("UserId"))).FirstOrDefault();

                    if (selectedRowsPrev != null)
                    {
                        MessageBox.Show("Scedule for previous date exists");
                        return;
                    }
                    DataTable dtFiltered=new DataTable();
                    try
                    {
                         dtFiltered = (from a in dtDoctors.AsEnumerable()
                                                where a.Field<DateTime>("DutyDate").Month == dateTimePicker1.Value.Month &&
                                                a.Field<int>("UserId") == int.Parse(row.Cells["Id"].Value.ToString())
                                                select a).CopyToDataTable();

                    }
                    catch
                    {

                    }
                    if (dtFiltered.Rows.Count >= 10)
                    {
                        MessageBox.Show("Maximum 10 24-hour on duty scedule are allowed");
                        return;
                    }
                    var selectedRowsDup = dtDoctors.AsEnumerable()
                     .Where(row1 => (row1.Field<string>("Speciality") == row.Cells["Speciality"].Value.ToString()) && (DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd")) == row1.Field<DateTime>("DutyDate"))).FirstOrDefault();
                    if (selectedRowsDup != null)
                    {
                        MessageBox.Show("Only one doctor from speciality is allowed to be on 24 hour duty");
                        return;
                    }
                     scedule = new DutyScedule();
                    scedule.DutyDate = dateTimePicker1.Value;
                    scedule.UserId = int.Parse(row.Cells["Id"].Value.ToString());
                    ConnectDb.insertScedule(scedule);
                    MessageBox.Show("Scedule added successfully");
                }
                else
                {
                    scedule = new DutyScedule();
                    scedule.DutyDate = dateTimePicker1.Value;
                    scedule.UserId = int.Parse(row.Cells["Id"].Value.ToString());
                    ConnectDb.insertScedule(scedule);
                    MessageBox.Show("Scedule added successfully");
                }


            }
            else if (row.Cells["Type"].Value.ToString().Equals("3"))
            {
                var selectedRows = dtDoctors.AsEnumerable()
                .Where(row1 => (row1.Field<DateTime>("DutyDate") == DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd")))&& (row1.Field<int>("UserId")==int.Parse(row.Cells["Id"].Value.ToString())));
                if (selectedRows != null)
                {
                    MessageBox.Show("Scedule for specified date exists");
                    return;
                }
                else

                {
                    scedule = new DutyScedule();
                    scedule.DutyDate = dateTimePicker1.Value;
                    scedule.UserId = int.Parse(row.Cells["Id"].Value.ToString());
                    ConnectDb.insertScedule(scedule);
                    MessageBox.Show("Scedule added successfully");
                }
            }
        }
    }
}
