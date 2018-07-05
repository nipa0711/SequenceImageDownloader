using System;
using System.IO;
using System.Net;
using System.Windows;

namespace ImageDownloader
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private string folderPath;
        private string Url;

        public void SetFolderPath(string path)
        {
            folderPath = path;
            txtLocation.Text = folderPath;
        }

        public string GetFolderPath()
        {
            return folderPath;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = GetFolderPath();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            String curSaveLoc = AppDomain.CurrentDomain.BaseDirectory;
            SetFolderPath(curSaveLoc);
        }

        private void BtnDownloadLoc_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetFolderPath(dialog.SelectedPath);
            }
        }

        private void BtnURL_Click(object sender, RoutedEventArgs e)
        {
            Url = txtUrl.Text;
            btnSeq.IsEnabled = true;
        }

        private void BtnSeq_Click(object sender, RoutedEventArgs e)
        {
            int i = Int32.Parse(txtStart.Text);
            int j = Int32.Parse(txtEnd.Text);
            string extension = txtFileExtension.Text;
            String digitCnt = "D" + txtCnt.Text;
            for (; i < j; i++)
            {
                string list = Url + i.ToString(digitCnt) + extension;
                list_url.Items.Add(list);
            }

            BtnDelSel.IsEnabled = true;
            BtnDelAll.IsEnabled = true;
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new WebClient();

                for (int i = 0; i < list_url.Items.Count; i++)
                {
                    string fileName = list_url.Items.GetItemAt(i).ToString().Replace(Url, "");
                    client.DownloadFile(list_url.Items.GetItemAt(i).ToString(), folderPath + "\\" + fileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("파일이 없거나 경로를 확인해주세요.\n" + "다운로드가 중단되었습니다.");
            }
        }

        private void BtnDelAll_Click(object sender, RoutedEventArgs e)
        {
            list_url.Items.Clear();
            BtnDelSel.IsEnabled = false;
            BtnDelAll.IsEnabled = false;
        }

        private void BtnDelSel_Click(object sender, RoutedEventArgs e)
        {
            list_url.Items.Remove(list_url.SelectedItems[0]);

            if (list_url.Items.Count < 1)
            {
                BtnDelSel.IsEnabled = false;
                BtnDelAll.IsEnabled = false;
            }
        }
    }
}
