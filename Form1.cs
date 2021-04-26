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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Role role= new Role();
            role.InitializeTypes();
            ConnectDb.fillUserTypes(comboBox1);
           ConnectDb.InsertUser();

             
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateControls())
            {
                Login login = new Login();
                login.Username = textBox1.Text;
                login.Password = textBox2.Text;
                login.Type = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
                if (login.verifyLogin(login))
                {
                    this.Hide();
                    Dashboard dashboard = new Dashboard(((KeyValuePair<int, string>)comboBox1.SelectedItem).Key);
                    dashboard.ShowDialog();
                }
                else
                    MessageBox.Show("Invalid Login Credentials");
            }
        }

        private bool ValidateControls()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Username Required");
                textBox1.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Password Required");
                textBox2.Focus();
                return false;
            }
            else
                return true;
        }
    }
}
