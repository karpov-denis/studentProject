using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestingApp
{
    public class Camera
    {
        private bool TurnedOn=false;
        private CheckBox _checkBox;
        private string id;
        private string Name;
        private bool IsSoundOn;
        private string ParentFolderName;
        public Camera(string uid,string CameraName,bool isSoundOn, string ParentFolder)
        {
            this.id = uid;
            this.Name = CameraName;
            this.IsSoundOn = isSoundOn;
            this.ParentFolderName = ParentFolder;
        }
        public Camera(string uid, string CameraName, bool isSoundOn)
        {
            this.id = uid;
            this.Name = CameraName;
            this.IsSoundOn = isSoundOn;
        }
        public string getCameraId => id;
        public string getCameraName => Name;
        public bool getSoundState => IsSoundOn;
        public string parentFolderName
        {
            get => ParentFolderName;
            set
            {
                ParentFolderName = value;
            }
        }
        public bool TurnOn
        {
            get => TurnedOn;
            set
            {
                TurnedOn = value;
            }
        }
        public CheckBox checkBox
        {
            get => _checkBox;
            set
            {
                _checkBox = value;
            }
        }

        public void CheckBoxChanged(object sender, EventArgs e)
        {

        }
    }
}
