using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //treeView1.Nodes.Add("Товары1");

            //treeView1.Nodes.Add("Товары2");
            treeView1.CheckBoxes = true;
            TreeNode node1 = new TreeNode("Товары1");
            node1.Nodes.Add(new TreeNode("Компьютер"));
            node1.Nodes.Add(new TreeNode("Телефон"));
            node1.Nodes.Add(new TreeNode("Ноутбук"));
            treeView1.Nodes.Add(node1);
            treeView1.Nodes[0].Checked = true;
            node1.Nodes[1].Checked = true;

            
            TreeNode node2 = new TreeNode("Товары2");
            node2.Nodes.Add(new TreeNode("Компьютер2"));
            node2.Nodes.Add(new TreeNode("Телефон2"));
            node2.Nodes.Add(new TreeNode("Ноутбук2"));
            treeView1.Nodes.Add(node2);
        }

        private void FillDriveNodes()
        {
            foreach(var item in DriveInfo.GetDrives())
            {
                TreeNode treeNode = new TreeNode { Text = item.Name };
                FillTreeNode(treeNode, item.Name);
                treeView2.Nodes.Add(treeNode);
            }
        }

        private void FillTreeNode(TreeNode dirNode, String path)
        {
            string[] dirs = Directory.GetDirectories(path);
            foreach(var dir in dirs)
            {
                TreeNode item = new TreeNode();
                dirNode.Nodes.Add(item);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FillDriveNodes();
        }

        private void treeView2_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;

            if(Directory.Exists(e.Node.FullPath))
            {
                dirs = Directory.GetDirectories(e.Node.FullPath);
                if (dirs.Length!=0)
                {
                    for (int i=0; i<dirs.Length; i++)
                    {
                        TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                        FillTreeNode(dirNode, dirs[i]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
            }
        }

        private void treeView2_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;

            if (Directory.Exists(e.Node.FullPath))
            {
                dirs = Directory.GetDirectories(e.Node.FullPath);
                if (dirs.Length != 0)
                {
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                        FillTreeNode(dirNode, dirs[i]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
               
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            treeView3.ImageList = imageList1;

            treeView3.Nodes.Add(new TreeNode("Number 1", 1, 1));
            var node1 = new TreeNode("Number 2", 1, 1);
            treeView3.Nodes.Add(node1);
            node1.Nodes.Add(new TreeNode("Number 3", 2, 1));
            node1.Nodes.Add(new TreeNode("Number 4", 3, 3));
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            listBox1.Items.Add(e.Data.GetData(DataFormats.StringFormat).ToString());
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.DoDragDrop(textBox1.Text, DragDropEffects.Copy);
        }
    }
}
