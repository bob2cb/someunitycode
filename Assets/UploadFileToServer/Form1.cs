using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadSubstance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button_upload.Click += new System.EventHandler(this.OnUpload);
            this.button_select.Click += new System.EventHandler(this.OnSelected);
        }
     
        private void OnUpload(object sender, EventArgs e)
        {
            string server_api = "http://192.168.31.85:8106/api/upload/cityResource";
            string filePath = this.label.Text;
            string filename = filePath.Substring(filePath.LastIndexOf("\\") + 1);

            using (var httpClient = new HttpClient())
            {
                using (MultipartFormDataContent formData = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                    formData.Add(fileContent,"file", "test3.png");
                    formData.Add(new StringContent("basicresios/substance"), "path");
                    var result = httpClient.PostAsync(server_api, formData).Result;
                    Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                }
            }     
        }

        private void OnSelected(object sender, EventArgs e)
        {
            //初始化一个OpenFileDialog类
            OpenFileDialog fileDialog = new OpenFileDialog();

            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择的文件，并判断文件大小不能超过20K，fileInfo.Length是以字节为单位的
                FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                this.label.Text = fileInfo.FullName;
            }
        }
    }
}