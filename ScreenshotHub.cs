using Ghost;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshot
{
    public class ScreenshotHub : HubBase
    {
        public string Shot()
        {
            var form = new ScreenshotForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var image = form.ScreenshotImage;
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    return "data:image/png;base64," + Convert.ToBase64String(stream.ToArray());
                }
            }
            return null;
        }
    }
}
