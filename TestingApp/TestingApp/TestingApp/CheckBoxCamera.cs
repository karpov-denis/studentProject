using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestingApp
{
    class CheckBoxCamera
    {
        CheckBox checkBox;
        Camera _Camera;
        public CheckBoxCamera()
        {

        }
        public CheckBox SetCheckBox
        {
            get => checkBox;
            set
            {
                checkBox = value;
            }
        }
        public Camera setCamera
        {
            get => _Camera;
            set
            {
                _Camera = value;
            }
        }
    }
}
