using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adoday2
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=.;Initial Catalog=ITI;Integrated Security=True");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlCommand selectcmd=new SqlCommand("select * from course",con);

            SqlDataAdapter adapter=new SqlDataAdapter();
            adapter.SelectCommand = selectcmd;

            dt=new DataTable();

            adapter.Fill(dt);

            DGV_course.DataSource= dt;
            btn_add.Visible = true;
            btn_update.Visible = false;


        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            DataRow r=dt.NewRow();
            r["crs_id"] = txt_id.Text;
            r["crs_name"]=txt_name.Text;
            r["crs_duration"] = txt_duaration.Text;
            r["top_id"]=cmb_topic.Text;

            dt.Rows.Add(r);
            txt_duaration.Text = txt_id.Text = txt_name.Text = cmb_topic.DisplayMember = "";

            DGV_course.DataSource= dt;

        }

        private void DGV_course_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_id.Text = DGV_course.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = DGV_course.SelectedRows[0].Cells[1].Value.ToString();
            txt_duaration.Text = DGV_course.SelectedRows[0].Cells[2].Value.ToString();
            cmb_topic.Text = DGV_course.SelectedRows[0].Cells[3].Value.ToString();

            txt_id.Enabled = false;
            btn_add.Visible = false;
            btn_update.Visible = true;

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if(item["crs_id"].ToString() == txt_id.Text)
                {
                    item["crs_name"] = txt_name.Text;
                    item["crs_duration"] = txt_duaration.Text;
                    item["top_id"] = cmb_topic.Text;
                                    }
                

              

            }
            DGV_course.DataSource = dt;
            txt_duaration.Text = txt_id.Text = txt_name.Text = cmb_topic.Text = "";
            txt_id.Enabled = true;
            btn_add.Visible = true;
            btn_update.Visible = true;

            MessageBox.Show("Updated!"); 
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SqlCommand insertCmd = new SqlCommand(" insert into Course values(@id , @name , @Duaration , @topid)", con);
            insertCmd.Parameters.Add("id", SqlDbType.Int, 4, "crs_id");
            insertCmd.Parameters.Add("name", SqlDbType.NVarChar, 50, "crs_name");
            insertCmd.Parameters.Add("Duaration", SqlDbType.Int, 4, "crs_duration");
            insertCmd.Parameters.Add("topid", SqlDbType.Int, 4, "top_id");

            SqlCommand UpdatedCmd = new SqlCommand(" update  Course set crs_name = @name ,crs_duration= @Duaration,top_id= @topid where crs_id= @id ", con);
            UpdatedCmd.Parameters.Add("id", SqlDbType.Int, 4, "crs_id");
            UpdatedCmd.Parameters.Add("name", SqlDbType.NVarChar, 50, "crs_name");
            UpdatedCmd.Parameters.Add("Duaration", SqlDbType.Int, 4, "crs_duration");
            UpdatedCmd.Parameters.Add("topid", SqlDbType.Int, 4, "top_id");




            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.InsertCommand = insertCmd;
            adpt.UpdateCommand = UpdatedCmd;

            adpt.Update(dt);
            MessageBox.Show("Database Updated!");

        }
    }
}
