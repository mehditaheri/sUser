using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Extensions.Caching.Distributed;

namespace Ste.Framework.Common;

public class Captcha
{
    public string Image { get; set; }
    public string Key { get; set; }
    public byte[] BinaryImage { get; set; }
}

public class CheckCaptcha
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public interface ICaptchaService
{
    Task<Result<Captcha>> Generate();
    Task<Result<bool>> IsValid(CheckCaptcha captcha);
}

public class CaptchaService : ICaptchaService
{
    private IDistributedCache _cache;

    public CaptchaService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<Result<Captcha>> Generate()
    {
        var text = Utility.RandomNumber(4);
        var captcha = new Captcha
        {
            Key = DateTime.Now.Ticks.ToString(),
        };
        await _cache.SetAsync(captcha.Key, text,
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(1)
            });
        text = Utility.ToPersianNumber(text);
        var img = new Bitmap(1, 1);
        var drawing = Graphics.FromImage(img);
        //var collection = new PrivateFontCollection();
        //collection.AddFontFile(HttpContext.Current.Server.MapPath("~/webfonts/fa-regular-400.ttf"));
        //var font = new Font(collection.Families.First(), 20);
        var font = new Font("tahoma", 20);
        var textSize = drawing.MeasureString(text, font);
        img.Dispose();
        drawing.Dispose();
        img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
        drawing = Graphics.FromImage(img);
        var backColor = Color.Transparent;
        var textColor = Color.FromArgb(Utility.RandomNumber(0, 150), Utility.RandomNumber(0, 150),
            Utility.RandomNumber(0, 150));
        drawing.Clear(backColor);
        Brush textBrush = new SolidBrush(textColor);
        drawing.DrawString(text, font, textBrush, 20, 10);

        drawing.Save();

        font.Dispose();
        textBrush.Dispose();
        drawing.Dispose();

        //var filter = new WaterWave();
        //filter.HorizontalWavesCount = Utility.RandomNumber(1, 5);
        //filter.HorizontalWavesAmplitude = Utility.RandomNumber(2, 5);
        //filter.VerticalWavesCount = Utility.RandomNumber(2, 5);
        //filter.VerticalWavesAmplitude = Utility.RandomNumber(1, 4);
        //// apply the filter
        //img = filter.Apply((Bitmap)img);

        var ms = new MemoryStream();
        img.Save(ms, ImageFormat.Png);
        img.Dispose();

        captcha.Image = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
        return new Result<Captcha>
        {
            Data = captcha
        };
    }

    public async Task<Result<bool>> IsValid(CheckCaptcha captcha)
    {
        if (string.IsNullOrWhiteSpace(captcha.Key) || string.IsNullOrWhiteSpace(captcha.Value))
        {
            return new Result<bool>
            {
                Success = true,
                Data = false
            };
        }
        var item = await _cache.GetAsync<string?>(captcha.Key);
        _cache.Remove(captcha.Key);
        return new Result<bool>
        {
            Success = true,
            Data = item != null && item.Equals(captcha.Value)
        };
    }
}