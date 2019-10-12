using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppConfigurations
{
    public class AppConfig
    {

        private void LoadConfigFiles(string selectedvalue = "")
        {
            string _path = Application.ExecutablePath.Replace(Application.ExecutablePath.Split('\\')[Application.ExecutablePath.Split('\\').Length - 1], "Configs\\");
            //var confingfiles = Directory.GetFiles(_path, "*.irconfig").ToList();
            //List<IRConfigs> lstIRConfigs = new List<IRConfigs>();
            
        }
        public void SaveConfig()
        { }
    }
}
