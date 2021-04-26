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
    public partial class NurseForm : Form
    {
        public NurseForm()
        {
            InitializeComponent();
        }

        public NurseForm(bool v)
        {
            InitializeComponent();
            this.v = v;
            groupBox1.Text = "Administrator Profile";
            this.Text = "Administartor Profile";
        }

        User user;
        int id = 0;
        private bool v;

        private void button1_Click(object sender, EventArgs e)
        {
           if(v)
            {
                user = new Nurse();
                user.Id = id;
                user.Name = txtName.Text.ToString();
                user.Surname = txtSurname.Text.ToString();
                user.PeselNo = txtPeselNo.Text.ToString();
                user.Username = txtUsername.Text.ToString();
                user.Password = txtPassword.Text.ToString();
                user.Type = 1;
                ConnectDb.Updateusers(user);
            }
           else

            {
                user = new Nurse();
                user.Id = id;
                user.Name = txtName.Text.ToString();
                user.Surname = txtSurname.Text.ToString();
                user.PeselNo = txtPeselNo.Text.ToString();
                user.Username = txtUsername.Text.ToString();
                user.Password = txtPassword.Text.ToString();
                user.Type = 3;
                ConnectDb.Updateusers(user);
            }
        }
    }
}
