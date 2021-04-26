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
    public partial class DoctorForm : Form
    {
        public DoctorForm()
        {
            InitializeComponent();
        }
        Doctor doctor;
        int Id = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            doctor = new Doctor();
            doctor.Id = Id;
            doctor.Name = txtName.Text.ToString();
            doctor.Surname = txtSurname.Text.ToString();
            doctor.PeselNo = txtPeselNo.Text.ToString();
            doctor.PwzNo = txtPwzNo.Text.ToString();
            doctor.Speciality = txtSpeciality.Text.ToString();
            doctor.Username = txtUsername.Text.ToString();
            doctor.Password = txtPassword.Text.ToString();
            doctor.Type = 2;
            ConnectDb.UpdateDoctors(doctor);
        }
    }
}
