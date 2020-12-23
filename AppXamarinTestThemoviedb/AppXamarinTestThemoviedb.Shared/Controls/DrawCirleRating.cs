namespace AppXamarinTestThemoviedb.Shared.Controls
{
	using System;

	using SkiaSharp;
	using SkiaSharp.Views.Forms;

	using Xamarin.Forms;

	public class DrawCirleRating : SKCanvasView
	{
		public static readonly BindableProperty StrokeWidthProperty =
			BindableProperty.Create(nameof(StrokeWidth), typeof(float), typeof(DrawCirleRating), 10f, propertyChanged: OnPropertyChanged);

		public static readonly BindableProperty ProgressProperty =
			BindableProperty.Create(nameof(Progress), typeof(float), typeof(DrawCirleRating), 0f, propertyChanged: OnPropertyChanged);

		public static readonly BindableProperty ProgressStartColorProperty =
			BindableProperty.Create(nameof(ProgressStartColor), typeof(Color), typeof(DrawCirleRating), Color.Blue, propertyChanged: OnPropertyChanged);

		public static readonly BindableProperty ProgressEndColorProperty =
			BindableProperty.Create(nameof(ProgressEndColor), typeof(Color), typeof(DrawCirleRating), Color.Red, propertyChanged: OnPropertyChanged);

		private const float StartAngle = -90;
		private const float SweepAngle = 360;

		public float StrokeWidth
		{
			get { return (float)this.GetValue(StrokeWidthProperty); }
			set { this.SetValue(StrokeWidthProperty, value); }
		}

		public float Progress
		{
			get { return (float)this.GetValue(ProgressProperty); }
			set { this.SetValue(ProgressProperty, value); }
		}

		public Color ProgressStartColor
		{
			get { return (Color)this.GetValue(ProgressStartColorProperty); }
			set { this.SetValue(ProgressStartColorProperty, value); }
		}

		public Color ProgressEndColor
		{
			get { return (Color)this.GetValue(ProgressEndColorProperty); }
			set { this.SetValue(ProgressEndColorProperty, value); }
		}

		protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
		{
			SKImageInfo info = args.Info;
			SKSurface surface = args.Surface;
			SKCanvas canvas = surface.Canvas;

			int size = Math.Min(info.Width, info.Height);
			int max = Math.Max(info.Width, info.Height);

			canvas.Translate((max - size) / 2, 0);

			canvas.Clear();
			canvas.Save();
			canvas.RotateDegrees(0, size / 2, size / 2);
			this.DrawProgressCircle(info, canvas);

			canvas.Restore();
		}

		private static void OnPropertyChanged(BindableObject bindable, object oldVal, object newVal)
		{
			var circleProgress = bindable as DrawCirleRating;
			circleProgress?.InvalidateSurface();
		}

		private void DrawProgressCircle(SKImageInfo info, SKCanvas canvas)
		{
			float progressAngle = SweepAngle * this.Progress;
			int size = Math.Min(info.Width, info.Height);

			var shader = SKShader.CreateSweepGradient(
				new SKPoint(size / 2, size / 2),
				new[]
				{
					this.ProgressStartColor.ToSKColor(),
					this.ProgressEndColor.ToSKColor(),
					this.ProgressStartColor.ToSKColor()
				},
				new[]
				{
					StartAngle / 360,
					(StartAngle + progressAngle + 1) / 360,
					(StartAngle + progressAngle + 2) / 360
				});

			var paint = new SKPaint
			{
				Shader = shader,
				StrokeWidth = this.StrokeWidth,
				IsStroke = true,
				IsAntialias = true,
				StrokeCap = SKStrokeCap.Round,
			};

			this.DrawCircle(info, canvas, paint, progressAngle);
		}

		private void DrawCircle(SKImageInfo info, SKCanvas canvas, SKPaint paint, float angle)
		{
			int size = Math.Min(info.Width, info.Height);
			float halfWidth = size / 2;
			float halfHeight = size / 2;

			using (SKPath path = new SKPath())
			{
				SKRect rect = new SKRect(
					this.StrokeWidth,
					this.StrokeWidth,
					size - this.StrokeWidth,
					size - this.StrokeWidth);

				path.AddArc(rect, StartAngle, angle);

				canvas.DrawPath(path, paint);
			}
		}
	}
}
