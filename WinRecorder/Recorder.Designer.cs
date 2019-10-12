namespace WinRecorder
{
    partial class Recorder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBrowseLaunchApp = new System.Windows.Forms.Button();
            this.txtLaunchAppLocation = new System.Windows.Forms.TextBox();
            this.btnLaunchApp = new System.Windows.Forms.Button();
            this.btnStopReplay = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.btnRecordKeyStrokes = new System.Windows.Forms.Button();
            this.btnTakeScreenShot = new System.Windows.Forms.Button();
            this.txtScriptWindow = new System.Windows.Forms.TextBox();
            this.btnSaveScript = new System.Windows.Forms.Button();
            this.btnLoadScript = new System.Windows.Forms.Button();
            this.txtDataStorageFolder = new System.Windows.Forms.TextBox();
            this.btnDataStorageFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBrowseLaunchApp
            // 
            this.btnBrowseLaunchApp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseLaunchApp.Location = new System.Drawing.Point(301, 49);
            this.btnBrowseLaunchApp.Name = "btnBrowseLaunchApp";
            this.btnBrowseLaunchApp.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseLaunchApp.TabIndex = 0;
            this.btnBrowseLaunchApp.Text = "Browse";
            this.btnBrowseLaunchApp.UseVisualStyleBackColor = true;
            this.btnBrowseLaunchApp.Click += new System.EventHandler(this.btnBrowseLaunchApp_Click);
            // 
            // txtLaunchAppLocation
            // 
            this.txtLaunchAppLocation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLaunchAppLocation.Location = new System.Drawing.Point(197, 18);
            this.txtLaunchAppLocation.Name = "txtLaunchAppLocation";
            this.txtLaunchAppLocation.ReadOnly = true;
            this.txtLaunchAppLocation.Size = new System.Drawing.Size(179, 23);
            this.txtLaunchAppLocation.TabIndex = 2;
            // 
            // btnLaunchApp
            // 
            this.btnLaunchApp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaunchApp.Location = new System.Drawing.Point(12, 12);
            this.btnLaunchApp.Name = "btnLaunchApp";
            this.btnLaunchApp.Size = new System.Drawing.Size(179, 34);
            this.btnLaunchApp.TabIndex = 3;
            this.btnLaunchApp.Text = "Launch App for Recording";
            this.btnLaunchApp.UseVisualStyleBackColor = true;
            this.btnLaunchApp.Click += new System.EventHandler(this.btnLaunchApp_Click);
            // 
            // btnStopReplay
            // 
            this.btnStopReplay.Enabled = false;
            this.btnStopReplay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopReplay.Location = new System.Drawing.Point(197, 116);
            this.btnStopReplay.Name = "btnStopReplay";
            this.btnStopReplay.Size = new System.Drawing.Size(179, 34);
            this.btnStopReplay.TabIndex = 4;
            this.btnStopReplay.Text = "Stop Replay";
            this.btnStopReplay.UseVisualStyleBackColor = true;
            this.btnStopReplay.Click += new System.EventHandler(this.btnStopReplay_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.Enabled = false;
            this.btnReplay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplay.Location = new System.Drawing.Point(12, 116);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(179, 34);
            this.btnReplay.TabIndex = 5;
            this.btnReplay.Text = "Replay";
            this.btnReplay.UseVisualStyleBackColor = true;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Enabled = false;
            this.btnStopRecording.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopRecording.Location = new System.Drawing.Point(197, 76);
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(179, 34);
            this.btnStopRecording.TabIndex = 6;
            this.btnStopRecording.Text = "StopRecording";
            this.btnStopRecording.UseVisualStyleBackColor = true;
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // btnRecordKeyStrokes
            // 
            this.btnRecordKeyStrokes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecordKeyStrokes.Location = new System.Drawing.Point(12, 76);
            this.btnRecordKeyStrokes.Name = "btnRecordKeyStrokes";
            this.btnRecordKeyStrokes.Size = new System.Drawing.Size(179, 34);
            this.btnRecordKeyStrokes.TabIndex = 7;
            this.btnRecordKeyStrokes.Text = "Record Keys and Clicks";
            this.btnRecordKeyStrokes.UseVisualStyleBackColor = true;
            this.btnRecordKeyStrokes.Click += new System.EventHandler(this.btnRecordKeyStrokes_Click);
            // 
            // btnTakeScreenShot
            // 
            this.btnTakeScreenShot.Enabled = false;
            this.btnTakeScreenShot.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTakeScreenShot.Location = new System.Drawing.Point(12, 156);
            this.btnTakeScreenShot.Name = "btnTakeScreenShot";
            this.btnTakeScreenShot.Size = new System.Drawing.Size(179, 34);
            this.btnTakeScreenShot.TabIndex = 8;
            this.btnTakeScreenShot.Text = "Take Screenshot";
            this.btnTakeScreenShot.UseVisualStyleBackColor = true;
            this.btnTakeScreenShot.Click += new System.EventHandler(this.btnTakeScreenShot_Click);
            // 
            // txtScriptWindow
            // 
            this.txtScriptWindow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScriptWindow.Location = new System.Drawing.Point(12, 198);
            this.txtScriptWindow.Multiline = true;
            this.txtScriptWindow.Name = "txtScriptWindow";
            this.txtScriptWindow.ReadOnly = true;
            this.txtScriptWindow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtScriptWindow.Size = new System.Drawing.Size(381, 289);
            this.txtScriptWindow.TabIndex = 9;
            this.txtScriptWindow.WordWrap = false;
            this.txtScriptWindow.TextChanged += new System.EventHandler(this.txtScriptWindow_TextChanged);
            // 
            // btnSaveScript
            // 
            this.btnSaveScript.Enabled = false;
            this.btnSaveScript.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveScript.Location = new System.Drawing.Point(12, 493);
            this.btnSaveScript.Name = "btnSaveScript";
            this.btnSaveScript.Size = new System.Drawing.Size(179, 34);
            this.btnSaveScript.TabIndex = 11;
            this.btnSaveScript.Text = "Save Script";
            this.btnSaveScript.UseVisualStyleBackColor = true;
            this.btnSaveScript.Click += new System.EventHandler(this.btnSaveScript_Click);
            // 
            // btnLoadScript
            // 
            this.btnLoadScript.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadScript.Location = new System.Drawing.Point(214, 493);
            this.btnLoadScript.Name = "btnLoadScript";
            this.btnLoadScript.Size = new System.Drawing.Size(179, 34);
            this.btnLoadScript.TabIndex = 10;
            this.btnLoadScript.Text = "Load Script";
            this.btnLoadScript.UseVisualStyleBackColor = true;
            this.btnLoadScript.Click += new System.EventHandler(this.btnLoadScript_Click);
            // 
            // txtDataStorageFolder
            // 
            this.txtDataStorageFolder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataStorageFolder.Location = new System.Drawing.Point(12, 564);
            this.txtDataStorageFolder.Name = "txtDataStorageFolder";
            this.txtDataStorageFolder.ReadOnly = true;
            this.txtDataStorageFolder.Size = new System.Drawing.Size(179, 23);
            this.txtDataStorageFolder.TabIndex = 13;
            this.txtDataStorageFolder.TextChanged += new System.EventHandler(this.txtDataStorageFolder_TextChanged);
            // 
            // btnDataStorageFolder
            // 
            this.btnDataStorageFolder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataStorageFolder.Location = new System.Drawing.Point(199, 564);
            this.btnDataStorageFolder.Name = "btnDataStorageFolder";
            this.btnDataStorageFolder.Size = new System.Drawing.Size(75, 23);
            this.btnDataStorageFolder.TabIndex = 12;
            this.btnDataStorageFolder.Text = "Browse";
            this.btnDataStorageFolder.UseVisualStyleBackColor = true;
            this.btnDataStorageFolder.Click += new System.EventHandler(this.btnDataStorageFoler_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 540);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Folder to store JSON and Screenshots";
            // 
            // Recorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 605);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDataStorageFolder);
            this.Controls.Add(this.btnDataStorageFolder);
            this.Controls.Add(this.btnSaveScript);
            this.Controls.Add(this.btnLoadScript);
            this.Controls.Add(this.txtScriptWindow);
            this.Controls.Add(this.btnTakeScreenShot);
            this.Controls.Add(this.btnRecordKeyStrokes);
            this.Controls.Add(this.btnStopRecording);
            this.Controls.Add(this.btnReplay);
            this.Controls.Add(this.btnStopReplay);
            this.Controls.Add(this.btnLaunchApp);
            this.Controls.Add(this.txtLaunchAppLocation);
            this.Controls.Add(this.btnBrowseLaunchApp);
            this.Name = "Recorder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Recording Utility";
            this.Load += new System.EventHandler(this.Recorder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseLaunchApp;
        private System.Windows.Forms.TextBox txtLaunchAppLocation;
        private System.Windows.Forms.Button btnLaunchApp;
        private System.Windows.Forms.Button btnStopReplay;
        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.Button btnStopRecording;
        private System.Windows.Forms.Button btnRecordKeyStrokes;
        private System.Windows.Forms.Button btnTakeScreenShot;
        private System.Windows.Forms.TextBox txtScriptWindow;
        private System.Windows.Forms.Button btnSaveScript;
        private System.Windows.Forms.Button btnLoadScript;
        private System.Windows.Forms.TextBox txtDataStorageFolder;
        private System.Windows.Forms.Button btnDataStorageFolder;
        private System.Windows.Forms.Label label2;
    }
}

