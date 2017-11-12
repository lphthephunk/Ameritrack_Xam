using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.ComponentModel;

namespace Ameritrack_Xam.Pages.ViewModels
{
    public class GalleryVM : INotifyPropertyChanged
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();

        Fault faultContext;

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
    

        public GalleryVM(Fault fault)
        {
            faultContext = fault;
        }

        public async Task TakePicture()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "FaultPictures",
                    Name = $"{DateTime.UtcNow}.jpg"
                };

                try
                {
                    var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                    await InsertFaultPicture(file);
                }
                catch (TaskCanceledException)
                {
                    IsBusy = false;
                }
                catch (AggregateException)
                {
                    IsBusy = false;
                }
            }
        }

        public async Task<List<ImageSource>> GetAllPictures(int? faultId)
        {
            List<ImageSource> imageSourceList = new List<ImageSource>();

            try
            {
                IsBusy = true;
                var pictures = await DatabaseService.GetFaultPictures(faultId);

                foreach (var source in pictures)
                {
                    imageSourceList.Add(ImageSource.FromStream(() => new MemoryStream(source.Picture)));
                }
            }
            finally
            {
                //IsBusy = false;
            }

            return imageSourceList;
        }

        private async Task InsertFaultPicture(Plugin.Media.Abstractions.MediaFile picture)
        {
            byte[] pictureBytes;

            using (var memoryStream = new MemoryStream())
            {
                picture.GetStream().CopyTo(memoryStream);
                picture.Dispose();
                pictureBytes = memoryStream.ToArray(); 
            }

            var dbPicture = new FaultPicture
            {
                Picture = pictureBytes,
                FaultId = faultContext.FaultId
            };

            await DatabaseService.InsertFaultPicture(dbPicture);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(name));

        }

        #endregion
    }
}
