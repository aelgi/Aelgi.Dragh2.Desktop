using Aelgi.Dragh2.Core.Enums;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public interface IUtilityService
    {
        SKColor ColorToSkia(Colors color);
    }

    public class UtilityService : IUtilityService
    {
        public SKColor ColorToSkia(Colors color)
        {
            switch (color)
            {
                case Colors.Primary: return new SKColor(126, 192, 238);
                case Colors.Background: return new SKColor(221, 221, 221);
                case Colors.Black: return new SKColor(0, 0, 0);
                case Colors.White: return new SKColor(255, 255, 255);
                case Colors.Red: return new SKColor(197, 7, 7);
                case Colors.Green: return new SKColor(37, 195, 9);
                case Colors.Orange: return new SKColor(245, 146, 41);
                case Colors.Blue: return new SKColor(31, 147, 255);
            }

            throw new NotImplementedException();
        }
    }
}
