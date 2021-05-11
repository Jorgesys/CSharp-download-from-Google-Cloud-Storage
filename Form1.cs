
//C# Download Object from Google Cloud Storage by Jorgesys

using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Google_Cloud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            CallAPI();
        }

        private Task CallAPI()
        {
         /*Documentation:
         *https://support.google.com/googleplay/android-developer/answer/6135870#export&zippy=%2Cdownload-reports-using-a-command-line-tool%2Cdownload-reports-using-a-client-library-service-account
         *https://stackoverflow.com/questions/49475897/google-cloud-api-service-authorisation-failure-forbidden-access
         *https://cloud.google.com/docs/authentication/production?hl=es#passing_code
         */
            DownloadFile("grfirebase-12345a.appspot.com" /*bucketName*/, "estructuraReg.json"  /*File in bucket*/, @"C:\Users\jorgesys\estructuraReg.json" /*äht file*/);
            return Task.CompletedTask;
        }

        public void DownloadFile(
        string bucketName = "your-unique-bucket-name",
        string objectName = "my-file-name",
        string localPath = "my-local-path/my-file-name")
        {
            Debug.WriteLine($"bucketName: {bucketName}.");
            Debug.WriteLine($"objectName: {objectName}.");
            Debug.WriteLine($"localPath: {localPath}.");
            //Set credential
            var credential = GoogleCredential.FromFile(@"C:\Data\Development Android\GRJorgesys-b729ddb6e510.json");
            var storage = StorageClient.Create(credential);

            //List buckets in Project
            //The role assigned to your service account in https://console.cloud.google.com/iam-admin/iam/project must have the storage.buckets.list access.
            var buckets = storage.ListBuckets("mygoogleproyectid-1798c"); //ProjectId
            foreach (var bucket in buckets)
            {
                Debug.WriteLine("bucket: " + bucket.Name);
            }
            Debug.WriteLine("storage.GetType(): " + storage.GetType());

            try
            {
                var outputFile = File.OpenWrite(localPath);
                //Download file to local path.
                storage.DownloadObject(bucketName, objectName, outputFile);
                Debug.WriteLine($"*Downloaded {objectName} to {localPath}.");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception downloading file. {e.Message}.");
            }
        }



    }
}
