using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ui
{
    public class UiClass
    {
        private readonly GameSettingsForm r_SettingsForm;
        public UiClass()
        {
            r_SettingsForm = new GameSettingsForm();
        }
        public void RunGame()
        {
            r_SettingsForm.ShowDialog();
        }
    }
}
