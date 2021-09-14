using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Xml;
using System.Net;
using System.IO;

namespace TestingApp
{
    public partial class MainPage : ContentPage
    {
          private List<Camera> ListofCameras;
        private string link = "http://demo.macroscop.com/configex?login=root";
        public Tab2 PageWithScreens;
        public List<Camera> GetList()
        {
            return ListofCameras;
        }

        public MainPage()
        {
            InitializeComponent();
            ListofCameras = getInfoFromHttp();

           
            ScrollView scrollView = new ScrollView();
            scrollView.VerticalOptions = LayoutOptions.FillAndExpand;
            StackLayout Scrolling = new StackLayout();
            scrollView.Content = Scrolling;
            foreach (Camera element in GetList())
            {

                CheckBox A = new CheckBox() { IsChecked = false, Color = Color.Red};
                CheckBoxCamera Cam = new CheckBoxCamera();
                element.checkBox = A;
                Cam.setCamera = element;
                Cam.SetCheckBox = A;
                string Sound;
                if (element.getSoundState)
                    Sound = "Звук Есть";
                else
                    Sound = "Звука нет";
                Scrolling.Children.Add(new Frame()
                {

                    Content = new StackLayout
                    {
                        Margin = new Thickness(5),
                        Spacing = 1,
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Children =
                        {
                             new Label() { Text = element.parentFolderName+"/"+element.getCameraName },
                             new Label() { Text = Sound },
                             new Label() { Text ="Renderer:" },
                             Cam.SetCheckBox
                             

                        }
                    }


                }) ;
                Cam.SetCheckBox.CheckedChanged += (sender, e) =>
                {
                    Cam.setCamera.TurnOn = !Cam.setCamera.TurnOn;
                    if (Cam.setCamera.TurnOn)
                    {
                        Cam.SetCheckBox.Color = Color.Green;
                        
                    }
                    else
                    {
                        Cam.SetCheckBox.Color = Color.Red;
                    }
                    PageWithScreens.Render();
                };
            }
            stackLayoutMain.Children.Add(scrollView);
        }

        public List<Camera> getInfoFromHttp()
        {
            List<Camera> res = new List<Camera>();
            var httpRequest = (HttpWebRequest)WebRequest.Create(link);
            var response = (HttpWebResponse)httpRequest.GetResponse();
            var receiveStream = response.GetResponseStream();
            var mySourceDoc = new XmlDocument();
            mySourceDoc.Load(receiveStream);
            receiveStream.Close();
            XmlElement xRoot = mySourceDoc.DocumentElement;
            foreach (XmlElement Elem in xRoot["Channels"].ChildNodes)
            {
                Camera newCamera = new Camera(Elem.GetAttribute("Id"), Elem.GetAttribute("Name"), Convert.ToBoolean(Elem.GetAttribute("IsSoundOn")));
                res.Add(newCamera);
            }
            foreach (XmlElement Elem in xRoot["RootSecurityObject"]["ChildSecurityObjects"].ChildNodes)
            {
                foreach (XmlNode subElem in Elem["ChildChannels"])
                {
                    if(res.Exists(x=>x.getCameraId==subElem.InnerText))
                    {
                        res.Find(x => x.getCameraId == subElem.InnerText).parentFolderName = Elem.GetAttribute("Name");
                    }
                }
            }
            return res;
        }
    }
    
}
