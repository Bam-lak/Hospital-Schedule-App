using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        DataTable dtDoctors = new DataTable();
       DataTable dtNurses=new DataTable();
        DataTable dtScedule = new DataTable();
        public Dashboard(int key)
        {
            InitializeComponent();
            this.key = key;
            dtDoctors = new DataTable();
            dtNurses = new DataTable();
            dtDoctors = ConnectDb.GetDoctors();
            dtNurses = ConnectDb.GetNurses();
            dataGridView1.DataSource = dtDoctors;
            dataGridView2.DataSource = dtNurses;
            dataGridView2.Columns["Type"].Visible = false;
            dataGridView2.Columns["Password"].Visible = false;
            dataGridView1.Columns["Type"].Visible = false;
            dataGridView1.Columns["Password"].Visible = false;
            dataGridView2.Columns["PwzNo"].Visible = false;
            btnNewDoctor.Visible = false;
            btnNewNurse.Visible = false;
            btnNewAdmin.Visible = false;
           if(key==1)
            {
                dataGridView1.ContextMenuStrip = contextMenuStrip1;
                dataGridView2.ContextMenuStrip = contextMenuStrip2;
                btnNewDoctor.Visible = true;
                btnNewNurse.Visible = true;
                btnNewAdmin.Visible = true;

            }

        }

        int index=0;

        int Id = 0;
        Doctor doctor;
        private int key;

        private void btnNewDoctor_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            



        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            index = e.RowIndex - 1;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
         

        }

        private void Dashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control&&e.KeyCode==Keys.S)
            {
                ConnectDb.UpdateDoctors(doctor);
                dataGridView1.DataSource = ConnectDb.GetDoctors();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoctorForm form = new DoctorForm();
            form.ShowDialog();
            dataGridView1.DataSource= ConnectDb.GetDoctors();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NurseForm form = new NurseForm();
            form.ShowDialog();
            dataGridView2.DataSource = ConnectDb.GetNurses();
        }

        private void addSceduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddScedule scedule = new AddScedule(dataGridView1.SelectedRows[0], true);
            scedule.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SeeScedule scedule = new SeeScedule();
            scedule.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dtNurses.DefaultView;
            dv.RowFilter = string.Format("Name Like '%{0}%'", textBox2.Text);
            dataGridView2.DataSource = dv;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            NurseForm nurse = new NurseForm(true);
            nurse.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("doctors.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, ConnectDb.GetDoctors());
            stream.Close();
             formatter = new BinaryFormatter();
             stream = new FileStream("nurses.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, dtNurses);
            stream.Close();
            formatter = new BinaryFormatter();
            stream = new FileStream("scedule.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, ConnectDb.getScedule());
            stream.Close();
            MessageBox.Show("Serialization successfull");


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("doctors.txt", FileMode.Open, FileAccess.Read);
             dtDoctors = new DataTable();
            dtDoctors = (DataTable)formatter.Deserialize(stream);
            stream.Close();
            formatter = new BinaryFormatter();
            stream = new FileStream("nurses.txt", FileMode.Open, FileAccess.Read);
            dtNurses = (DataTable)formatter.Deserialize(stream);
            stream.Close();
            formatter = new BinaryFormatter();
            stream = new FileStream("scedule.txt", FileMode.Open, FileAccess.Read);
            dtScedule = (DataTable)formatter.Deserialize(stream);
            stream.Close();
            MessageBox.Show("Deserialization successfull");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dtDoctors.DefaultView;
            dv.RowFilter = string.Format("Name Like '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv;
        }
    }
}
