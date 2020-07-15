using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace UiCommon.Extensions
{
    [MarkupExtensionReturnType(typeof(Brush))]
    public class RevealBrushExtension : DynamicResourceExtension
    {
        [ThreadStatic]
        private static Dictionary<RadialGradientBrush, WeakReference<FrameworkElement>> _globalRevealingElements;

        /// <summary>
        /// The color to use for rendering in case the <see cref="MarkupExtension"/> can't work correctly.
        /// </summary>
        public Color FallbackColor { get; set; } = Colors.White;

        /// <summary>
        /// Gets or sets a value that specifies the base background color for the brush.
        /// </summary>
        public Color Color { get; set; } = Colors.White;

        public Color ToColor { get; set; } = Colors.Transparent;

        public Transform Transform { get; set; } = Transform.Identity;

        public Transform RelativeTransform { get; set; } = Transform.Identity;

        public double Opacity { get; set; } = 1.0;

        public double Radius { get; set; } = 100;

        public new string ResourceKey { get; }

        public RevealBrushExtension()
        {
            base.ResourceKey = ResourceKey = string.Empty;
        }

        public RevealBrushExtension(Color color) : this()
        {
            Color = color;
            FallbackColor = color;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) return this;
            // 如果没有服务，则直接返回。
            if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget service)) return this;
            // MarkupExtension 在样式模板中，返回 this 以延迟提供值。
            if (service.TargetObject.GetType().Name.EndsWith("SharedDp")) return this;
            if (!(service.TargetObject is FrameworkElement element)) return this;
            if (DesignerProperties.GetIsInDesignMode(element)) return new SolidColorBrush(FallbackColor);

            var brush = CreateGlobalBrush(element);
            return brush;
        }

        private Brush CreateGlobalBrush(FrameworkElement element)
        {
            var brush = CreateRadialGradientBrush();
            if (_globalRevealingElements is null)
            {
                CompositionTarget.Rendering -= OnRendering;
                CompositionTarget.Rendering += OnRendering;
                _globalRevealingElements = new Dictionary<RadialGradientBrush, WeakReference<FrameworkElement>>();
            }

            _globalRevealingElements.Add(brush, new WeakReference<FrameworkElement>(element));
            return brush;
        }

        private void OnRendering(object sender, EventArgs e)
        {
            if (_globalRevealingElements is null)
            {
                return;
            }

            var toCollect = new List<RadialGradientBrush>();
            foreach (var pair in _globalRevealingElements)
            {
                var brush = pair.Key;
                var weak = pair.Value;
                if (weak.TryGetTarget(out var element))
                {
                    Reveal(brush, element);
                }
                else
                {
                    toCollect.Add(brush);
                }
            }

            foreach (var brush in toCollect)
            {
                _globalRevealingElements.Remove(brush);
            }

            void Reveal(RadialGradientBrush brush, IInputElement element)
            {
                UpdateBrush(brush, Mouse.GetPosition(element));
            }
        }

        private void UpdateBrush(RadialGradientBrush brush, Point origin)
        {
            if (IsUsingMouseOrStylus())
            {
                brush.GradientOrigin = origin;
                brush.Center = origin;
            }
            else
            {
                brush.Center = new Point(double.NegativeInfinity, double.NegativeInfinity);
            }
        }

        private RadialGradientBrush CreateRadialGradientBrush()
        {
            var brush = new RadialGradientBrush(Color, ToColor)
            {
                MappingMode = BrushMappingMode.Absolute,
                RadiusX = Radius,
                RadiusY = Radius,
                Opacity = Opacity,
                Transform = Transform,
                RelativeTransform = RelativeTransform,
                Center = new Point(double.NegativeInfinity, double.NegativeInfinity),
            };
            return brush;
        }

        private bool IsUsingMouseOrStylus()
        {
            var device = Stylus.CurrentStylusDevice;
            if (device is null)
            {
                return true;
            }

            if (device.TabletDevice.Type == TabletDeviceType.Stylus)
            {
                return true;
            }

            return false;
        }
    }
}