using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MT2.pubuliu
{
    class WaterfallPanel:Panel
    {
        public int ColumnNum //列表可控
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }
       

        // Using a DependencyProperty as the backing store for ColumnCount.  This enables animation, styling, binding, etc...  
        public static readonly DependencyProperty ColumnCountProperty =
                    DependencyProperty.Register("ColumnNum", typeof(int), typeof(WaterfallPanel), new PropertyMetadata(2));

        //控制瀑布流的方向
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(VirtualizingPanel), new PropertyMetadata(Orientation.Vertical));

        //辅助参考http://blog.csdn.net/zmq570235977/article/details/50392283
        //参考原文通过Measure & Arrange实现UWP瀑布流布局http://www.cnblogs.com/ms-uap/p/4715195.html
        protected override Size MeasureOverride(Size availableSize)
        {
           
            // 记录每个流的长度。因为我们用选取最短的流来添加下一个元素。  
            KeyValuePair<double, int>[] flowLens = new KeyValuePair<double, int>[ColumnNum];
            if (Orientation == Orientation.Vertical )
            {
                foreach (int idx in Enumerable.Range(0, ColumnNum))
                {
                    flowLens[idx] = new KeyValuePair<double, int>(0.0, idx);
                }

                // 我们就用2个纵向流来演示，获取每个流的宽度。  
                double flowWidth = availableSize.Width / ColumnNum;

                // 为子控件提供沿着流方向上，无限大的空间  
                Size elemMeasureSize = new Size(flowWidth, double.PositiveInfinity);

                foreach (UIElement elem in Children)
                {
                    // 让子控件计算它的大小。  
                    elem.Measure(elemMeasureSize);
                    Size elemSize = elem.DesiredSize;

                    double elemLen = elemSize.Height;
                    var pair = flowLens[0];

                    // 子控件添加到最短的流上，并重新计算最短流。  
                    // 因为我们为了求得流的长度，必须在计算大小这一步时就应用一次布局。但实际的布局还是会在Arrange步骤中完成。  
                    flowLens[0] = new KeyValuePair<double, int>(pair.Key + elemLen, pair.Value);
                    flowLens = flowLens.OrderBy(p => p.Key).ToArray();
                }
                return new Size(availableSize.Width, flowLens.Last().Key);//返回值是该元素本身实际需要的大小。

            }
            else
            {
                foreach (int idx in Enumerable.Range (0,ColumnNum))
                {
                    flowLens[idx] = new KeyValuePair<double, int>(0.0, idx);
                }

                double flowHeigh =50;

                Size elemMeasureSize = new Size(double.PositiveInfinity, flowHeigh);

                foreach (UIElement elem in Children )
                {
                    elem.Measure(elemMeasureSize);
                    Size elemSize = elem.DesiredSize;

                    double elemLen = elemSize.Width;
                    var pair = flowLens[0];

                    flowLens[0] = new KeyValuePair<double, int>(pair.Key + elemLen, pair.Value);
                    flowLens = flowLens.OrderBy(p => p.Key).ToArray();
                }
                return new Size(flowLens.Last().Key, 50);//返回值是该元素本身实际需要的大小。

            }

        }
       
        protected override Size ArrangeOverride(Size finalSize)
        {
            // 同样记录流的长度。  
            KeyValuePair<double, int>[] flowLens = new KeyValuePair<double, int>[ColumnNum];

            if (Orientation == Orientation.Vertical)
            {
                double flowWidth = finalSize.Width / ColumnNum;

                // 要用到流的横坐标了，我们用一个数组来记录（其实最初是想多加些花样，用数组来方便索引横向偏移。不过本例中就只进行简单的乘法了）  
                double[] xs = new double[ColumnNum];

                foreach (int idx in Enumerable.Range(0, ColumnNum))
                {
                    flowLens[idx] = new KeyValuePair<double, int>(0.0, idx);
                    xs[idx] = idx * flowWidth;
                }

                foreach (UIElement elem in Children)
                {
                    // 直接获取子控件大小。  
                    Size elemSize = elem.DesiredSize;
                    double elemLen = elemSize.Height;

                    var pair = flowLens[0];
                    double chosenFlowLen = pair.Key;
                    int chosenFlowIdx = pair.Value;

                    // 此时，我们需要设定新添加的空间的位置了，其实比measure就多了一个Point信息。接在流中上一个元素的后面。  
                    Point pt = new Point(xs[chosenFlowIdx], chosenFlowLen);

                    // 调用Arrange进行子控件布局。并让子控件利用上整个流的宽度。  
                    elem.Arrange(new Rect(pt, new Size(flowWidth, elemSize.Height)));

                    // 重新计算最短流。  
                    flowLens[0] = new KeyValuePair<double, int>(chosenFlowLen + elemLen, chosenFlowIdx);
                    flowLens = flowLens.OrderBy(p => p.Key).ToArray();
                }
            }
            else
            {
                List<Double> offsetX = new List<Double>();
                List<Double> offsetY = new List<Double>();
                int minIndex = 0;

                for (int i =0; i<ColumnNum;i ++)
                {
                    offsetX.Add(0);
                    offsetY.Add(getoffsex(i));
                }

                foreach (var item in this.Children)
                {
                    double min = offsetX.Min();
                    minIndex = offsetX.IndexOf(min);

                    item.Arrange(new Rect(offsetX[minIndex], offsetY[minIndex], item.DesiredSize.Width, item.DesiredSize.Height));
                    offsetX[minIndex] += (item.DesiredSize.Width + 3);
                }
            }
            #region 备份
            //else
            //{
            //    //这里是控制横向显示有多少的，逻辑有问题
            //    double flowWidth = finalSize.Width / 10;

            //    // 要用到流的横坐标了，我们用一个数组来记录（其实最初是想多加些花样，用数组来方便索引横向偏移。不过本例中就只进行简单的乘法了）  
            //    double[] xs = new double[ColumnNum];

            //    foreach (int idx in Enumerable.Range(0, ColumnNum))
            //    {
            //        flowLens[idx] = new KeyValuePair<double, int>(0.0, idx);
            //        xs[idx] = idx * flowWidth;
            //    }

            //    foreach (UIElement elem in Children)
            //    {
            //        // 直接获取子控件大小。  
            //        Size elemSize = elem.DesiredSize;
            //        double elemLen = elemSize.Width;

            //        var pair = flowLens[0];
            //        double chosenFlowLen = pair.Key;
            //        int chosenFlowIdx = pair.Value;

            //        // 此时，我们需要设定新添加的空间的位置了，其实比measure就多了一个Point信息。接在流中上一个元素的后面。  
            //        Point pt = new Point(xs[chosenFlowIdx], chosenFlowLen);

            //        // 调用Arrange进行子控件布局。并让子控件利用上整个流的宽度。  
            //        elem.Arrange(new Rect(pt, new Size(elemSize.Width, elemSize.Height)));

            //        // 重新计算最短流。  
            //        flowLens[0] = new KeyValuePair<double, int>(chosenFlowLen + elemLen, chosenFlowIdx);
            //        flowLens = flowLens.OrderBy(p => p.Key).ToArray();
            //    }

            //}

            #endregion


            // 直接返回该方法的参数。  
            return finalSize;
        }

        private double getoffsex(int i)
        {

            var index = i * (this.DesiredSize.Width + 3) / ColumnNum;
            return index;
        }
    }
}
