using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinRecorder
{
    public class WMIReader
    {
        ManagementClass objMC;
        ManagementObjectCollection objMOC;
        public WMIReader()
        { 
        
        
        
        }
        public Win32_Process GetComputerDetails()
        {
            objMC = new ManagementClass("Win32_ComputerSystem");
            objMOC = objMC.GetInstances();

            Win32_Process objData = new Win32_Process();
            var _type = typeof(Win32_Process);
            var _properties = _type.GetProperties();
            foreach (ManagementObject item in objMOC)
            {
                for (int i = 0; i < _properties.Length; i++)
                {
                    try
                    {

                        if (item.Properties[_properties[i].Name].Value != null)
                            objData.GetType().GetProperty(_properties[i].Name).SetValue(objData, item.Properties[_properties[i].Name].Value, null);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return objData;
        }
    }
    public class Win32_Process
    {
        string CreationClassName;
        string Caption;
        string CommandLine;
        string CreationDate;
        string CSCreationClassName;
        string CSName;
        string Description;
        string ExecutablePath;
        string ExecutionState;
        string Handle;
        string HandleCount;
        string InstallDate;
        string KernelModeTime;
        string MaximumWorkingSetSize;
        string MinimumWorkingSetSize;
        string Name;
        string OSCreationClassName;
        string OSName;
        string OtherOperationCount;
        string OtherTransferCount;
        string PageFaults;
        string PageFileUsage;
        string ParentProcessId;
        string PeakPageFileUsage;
        string PeakVirtualSize;
        string PeakWorkingSetSize;
        string Priority;
        string PrivatePageCount;
        string ProcessId;
        string QuotaNonPagedPoolUsage;
        string QuotaPagedPoolUsage;
        string QuotaPeakNonPagedPoolUsage;
        string QuotaPeakPagedPoolUsage;
        string ReadOperationCount;
        string ReadTransferCount;
        string SessionId;
        string Status;
        string TerminationDate;
        string ThreadCount;
        string UserModeTime;
        string VirtualSize;
        string WindowsVersion;
        string WorkingSetSize;
        string WriteOperationCount;
        string WriteTransferCount;
    };
}
