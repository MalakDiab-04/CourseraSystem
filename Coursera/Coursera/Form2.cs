using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursera
{
    public partial class Form2 : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constr = "Data source=orcl;User Id=scott;Password=tiger;";
            string cmdstr = "select AssessmentID,UserID, Score, AssessmentType,SubmissionDate from Assessments where CourseID=:course";
            adapter = new OracleDataAdapter(cmdstr,constr);
            adapter.SelectCommand.Parameters.Add("course", textBox1.Text);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }
    }
}
