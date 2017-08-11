using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Couchbase.Configuration;
using Couchbase;
using Couchbase.Configuration.Client;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientConfiguration ccf = new ClientConfiguration();
            ccf.Servers.Add(new Uri("http://192.168.1.200:8091/pools/default"));

           
            try
            {
          
            Cluster Cluster = new Cluster(ccf);
            Cluster.OpenBucket();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            ClusterHelper.Initialize();
            var cluster = ClusterHelper.Get();
            using (var bucket = cluster.OpenBucket("default"))
            {
                var result = bucket.Insert("fookey", "foovalue");
              //  ViewBag.InsertOne = result.Success;
                var temp = bucket.Get<string>("fookey");
             //   ViewBag.InsertOneResult = temp.Value;
                result = bucket.Upsert("fookey", "foovalue2");
             //   ViewBag.InsertTwo = result.Success;
                var result1 = bucket.Remove("fookey");
               // ViewBag.InsertThree = result1.Success;
            }
        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


}
    }
}
