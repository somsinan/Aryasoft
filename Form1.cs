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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace AryaSoft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {

            //YOL DİZİNİN KODU
            SaveFileDialog saveFileDialog1 = new SaveFileDialog() ;

            saveFileDialog1.Title = "yedeklnecek yolu beliritniz";
            saveFileDialog1.Filter = "yedekleme dosyaları(*.bak)|*.bak|Tüm dosyalar(*.*)|*.*";

            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {

                txtPath.Text = saveFileDialog1.FileName;

            }




        }

        private void btn_Back_Up_Click(object sender, EventArgs e)
        {

            //Server connection 3 adet parametre alıyor serverIstance usarname,password
            Server dbserver = new Server(new ServerConnection(Database_txt.Text));
            Backup dbBackUP = new Backup();

            dbBackUP.Action = BackupActionType.Database;
            dbBackUP.Database = Database_txt.Text;
            //EKSİK VAR BAK
            //
            //dbBackUP.Devices.AddDevice(path_txt.Text);

            dbBackUP.Devices.AddDevice(txtPath.Text, DeviceType.File);


            dbBackUP.Initialize = true;

            dbBackUP.SqlBackup(dbserver);

            dbBackUP.Complete += DbBackUP_Complete;


        }

        //YEDEKLEME BASARLI POPUP
        private void DbBackUP_Complete(object sender, ServerMessageEventArgs e)
        {
            try
            {
                MessageBox.Show("YEDEKLEME BAŞARALI","WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex )
            {

                MessageBox.Show("YEDEKLEME BAŞARISIZ", "WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
        }
    }
}
