using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FileConnector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //查找所有文件
        }

        string folder_path;
        private void button1_Click(object sender, EventArgs e)
        {
            //选择文件目录
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folder_path = folderBrowserDialog1.SelectedPath;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //文件名字符
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //上移
            int sel_index = listBox2.SelectedIndex;
            string sel_str = listBox2.SelectedItem.ToString();
            if (sel_index > 0)
            {
                //将当前选中的项和前一项交换，并交换列表框中的选中序号
                listBox2.Items[sel_index] = listBox2.Items[sel_index - 1];
                listBox2.Items[sel_index - 1] = sel_str;
                listBox2.SetSelected(sel_index, false);
                listBox2.SetSelected(sel_index - 1, true);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //合并文件
            if (File.Exists(dest_file))
            {
                File.Delete(dest_file);
            }
            FileStream fs_dest = new FileStream(dest_file, FileMode.CreateNew, FileAccess.Write);
            byte[] DataBuffer = new byte[100000];
            byte[] file_name_buf;
            FileStream fs_source = null;
            int read_len;
            FileInfo fi_a = null;

            //逐个文件写入
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                fi_a = new FileInfo(listBox2.Items[i].ToString());
                file_name_buf = Encoding.Default.GetBytes(fi_a.Name);

                //写入文件名
                fs_dest.Write(file_name_buf, 0, file_name_buf.Length);



                //换行
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);
                fs_source = new FileStream(fi_a.FullName, FileMode.Open, FileAccess.Read);
                read_len = fs_source.Read(DataBuffer, 0, 100000);
                while (read_len > 0)
                {
                    fs_dest.Write(DataBuffer, 0, read_len); ;
                    read_len = fs_source.Read(DataBuffer, 0, 100000);
                }

                // 换行
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);
                fs_source.Close();
            }

            //关闭文件流并打开目标文件
            fs_source.Dispose();
            fs_dest.Flush();
            fs_dest.Close();
            fs_dest.Dispose();
            Process.Start(dest_file);
        }

        public static string[] folder_files;
        private void button2_Click(object sender, EventArgs e)
        {
            //查找所有文件
            if(Directory.Exists(folder_path))//文件目录是否存在
            {
                //搜索给定字符串文件
                folder_files = Directory.GetFiles(folder_path, textBox1.Text, SearchOption.AllDirectories);
                listBox1.Items.Clear();
                int selected_index = 0;
                foreach(string folder_file in folder_files)
                {
                    selected_index = listBox1.Items.Add(folder_files);
                    listBox1.SetSelected(selected_index, true);
                }
               
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //下移
            int sel_index = listBox2.SelectedIndex;
            string sel_str = listBox2.SelectedItem.ToString();
            if (sel_index > 0)
            {
                //将当前选中的项和前一项交换，并交换列表框中的选中序号
                listBox2.Items[sel_index] = listBox2.Items[sel_index + 1];
                listBox2.Items[sel_index + 1] = sel_str;
                listBox2.SetSelected(sel_index, false);
                listBox2.SetSelected(sel_index + 1, true);
            }

        }

        public static string dest_file;
        private void button7_Click(object sender, EventArgs e)
        {
            //目标文件名
            saveFileDialog1.Title = "选择要合并后的文件";
            saveFileDialog1.InitialDirectory = System.Environment.SpecialFolder.DesktopDirectory.ToString();
            saveFileDialog1.OverwritePrompt = false;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dest_file = saveFileDialog1.FileName;
                label2.Text = dest_file;
            }
        }
    }
}
