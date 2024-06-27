using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace Coursera
{
    public partial class Form1 : Form
    {
        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select CourseName from CourseInfo ";
            c.CommandType = CommandType.Text;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
               comboBox1.Items.Add(dr[0]);
            }
            dr.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select CourseID,CourseLevel,Category,CourseRating,CoursePrice,CourseDuration from CourseInfo where CourseName =:name";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("name", comboBox1.SelectedItem);
            OracleDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                textBox5.Text = dr[3].ToString();
                textBox4.Text = dr[4].ToString();
                textBox6.Text = dr[5].ToString();

            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into CourseInfo(CourseID,CourseName,CourseLevel,Category,CourseRating,CoursePrice,CourseDuration) values (:idd,:namee,:levell,:categoryy,:ratingg,:pricee,:durationn)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("idd", textBox1.Text);
            cmd.Parameters.Add("namee", comboBox1.Text);
            cmd.Parameters.Add("levell", textBox2.Text);
            cmd.Parameters.Add("categoryy", textBox3.Text);
            cmd.Parameters.Add("ratingg",textBox5.Text);
            cmd.Parameters.Add("pricee", textBox4.Text);
            cmd.Parameters.Add("durationn",textBox6.Text);
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                comboBox1.Items.Add(comboBox1.Text);
                MessageBox.Show("New Course Added Successfully");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String n;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GetNumberOfStudents";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("idd",textBox1.Text);
            cmd.Parameters.Add("Num", OracleDbType.Int32, ParameterDirection.Output);
            cmd.ExecuteNonQuery();
            n = cmd.Parameters["Num"].Value.ToString();
            textBox7.Text = n;

            OracleCommand cd = new OracleCommand();
            cd.Connection = conn;
            cd.CommandText = "GetStudentNames";
            cd.CommandType = CommandType.StoredProcedure;
            cd.Parameters.Add("idd", textBox1.Text);
            cd.Parameters.Add("Names", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0]);  
            }
            dr.Close();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select UserID,Country,Email from Users where Username =:name";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("name", comboBox2.SelectedItem);
            OracleDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                textBox8.Text = dr[0].ToString();
                textBox11.Text = dr[1].ToString();
                textBox10.Text = dr[2].ToString();

            }
            dr.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }
    }
}
