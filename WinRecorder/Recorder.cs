using ErrorLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Reflection;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using System.IO;
using System.Drawing.Imaging;
namespace WinRecorder
{
    public partial class Recorder : Form
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rectangle rect);
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;



        private IKeyboardMouseEvents objGlobalHooks;
        private InputSimulator objInputSimulator;
        RecordedSession objSession;
        RecordedSession objLoadedScript;
        bool _IsSessionActive = false;
        Process LaunchedApp = null;
        IntPtr LaunchedAppHandle = IntPtr.Zero;
        bool IsSessionActive
        {
            get { return _IsSessionActive; }
            set
            {
                _IsSessionActive = value;

                if (_IsSessionActive == true)
                {
                    btnStopRecording.Enabled = true;
                    btnTakeScreenShot.Enabled = true;
                    btnRecordKeyStrokes.Enabled = false;

                    btnSaveScript.Enabled = false;


                }
                else
                {
                    btnStopRecording.Enabled = false;
                    btnTakeScreenShot.Enabled = false;
                    btnRecordKeyStrokes.Enabled = true;
                    if (objSession != null && objSession.Events.Count > 0)
                    {
                        btnSaveScript.Enabled = true;
                        btnReplay.Enabled = true;
                    }
                    else
                    {
                        btnSaveScript.Enabled = false;
                        btnReplay.Enabled = false;

                    }
                }
            }
        }

        bool _IsReplayActive = false;
        bool IsReplayActive
        {
            get { return _IsReplayActive; }
            set
            {
                _IsReplayActive = value;

                if (_IsReplayActive == true)
                {

                    btnStopReplay.Enabled = true;
                    btnStopRecording.Enabled = false;
                    btnTakeScreenShot.Enabled = false;

                    btnLaunchApp.Enabled = false;
                    btnBrowseLaunchApp.Enabled = false;
                    btnRecordKeyStrokes.Enabled = false;
                    btnReplay.Enabled = false;
                    btnSaveScript.Enabled = false;
                    btnLoadScript.Enabled = false;

                }
                else
                {
                    EnableControls(this);
                    btnStopReplay.Enabled = false;
                    btnStopRecording.Enabled = false;
                    btnTakeScreenShot.Enabled = false;

                    btnLaunchApp.Enabled = true;
                    btnBrowseLaunchApp.Enabled = true;
                    btnRecordKeyStrokes.Enabled = true;
                    btnReplay.Enabled = true;
                    btnSaveScript.Enabled = true;
                    btnLoadScript.Enabled = true;
                }
            }
        }
        public bool _IsScriptLoaded = false;
        bool IsScriptLoaded
        {
            get { return _IsScriptLoaded; }
            set
            {
                _IsScriptLoaded = value;
                if (objSession != null && objSession.Events.Count > 0)
                {
                    btnSaveScript.Enabled = true;
                    btnReplay.Enabled = true;


                }
                else
                {
                    btnSaveScript.Enabled = false;
                    btnReplay.Enabled = false;
                }
            }
        }


        public Recorder()
        {
            InitializeComponent();
        }
        private void Recorder_Load(object sender, EventArgs e)
        {
            try
            {
                GetAppConfig();
                btnBrowseLaunchApp.Focus();

            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside Recorder_Load in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }
        private void DisableControls(Control con, List<string> SkippedControlId)
        {
            foreach (Control c in con.Controls)
            {
                DisableControls(c, SkippedControlId);
            }
            if (SkippedControlId.IndexOf(con.Name) < 0)
                con.Enabled = false;
            else
                con.Enabled = true;
        }

        private void EnableControls(Control con)
        {
            if (con != null)
            {
                con.Enabled = true;
                EnableControls(con.Parent);
            }
        }
        private void GetAppConfig()
        {
            try
            {
                try
                {
                    string _configDirectoryPath = Application.ExecutablePath.Replace(Application.ExecutablePath.Split('\\')[Application.ExecutablePath.Split('\\').Length - 1], "Configs\\");
                    string _configFilePath = _configDirectoryPath + "config.ini";
                    string storagepath = Application.ExecutablePath.Replace(Application.ExecutablePath.Split('\\')[Application.ExecutablePath.Split('\\').Length - 1], "AppStorage\\");
                    if (!Directory.Exists(_configDirectoryPath))
                    {
                        Directory.CreateDirectory(_configDirectoryPath);
                        StreamWriter objStreamWriter = new StreamWriter(_configFilePath, false);
                        objStreamWriter.WriteLine(storagepath);
                        objStreamWriter.Close();
                    }
                    else if (!File.Exists(_configFilePath))
                    {
                        StreamWriter objStreamWriter = new StreamWriter(_configFilePath, false);
                        objStreamWriter.WriteLine(storagepath);
                        objStreamWriter.Close();
                    }
                    else
                    {
                        StreamReader objReader = new StreamReader(_configFilePath);
                        storagepath = objReader.ReadLine();
                        objReader.Close();
                    }
                    txtDataStorageFolder.Text = storagepath;
                }
                catch (Exception ex)
                {

                    Logger.Log("Error Occured inside Level-1 GetAppConfig in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
                }
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside Level-2 GetAppConfig in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }
        private void UpdateAppConfig()
        {
            try
            {
                string _path = Application.ExecutablePath.Replace(Application.ExecutablePath.Split('\\')[Application.ExecutablePath.Split('\\').Length - 1], "Configs\\");
                string _configFile = _path + "config.ini";
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                StreamWriter objStreamWriter = new StreamWriter(_configFile, false);
                objStreamWriter.WriteLine(txtDataStorageFolder.Text);
                objStreamWriter.Close();
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside UpdateAppConfig in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }
        private void txtScriptWindow_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtScriptWindow.SelectionStart = txtScriptWindow.Text.Length;
                txtScriptWindow.ScrollToCaret();
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside txtScriptWindow_TextChanged in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        private void LogMessage(string Message)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    txtScriptWindow.Text += Message + Environment.NewLine;
                }));

            }
            catch (Exception ex)
            {
                Logger.Log("Error Occured inside LogMessage in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
                LogMessage("Error : " + ex.Message);
            }
        }

        private void btnBrowseLaunchApp_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog objOpenFileDialog = new OpenFileDialog();
                if (DialogResult.OK == objOpenFileDialog.ShowDialog())
                {
                    txtLaunchAppLocation.Text = objOpenFileDialog.FileName;
                    txtLaunchAppLocation.Focus();
                    txtLaunchAppLocation.SelectionStart = txtLaunchAppLocation.Text.Length;
                }
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside btnBrowseLaunchApp_Click in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        private void btnDataStorageFoler_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog objFolderBrowserDialog = new FolderBrowserDialog();
                if (objFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDataStorageFolder.Text = objFolderBrowserDialog.SelectedPath;
                    txtDataStorageFolder.Focus();
                    txtDataStorageFolder.SelectionStart = txtDataStorageFolder.Text.Length;
                    UpdateAppConfig();
                }
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside btnDataStorageFoler_Click in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }



        private void btnRecordKeyStrokes_Click(object sender, EventArgs e)
        {
            try
            {
                objSession = new RecordedSession();
                if (LaunchedApp != null)
                {
                    Rectangle rect = new Rectangle();
                    GetWindowRect(LaunchedApp.Handle, out rect);
                    Session.Delay = DateTime.Now.Subtract(LaunchedApp.StartTime).TotalMilliseconds;
                    Session.StartTime = DateTime.Now;
                    objSession.Events.Add(new WinEvent()
                    {
                        Delay = Session.Delay.ToString("0.00"),
                        PerformedEvent = EventTypes.OpenApp.ToString(),
                        AppPosition = rect,
                        AppPath = LaunchedApp.StartInfo.FileName
                    });
                }
                objSession.Application = txtLaunchAppLocation.Text.Split('\\')[txtLaunchAppLocation.Text.Split('\\').Length - 1];
                objSession.RecorderVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                objSession.CreationDate = DateTime.Now.ToString();
                objSession.Name = "Test-Session";
                Session.StartTime = DateTime.Now;

                LogMessage("Recording Started.");
                objGlobalHooks = Hook.GlobalEvents();
                objGlobalHooks.MouseClick += objGlobalHooks_MouseClick;
                objGlobalHooks.MouseDoubleClick += objGlobalHooks_MouseDoubleClick;
                objGlobalHooks.MouseDown += objGlobalHooks_MouseDown;
                objGlobalHooks.MouseUp += objGlobalHooks_MouseUp;
                objGlobalHooks.MouseMove += objGlobalHooks_MouseMove;

                objGlobalHooks.KeyDown += objGlobalHooks_KeyDown;
                objGlobalHooks.KeyUp += objGlobalHooks_KeyUp;
                objGlobalHooks.KeyPress += objGlobalHooks_KeyPress;



                IsSessionActive = true;
            }
            catch (Exception ex)
            {
                IsSessionActive = false;
                Logger.Log("Error Occured inside btnRecordKeyStrokes_Click in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        void objGlobalHooks_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                objSession.Events.Add(new WinEvent()
                {

                    Delay = Session.Delay.ToString("0.00"),
                    PerformedEvent = EventTypes.KeyPress.ToString(),
                    PressedKey = e.KeyChar

                });
                LogMessage("Key Press: " + e.KeyChar.ToString());
            }
            catch (Exception ex)
            {
                Logger.Log("Error Occured inside objGlobalHooks_KeyPress in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        void objGlobalHooks_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                objSession.Events.Add(new WinEvent()
                {
                    Delay = Session.Delay.ToString("0.00"),
                    PerformedEvent = EventTypes.KeyUp.ToString(),
                    KeyCode = e.KeyValue
                });

            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside objGlobalHooks_KeyUp in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        void objGlobalHooks_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                objSession.Events.Add(new WinEvent()
                {
                    Delay = Session.Delay.ToString("0.00"),
                    PerformedEvent = EventTypes.KeyDown.ToString(),
                    KeyCode = e.KeyValue
                });
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside objGlobalHooks_KeyDown in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }

        }

        void objGlobalHooks_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                objSession.Events.Add(new WinEvent()
                {
                    Delay = Session.Delay.ToString("0.00"),
                    PerformedEvent = EventTypes.MMove.ToString(),
                    Wheel = e.Delta,
                    X = e.X,
                    Y = e.Y,
                    Button = e.Button.ToString()
                });
                LogMessage("Mouse Move : " + e.X + "," + e.Y);
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside objGlobalHooks_MouseMove in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }

        }

        void objGlobalHooks_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                objSession.Events.Add(new WinEvent()
                {
                    Delay = Session.Delay.ToString("0.00"),
                    PerformedEvent = EventTypes.MUp.ToString(),
                    Wheel = e.Delta,
                    X = e.X,
                    Y = e.Y,
                    Button = e.Button.ToString()
                });
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside objGlobalHooks_MouseUp in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        void objGlobalHooks_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                objSession.Events.Add(new WinEvent()
                {
                    Delay = Session.Delay.ToString("0.00"),
                    PerformedEvent = EventTypes.MDown.ToString(),
                    Wheel = e.Delta,
                    X = e.X,
                    Y = e.Y,
                    Button = e.Button.ToString()
                });
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside objGlobalHooks_MouseDown in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        void objGlobalHooks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                objSession.Events.Add(new WinEvent()
                {
                    Delay = Session.Delay.ToString("0.00"),
                    PerformedEvent = EventTypes.MDoubleClick.ToString(),
                    Wheel = e.Delta,
                    X = e.X,
                    Y = e.Y,
                    Button = e.Button.ToString()
                });
                LogMessage("Mouse Double Click : " + e.Button);
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside objGlobalHooks_MouseDoubleClick in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        void objGlobalHooks_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                objSession.Events.Add(new WinEvent()
                {
                    Delay = Session.Delay.ToString("0.00"),
                    PerformedEvent = EventTypes.MClick.ToString(),
                    Wheel = e.Delta,
                    X = e.X,
                    Y = e.Y,
                    Button = e.Button.ToString()
                });
                LogMessage("Mouse Click : " + e.Button);
            }
            catch (Exception ex)
            {

                Logger.Log("Error Occured inside objGlobalHooks_MouseClick in Recorder.cs, " + ex.Message + ", " + ex.InnerException);

            }
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                objGlobalHooks.MouseClick -= objGlobalHooks_MouseClick;
                objGlobalHooks.MouseDoubleClick -= objGlobalHooks_MouseDoubleClick;
                objGlobalHooks.MouseDown -= objGlobalHooks_MouseDown;
                objGlobalHooks.MouseUp -= objGlobalHooks_MouseUp;
                objGlobalHooks.MouseMove -= objGlobalHooks_MouseMove;


                objGlobalHooks.KeyDown -= objGlobalHooks_KeyDown;
                objGlobalHooks.KeyUp -= objGlobalHooks_KeyUp;
                objGlobalHooks.KeyPress -= objGlobalHooks_KeyPress;
                Thread.Sleep(250);
                var json = JsonConvert.SerializeObject(objSession, Formatting.Indented);

                txtScriptWindow.Text += json + Environment.NewLine;

                IsSessionActive = false;
                LogMessage("Recording Stopped.");
                Cursor = Cursors.Default;


            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Logger.Log("Error Occured inside btnStopRecording_Click in Recorder.cs, " + ex.Message + ", " + ex.InnerException);
            }
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            try
            {
                IsReplayActive = true;
                LogMessage("Replay Started.");
                Thread thrd_Replay = new Thread(delegate()
                {
                    Thread.Sleep(5000);
                    objInputSimulator = new InputSimulator();
                    for (int i = 0; i < objSession.Events.Count; i++)
                    {
                        if (!IsReplayActive)
                            break;
                        var command = objSession.Events[i];
                        //objSession.Events.ForEach(command =>
                        //{

                        try
                        {
                            Thread.Sleep((int)double.Parse(command.Delay));
                            switch (command.PerformedEvent)
                            {
                                case "MMove":
                                    MouseInput.PerformMouseEvent("MOVE", command.X, command.Y, this);
                                    LogMessage("Mouse Move : " + command.X + "," + command.Y);
                                    break;
                                case "MClick":
                                    LogMessage("Mouse Click : " + command.Button);
                                    break;
                                case "MDown":
                                    MouseInput.PerformMouseEvent("DOWN", command.X, command.Y, this);
                                    break;
                                case "MUp":
                                    MouseInput.PerformMouseEvent("UP", command.X, command.Y, this);
                                    break;
                                case "MDoubleClick":
                                    LogMessage("Mouse Double Click : " + command.Button);
                                    break;
                                case "MWheel":
                                    break;
                                case "KeyUp":
                                    try
                                    {
                                        var vk = (VirtualKeyCode)command.KeyCode;
                                        objInputSimulator.Keyboard.KeyUp(vk);
                                    }
                                    catch (Exception ex)
                                    {


                                    }

                                    break;
                                case "KeyDown":
                                    try
                                    {
                                        var vk = (VirtualKeyCode)command.KeyCode;
                                        objInputSimulator.Keyboard.KeyDown(vk);
                                    }
                                    catch (Exception ex)
                                    {


                                    }
                                    break;
                                case "OpenApp":
                                    try
                                    {
                                        StartProcess(command.AppPath);
                                        Thread.Sleep(250);
                                        SetWindowPos(LaunchedApp.Handle, 0, command.AppPosition.Left, command.AppPosition.Top, command.AppPosition.Right, command.AppPosition.Bottom, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
                                        LogMessage("Open Application : " + command.AppPath);
                                    }
                                    catch (Exception ex)
                                    {

                                        throw;
                                    }
                                    break;
                                case "ScreenShot":
                                    try
                                    {
                                        if (!Directory.Exists(command.SSPath))
                                        {
                                            Directory.CreateDirectory(command.SSPath);
                                        }
                                        SaveScreenShot(command.SSPath);
                                        LogMessage("Screen Shot Taken.");
                                    }
                                    catch (Exception ex)
                                    {

                                        throw;
                                    }
                                    break;
                                case "KeyPress":
                                    LogMessage("Key Press: " + command.PressedKey.ToString());
                                    break;
                                default:
                                    break;
                            }

                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                        // });
                    }
                    this.Invoke(new Action(() =>
                    {
                        IsReplayActive = false;
                        LogMessage("Replay Stopped.");
                    }));

                });
                thrd_Replay.IsBackground = true;
                thrd_Replay.Start();
            }
            catch (Exception ex)
            {
                IsReplayActive = false;
                throw;
            }
        }

        private void btnLaunchApp_Click(object sender, EventArgs e)
        {
            try
            {

                if (StartProcess(txtLaunchAppLocation.Text))
                {
                    txtScriptWindow.Text += Environment.NewLine + "App Launched Successfully." + Environment.NewLine + "Launched App Path : " + LaunchedApp.StartInfo.FileName;
                }

            }
            catch (Exception ex)
            {

                txtScriptWindow.Text += Environment.NewLine + "Some unexpected error occured while launching app." + Environment.NewLine + "Details : " + ex.Message;
            }
        }

        private bool StartProcess(string ProcessPath)
        {
            LaunchedApp = Process.Start(ProcessPath);
            try
            {
                while (LaunchedApp.Handle == IntPtr.Zero)
                {
                    // Discard cached information about the process
                    // because MainWindowHandle might be cached.
                    LaunchedApp.Refresh();
                    // proc.
                    Thread.Sleep(10);
                }
                //var handle = proc.Handle;
                //Rectangle rect = new Rectangle();
                //GetWindowRect(handle, out rect);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                // The process has probably exited,
                // so accessing MainWindowHandle threw an exception
            }
        }

        private void txtDataStorageFolder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtDataStorageFolder.Focus();
                txtDataStorageFolder.SelectionStart = txtDataStorageFolder.Text.Length;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnTakeScreenShot_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                 * This method will store screen shots in specified storage folder
                 * If specified storage folde does not exists, it will store at default location 
                 * i.e ApplicationInstallationFolder\AppStorage
                 */
                this.Cursor = Cursors.WaitCursor;
                btnTakeScreenShot.Enabled = false;
                Session.Delay = DateTime.Now.Subtract(Session.StartTime).TotalMilliseconds;
                Session.StartTime = DateTime.Now;
                string storageFolder = txtDataStorageFolder.Text;
                if (!Directory.Exists(storageFolder))
                {
                    Directory.CreateDirectory(storageFolder);
                    UpdateAppConfig();
                }
                storageFolder = txtDataStorageFolder.Text;
                if (SaveScreenShot(storageFolder))
                {
                    if (objSession != null)
                    {
                        objSession.Events.Add(new WinEvent()
                        {
                            Delay = Session.Delay.ToString("0.00"),
                            PerformedEvent = EventTypes.ScreenShot.ToString(),
                            SSPath = storageFolder
                        });
                    }
                }
                LogMessage("Screen Shot Taken.");
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                btnTakeScreenShot.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private bool SaveScreenShot(string storageFolder)
        {
            try
            {
                new InputSimulator().Keyboard.KeyPress(VirtualKeyCode.SNAPSHOT);
                string screenShotPath = Path.Combine(storageFolder, "Screenshot-" + DateTime.Now.ToString("MM-dd-yyyy-hhmmss") + ".png");
                if (Clipboard.ContainsImage() == true)
                {
                    Image image = (Image)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    image.Save(screenShotPath, System.Drawing.Imaging.ImageFormat.Png);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private void btnSaveScript_Click(object sender, EventArgs e)
        {
            try
            {
                string storageFolder = txtDataStorageFolder.Text;
                if (string.IsNullOrEmpty(storageFolder))
                {
                    MessageBox.Show("Please select proper Folder to store JSON and Screenshots.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDataStorageFolder.Focus();
                    return;
                }
                if (!Directory.Exists(storageFolder))
                {
                    Directory.CreateDirectory(storageFolder);
                    UpdateAppConfig();
                }

                string ScriptPath = Path.Combine(storageFolder, "Script-" + DateTime.Now.ToString("MM-dd-yyyy-HHmmss") + ".json");
                var jsonString = JsonConvert.SerializeObject(objSession, Formatting.Indented);
                StreamWriter objStreamWriter = new StreamWriter(ScriptPath, false);
                objStreamWriter.WriteLine(jsonString);
                objStreamWriter.Close();
                txtScriptWindow.Text += Environment.NewLine + "Script Saved Successfully." + Environment.NewLine + "Saved Script Path : " + ScriptPath;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnStopReplay_Click(object sender, EventArgs e)
        {
            try
            {
                IsReplayActive = false;
                LogMessage("Replay Stopped.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnLoadScript_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog objOpenFileDialog = new OpenFileDialog();
                objOpenFileDialog.Filter = "Json Files|*.json";
                if (DialogResult.OK == objOpenFileDialog.ShowDialog())
                {
                    try
                    {
                        StreamReader objReader = new StreamReader(objOpenFileDialog.FileName);

                        string data = objReader.ReadToEnd();
                        objReader.Close();
                        objLoadedScript = new RecordedSession();
                        var json = JsonConvert.DeserializeObject(data, typeof(RecordedSession));
                        objLoadedScript = (RecordedSession)json;

                        objSession = objLoadedScript;
                        txtScriptWindow.Text += Environment.NewLine + "Script Loaded Successfully." + Environment.NewLine + "Loaded Script Path : " + objOpenFileDialog.FileName;
                        IsScriptLoaded = true;
                    }
                    catch (Exception ex)
                    {
                        txtScriptWindow.Text += Environment.NewLine + "Failed to Load Script." + Environment.NewLine + "Loaded Script Path : " + objOpenFileDialog.FileName;

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
