using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    class ConnectDb
    {
        public static string connectionString = @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Hospital.mdf;integrated security=True;MultipleActiveResultSets=True;";
        public static SqlConnection con=new SqlConnection(connectionString);
         
        internal static object getScedule()
        {
            cmd = new SqlCommand("SELECT u.Name,t.Type,u.Speciality,d.DutyDate As 'on-call duty date' FROM DutyScedule d INNER JOIN Users u on d.UserId=u.Id INNER JOIN UserTypes t on u.Type=t.Id ORDER BY d.DutyDate DESC",OpenConnection());
            sdr = cmd.ExecuteReader();
            var dt = new DataTable();
             dt.Load(sdr);
            return dt;
        }

        public static SqlCommand cmd;
        public static SqlDataReader sdr;

        internal static DataTable GetDoctors()
        {
           
            cmd = new SqlCommand("SELECT * FROM Users Where Type=2", OpenConnection());
            sdr= cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(sdr);
            return dt;
           
        }

        internal static DataTable GetDoctorsScedule()
        {
            cmd = new SqlCommand("SELECT * FROM DutyScedule d INNER JOIN Users u on d.UserId=u.Id   Where u.Type=2", OpenConnection());
            sdr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(sdr);
            return dt;
        }
        internal static DataTable GetNursesScedule()
        {
            cmd = new SqlCommand("SELECT * FROM DutyScedule d INNER JOIN Users u on d.UserId=u.Id   Where u.Type=3", OpenConnection());
            sdr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(sdr);
            return dt;
        }

        internal static DataTable GetNurses()
        {
            cmd = new SqlCommand("SELECT * FROM Users Where Type=3", OpenConnection());
            sdr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(sdr);
            return dt;
        }

        internal static void Updateusers(User user)
        {
            cmd = new SqlCommand(@"IF NOT EXISTS(SELECT * FROM Users Where Id=@id)
                BEGIN INSERT INTO Users( Name,Surname,PeselNo,Username,Password,Type) Values(@name,@surname,@peselno
                   ,@username,@password,@type)
           END
           ELSE 
             BEGIN UPDATE Users SET Name=@name,Surname=@surname,PeselNo=@peselno,Username=@username,Password=@password,Type=@type END", OpenConnection());

            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@surname", user.Surname);
            cmd.Parameters.AddWithValue("@peselno", user.PeselNo);
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@type", user.Type);
            cmd.ExecuteNonQuery();
            MessageBox.Show("New User created successfully");
        }

        //connection to database
        public static SqlConnection OpenConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            return con;
        }

        internal static void insertScedule(DutyScedule scedule)
        {
            cmd = new SqlCommand("INSERT INTO DutyScedule Values(@date,@userId) ", OpenConnection());
            cmd.Parameters.AddWithValue("@date", scedule.DutyDate);
            cmd.Parameters.AddWithValue("@userId", scedule.UserId);
            cmd.ExecuteNonQuery();
        }

        //insert record into user table for admin
        public static void InsertUser()
        {
           
                cmd = new SqlCommand(@"INSERT INTO Users(Username,Password,Type) Values(@username,@password,@type)", OpenConnection());
                cmd.Parameters.AddWithValue("@username", "admin");
            cmd.Parameters.AddWithValue("@password", "123456");
            cmd.Parameters.AddWithValue("@type", "1");
            cmd.ExecuteNonQuery();
           
        }
        //insert user roles one time.
        public static void InsertUserType(List<Role> types)
        {
            foreach(Role type in types)
            {
                cmd = new SqlCommand(@"INSERT INTO UserTypes Values(@type)", OpenConnection());
                cmd.Parameters.AddWithValue("@type", type.Type);
                cmd.ExecuteNonQuery();
            }
        }

        internal static void UpdateDoctors(Doctor doctor)
        {
            cmd = new SqlCommand(@"IF NOT EXISTS(SELECT * FROM Users Where Id=@id)
                BEGIN INSERT INTO Users( Name,Surname,PeselNo,Speciality,PwzNo,Username,Password,Type) Values(@name,@surname,@peselno,@speciality
                   ,@pwzno,@username,@password,@type)
           END
           ELSE 
             BEGIN UPDATE Users SET Name=@name,Surname=@surname,PeselNo=@peselno,Speciality=@speciality,PwzNo=@pwzno,Username=@username,Password=@password,Type=@type END",OpenConnection());

            cmd.Parameters.AddWithValue("@id", doctor.Id);
            cmd.Parameters.AddWithValue("@name", doctor.Name);
            cmd.Parameters.AddWithValue("@surname", doctor.Surname);
            cmd.Parameters.AddWithValue("@peselno", doctor.PeselNo);
            cmd.Parameters.AddWithValue("@speciality", doctor.Speciality);
            cmd.Parameters.AddWithValue("@pwzno", doctor.PwzNo);
            cmd.Parameters.AddWithValue("@username", doctor.Username);
            cmd.Parameters.AddWithValue("@password", doctor.Password);
            cmd.Parameters.AddWithValue("@type", doctor.Type);
            cmd.ExecuteNonQuery();
            MessageBox.Show("New User created successfully");

        }

        //get user types from database and fill list
        public static void fillUserTypes(ComboBox cmb)
        {
            //query to get data
            cmd = new SqlCommand("SELECT * FROM UserTypes", OpenConnection());
            sdr = cmd.ExecuteReader();
            //fill datatable from data
            DataTable dt = new DataTable();
            dt.Load(sdr);
            //autocomplete suggestion
            cmb.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmb.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb.AutoCompleteSource = AutoCompleteSource.CustomSource;
           AutoCompleteStringCollection comboCollection = new AutoCompleteStringCollection();
            Dictionary<int, String> comboSource;
            comboSource = new Dictionary<int, String>();

            foreach (DataRow dr in dt.Rows)
            {
                comboSource.Add(int.Parse(dr["Id"].ToString()), dr["Type"].ToString());
                comboCollection.Add(dr["Type"].ToString());
            }
            if (dt.Rows.Count > 0)
            {
                cmb.DataSource = new BindingSource(comboSource, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = 0;
                cmb.AutoCompleteCustomSource = comboCollection;
            }
        }

        //verify user login credentials
        internal static bool Authentication(Login login)
        {
            cmd = new SqlCommand("SELECT * FROM Users where Username=@username and Password=@password and Type=@type", OpenConnection());
            cmd.Parameters.AddWithValue("@username", login.Username);
            cmd.Parameters.AddWithValue("@password", login.Password);
            cmd.Parameters.AddWithValue("@type", login.Type);
            sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
                return true;
            else
                return false;
        }



    }
}
