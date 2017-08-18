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
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
//dd
        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                string key = textBox1.Text;
                string value = textBox2.Text;

                using (var bucket = Cluster.OpenBucket("osms_cb"))
                {
                    
                    if(bucket.Exists(key))
                    {
                        var result = bucket.Replace(key, value);

                        Console.WriteLine(String.Format("======Replace======={0},{1}", key, result.Success));
                    }
                    else
                    {
                        var result = bucket.Insert(key, value);

                        Console.WriteLine(String.Format("======Insert======={0},{1}", key, result.Success));
                    }
                    
                    
                }
            }
            catch (Exception ex)
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                string key = textBox3.Text;
                using (var bucket = Cluster.OpenBucket("osms_cb"))
                {

                    if (bucket.Exists(key))
                    {
                        // var result =  bucket.Get(key);
                        string strJson = bucket.Get<string>(key).Value;

                        /*
                        if (string.IsNullOrEmpty(strJson))
                        {
                         
                        }
                        else
                        {
                            return JsonConvert.DeserializeObject(strJson, typeof(T));
                        }
                        */


                        Console.WriteLine(String.Format("======get======={0},{1}", key, strJson));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public Cluster Cluster;
        private void button4_Click(object sender, EventArgs e)
        {
            ClientConfiguration ccf = new ClientConfiguration();
            ccf.Servers.Add(new Uri("http://192.168.1.200:8091/pools"));


            try
            {

                Cluster = new Cluster(ccf);
                Cluster.OpenBucket();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

       }
    }
}
