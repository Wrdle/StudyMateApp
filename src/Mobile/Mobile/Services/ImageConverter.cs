using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Services
{
    public class ImageConverter
    {
        //------------------------------
        //          Fields
        //------------------------------

        //------------------------------
        //          Constructors
        //------------------------------

        public ImageConverter()
        {

        }

        //------------------------------
        //          Methods
        //------------------------------

        public async Task<byte[]> ImageToBytes(ImageSource imageSource)
        {
            if (imageSource == null)
            {
                return new byte[] { };
            }

            var cancellationToken = System.Threading.CancellationToken.None;
            using (var imageStream = await ((StreamImageSource)imageSource).Stream(cancellationToken))
            using (var byteStream = new MemoryStream())
            {
                await imageStream.CopyToAsync(byteStream);
                return byteStream.ToArray();
            }
        }

        public ImageSource BytesToImage(byte[] bytes)
        {
            if (bytes != null && bytes.Length < 1)
            {
                return null;
            }

            try
            {
                Stream stream = new MemoryStream(bytes);
                return ImageSource.FromStream(() => { return stream; });
            }
            catch
            {
                return null;
            }
        }

    }
}
