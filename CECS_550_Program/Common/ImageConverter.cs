using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace CECS_550_Program.Common
{
    public class ImageConverter
    {
        public async Task<BitmapImage> LoadImageAsync(byte[] imageBuffer)
        {
            using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                // Writes the image byte array in an InMemoryRandomAccessStream
                // that is needed to set the source of BitmapImage.
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(imageBuffer);
                    await writer.StoreAsync();
                }

                var image = new BitmapImage();
                await image.SetSourceAsync(ms);

                return image;
            }
        }

        public async Task<byte[]> ToByteArray(StorageFile file)
        {
            using (DataReader inputStream = new DataReader(await file.OpenSequentialReadAsync()))
            {
                byte[] buffer = new byte[inputStream.UnconsumedBufferLength];
                await inputStream.LoadAsync(inputStream.UnconsumedBufferLength);
                inputStream.ReadBytes(buffer);

                return buffer;
            }
        }
    }
}
