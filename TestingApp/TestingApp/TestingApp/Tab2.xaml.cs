using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
namespace TestingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tab2 : ContentPage
    {
        ScrollView scrollView;
        StackLayout Scrolling;
        public MainPage PageWithList;
        private Dictionary<string,View> usedUID = new Dictionary<string, View>();
        private string link = "http://demo.macroscop.com/mobile?channelid=";
        private string link2 = "&oneframeonly=true&login=root";
        private View newItem(Camera element)
        {
            


            Frame result = new Frame()
            {
                Content = new StackLayout
                {
                    Margin = new Thickness(5),
                    Spacing = 0,
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                                new Label { Text = element.getCameraName, HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions=LayoutOptions.Center }
                                

                    }
                }

            };
            call(element.getCameraId, result);
            return result;
        }


        private async void call(string uid, Frame results)
        {
            System.Uri uri;
            System.Uri.TryCreate(link + uid + link2, UriKind.Absolute, out uri);
            Task<ImageSource> result = Task<ImageSource>.Factory.StartNew(() => ImageSource.FromUri(uri));
            Image A = new Image()
            {
                HeightRequest = 150,
                WidthRequest = 150,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            A.Source = await result;
            ((StackLayout)(results.Content)).Children.Add(A);


            //WebClient webClient = new WebClient();
            //byte[] imageBytes = null;
            //imageBytes = await webClient.DownloadDataTaskAsync(uri);
            //string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //string localFilename = "image.png";
            //string localPath = System.IO.Path.Combine(documentsPath, localFilename);
            //FileStream fs = new FileStream(localPath, FileMode.OpenOrCreate);
            //await fs.WriteAsync(imageBytes, 0, imageBytes.Length);
            //fs.Close();



            //var byteArray = await new HttpClient().GetByteArrayAsync(link + uid + link2);

            //Image image = new Image()
            //{
            //    HeightRequest = 150,
            //    WidthRequest = 150,
            //    HorizontalOptions = LayoutOptions.EndAndExpand,
            //    VerticalOptions = LayoutOptions.Center
            //};
            //image.Source = ImageSource.FromStream(() => new MemoryStream(byteArray));
            //A.Source = ImageSource.FromStream(() => new MemoryStream(byteArray));
        }
        public void Render()
        {
            
            List<Camera> ListofCameras = PageWithList.GetList();
            foreach (Camera element in ListofCameras)
            {
                View value;
                if (!element.TurnOn)
                {
                    
                    if(usedUID.TryGetValue(element.getCameraId,out value))
                    {
                        Scrolling.Children.Remove(value);
                        usedUID.Remove(element.getCameraId);

                    }
                }
                else if (element.TurnOn)
                {
                    if (!usedUID.TryGetValue(element.getCameraId, out value))
                    {
                        value = newItem(element);
                        usedUID.Add(element.getCameraId,value);
                        Scrolling.Children.Add(value);
                    }

                }
            }

        }

        public async void MinuteUpdate()
        {
            while (true)
            {
                Render();
                await Task.Delay(60000);
            }
        }

        public Tab2()
        {
            InitializeComponent();
            scrollView = new ScrollView();
            scrollView.VerticalOptions = LayoutOptions.FillAndExpand;
             Scrolling = new StackLayout();
            scrollView.Content = Scrolling;
            stackLayoutSecond.Children.Add(scrollView);
        }
        
    }
}