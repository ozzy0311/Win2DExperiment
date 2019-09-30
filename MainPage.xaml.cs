using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Win2DExperiment
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            //// 1
            //args.DrawingSession.DrawText("Hello, World!", 100, 100, Colors.Black);
            //args.DrawingSession.DrawCircle(125, 125, 100, Colors.Green);
            //args.DrawingSession.DrawLine(0, 0, 50, 200, Colors.Red);

            //// 2
            //for (int i = 0; i < 100; i++)
            //{
            //    args.DrawingSession.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //    args.DrawingSession.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //    args.DrawingSession.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //}

            ////3
            //CanvasCommandList cl = new CanvasCommandList(sender);
            //using (CanvasDrawingSession clds = cl.CreateDrawingSession())
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        clds.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //        clds.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //        clds.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //    }

            //    GaussianBlurEffect blur = new GaussianBlurEffect();
            //    blur.Source = cl;
            //    blur.BlurAmount = 10.0f;
            //    args.DrawingSession.DrawImage(blur);
            //}

            //4
            args.DrawingSession.DrawImage(blur);
        }

        GaussianBlurEffect blur;

        void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.canvas.RemoveFromVisualTree();
            this.canvas = null;
        }

        Random rnd = new Random();
        private Vector2 RndPosition()
        {
            double x = rnd.NextDouble() * 500f;
            double y = rnd.NextDouble() * 500f;
            return new Vector2((float)x, (float)y);
        }

        private float RndRadius()
        {
            return (float)rnd.NextDouble() * 150f;
        }

        private byte RndByte()
        {
            return (byte)rnd.Next(256);
        }

        private void canvas_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            CanvasCommandList cl = new CanvasCommandList(sender);
            using (CanvasDrawingSession clds = cl.CreateDrawingSession())
            {
                for (int i = 0; i < 100; i++)
                {
                    clds.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
                    clds.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
                    clds.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
                }
            }

            blur = new GaussianBlurEffect
            {
                Source = cl,
                BlurAmount = 10.0f
            };

        }

        private void canvas_DrawAnimated(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            float radius = (float)(1 + Math.Sin(args.Timing.TotalTime.TotalSeconds)) * 10f;
            blur.BlurAmount = radius;
            args.DrawingSession.DrawImage(blur);
        }
    }
}
