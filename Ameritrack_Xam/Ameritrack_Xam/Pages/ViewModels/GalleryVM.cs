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

namespace Ameritrack_Xam.Pages.ViewModels
{
    public class GalleryVM
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();

        Fault faultContext;

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

                var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                await InsertFaultPicture(file);
            }
        }

        public async Task<List<ImageSource>> GetAllPictures(int? faultId)
        {
            var pictures = await DatabaseService.GetFaultPictures(faultId);
            List<ImageSource> imageSourceList = new List<ImageSource>();

            foreach(var source in pictures)
            {
                imageSourceList.Add(ImageSource.FromStream(() => new MemoryStream(source.Picture)));
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
    }
}
