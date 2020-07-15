using System.Windows;
using UiCommon.Models;

namespace UiCommon.Common
{
    /// <summary>
    /// 只读属性的绑定
    /// https://stackoverflow.com/questions/1083224/pushing-read-only-gui-properties-back-into-viewmodel/3667609#3667609
    /// </summary>
    /// <example>
    ///  <Canvas>
    ///      <ReadOnlyBinding.DataPipes>
    ///          <BindingPipeCollection>
    ///              <BindingPipe Source = "{Binding RelativeSource={RelativeSource AncestorType={x:Type Canvas}}, Path=ActualWidth}"
    ///                  Target="{Binding Path=ViewportWidth, Mode=OneWayToSource}"/>
    ///              <BindingPipe Source = "{Binding RelativeSource={RelativeSource AncestorType={x:Type Canvas}}, Path=ActualHeight}"
    ///                  Target="{Binding Path=ViewportHeight, Mode=OneWayToSource}"/>
    ///          </BindingPipeCollection>
    ///      </ReadOnlyBinding.DataPipes>
    ///  </Canvas>
    /// </example>
    public class ReadOnlyBinding
    {
        public static readonly DependencyProperty DataPipesProperty =
            DependencyProperty.RegisterAttached("DataPipes",
                typeof(BindingPipeCollection),
                typeof(BindingPipe),
                new UIPropertyMetadata(null));

        public static void SetDataPipes(DependencyObject o, BindingPipeCollection value)
        {
            o.SetValue(DataPipesProperty, value);
        }

        public static BindingPipeCollection GetDataPipes(DependencyObject o)
        {
            return (BindingPipeCollection) o.GetValue(DataPipesProperty);
        }
    }
}