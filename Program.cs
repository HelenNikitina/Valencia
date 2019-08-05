using System;
using System.Windows.Forms;

namespace SewingCompanyManagement
{
    static class Program
    {
        [STAThread]
        static int Main()
        {
            var entranceForm = new EntranceForm();
            entranceForm.StartPosition = FormStartPosition.CenterScreen;
            bool needGoOn = true;
            while (needGoOn)
            {
                entranceForm.ShowDialog();
                switch (entranceForm.CurrentEntranceType)
                {
                    case EntranceType.Admin:
                        new frmAdmin().ShowDialog();
                        break;
                    case EntranceType.Technolog:
                        new frmTechnologist().ShowDialog();
                        break;
                    case EntranceType.Master:
                        new frmMaster().ShowDialog();
                        break;
                    case EntranceType.Manager:
                        new frmManager().ShowDialog();
                        break;
                    case EntranceType.Exit:
                        needGoOn = false;
                        break;
                }
            }
            return 0;
        }

        
    }
}
